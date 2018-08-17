namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EquipmentSerialNumberchangedtononunique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Equipments", "IDX_SerialNumber");
            CreateIndex("dbo.Equipments", "SerialNumber", name: "IDX_SerialNumber");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Equipments", "IDX_SerialNumber");
            CreateIndex("dbo.Equipments", "SerialNumber", unique: true, name: "IDX_SerialNumber");
        }
    }
}
