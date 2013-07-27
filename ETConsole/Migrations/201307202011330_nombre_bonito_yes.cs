namespace ETConsole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nombre_bonito_yes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Apellido = c.String(nullable: false),
                        Telefono = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tests");
        }
    }
}
