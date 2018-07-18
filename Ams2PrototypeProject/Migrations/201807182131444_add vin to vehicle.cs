namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvintovehicle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vehicles", "VIN", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vehicles", "VIN");
        }
    }
}
