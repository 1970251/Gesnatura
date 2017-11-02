namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFotoAtlas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Especies", "Percurso_ID", c => c.Int());
            CreateIndex("dbo.Especies", "Percurso_ID");
            AddForeignKey("dbo.Especies", "Percurso_ID", "dbo.Percursoes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Especies", "Percurso_ID", "dbo.Percursoes");
            DropIndex("dbo.Especies", new[] { "Percurso_ID" });
            DropColumn("dbo.Especies", "Percurso_ID");
        }
    }
}
