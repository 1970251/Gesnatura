namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fotos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FotoPercursos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PercursoID = c.Int(nullable: false),
                        Caminho = c.String(),
                        GPS_Lat = c.Single(nullable: false),
                        GPS_Long = c.Single(nullable: false),
                        Aprovado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Percursoes", t => t.PercursoID, cascadeDelete: true)
                .Index(t => t.PercursoID);
            
            CreateTable(
                "dbo.FotoPois",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PoiID = c.Int(nullable: false),
                        Caminho = c.String(),
                        GPS_Lat = c.Single(nullable: false),
                        GPS_Long = c.Single(nullable: false),
                        Aprovado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.POIs", t => t.PoiID, cascadeDelete: true)
                .Index(t => t.PoiID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FotoPois", "PoiID", "dbo.POIs");
            DropForeignKey("dbo.FotoPercursos", "PercursoID", "dbo.Percursoes");
            DropIndex("dbo.FotoPois", new[] { "PoiID" });
            DropIndex("dbo.FotoPercursos", new[] { "PercursoID" });
            DropTable("dbo.FotoPois");
            DropTable("dbo.FotoPercursos");
        }
    }
}
