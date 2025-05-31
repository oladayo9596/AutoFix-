using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AutoFix.Models
{
    public class Mechanic : ApplicationUser
    {
        public List<string> Skills { get; set; } = new List<string>();
        
        public List<string> Services { get; set; } = new List<string>();
        
        public string Bio { get; set; } = string.Empty;
        
        public string Address { get; set; } = string.Empty;
        
        public double Rating { get; set; } = 0.0;
        
        public int CompletedOrders { get; set; } = 0;
        
        public Mechanic()
        {
            Role = "Mechanic";
        }
    }
}
