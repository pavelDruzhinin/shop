using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication4.DataAccess.Mapping
{
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            ToTable("Customers");
            HasKey(x => x.Id);
            HasMany(x => x.Orders);
        }
    }
}