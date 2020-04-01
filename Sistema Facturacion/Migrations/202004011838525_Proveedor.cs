namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Proveedor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Productoes", "ProveedorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Productoes", "ProveedorId");
            AddForeignKey("dbo.Productoes", "ProveedorId", "dbo.Personas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Productoes", "ProveedorId", "dbo.Personas");
            DropIndex("dbo.Productoes", new[] { "ProveedorId" });
            DropColumn("dbo.Productoes", "ProveedorId");
        }
    }
}
