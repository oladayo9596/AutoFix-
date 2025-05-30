using AutoFix.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace AutoFix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiagnosticController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly MongoDbSettings _settings;

        public DiagnosticController(MongoDbContext context, IOptions<MongoDbSettings> settings)
        {
            _context = context;
            _settings = settings.Value;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                var result = new
                {
                    ConnectionString = _settings.ConnectionString.Substring(0, 20) + "...[redacted]",
                    DatabaseName = _settings.DatabaseName
                };

                // Test if we can connect
                var client = new MongoClient(_settings.ConnectionString);
                var database = client.GetDatabase(_settings.DatabaseName);
                
                // Test if we can ping
                await database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
                
                // Test if we can read collections
                var collections = await database.ListCollectionNamesAsync();
                var collectionList = await collections.ToListAsync();
                
                return Ok(new
                {
                    Message = "Connection successful",
                    Settings = result,
                    Collections = collectionList
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new 
                {
                    Message = "Connection failed",
                    Error = ex.Message,
                    InnerError = ex.InnerException?.Message,
                    StackTrace = ex.StackTrace
                });
            }
        }
    }
}
