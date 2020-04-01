namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Categoria : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Personas", "CategoriaId_Id", "dbo.Categorias");
            RenameColumn(table: "dbo.Personas", name: "CategoriaId_Id", newName: "CategoriaId");
            RenameIndex(table: "dbo.Personas", name: "IX_CategoriaId_Id", newName: "IX_CategoriaId");
            AddForeignKey("dbo.Personas", "CategoriaId", "dbo.Categorias", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Personas", "CategoriaId", "dbo.Categorias");
            RenameIndex(table: "dbo.Personas", name: "IX_CategoriaId", newName: "IX_CategoriaId_Id");
            RenameColumn(table: "dbo.Personas", name: "CategoriaId", newName: "CategoriaId_Id");
            AddForeignKey("dbo.Personas", "CategoriaId_Id", "dbo.Categorias", "Id");
        }
    }
}
