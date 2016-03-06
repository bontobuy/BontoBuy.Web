namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class ResolveMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.ReturnStatus",
            //    c => new
            //        {
            //            ReturnStatusId = c.Int(nullable: false, identity: true),
            //            Status = c.String(),
            //        })
            //    .PrimaryKey(t => t.ReturnStatusId);

            AlterColumn("dbo.Admin", "AdminId", c => c.Int(nullable: false, identity: true));

            //DropColumn("dbo.Payment", "PaymentDate");
        }

        public override void Down()
        {
            //AddColumn("dbo.Payment", "PaymentDate", c => c.Int(nullable: false));
            AlterColumn("dbo.Admin", "AdminId", c => c.Int(nullable: false));

            //DropTable("dbo.ReturnStatus");
        }
    }
}