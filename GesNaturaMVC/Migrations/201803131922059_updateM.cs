namespace GesNaturaMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PercursoComentarios", "DataHora", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PercursoComentarios", "DataHora", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
    }
}
