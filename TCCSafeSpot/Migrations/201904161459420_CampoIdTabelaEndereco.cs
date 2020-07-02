namespace TCCSafeSpot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoIdTabelaEndereco : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CrimeSSP", "Endereco_Cep", "dbo.Endereco");
            DropForeignKey("dbo.CrimeCadastrado", "Endereco_Cep", "dbo.Endereco");
            DropIndex("dbo.CrimeCadastrado", new[] { "Endereco_Cep" });
            DropIndex("dbo.CrimeSSP", new[] { "Endereco_Cep" });
            DropColumn("dbo.CrimeCadastrado", "EnderecoId");
            DropColumn("dbo.CrimeSSP", "EnderecoId");
            RenameColumn(table: "dbo.CrimeCadastrado", name: "Endereco_Cep", newName: "EnderecoId");
            RenameColumn(table: "dbo.CrimeSSP", name: "Endereco_Cep", newName: "EnderecoId");
            DropPrimaryKey("dbo.Endereco");
            AddColumn("dbo.Endereco", "Id", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.Endereco", "Estado", c => c.String(maxLength: 3));
            AlterColumn("dbo.CrimeCadastrado", "EnderecoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Endereco", "Cep", c => c.String());
            AlterColumn("dbo.CrimeSSP", "EnderecoId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Endereco", "Id");
            CreateIndex("dbo.CrimeCadastrado", "EnderecoId");
            CreateIndex("dbo.CrimeSSP", "EnderecoId");
            AddForeignKey("dbo.CrimeSSP", "EnderecoId", "dbo.Endereco", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CrimeCadastrado", "EnderecoId", "dbo.Endereco", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CrimeCadastrado", "EnderecoId", "dbo.Endereco");
            DropForeignKey("dbo.CrimeSSP", "EnderecoId", "dbo.Endereco");
            DropIndex("dbo.CrimeSSP", new[] { "EnderecoId" });
            DropIndex("dbo.CrimeCadastrado", new[] { "EnderecoId" });
            DropPrimaryKey("dbo.Endereco");
            AlterColumn("dbo.CrimeSSP", "EnderecoId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Endereco", "Cep", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.CrimeCadastrado", "EnderecoId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Endereco", "Estado");
            DropColumn("dbo.Endereco", "Id");
            AddPrimaryKey("dbo.Endereco", "Cep");
            RenameColumn(table: "dbo.CrimeSSP", name: "EnderecoId", newName: "Endereco_Cep");
            RenameColumn(table: "dbo.CrimeCadastrado", name: "EnderecoId", newName: "Endereco_Cep");
            AddColumn("dbo.CrimeSSP", "EnderecoId", c => c.Int(nullable: false));
            AddColumn("dbo.CrimeCadastrado", "EnderecoId", c => c.Int(nullable: false));
            CreateIndex("dbo.CrimeSSP", "Endereco_Cep");
            CreateIndex("dbo.CrimeCadastrado", "Endereco_Cep");
            AddForeignKey("dbo.CrimeCadastrado", "Endereco_Cep", "dbo.Endereco", "Cep", cascadeDelete: true);
            AddForeignKey("dbo.CrimeSSP", "Endereco_Cep", "dbo.Endereco", "Cep", cascadeDelete: true);
        }
    }
}
