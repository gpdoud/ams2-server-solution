namespace Ams2PrototypeProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedActiveDateCreatedDateUpdatedtoVehiclemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vehicles", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Vehicles", "DateUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "DateUpdated");
            DropColumn("dbo.Vehicles", "DateCreated");
            DropColumn("dbo.Vehicles", "Active");
        }
    }
}
