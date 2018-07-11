namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcodeyearmakemfgmodeltoequipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "Code", c => c.String());
            AddColumn("dbo.Equipments", "Make", c => c.String());
            AddColumn("dbo.Equipments", "Model", c => c.String());
            AddColumn("dbo.Equipments", "Year", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipments", "Year");
            DropColumn("dbo.Equipments", "Model");
            DropColumn("dbo.Equipments", "Make");
            DropColumn("dbo.Equipments", "Code");
        }
    }
}
