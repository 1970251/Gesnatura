namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Percursoes", "Localidade", c => c.String());
            AddColumn("dbo.Percursoes", "Classificacao", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Percursoes", "Classificacao");
            DropColumn("dbo.Percursoes", "Localidade");
        }
    }
}
