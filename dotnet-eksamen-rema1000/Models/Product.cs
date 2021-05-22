using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_eksamen_rema1000.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Unit { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int StockAmount { get; set; }
        public Supplier Supplier { get; set; }
    }
}
