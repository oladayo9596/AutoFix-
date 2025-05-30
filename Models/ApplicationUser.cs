using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutoFix.Models
{
    public class ApplicationUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string FullName { get; set; }
        
        public string UserName { get; set; }
        
        public string Email { get; set; }
        
        public string PasswordHash { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string Role { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        public DateTime? LastLoginDate { get; set; }
    }
}
