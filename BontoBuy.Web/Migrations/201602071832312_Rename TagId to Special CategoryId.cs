namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RenameTagIdtoSpecialCategoryId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SpecialCategory", name: "TagId", newName: "SpecialCatId");
        }

        public override void Down()
        {
            RenameColumn(table: "dbo.Specification", name: "SpecialCatId", newName: "TagId");
        }
    }
}