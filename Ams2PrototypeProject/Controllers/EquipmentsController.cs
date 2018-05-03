using Ams2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ams2.Controllers {

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class EquipmentsController : ApiController {

		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public IEnumerable<Equipment> GetVehicles() {
			return db.Equipments.ToList();
		}

		[HttpGet]
		[ActionName("Get")]
		public Equipment GetVehicle(int? id) {
			if (id == null) return null;
			var equipment = db.Equipments.Find(id);
			return equipment; // may be null 
		}

		[HttpPost]
		[ActionName("Create")]
		public bool PutVehicle([FromBody] Equipment equipment) {
			if (equipment == null) return false;
			if (!ModelState.IsValid) return false;
			// add the asset first
			var asset = db.Assets.Add(equipment.Asset);
			SaveChanges(); // so the asset exists for the equipment
			equipment.AssetId = asset.Id; // this gets the generated PK
			db.Equipments.Add(equipment);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Change")]
		public bool PostVehicle([FromBody] Equipment equipment) {
			if (equipment == null) return false;
			if (!ModelState.IsValid) return false;
			var asset2 = db.Assets.Find(equipment.Asset.Id);
			if (asset2 == null) return false;
			asset2.Copy(equipment.Asset);
			SaveChanges();
			var vehicle2 = db.Equipments.Find(equipment.Id);
			if (vehicle2 == null) return false;
			vehicle2.Copy(equipment);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Remove")]
		public bool DeleteVehicle([FromBody] Equipment equipment) {
			if (equipment == null) return false;
			var vehicle2 = db.Equipments.Find(equipment.Id);
			if (vehicle2 == null) return false;
			var asset2Id = vehicle2.Asset.Id;
			var asset2 = db.Assets.Find(asset2Id);
			db.Equipments.Remove(vehicle2);
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
