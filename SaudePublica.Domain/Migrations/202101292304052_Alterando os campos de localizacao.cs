namespace SaudePublica.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alterandooscamposdelocalizacao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Consultorio", "latitude", c => c.String(nullable: false));
            AlterColumn("dbo.Consultorio", "longitude", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Consultorio", "longitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Consultorio", "latitude", c => c.Double(nullable: false));
        }
    }
}
