namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addusercategorydepartmentfkstoasset : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Assets", "UserId", c => c.Int());
            AddColumn("dbo.Assets", "CategoryId", c => c.Int());
            AddColumn("dbo.Assets", "DepartmentId", c => c.Int());
            CreateIndex("dbo.Assets", "UserId");
            CreateIndex("dbo.Assets", "CategoryId");
            CreateIndex("dbo.Assets", "DepartmentId");
            AddForeignKey("dbo.Assets", "CategoryId", "dbo.Categories", "Id");
            AddForeignKey("dbo.Assets", "DepartmentId", "dbo.Departments", "Id");
            AddForeignKey("dbo.Assets", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assets", "UserId", "dbo.Users");
            DropForeignKey("dbo.Assets", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.Assets", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Assets", new[] { "DepartmentId" });
            DropIndex("dbo.Assets", new[] { "CategoryId" });
            DropIndex("dbo.Assets", new[] { "UserId" });
            DropColumn("dbo.Assets", "DepartmentId");
            DropColumn("dbo.Assets", "CategoryId");
            DropColumn("dbo.Assets", "UserId");
        }
    }
}
