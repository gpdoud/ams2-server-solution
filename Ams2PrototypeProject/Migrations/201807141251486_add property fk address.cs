namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpropertyfkaddress : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Description = c.String(),
                        AssetId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Assets", t => t.AssetId, cascadeDelete: true)
                .Index(t => t.AssetId)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "AssetId", "dbo.Assets");
            DropForeignKey("dbo.Properties", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Properties", new[] { "AddressId" });
            DropIndex("dbo.Properties", new[] { "AssetId" });
            DropTable("dbo.Properties");
        }
    }
}
