namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterPoi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POIs", "Descricao", c => c.String(nullable: false));
            DropColumn("dbo.POIs", "Localizacao");
        }
        
        public override void Down()
        {
            AddColumn("dbo.POIs", "Localizacao", c => c.String(nullable: false));
            DropColumn("dbo.POIs", "Descricao");
        }
    }
}
