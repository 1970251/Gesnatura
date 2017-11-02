namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FotoAtlas : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        ReinoID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Reinoes", t => t.ReinoID, cascadeDelete: true)
                .Index(t => t.ReinoID);
            
            CreateTable(
                "dbo.Reinoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Especies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        NomeCientifico = c.String(),
                        GeneroID = c.Int(nullable: false),
                        Descricao = c.String(),
                        Calendario = c.String(),
                        Abundancia = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Generoes", t => t.GeneroID, cascadeDelete: true)
                .Index(t => t.GeneroID);
            
            CreateTable(
                "dbo.FotoAtlas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EspecieID = c.Int(nullable: false),
                        Caminho = c.String(),
                        Aprovado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Especies", t => t.EspecieID, cascadeDelete: true)
                .Index(t => t.EspecieID);
            
            CreateTable(
                "dbo.Generoes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        FamiliaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Familias", t => t.FamiliaID, cascadeDelete: true)
                .Index(t => t.FamiliaID);
            
            CreateTable(
                "dbo.Familias",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        OrdemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ordems", t => t.OrdemID, cascadeDelete: true)
                .Index(t => t.OrdemID);
            
            CreateTable(
                "dbo.Ordems",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        ClasseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Classes", t => t.ClasseID, cascadeDelete: true)
                .Index(t => t.ClasseID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Especies", "GeneroID", "dbo.Generoes");
            DropForeignKey("dbo.Generoes", "FamiliaID", "dbo.Familias");
            DropForeignKey("dbo.Familias", "OrdemID", "dbo.Ordems");
            DropForeignKey("dbo.Ordems", "ClasseID", "dbo.Classes");
            DropForeignKey("dbo.FotoAtlas", "EspecieID", "dbo.Especies");
            DropForeignKey("dbo.Classes", "ReinoID", "dbo.Reinoes");
            DropIndex("dbo.Ordems", new[] { "ClasseID" });
            DropIndex("dbo.Familias", new[] { "OrdemID" });
            DropIndex("dbo.Generoes", new[] { "FamiliaID" });
            DropIndex("dbo.FotoAtlas", new[] { "EspecieID" });
            DropIndex("dbo.Especies", new[] { "GeneroID" });
            DropIndex("dbo.Classes", new[] { "ReinoID" });
            DropTable("dbo.Ordems");
            DropTable("dbo.Familias");
            DropTable("dbo.Generoes");
            DropTable("dbo.FotoAtlas");
            DropTable("dbo.Especies");
            DropTable("dbo.Reinoes");
            DropTable("dbo.Classes");
        }
    }
}
