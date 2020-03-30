namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categorias : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Clientes", "CategoriaId", c => c.String());
            AddColumn("dbo.Clientes", "Categoria_Id", c => c.Int());
            CreateIndex("dbo.Clientes", "Categoria_Id");
            AddForeignKey("dbo.Clientes", "Categoria_Id", "dbo.Categorias", "Id");
            DropColumn("dbo.Clientes", "Categoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clientes", "Categoria", c => c.String());
            DropForeignKey("dbo.Clientes", "Categoria_Id", "dbo.Categorias");
            DropIndex("dbo.Clientes", new[] { "Categoria_Id" });
            DropColumn("dbo.Clientes", "Categoria_Id");
            DropColumn("dbo.Clientes", "CategoriaId");
            DropTable("dbo.Categorias");
        }
    }
}
