namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedpropertytoincludeaddresscitystatezip : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Properties", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Properties", new[] { "AddressId" });
            AddColumn("dbo.Properties", "Address1", c => c.String());
            AddColumn("dbo.Properties", "Address2", c => c.String());
            AddColumn("dbo.Properties", "Address3", c => c.String());
            AddColumn("dbo.Properties", "City", c => c.String());
            AddColumn("dbo.Properties", "State", c => c.String());
            AddColumn("dbo.Properties", "Zipcode", c => c.String());
            DropColumn("dbo.Properties", "AddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Properties", "AddressId", c => c.Int());
            DropColumn("dbo.Properties", "Zipcode");
            DropColumn("dbo.Properties", "State");
            DropColumn("dbo.Properties", "City");
            DropColumn("dbo.Properties", "Address3");
            DropColumn("dbo.Properties", "Address2");
            DropColumn("dbo.Properties", "Address1");
            CreateIndex("dbo.Properties", "AddressId");
            AddForeignKey("dbo.Properties", "AddressId", "dbo.Addresses", "Id");
        }
    }
}
