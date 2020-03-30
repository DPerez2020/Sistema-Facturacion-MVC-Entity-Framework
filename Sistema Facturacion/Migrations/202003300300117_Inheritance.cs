namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inheritance : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Clientes", newName: "Personas");
            AddColumn("dbo.Personas", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.Proveedors");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Proveedors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RNC_Cedula = c.String(),
                        Nombre = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Personas", "Discriminator");
            RenameTable(name: "dbo.Personas", newName: "Clientes");
        }
    }
}
