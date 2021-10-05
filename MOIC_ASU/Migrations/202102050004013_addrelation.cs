namespace MOIC_ASU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TempArticales", "CategoryId", c => c.Int(nullable: false));
            AddColumn("dbo.TempArticales", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.TempArticales", "CategoryId");
            CreateIndex("dbo.TempArticales", "UserID");
            AddForeignKey("dbo.TempArticales", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TempArticales", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TempArticales", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.TempArticales", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TempArticales", new[] { "UserID" });
            DropIndex("dbo.TempArticales", new[] { "CategoryId" });
            DropColumn("dbo.TempArticales", "UserID");
            DropColumn("dbo.TempArticales", "CategoryId");
        }
    }
}
