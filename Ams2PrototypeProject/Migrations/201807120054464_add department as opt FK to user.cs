namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddepartmentasoptFKtouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DepartmentId", c => c.Int());
            CreateIndex("dbo.Users", "DepartmentId");
            AddForeignKey("dbo.Users", "DepartmentId", "dbo.Departments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "DepartmentId", "dbo.Departments");
            DropIndex("dbo.Users", new[] { "DepartmentId" });
            DropColumn("dbo.Users", "DepartmentId");
        }
    }
}
