namespace Ams2PrototypeProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedAddressandFKtoAddressinAsset : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zipcode = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Assets", "AddressId", c => c.Int());
            CreateIndex("dbo.Assets", "AddressId");
            AddForeignKey("dbo.Assets", "AddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Assets", new[] { "AddressId" });
            DropColumn("dbo.Assets", "AddressId");
            DropTable("dbo.Addresses");
        }
    }
}
