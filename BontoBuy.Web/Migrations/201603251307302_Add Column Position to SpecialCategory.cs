namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnPositiontoSpecialCategory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SpecialCategory", "Position", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SpecialCategory", "Position");
        }
    }
}
