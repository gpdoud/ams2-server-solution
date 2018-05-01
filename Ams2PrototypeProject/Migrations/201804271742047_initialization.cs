namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        AcquiredDate = c.DateTime(),
                        DisposedDate = c.DateTime(),
                        PONumber = c.String(),
                        Cost = c.Decimal(precision: 18, scale: 2),
                        OutForRepairDate = c.DateTime(),
                        ReturnFromRepairDate = c.DateTime(),
                        RetiredDate = c.DateTime(),
                        SurplusDate = c.DateTime(),
                        ResidualValue = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Loggers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Application = c.String(),
                        Classname = c.String(),
                        Method = c.String(),
                        LogLevel = c.String(),
                        Message = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SystemConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Category = c.String(),
                        SysKey = c.String(),
                        SysValue = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        AssetId = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Year = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assets", t => t.AssetId, cascadeDelete: true)
                .Index(t => t.AssetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vehicles", "AssetId", "dbo.Assets");
            DropIndex("dbo.Vehicles", new[] { "AssetId" });
            DropTable("dbo.Vehicles");
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.Loggers");
            DropTable("dbo.Assets");
        }
    }
}
