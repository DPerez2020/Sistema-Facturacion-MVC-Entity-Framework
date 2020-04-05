namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Facturacion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Facturacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Id_Cliente = c.Int(nullable: false),
                        CantidadProducto = c.Int(nullable: false),
                        TotalProducto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalPagado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fecha = c.DateTime(nullable: false),
                        DetalleFactura_Id = c.Int(),
                        Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DetalleFacturas", t => t.DetalleFactura_Id)
                .ForeignKey("dbo.Personas", t => t.Cliente_Id)
                .Index(t => t.DetalleFactura_Id)
                .Index(t => t.Cliente_Id);
            
            CreateTable(
                "dbo.DetalleFacturas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FacturaId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Productoes", "DetalleFactura_Id", c => c.Int());
            CreateIndex("dbo.Productoes", "DetalleFactura_Id");
            AddForeignKey("dbo.Productoes", "DetalleFactura_Id", "dbo.DetalleFacturas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Facturacions", "Cliente_Id", "dbo.Personas");
            DropForeignKey("dbo.Facturacions", "DetalleFactura_Id", "dbo.DetalleFacturas");
            DropForeignKey("dbo.Productoes", "DetalleFactura_Id", "dbo.DetalleFacturas");
            DropIndex("dbo.Productoes", new[] { "DetalleFactura_Id" });
            DropIndex("dbo.Facturacions", new[] { "Cliente_Id" });
            DropIndex("dbo.Facturacions", new[] { "DetalleFactura_Id" });
            DropColumn("dbo.Productoes", "DetalleFactura_Id");
            DropTable("dbo.DetalleFacturas");
            DropTable("dbo.Facturacions");
        }
    }
}
