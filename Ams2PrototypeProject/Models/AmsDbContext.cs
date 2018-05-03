using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Ams2.Models {

	public class AmsDbContext : DbContext {

		public AmsDbContext() : base("name=AmsDb") {
		}

		public DbSet<Address> Addresses { get; set; }
		public DbSet<Asset> Assets { get; set; }
		public DbSet<Equipment> Equipments { get; set; }
		public DbSet<Logger> Loggers { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<SystemConfig> SystemConfig { get; set; }
		public DbSet<User> Users { get; set; }
	}
}