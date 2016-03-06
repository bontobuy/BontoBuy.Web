namespace BontoBuy.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddReturnStatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnStatus",
                c => new
                    {
                        ReturnStatusId = c.Int(nullable: false, identity: true),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.ReturnStatusId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReturnStatus");
        }
    }
}
