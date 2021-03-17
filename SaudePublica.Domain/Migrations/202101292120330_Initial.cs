namespace SaudePublica.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Consultorio",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 100),
                        numeroSala = c.Int(nullable: false),
                        andar = c.Int(nullable: false),
                        setor = c.String(nullable: false, maxLength: 100),
                        latitude = c.Int(nullable: false),
                        longitude = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Profissional",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        nome = c.String(nullable: false, maxLength: 100),
                        funcao = c.String(nullable: false, maxLength: 100),
                        registroProfissional = c.String(nullable: false, maxLength: 100),
                        idEspecialidade = c.Int(nullable: false),
                        cpf = c.String(nullable: false, maxLength: 14),
                        dataNascimento = c.DateTime(nullable: false),
                        sexo = c.Int(nullable: false),
                        cep = c.String(maxLength: 9),
                        logradouro = c.String(maxLength: 500),
                        numeroLogradouro = c.Int(nullable: false),
                        complemento = c.String(maxLength: 500),
                        bairro = c.String(maxLength: 200),
                        telefone = c.String(maxLength: 100),
                        celular = c.String(maxLength: 100),
                        email = c.String(nullable: false, maxLength: 256),
                        statusProfissional = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Especialidade", t => t.idEspecialidade, cascadeDelete: true)
                .Index(t => t.idEspecialidade);
            
            CreateTable(
                "dbo.Especialidade",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        descricao = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.ProfissionalConsultorio",
                c => new
                    {
                        Profissional_id = c.Int(nullable: false),
                        Consultorio_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Profissional_id, t.Consultorio_id })
                .ForeignKey("dbo.Profissional", t => t.Profissional_id, cascadeDelete: true)
                .ForeignKey("dbo.Consultorio", t => t.Consultorio_id, cascadeDelete: true)
                .Index(t => t.Profissional_id)
                .Index(t => t.Consultorio_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profissional", "idEspecialidade", "dbo.Especialidade");
            DropForeignKey("dbo.ProfissionalConsultorio", "Consultorio_id", "dbo.Consultorio");
            DropForeignKey("dbo.ProfissionalConsultorio", "Profissional_id", "dbo.Profissional");
            DropIndex("dbo.ProfissionalConsultorio", new[] { "Consultorio_id" });
            DropIndex("dbo.ProfissionalConsultorio", new[] { "Profissional_id" });
            DropIndex("dbo.Profissional", new[] { "idEspecialidade" });
            DropTable("dbo.ProfissionalConsultorio");
            DropTable("dbo.Especialidade");
            DropTable("dbo.Profissional");
            DropTable("dbo.Consultorio");
        }
    }
}
