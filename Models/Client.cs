using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutoFix.Models
{
    public class Client : ApplicationUser
    {
        // No additional properties for now, inherits all from ApplicationUser
        // Role will be set to "Client" by default
        
        public Client()
        {
            Role = "Client";
        }
    }
}
