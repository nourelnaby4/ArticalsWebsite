namespace MOIC_ASU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addArticale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Articales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AtricaleTitle = c.String(),
                        AtricaleCover = c.String(),
                        AtricaleContant = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Articales", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Articales", new[] { "CategoryId" });
            DropTable("dbo.Articales");
        }
    }
}
