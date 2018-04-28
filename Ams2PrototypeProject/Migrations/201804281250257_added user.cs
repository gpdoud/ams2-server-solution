namespace Ams2PrototypeProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addeduser : DbMigration
    {
        public override void Up()
        {
			CreateTable(
				"dbo.Users",
				c => new {
					Id = c.Int(nullable: false, identity: true),
					Username = c.String(),
					Password = c.String(),
					Firstname = c.String(),
					Lastname = c.String(),
					Phone = c.String(),
					Email = c.String(),
					Active = c.Boolean(nullable: false),
					DateCreated = c.DateTime(nullable: false),
					DateUpdated = c.DateTime(),
				})
				.PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Username" });
            DropTable("dbo.Users");
        }
    }
}
