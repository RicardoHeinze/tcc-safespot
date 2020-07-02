namespace TCCSafeSpot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoDataCrimeCadastrado : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CrimeCadastrado", "Data", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CrimeCadastrado", "Data", c => c.DateTime(nullable: false));
        }
    }
}
