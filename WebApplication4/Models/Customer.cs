﻿using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public DateTime Birthday { get; set; }
        public List<Order> Orders { get; set; }
    }
}