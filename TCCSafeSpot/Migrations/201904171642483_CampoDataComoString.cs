namespace TCCSafeSpot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoDataComoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CrimeSSP", "Data", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CrimeSSP", "Data", c => c.DateTime(nullable: false));
        }
    }
}
