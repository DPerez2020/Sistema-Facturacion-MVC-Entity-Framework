namespace Sistema_Facturacion.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cliente2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Personas", "Categoria");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Personas", "Categoria", c => c.String());
        }
    }
}
