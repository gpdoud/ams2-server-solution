namespace Ams2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Ams2.Models.AmsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Ams2.Models.AmsDbContext context)
        {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data.
			context.Addresses.AddOrUpdate(a =>
				a.Name,
				new Models.Address("Corp", "Corporate Office", "150 Parkway Dr.", "Suite 150", null, "Mason", "OH", "45040")
			);
        }
    }
}
