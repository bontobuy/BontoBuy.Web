namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveTermsandConditions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Model", "TermsAndConditions", c => c.String());
            DropColumn("dbo.Item", "TermsAndConditions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "TermsAndConditions", c => c.String());
            DropColumn("dbo.Model", "TermsAndConditions");
        }
    }
}
