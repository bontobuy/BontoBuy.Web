namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveModelSpecDescription : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ModelSpecViewModels", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ModelSpecViewModels", "Description", c => c.String());
        }
    }
}
