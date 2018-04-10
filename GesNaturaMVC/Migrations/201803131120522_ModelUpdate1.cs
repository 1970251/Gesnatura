namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PercursoComentarios", "DataHora", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PercursoComentarios", "DataHora", c => c.DateTime(nullable: false));
        }
    }
}
