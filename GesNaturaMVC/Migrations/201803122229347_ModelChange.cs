namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelChange : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PercursoComentarios",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PercursoID = c.Int(nullable: false),
                        Comentario = c.String(),
                        Classificacao = c.Int(nullable: false),
                        DataHora = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Percursoes", t => t.PercursoID, cascadeDelete: true)
                .Index(t => t.PercursoID);
            
            DropColumn("dbo.Percursoes", "Classificacao");
            DropColumn("dbo.Percursoes", "Comentario");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Percursoes", "Comentario", c => c.String());
            AddColumn("dbo.Percursoes", "Classificacao", c => c.Int(nullable: false));
            DropForeignKey("dbo.PercursoComentarios", "PercursoID", "dbo.Percursoes");
            DropIndex("dbo.PercursoComentarios", new[] { "PercursoID" });
            DropTable("dbo.PercursoComentarios");
        }
    }
}
