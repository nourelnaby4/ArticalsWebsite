namespace MOIC_ASU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TempArticale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TempArticales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArticaleTitle = c.String(),
                        ArticaleCover = c.String(),
                        ArticaleContent = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TempArticales");
        }
    }
}
