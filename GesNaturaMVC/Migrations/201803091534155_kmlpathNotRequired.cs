namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kmlpathNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Percursoes", "KmlPath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Percursoes", "KmlPath", c => c.String(nullable: false));
        }
    }
}
