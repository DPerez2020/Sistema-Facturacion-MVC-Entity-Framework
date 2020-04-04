namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Existencia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entradas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                        ProveedorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productoes", t => t.ProductoId, cascadeDelete: false)
                .ForeignKey("dbo.Personas", t => t.ProveedorId, cascadeDelete: false)
                .Index(t => t.ProductoId)
                .Index(t => t.ProveedorId);
            
            CreateTable(
                "dbo.Existencias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductoId = c.Int(nullable: false),
                        Cantidad = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Productoes", "Existencia_Id", c => c.Int());
            CreateIndex("dbo.Productoes", "Existencia_Id");
            AddForeignKey("dbo.Productoes", "Existencia_Id", "dbo.Existencias", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Entradas", "ProveedorId", "dbo.Personas");
            DropForeignKey("dbo.Entradas", "ProductoId", "dbo.Productoes");
            DropForeignKey("dbo.Productoes", "Existencia_Id", "dbo.Existencias");
            DropIndex("dbo.Productoes", new[] { "Existencia_Id" });
            DropIndex("dbo.Entradas", new[] { "ProveedorId" });
            DropIndex("dbo.Entradas", new[] { "ProductoId" });
            DropColumn("dbo.Productoes", "Existencia_Id");
            DropTable("dbo.Existencias");
            DropTable("dbo.Entradas");
        }
    }
}
