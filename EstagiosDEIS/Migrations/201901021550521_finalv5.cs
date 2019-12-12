namespace EstagiosDEIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalv5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CandidaturaEPropostas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        numCandidatura = c.Int(nullable: false),
                        numProposta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.Candidaturas", "NumAluno", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidaturas", "NumAluno");
            DropTable("dbo.CandidaturaEPropostas");
        }
    }
}
