namespace MOIC_ASU.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class error_alpha : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articales", "ArticaleTitle", c => c.String());
            AddColumn("dbo.Articales", "ArticaleCover", c => c.String());
            AddColumn("dbo.Articales", "ArticaleContent", c => c.String());
            DropColumn("dbo.Articales", "AtricaleTitle");
            DropColumn("dbo.Articales", "AtricaleCover");
            DropColumn("dbo.Articales", "AtricaleContant");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Articales", "AtricaleContant", c => c.String());
            AddColumn("dbo.Articales", "AtricaleCover", c => c.String());
            AddColumn("dbo.Articales", "AtricaleTitle", c => c.String());
            DropColumn("dbo.Articales", "ArticaleContent");
            DropColumn("dbo.Articales", "ArticaleCover");
            DropColumn("dbo.Articales", "ArticaleTitle");
        }
    }
}
