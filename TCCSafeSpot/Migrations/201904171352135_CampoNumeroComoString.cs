namespace TCCSafeSpot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoNumeroComoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Endereco", "Numero", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Endereco", "Numero", c => c.Int(nullable: false));
        }
    }
}
