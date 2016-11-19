namespace WebApplication4.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginAndPasswordToCustomers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Login", c => c.String());
            AddColumn("dbo.Customers", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "Password");
            DropColumn("dbo.Customers", "Login");
        }
    }
}
