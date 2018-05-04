namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedserialnumbertoEquipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipments", "Description", c => c.String());
            AddColumn("dbo.Equipments", "SerialNumber", c => c.String());
            DropColumn("dbo.Equipments", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipments", "Name", c => c.String());
            DropColumn("dbo.Equipments", "SerialNumber");
            DropColumn("dbo.Equipments", "Description");
        }
    }
}
