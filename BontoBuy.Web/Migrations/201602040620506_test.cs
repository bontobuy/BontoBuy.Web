namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Brand", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Category", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Item", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Product", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.ModelSpecViewModels", "Value", c => c.String(nullable: false));
            AlterColumn("dbo.Specification", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Tag", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Photo", "ImagePath", c => c.String(nullable: false));
        }

        public override void Down()
        {
            AddColumn("dbo.Item", "Brand", c => c.String());
            AlterColumn("dbo.Photo", "ImagePath", c => c.String());
            AlterColumn("dbo.Tag", "Description", c => c.String());
            AlterColumn("dbo.Specification", "Description", c => c.String());
            AlterColumn("dbo.ModelSpecViewModels", "Value", c => c.String());
            AlterColumn("dbo.Product", "Description", c => c.String());
            AlterColumn("dbo.Item", "Description", c => c.String());
            AlterColumn("dbo.Category", "Description", c => c.String());
            AlterColumn("dbo.Brand", "Name", c => c.String());
        }
    }
}