namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addindextoequipmentserialnumber : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equipments", "SerialNumber", c => c.String(maxLength: 50));
            CreateIndex("dbo.Equipments", "SerialNumber", unique: true, name: "IDX_SerialNumber");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Equipments", "IDX_SerialNumber");
            AlterColumn("dbo.Equipments", "SerialNumber", c => c.String());
        }
    }
}
