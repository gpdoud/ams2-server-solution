namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addedlicenseplatetovehicle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "LicensePlate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "LicensePlate");
        }
    }
}
