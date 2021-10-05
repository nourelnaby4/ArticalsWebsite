namespace MOIC_ASU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adduserarticales : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articales", "UserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Articales", "UserID");
            AddForeignKey("dbo.Articales", "UserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articales", "UserID", "dbo.AspNetUsers");
            DropIndex("dbo.Articales", new[] { "UserID" });
            DropColumn("dbo.Articales", "UserID");
        }
    }
}
