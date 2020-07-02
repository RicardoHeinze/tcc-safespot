namespace TCCSafeSpot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BDInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CrimeCadastrado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Periodo = c.String(maxLength: 15),
                        TipoCrimeId = c.Int(nullable: false),
                        EnderecoId = c.Int(nullable: false),
                        Endereco_Cep = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.Endereco_Cep, cascadeDelete: true)
                .ForeignKey("dbo.TipoCrime", t => t.TipoCrimeId, cascadeDelete: true)
                .Index(t => t.TipoCrimeId)
                .Index(t => t.Endereco_Cep);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Cep = c.String(nullable: false, maxLength: 128),
                        CidadeBO = c.String(maxLength: 50),
                        Bairro = c.String(),
                        Logradouro = c.String(),
                        Numero = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Cep);
            
            CreateTable(
                "dbo.CrimeSSP",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        Data = c.DateTime(nullable: false),
                        Periodo = c.String(),
                        TipoCrimeId = c.Int(nullable: false),
                        EnderecoId = c.Int(nullable: false),
                        Endereco_Cep = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.Endereco_Cep, cascadeDelete: true)
                .ForeignKey("dbo.TipoCrime", t => t.TipoCrimeId, cascadeDelete: true)
                .Index(t => t.TipoCrimeId)
                .Index(t => t.Endereco_Cep);
            
            CreateTable(
                "dbo.TipoCrime",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VitimaCrimeCadastrado",
                c => new
                    {
                        CrimeCadastradoId = c.Int(nullable: false),
                        VitimaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CrimeCadastradoId, t.VitimaId })
                .ForeignKey("dbo.CrimeCadastrado", t => t.CrimeCadastradoId, cascadeDelete: true)
                .ForeignKey("dbo.Vitima", t => t.VitimaId, cascadeDelete: true)
                .Index(t => t.CrimeCadastradoId)
                .Index(t => t.VitimaId);
            
            CreateTable(
                "dbo.Vitima",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        DataNascimento = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VitimaCrimeCadastrado", "VitimaId", "dbo.Vitima");
            DropForeignKey("dbo.VitimaCrimeCadastrado", "CrimeCadastradoId", "dbo.CrimeCadastrado");
            DropForeignKey("dbo.CrimeCadastrado", "TipoCrimeId", "dbo.TipoCrime");
            DropForeignKey("dbo.CrimeCadastrado", "Endereco_Cep", "dbo.Endereco");
            DropForeignKey("dbo.CrimeSSP", "TipoCrimeId", "dbo.TipoCrime");
            DropForeignKey("dbo.CrimeSSP", "Endereco_Cep", "dbo.Endereco");
            DropIndex("dbo.VitimaCrimeCadastrado", new[] { "VitimaId" });
            DropIndex("dbo.VitimaCrimeCadastrado", new[] { "CrimeCadastradoId" });
            DropIndex("dbo.CrimeSSP", new[] { "Endereco_Cep" });
            DropIndex("dbo.CrimeSSP", new[] { "TipoCrimeId" });
            DropIndex("dbo.CrimeCadastrado", new[] { "Endereco_Cep" });
            DropIndex("dbo.CrimeCadastrado", new[] { "TipoCrimeId" });
            DropTable("dbo.Vitima");
            DropTable("dbo.VitimaCrimeCadastrado");
            DropTable("dbo.TipoCrime");
            DropTable("dbo.CrimeSSP");
            DropTable("dbo.Endereco");
            DropTable("dbo.CrimeCadastrado");
        }
    }
}
