namespace EstagiosDEIS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class finalv1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Candidaturas", "PropostasSelect", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Candidaturas", "PropostasSelect");
        }
    }
}
