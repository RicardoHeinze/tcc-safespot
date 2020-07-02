namespace TCCSafeSpot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudaRelacionamentoVitima : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VitimaCrimeCadastrado", "CrimeCadastradoId", "dbo.CrimeCadastrado");
            DropForeignKey("dbo.CrimeCadastrado", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.CrimeCadastrado", "TipoCrimeId", "dbo.TipoCrime");
            DropForeignKey("dbo.CrimeSSP", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.CrimeSSP", "TipoCrimeId", "dbo.TipoCrime");
            DropForeignKey("dbo.VitimaCrimeCadastrado", "VitimaId", "dbo.Vitima");
            DropIndex("dbo.VitimaCrimeCadastrado", new[] { "CrimeCadastradoId" });
            DropIndex("dbo.VitimaCrimeCadastrado", new[] { "VitimaId" });
            AddColumn("dbo.CrimeCadastrado", "VitimaId", c => c.Int(nullable: false));
            CreateIndex("dbo.CrimeCadastrado", "VitimaId");
            AddForeignKey("dbo.CrimeCadastrado", "EnderecoId", "dbo.Endereco", "Id");
            AddForeignKey("dbo.CrimeCadastrado", "TipoCrimeId", "dbo.TipoCrime", "Id");
            AddForeignKey("dbo.CrimeSSP", "EnderecoId", "dbo.Endereco", "Id");
            AddForeignKey("dbo.CrimeSSP", "TipoCrimeId", "dbo.TipoCrime", "Id");
            AddForeignKey("dbo.CrimeCadastrado", "VitimaId", "dbo.Vitima", "Id");
            DropTable("dbo.VitimaCrimeCadastrado");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VitimaCrimeCadastrado",
                c => new
                    {
                        CrimeCadastradoId = c.Int(nullable: false),
                        VitimaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CrimeCadastradoId, t.VitimaId });
            
            DropForeignKey("dbo.CrimeCadastrado", "VitimaId", "dbo.Vitima");
            DropForeignKey("dbo.CrimeSSP", "TipoCrimeId", "dbo.TipoCrime");
            DropForeignKey("dbo.CrimeSSP", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.CrimeCadastrado", "TipoCrimeId", "dbo.TipoCrime");
            DropForeignKey("dbo.CrimeCadastrado", "EnderecoId", "dbo.Endereco");
            DropIndex("dbo.CrimeCadastrado", new[] { "VitimaId" });
            DropColumn("dbo.CrimeCadastrado", "VitimaId");
            CreateIndex("dbo.VitimaCrimeCadastrado", "VitimaId");
            CreateIndex("dbo.VitimaCrimeCadastrado", "CrimeCadastradoId");
            AddForeignKey("dbo.VitimaCrimeCadastrado", "VitimaId", "dbo.Vitima", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CrimeSSP", "TipoCrimeId", "dbo.TipoCrime", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CrimeSSP", "EnderecoId", "dbo.Endereco", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CrimeCadastrado", "TipoCrimeId", "dbo.TipoCrime", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CrimeCadastrado", "EnderecoId", "dbo.Endereco", "Id", cascadeDelete: true);
            AddForeignKey("dbo.VitimaCrimeCadastrado", "CrimeCadastradoId", "dbo.CrimeCadastrado", "Id", cascadeDelete: true);
        }
    }
}
