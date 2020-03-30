namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cliente : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Personas", "CategoriaId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personas", "CategoriaId", c => c.String());
        }
    }
}
