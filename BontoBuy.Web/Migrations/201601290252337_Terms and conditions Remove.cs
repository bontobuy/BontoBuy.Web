namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TermsandconditionsRemove : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Item", "TermsAndConditions");
            DropColumn("dbo.Model", "TermsAndConditions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Model", "TermsAndConditions", c => c.String());
            AddColumn("dbo.Item", "TermsAndConditions", c => c.String());
        }
    }
}
