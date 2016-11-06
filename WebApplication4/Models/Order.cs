using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Discount { get; set; }
        public string Address { get; set; }
        public List<OrderPosition> OrderPositions { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
    }
}