namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fotoPoisAlterPoi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.POIs", "FotoPoi_ID", c => c.Int());
            AddColumn("dbo.FotoPois", "POI_ID", c => c.Int());
            CreateIndex("dbo.POIs", "FotoPoi_ID");
            CreateIndex("dbo.FotoPois", "POI_ID");
            AddForeignKey("dbo.POIs", "FotoPoi_ID", "dbo.FotoPois", "ID");
            AddForeignKey("dbo.FotoPois", "POI_ID", "dbo.POIs", "ID");
            DropColumn("dbo.FotoPois", "GPS_Lat");
            DropColumn("dbo.FotoPois", "GPS_Long");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FotoPois", "GPS_Long", c => c.Single(nullable: false));
            AddColumn("dbo.FotoPois", "GPS_Lat", c => c.Single(nullable: false));
            DropForeignKey("dbo.FotoPois", "POI_ID", "dbo.POIs");
            DropForeignKey("dbo.POIs", "FotoPoi_ID", "dbo.FotoPois");
            DropIndex("dbo.FotoPois", new[] { "POI_ID" });
            DropIndex("dbo.POIs", new[] { "FotoPoi_ID" });
            DropColumn("dbo.FotoPois", "POI_ID");
            DropColumn("dbo.POIs", "FotoPoi_ID");
        }
    }
}
