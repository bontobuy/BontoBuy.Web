namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ColumnHasApprovedToTableReturn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Return", "HasApproved", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Return", "HasApproved", c => c.String());
        }
    }
}