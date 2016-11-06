using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using WebApplication4.Models;

namespace WebApplication4.DataAccess.Mapping
{
    public class CategoryMap : EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Categories");
            HasMany(x => x.Products);
            HasKey(x => x.Id);
        }
    }
}