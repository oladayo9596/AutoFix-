using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoFix.Data;
using AutoFix.Models;
using MongoDB.Driver;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace AutoFix.Services
{    public interface IOrderService
    {
        Task<ClientOrder> CreateOrderAsync(ClientOrder order);
        Task<MechanicOrder> AcceptOrderAsync(string orderId, string mechanicId, string mechanicName, string notes);
        Task<ClientOrder> DeclineOrderAsync(string orderId, string mechanicId, string reason);
        Task<ClientOrder> CompleteOrderAsync(string orderId, string mechanicId);
        Task<bool> CancelOrderAsync(string orderId, string clientId);
        Task<List<ClientOrder>> GetClientOrdersAsync(string clientId);
        Task<List<ClientOrder>> GetMechanicOrdersAsync(string mechanicId);
        Task<ClientOrder> GetOrderByIdAsync(string orderId);
        Task<List<ClientOrder>> GetPendingOrdersAsync();
    }

    public class OrderService : IOrderService
    {
        private readonly MongoDbContext _context;
        private readonly ILogger<OrderService> _logger;

        public OrderService(MongoDbContext context, ILogger<OrderService> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ClientOrder> CreateOrderAsync(ClientOrder order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            try
            {
                _logger.LogInformation("Creating new order for client {ClientId} - Service: {ServiceType}", 
                                       order.ClientId, order.ServiceType);

                // Ensure required fields are set
                if (string.IsNullOrEmpty(order.ClientId))
                {
                    throw new ArgumentException("ClientId is required", nameof(order));
                }

                if (string.IsNullOrEmpty(order.ServiceType))
                {
                    throw new ArgumentException("ServiceType is required", nameof(order));
                }

                // Set default values if not already set
                order.Status = OrderStatus.Pending;
                order.OrderDate = DateTime.Now;

                await _context.ClientOrders.InsertOneAsync(order);
                
                _logger.LogInformation("Successfully created order {OrderId} for client {ClientId}", 
                                       order.Id, order.ClientId);
                return order;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when creating order for client {ClientId}: {Message}", 
                                 order.ClientId, ex.Message);
                throw new InvalidOperationException($"Database error when creating order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when creating order for client {ClientId}: {Message}", 
                                 order.ClientId, ex.Message);
                throw;
            }
        }        public async Task<MechanicOrder> AcceptOrderAsync(string orderId, string mechanicId, string mechanicName, string notes)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("OrderId is required", nameof(orderId));
            }

            if (string.IsNullOrEmpty(mechanicId))
            {
                throw new ArgumentException("MechanicId is required", nameof(mechanicId));
            }

            try
            {
                _logger.LogInformation("Mechanic {MechanicId} accepting order {OrderId}", mechanicId, orderId);

                // Update the client order status
                var orderFilter = Builders<ClientOrder>.Filter.Eq(o => o.Id, orderId);
                var orderUpdate = Builders<ClientOrder>.Update
                    .Set(o => o.Status, OrderStatus.Accepted)
                    .Set(o => o.MechanicId, mechanicId)
                    .Set(o => o.MechanicName, mechanicName);
                
                var updateResult = await _context.ClientOrders.UpdateOneAsync(orderFilter, orderUpdate);
                
                if (updateResult.MatchedCount == 0)
                {
                    throw new InvalidOperationException($"Order {orderId} not found");
                }

                // Create new mechanic order
                var mechanicOrder = new MechanicOrder
                {
                    OrderId = orderId,
                    MechanicId = mechanicId,
                    MechanicName = mechanicName ?? "Unknown Mechanic",
                    Notes = notes ?? string.Empty,
                    AcceptedDate = DateTime.Now,
                    Status = OrderStatus.Accepted
                };
                
                await _context.MechanicOrders.InsertOneAsync(mechanicOrder);
                
                _logger.LogInformation("Order {OrderId} successfully accepted by mechanic {MechanicId}", orderId, mechanicId);
                return mechanicOrder;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when accepting order {OrderId} by mechanic {MechanicId}: {Message}", 
                                 orderId, mechanicId, ex.Message);
                throw new InvalidOperationException($"Database error when accepting order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when accepting order {OrderId} by mechanic {MechanicId}: {Message}", 
                                 orderId, mechanicId, ex.Message);
                throw;
            }
        }

        public async Task<ClientOrder> DeclineOrderAsync(string orderId, string mechanicId, string reason)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("OrderId is required", nameof(orderId));
            }

            try
            {
                _logger.LogInformation("Declining order {OrderId} with reason: {Reason}", orderId, reason);

                var filter = Builders<ClientOrder>.Filter.Eq(o => o.Id, orderId);
                var update = Builders<ClientOrder>.Update
                    .Set(o => o.Status, OrderStatus.Declined)
                    .Set(o => o.Notes, reason ?? "No reason provided");
                
                var result = await _context.ClientOrders.FindOneAndUpdateAsync(filter, update);
                
                if (result == null)
                {
                    throw new InvalidOperationException($"Order {orderId} not found");
                }

                _logger.LogInformation("Order {OrderId} successfully declined", orderId);
                return result;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when declining order {OrderId}: {Message}", orderId, ex.Message);
                throw new InvalidOperationException($"Database error when declining order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when declining order {OrderId}: {Message}", orderId, ex.Message);
                throw;
            }
        }        public async Task<ClientOrder> CompleteOrderAsync(string orderId, string mechanicId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("OrderId is required", nameof(orderId));
            }

            if (string.IsNullOrEmpty(mechanicId))
            {
                throw new ArgumentException("MechanicId is required", nameof(mechanicId));
            }

            try
            {
                _logger.LogInformation("Completing order {OrderId} by mechanic {MechanicId}", orderId, mechanicId);

                // Update client order
                var orderFilter = Builders<ClientOrder>.Filter.Eq(o => o.Id, orderId);
                var orderUpdate = Builders<ClientOrder>.Update.Set(o => o.Status, OrderStatus.Completed);
                var orderResult = await _context.ClientOrders.UpdateOneAsync(orderFilter, orderUpdate);
                
                if (orderResult.MatchedCount == 0)
                {
                    throw new InvalidOperationException($"Order {orderId} not found");
                }
                
                // Update mechanic order
                var mechanicOrderFilter = Builders<MechanicOrder>.Filter.And(
                    Builders<MechanicOrder>.Filter.Eq(mo => mo.OrderId, orderId),
                    Builders<MechanicOrder>.Filter.Eq(mo => mo.MechanicId, mechanicId)
                );
                
                var mechanicOrderUpdate = Builders<MechanicOrder>.Update
                    .Set(mo => mo.Status, OrderStatus.Completed)
                    .Set(mo => mo.CompletionDate, DateTime.Now);
                
                await _context.MechanicOrders.UpdateOneAsync(mechanicOrderFilter, mechanicOrderUpdate);
                
                var completedOrder = await _context.ClientOrders.Find(orderFilter).FirstOrDefaultAsync();
                _logger.LogInformation("Order {OrderId} successfully completed", orderId);
                return completedOrder;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when completing order {OrderId}: {Message}", orderId, ex.Message);
                throw new InvalidOperationException($"Database error when completing order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when completing order {OrderId}: {Message}", orderId, ex.Message);
                throw;
            }
        }        public async Task<List<ClientOrder>> GetClientOrdersAsync(string clientId)
        {
            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("ClientId is required", nameof(clientId));
            }

            try
            {
                _logger.LogDebug("Retrieving orders for client {ClientId}", clientId);
                var filter = Builders<ClientOrder>.Filter.Eq(o => o.ClientId, clientId);
                var orders = await _context.ClientOrders.Find(filter).ToListAsync();
                _logger.LogDebug("Found {Count} orders for client {ClientId}", orders.Count, clientId);
                return orders;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when retrieving orders for client {ClientId}: {Message}", clientId, ex.Message);
                throw new InvalidOperationException($"Database error when retrieving client orders: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when retrieving orders for client {ClientId}: {Message}", clientId, ex.Message);
                throw;
            }
        }

        public async Task<List<ClientOrder>> GetMechanicOrdersAsync(string mechanicId)
        {
            if (string.IsNullOrEmpty(mechanicId))
            {
                throw new ArgumentException("MechanicId is required", nameof(mechanicId));
            }

            try
            {
                _logger.LogDebug("Retrieving orders for mechanic {MechanicId}", mechanicId);
                var filter = Builders<ClientOrder>.Filter.Eq(o => o.MechanicId, mechanicId);
                var orders = await _context.ClientOrders.Find(filter).ToListAsync();
                _logger.LogDebug("Found {Count} orders for mechanic {MechanicId}", orders.Count, mechanicId);
                return orders;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when retrieving orders for mechanic {MechanicId}: {Message}", mechanicId, ex.Message);
                throw new InvalidOperationException($"Database error when retrieving mechanic orders: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when retrieving orders for mechanic {MechanicId}: {Message}", mechanicId, ex.Message);
                throw;
            }
        }

        public async Task<ClientOrder> GetOrderByIdAsync(string orderId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("OrderId is required", nameof(orderId));
            }

            try
            {
                _logger.LogDebug("Retrieving order {OrderId}", orderId);
                var filter = Builders<ClientOrder>.Filter.Eq(o => o.Id, orderId);
                var order = await _context.ClientOrders.Find(filter).FirstOrDefaultAsync();
                  if (order == null)
                {
                    _logger.LogWarning("Order {OrderId} not found", orderId);
                    return null!;
                }

                _logger.LogDebug("Successfully retrieved order {OrderId}", orderId);
                return order;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when retrieving order {OrderId}: {Message}", orderId, ex.Message);
                throw new InvalidOperationException($"Database error when retrieving order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when retrieving order {OrderId}: {Message}", orderId, ex.Message);
                throw;
            }
        }        public async Task<List<ClientOrder>> GetPendingOrdersAsync()
        {
            try
            {
                _logger.LogDebug("Retrieving all pending orders");
                var filter = Builders<ClientOrder>.Filter.Eq(o => o.Status, OrderStatus.Pending);
                var orders = await _context.ClientOrders.Find(filter).ToListAsync();
                _logger.LogDebug("Found {Count} pending orders", orders.Count);
                return orders;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when retrieving pending orders: {Message}", ex.Message);
                throw new InvalidOperationException($"Database error when retrieving pending orders: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when retrieving pending orders: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> CancelOrderAsync(string orderId, string clientId)
        {
            if (string.IsNullOrEmpty(orderId))
            {
                throw new ArgumentException("OrderId is required", nameof(orderId));
            }

            if (string.IsNullOrEmpty(clientId))
            {
                throw new ArgumentException("ClientId is required", nameof(clientId));
            }

            try
            {
                _logger.LogInformation("Cancelling order {OrderId} by client {ClientId}", orderId, clientId);

                // First retrieve the order to ensure it exists and belongs to the client
                var filter = Builders<ClientOrder>.Filter.And(
                    Builders<ClientOrder>.Filter.Eq(o => o.Id, orderId),
                    Builders<ClientOrder>.Filter.Eq(o => o.ClientId, clientId)
                );

                // Additional check to ensure order is in a cancellable state (only pending orders can be cancelled)
                filter = Builders<ClientOrder>.Filter.And(
                    filter,
                    Builders<ClientOrder>.Filter.Eq(o => o.Status, OrderStatus.Pending)
                );

                var order = await _context.ClientOrders.Find(filter).FirstOrDefaultAsync();

                if (order == null)
                {
                    _logger.LogWarning("Order {OrderId} not found or not cancellable", orderId);
                    return false;
                }

                // Delete the order from the database
                var result = await _context.ClientOrders.DeleteOneAsync(filter);

                if (result.DeletedCount == 0)
                {
                    _logger.LogWarning("Order {OrderId} not deleted, possibly already processed", orderId);
                    return false;
                }

                _logger.LogInformation("Order {OrderId} successfully cancelled and deleted", orderId);
                return true;
            }
            catch (MongoException ex)
            {
                _logger.LogError(ex, "MongoDB error when cancelling order {OrderId}: {Message}", orderId, ex.Message);
                throw new InvalidOperationException($"Database error when cancelling order: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error when cancelling order {OrderId}: {Message}", orderId, ex.Message);
                throw;
            }
        }
    }
}
