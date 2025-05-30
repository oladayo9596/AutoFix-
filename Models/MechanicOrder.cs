using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutoFix.Models
{
    public class MechanicOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; } = string.Empty;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string MechanicId { get; set; } = string.Empty;
        
        public string MechanicName { get; set; } = string.Empty;
        
        public OrderStatus Status { get; set; } = OrderStatus.Accepted;
        
        public DateTime AcceptedDate { get; set; } = DateTime.Now;
        
        public string Notes { get; set; } = string.Empty;
        
        public DateTime? CompletionDate { get; set; }
    }
}
