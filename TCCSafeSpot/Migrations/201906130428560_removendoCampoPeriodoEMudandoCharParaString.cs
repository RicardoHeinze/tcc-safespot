namespace TCCSafeSpot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removendoCampoPeriodoEMudandoCharParaString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vitima", "Sexo", c => c.String(maxLength: 2));
            DropColumn("dbo.CrimeCadastrado", "Periodo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CrimeCadastrado", "Periodo", c => c.String(maxLength: 15));
            DropColumn("dbo.Vitima", "Sexo");
        }
    }
}
