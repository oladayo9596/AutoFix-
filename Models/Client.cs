using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutoFix.Models
{
    public class Client : ApplicationUser
    {
        // Additional properties for Client
        public string Address { get; set; } = string.Empty;
        
        // Vehicle information stored as dictionary
        public Dictionary<string, string> VehicleInformation { get; set; } = new Dictionary<string, string>();
        
        public Client()
        {
            Role = "Client";
        }
    }
}
