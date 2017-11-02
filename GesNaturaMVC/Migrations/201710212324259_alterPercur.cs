namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterPercur : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Percursoes", "DuracaoAproximada", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Percursoes", "DuracaoAproximada", c => c.String(nullable: false));
        }
    }
}
