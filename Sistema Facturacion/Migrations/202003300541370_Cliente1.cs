namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cliente1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Personas", name: "Categoria_Id", newName: "CategoriaId_Id");
            RenameIndex(table: "dbo.Personas", name: "IX_Categoria_Id", newName: "IX_CategoriaId_Id");
            AddColumn("dbo.Personas", "Categoria", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Personas", "Categoria");
            RenameIndex(table: "dbo.Personas", name: "IX_CategoriaId_Id", newName: "IX_Categoria_Id");
            RenameColumn(table: "dbo.Personas", name: "CategoriaId_Id", newName: "Categoria_Id");
        }
    }
}
