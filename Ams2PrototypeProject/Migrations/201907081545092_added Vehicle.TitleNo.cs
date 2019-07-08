namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedVehicleTitleNo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "TitleNo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "TitleNo");
        }
    }
}
