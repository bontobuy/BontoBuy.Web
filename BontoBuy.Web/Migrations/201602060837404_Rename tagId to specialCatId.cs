namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class RenametagIdtospecialCatId : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Specification", name: "TagId", newName: "SpecialCatId");
            RenameIndex(table: "dbo.Specification", name: "IX_TagId", newName: "IX_SpecialCatId");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.Specification", name: "IX_SpecialCatId", newName: "IX_TagId");
            RenameColumn(table: "dbo.Specification", name: "SpecialCatId", newName: "TagId");
        }
    }
}