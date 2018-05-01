using Ams2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ams2.Controllers {

	/// <summary>
	/// The Vehicle class is an specific Asset although the Asset table
	/// is separate from the Vehicle table. Therefore, when a Vehicle is
	/// added, changed, or deleted, both the Asset and the Vehicle must 
	/// be added, changed, or deleted together.
	/// 
	/// The Vehicle to Asset is a one-to-one relationship.
	/// </summary>
	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class VehiclesController : ApiController {

		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public IEnumerable<Vehicle> GetVehicles() {
			return db.Vehicles.ToList();
		}

		[HttpGet]
		[ActionName("Get")]
		public Vehicle GetVehicle(int? id) {
			if (id == null) return null;
			var vehicle = db.Vehicles.Find(id);
			return vehicle; // may be null 
		}

		[HttpPost]
		[ActionName("Create")]
		public bool PutVehicle([FromBody] Vehicle vehicle) {
			if (vehicle == null) return false;
			if (!ModelState.IsValid) return false;
			// add the asset first
			var asset = db.Assets.Add(vehicle.Asset);
			SaveChanges(); // so the asset exists for the vehicle
			vehicle.AssetId = asset.Id; // this gets the generated PK
			db.Vehicles.Add(vehicle);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Change")]
		public bool PostVehicle([FromBody] Vehicle vehicle) {
			if (vehicle == null) return false;
			if (!ModelState.IsValid) return false;
			var asset2 = db.Assets.Find(vehicle.Asset.Id);
			if (asset2 == null) return false;
			asset2.Copy(vehicle.Asset);
			SaveChanges();
			var vehicle2 = db.Vehicles.Find(vehicle.Id);
			if (vehicle2 == null) return false;
			vehicle2.Copy(vehicle);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Remove")]
		public bool DeleteVehicle([FromBody] Vehicle vehicle) {
			if (vehicle == null) return false;
			var vehicle2 = db.Vehicles.Find(vehicle.Id);
			if (vehicle2 == null) return false;
			var asset2Id = vehicle2.Asset.Id;
			var asset2 = db.Assets.Find(asset2Id);
			db.Vehicles.Remove(vehicle2);
			SaveChanges();
			db.Assets.Remove(asset2);
			return SaveChanges();
		}

		private bool SaveChanges() {
			try {
				db.SaveChanges();
				return true;
			} catch (Exception) {
			}
			return false;
		}
	}
}
