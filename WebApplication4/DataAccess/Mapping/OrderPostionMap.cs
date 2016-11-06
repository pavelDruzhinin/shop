using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication4.DataAccess.Mapping
{
    public class OrderPostionMap : EntityTypeConfiguration<OrderPosition>
    {
        public OrderPostionMap()
        {
            ToTable("OrderPositions");
            HasKey(x => x.Id);
            HasRequired(x => x.Product);
            HasRequired(x => x.Order);
        }
    }
}