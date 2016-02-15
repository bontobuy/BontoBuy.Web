namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDtCreatedModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "DtCreated", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Model", "DtCreated");
        }
    }
}
