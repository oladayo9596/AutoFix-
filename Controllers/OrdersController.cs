using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoFix.Models;
using AutoFix.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AutoFix.Controllers
{
    [Authorize]    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> MyOrders()
        {
            var clientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (clientId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = await _orderService.GetClientOrdersAsync(clientId);
            return View(orders);
        }

        [Authorize(Roles = "Client")]
        public IActionResult CreateOrder()
        {
            return View();
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateOrder(ClientOrder order)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill in all required fields correctly.";
                return View(order);
            }

            try
            {
                // Set client info
                var clientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(clientId))
                {
                    _logger.LogWarning("Client ID not found in claims");
                    TempData["ErrorMessage"] = "User session expired. Please login again.";
                    return RedirectToAction("Login", "Account");
                }

                var fullName = User.FindFirst("FullName")?.Value ?? "Unknown Client";
                
                if (order == null)
                {
                    order = new ClientOrder();
                }
                
                // Validate required fields
                if (string.IsNullOrWhiteSpace(order.ServiceType))
                {
                    ModelState.AddModelError("ServiceType", "Service type is required.");
                    TempData["ErrorMessage"] = "Service type is required.";
                    return View(order);
                }

                if (string.IsNullOrWhiteSpace(order.Description))
                {
                    ModelState.AddModelError("Description", "Description is required.");
                    TempData["ErrorMessage"] = "Service description is required.";
                    return View(order);
                }

                if (string.IsNullOrWhiteSpace(order.Location))
                {
                    ModelState.AddModelError("Location", "Service location is required.");
                    TempData["ErrorMessage"] = "Service location is required.";
                    return View(order);
                }
                
                order.ClientId = clientId;
                order.ClientName = fullName;
                order.Status = OrderStatus.Pending;
                order.OrderDate = DateTime.Now;

                var createdOrder = await _orderService.CreateOrderAsync(order);
                
                if (createdOrder != null && !string.IsNullOrEmpty(createdOrder.Id))
                {
                    _logger.LogInformation("New order {OrderId} created successfully by client {ClientId}", createdOrder.Id, clientId);
                    TempData["SuccessMessage"] = $"Service request for '{order.ServiceType}' has been created successfully! Request ID: #{createdOrder.Id.Substring(createdOrder.Id.Length - 6)}";
                }
                else
                {
                    _logger.LogWarning("Order creation returned null or empty ID for client {ClientId}", clientId);
                    TempData["WarningMessage"] = "Service request was created but there may have been an issue. Please check your orders list.";
                }

                return RedirectToAction(nameof(MyOrders));
            }
            catch (ArgumentException argEx)
            {
                _logger.LogError(argEx, "Validation error creating order for client {ClientId}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                TempData["ErrorMessage"] = $"Validation error: {argEx.Message}";
                ModelState.AddModelError("", argEx.Message);
                return View(order);
            }
            catch (InvalidOperationException opEx)
            {
                _logger.LogError(opEx, "Database error creating order for client {ClientId}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                TempData["ErrorMessage"] = "There was a problem connecting to the database. Please try again later.";
                ModelState.AddModelError("", "Database connection error. Please try again.");
                return View(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating order for client {ClientId}", User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                TempData["ErrorMessage"] = "An unexpected error occurred while creating your service request. Please try again.";
                ModelState.AddModelError("", "An error occurred while creating your order. Please try again.");
                return View(order);
            }
        }

        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> PendingOrders()
        {
            var orders = await _orderService.GetPendingOrdersAsync();
            return View(orders);
        }

        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> MyAcceptedOrders()
        {
            var mechanicId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (mechanicId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = await _orderService.GetMechanicOrdersAsync(mechanicId);
            return View(orders);
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> AcceptOrder(string orderId, string notes)
        {
            try
            {
                var mechanicId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(mechanicId))
                {
                    _logger.LogWarning("Mechanic ID not found in claims when accepting order");
                    return RedirectToAction("Login", "Account");
                }

                var mechanicName = User.FindFirst("FullName")?.Value ?? "Unknown Mechanic";
                
                if (string.IsNullOrEmpty(orderId))
                {
                    _logger.LogWarning("Order ID is null or empty when accepting order");
                    return BadRequest("Order ID is required");
                }

                await _orderService.AcceptOrderAsync(orderId, mechanicId, mechanicName, notes ?? string.Empty);
                _logger.LogInformation("Order {OrderId} accepted by mechanic {MechanicId}", orderId, mechanicId);
                return RedirectToAction(nameof(MyAcceptedOrders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error accepting order {OrderId}", orderId);
                TempData["ErrorMessage"] = "An error occurred while accepting the order";
                return RedirectToAction(nameof(PendingOrders));
            }
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> DeclineOrder(string orderId, string reason)
        {
            try
            {
                var mechanicId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(mechanicId))
                {
                    _logger.LogWarning("Mechanic ID not found in claims when declining order");
                    return RedirectToAction("Login", "Account");
                }
                
                if (string.IsNullOrEmpty(orderId))
                {
                    _logger.LogWarning("Order ID is null or empty when declining order");
                    return BadRequest("Order ID is required");
                }

                await _orderService.DeclineOrderAsync(orderId, mechanicId, reason ?? "No reason provided");
                _logger.LogInformation("Order {OrderId} declined by mechanic {MechanicId}", orderId, mechanicId);
                return RedirectToAction(nameof(PendingOrders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error declining order {OrderId}", orderId);
                TempData["ErrorMessage"] = "An error occurred while declining the order";
                return RedirectToAction(nameof(PendingOrders));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> CompleteOrder(string orderId)
        {
            try
            {
                var mechanicId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(mechanicId))
                {
                    _logger.LogWarning("Mechanic ID not found in claims when completing order");
                    return RedirectToAction("Login", "Account");
                }
                
                if (string.IsNullOrEmpty(orderId))
                {
                    _logger.LogWarning("Order ID is null or empty when completing order");
                    return BadRequest("Order ID is required");
                }

                await _orderService.CompleteOrderAsync(orderId, mechanicId);
                _logger.LogInformation("Order {OrderId} completed by mechanic {MechanicId}", orderId, mechanicId);
                return RedirectToAction(nameof(MyAcceptedOrders));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error completing order {OrderId}", orderId);
                TempData["ErrorMessage"] = "An error occurred while completing the order";
                return RedirectToAction(nameof(MyAcceptedOrders));
            }
        }        [Authorize]
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    _logger.LogWarning("Order ID is null or empty when viewing details");
                    return BadRequest("Order ID is required");
                }
                
                var order = await _orderService.GetOrderByIdAsync(id);
                if (order == null)
                {
                    _logger.LogWarning("Order {OrderId} not found", id);
                    return NotFound();
                }

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                {
                    _logger.LogWarning("User ID not found in claims when viewing order details");
                    return RedirectToAction("Login", "Account");
                }
                
                var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

                // Check if the user has access to this order
                if (userRole == "Client" && order.ClientId != userId)
                {
                    _logger.LogWarning("Client {UserId} attempted to access order {OrderId} belonging to another client", userId, id);
                    return Forbid();
                }
                else if (userRole == "Mechanic" && 
                        order.Status != OrderStatus.Pending && 
                        order.MechanicId != userId)
                {
                    _logger.LogWarning("Mechanic {UserId} attempted to access order {OrderId} assigned to another mechanic", userId, id);
                    return Forbid();
                }

                return View(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving order details for {OrderId}", id);
                return RedirectToAction("Index", "Home");
            }
        }

        [Authorize(Roles = "Mechanic")]
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var mechanicId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(mechanicId))
                {
                    return RedirectToAction("Login", "Account");
                }

                // Get all orders for the mechanic
                var mechanicOrders = await _orderService.GetMechanicOrdersAsync(mechanicId);
                
                // Get counts for different statuses
                var acceptedOrders = mechanicOrders.Where(o => o.Status == OrderStatus.Accepted).ToList();
                var completedOrders = mechanicOrders.Where(o => o.Status == OrderStatus.Completed).ToList();
                
                // Get all pending orders
                var pendingOrders = await _orderService.GetPendingOrdersAsync();
                
                // Create the dashboard view model
                var dashboardViewModel = new Dictionary<string, object>
                {
                    { "AcceptedOrders", acceptedOrders.Take(5).ToList() },
                    { "CompletedOrders", completedOrders.Take(5).ToList() },
                    { "PendingOrders", pendingOrders.Take(5).ToList() },
                    { "AcceptedCount", acceptedOrders.Count },
                    { "CompletedCount", completedOrders.Count },
                    { "PendingCount", pendingOrders.Count }
                };
                
                return View(dashboardViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading mechanic dashboard");
                TempData["ErrorMessage"] = "An error occurred while loading your dashboard";
                return RedirectToAction("Index", "Home");
            }
        }
        
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> FilterOrders(string status)
        {
            try
            {
                var clientId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(clientId))
                {
                    return RedirectToAction("Login", "Account");
                }
                
                var orders = await _orderService.GetClientOrdersAsync(clientId);
                
                // Filter orders by status if provided
                if (!string.IsNullOrEmpty(status) && Enum.TryParse<OrderStatus>(status, true, out var orderStatus))
                {
                    orders = orders.Where(o => o.Status == orderStatus).ToList();
                }
                
                ViewBag.CurrentFilter = status;
                return View("MyOrders", orders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering orders");
                TempData["ErrorMessage"] = "An error occurred while filtering your orders";
                return RedirectToAction(nameof(MyOrders));
            }
        }
    }
}
