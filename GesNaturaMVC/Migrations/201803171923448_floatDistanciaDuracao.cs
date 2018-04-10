namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class floatDistanciaDuracao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Percursoes", "Distancia", c => c.Single(nullable: false));
            AlterColumn("dbo.Percursoes", "DuracaoAproximada", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Percursoes", "DuracaoAproximada", c => c.Int(nullable: false));
            AlterColumn("dbo.Percursoes", "Distancia", c => c.Int(nullable: false));
        }
    }
}
