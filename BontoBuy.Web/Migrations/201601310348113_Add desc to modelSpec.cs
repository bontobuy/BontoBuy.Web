namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdddesctomodelSpec : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ModelSpecViewModels", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ModelSpecViewModels", "Description");
        }
    }
}
