using Ams2.Models;
using Ams2.Utility;
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
		public JsonResponse GetVehicles() {
			return new JsonResponse(db.Vehicles.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetVehicle(int? id) {
			if (id == null)
				return new JsonResponse { Message = "Parameter id cannot be null" };
			var vehicle = db.Vehicles.Find(id);
			if (vehicle == null)
				return new JsonResponse { Message = $"Vehicle id={id} not found" };
			return new JsonResponse(vehicle); // may be null 
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse PutVehicle([FromBody] Vehicle vehicle) {
			if (vehicle == null)
				return new JsonResponse { Message = "Parameter vehicle cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			// add the asset first
			var asset = db.Assets.Add(vehicle.Asset);
			db.SaveChanges(); // so the asset exists for the vehicle
			vehicle.AssetId = asset.Id; // this gets the generated PK
			db.Vehicles.Add(vehicle);
			var resp = new JsonResponse { Message = "Vehicle Created", Data = vehicle };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse PostVehicle([FromBody] Vehicle vehicle) {
			if (vehicle == null)
				return new JsonResponse { Message = "Parameter vehicle cannot be null" };
			if (vehicle.Asset.AddressId == null)
				vehicle.Asset.Address = null;
			if (vehicle.Asset.DepartmentId == null)
				vehicle.Asset.Department = null;
			if (vehicle.Asset.CategoryId == null)
				vehicle.Asset.Category = null;
			if (vehicle.Asset.UserId == null)
				vehicle.Asset.User = null;
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			db.Entry(vehicle.Asset).State = System.Data.Entity.EntityState.Modified;
			db.Entry(vehicle).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "Vehicle Changed", Data = vehicle };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse DeleteVehicle([FromBody] Vehicle vehicle) {
			if (vehicle == null)
				return new JsonResponse { Message = "Parameter vehicle cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			//db.Entry(vehicle).State = System.Data.Entity.EntityState.Deleted;
			db.Entry(vehicle.Asset).State = System.Data.Entity.EntityState.Deleted;
			var resp = new JsonResponse { Message = "Vehicle Removed", Data = vehicle };
			return SaveChanges(resp);
		}

		private JsonResponse SaveChanges(JsonResponse resp) {
			try {
				db.SaveChanges();
				return resp ?? JsonResponse.Ok;
			} catch (Exception ex) {
				return new JsonResponse { Message = ex.Message, Error = ex };
			}
		}
	}
}
