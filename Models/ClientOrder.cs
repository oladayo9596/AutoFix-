using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutoFix.Models
{
    public enum OrderStatus
    {
        Pending,
        Accepted,
        Declined,
        Completed,
        Cancelled
    }
    
    public class ClientOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string ClientId { get; set; } = string.Empty;
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string MechanicId { get; set; } = string.Empty;
        
        public string ClientName { get; set; } = string.Empty;
        
        public string MechanicName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Service type is required")]
        public string ServiceType { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Description is required")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters long")]
        [MaxLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;
        
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "Scheduled time is required")]
        public DateTime? ScheduledTime { get; set; }
        
        [Required(ErrorMessage = "Service location is required")]
        public string Location { get; set; } = string.Empty;
          [Range(0, double.MaxValue, ErrorMessage = "Estimated price must be a positive number")]
        public decimal EstimatedPrice { get; set; }
        
        public string Notes { get; set; } = string.Empty;
    }
}
