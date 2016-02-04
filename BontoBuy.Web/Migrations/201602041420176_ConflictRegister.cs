namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ConflictRegister : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "SupplierId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "Website", c => c.String());
            AddColumn("dbo.AspNetUsers", "Discriminator", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Discriminator");
            DropColumn("dbo.AspNetUsers", "Website");
            DropColumn("dbo.AspNetUsers", "SupplierId");
        }
    }
}
