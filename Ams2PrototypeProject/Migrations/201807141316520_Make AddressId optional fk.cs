namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeAddressIdoptionalfk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Properties", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Properties", new[] { "AddressId" });
            AlterColumn("dbo.Properties", "AddressId", c => c.Int());
            CreateIndex("dbo.Properties", "AddressId");
            AddForeignKey("dbo.Properties", "AddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Properties", new[] { "AddressId" });
            AlterColumn("dbo.Properties", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Properties", "AddressId");
            AddForeignKey("dbo.Properties", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
