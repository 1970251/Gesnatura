namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatePercurso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Percursoes", "Comentario", c => c.String());
            AddColumn("dbo.Percursoes", "Aprovado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Percursoes", "Aprovado");
            DropColumn("dbo.Percursoes", "Comentario");
        }
    }
}
