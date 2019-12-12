namespace EstagiosDEIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalv4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Mensagens",
                c => new
                    {
                        NumMensagem = c.Int(nullable: false, identity: true),
                        Remetente = c.String(),
                        Destinatario = c.String(),
                        Mensagem = c.String(),
                        DataMensagem = c.DateTime(),
                    })
                .PrimaryKey(t => t.NumMensagem);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Mensagens");
        }
    }
}
