using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ams2.Models {

	public class AmsDbContext : DbContext {

		public AmsDbContext() : base("name=AmsDb") {
			Database.SetInitializer(new MigrateDatabaseToLatestVersion<AmsDbContext, Migrations.Configuration>());
		}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Asset> Assets { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Equipment> Equipments { get; set; }
		public DbSet<Logger> Loggers { get; set; }
		public DbSet<Property> Properties { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<SystemConfig> SystemConfig { get; set; }
		public DbSet<User> Users { get; set; }
	}
}