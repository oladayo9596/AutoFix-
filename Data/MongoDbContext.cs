using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;
using AutoFix.Models;

namespace AutoFix.Data
{    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string ClientsCollectionName { get; set; } = string.Empty;
        public string MechanicsCollectionName { get; set; } = string.Empty;
        public string ClientOrdersCollectionName { get; set; } = string.Empty;
        public string MechanicOrdersCollectionName { get; set; } = string.Empty;
    }public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbSettings _settings;        private readonly ILogger<MongoDbContext>? _logger;

        public MongoDbContext(IOptions<MongoDbSettings> settings, ILogger<MongoDbContext>? logger = null)
        {
            _settings = settings.Value;
            _logger = logger;            try
            {
                var clientSettings = MongoClientSettings.FromConnectionString(_settings.ConnectionString);
                clientSettings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);
                clientSettings.ConnectTimeout = TimeSpan.FromSeconds(30);
                clientSettings.SocketTimeout = TimeSpan.FromSeconds(30);
                clientSettings.RetryWrites = true;
                clientSettings.MaxConnectionPoolSize = 100;
                  var client = new MongoClient(clientSettings);
                _database = client.GetDatabase(_settings.DatabaseName);
                
                _logger?.LogInformation("MongoDB context initialized with database: {DatabaseName}", _settings.DatabaseName);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Failed to connect to MongoDB: {Message}", ex.Message);
                Console.Error.WriteLine($"MongoDB Connection Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.Error.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                throw; // Re-throw to let the application handle it appropriately
            }
        }

        public IMongoCollection<Client> Clients => 
            _database.GetCollection<Client>(_settings.ClientsCollectionName);

        public IMongoCollection<Mechanic> Mechanics => 
            _database.GetCollection<Mechanic>(_settings.MechanicsCollectionName);

        public IMongoCollection<ClientOrder> ClientOrders => 
            _database.GetCollection<ClientOrder>(_settings.ClientOrdersCollectionName);

        public IMongoCollection<MechanicOrder> MechanicOrders => 
            _database.GetCollection<MechanicOrder>(_settings.MechanicOrdersCollectionName);
    }
}
