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

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class EquipmentsController : AmsWebApiController {

		//private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public JsonResponse GetEquipment() {
			return new JsonResponse(db.Equipments.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetEquipment(int? id) {
			if (id == null)
				return new JsonResponse { Message = "Parameter id cannot be null" };
			var equipment = db.Equipments.Find(id);
			if(equipment == null)
				return new JsonResponse { Message = $"Equipment id={id} not found" };
			return new JsonResponse(equipment); 
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse CreateEquipment([FromBody] Equipment equipment) {
			if (equipment == null)
				return new JsonResponse { Message = "Parameter equipment cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			// add the asset first
			// needs all the asset data entered already
			var asset = db.Assets.Add(equipment.Asset);
			var recsAffected = db.SaveChanges(); // so the asset exists for the equipment
			if (recsAffected != 1)
				return new JsonResponse("Create asset failed while attempting to add equipment");
			equipment.AssetId = asset.Id; // this gets the generated PK
			equipment.DateCreated = DateTime.Now;
			db.Equipments.Add(equipment);
			var resp = new JsonResponse { Message = "Equipment Created", Data = equipment };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse ChangeEquipment([FromBody] Equipment equipment) {
			if (equipment == null)
				return new JsonResponse { Message = "Parameter equipment cannot be null" };
			// issue #11
			// If the addressId in the asset is set to null (clears the address dropdown)
			// set the Asset instance to null also.
			//if (equipment.Asset.AddressId == null)
			//	equipment.Asset.Address = null;
			ClearAssetVirtuals(equipment);
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			equipment.DateUpdated = DateTime.Now;
			db.Entry(equipment.Asset).State = System.Data.Entity.EntityState.Modified;
			db.Entry(equipment).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "Equipment Changed", Data = equipment };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse RemoveEquipment([FromBody] Equipment equipment) {
			if (equipment == null)
				return new JsonResponse { Message = "Parameter equipment cannot be null" };
			db.Entry(equipment.Asset).State = System.Data.Entity.EntityState.Deleted;
			// the related equipment record will be deleted also because
			// of cascading delete
			var resp = new JsonResponse { Message = "Equipment Removed", Data = equipment };
			return SaveChanges(resp);
		}

		//private JsonResponse SaveChanges(JsonResponse resp) {
		//	try {
		//		db.SaveChanges();
		//		return resp ?? JsonResponse.Ok;
		//	} catch (Exception ex) {
		//		return new JsonResponse { Message = ex.Message, Error = ex };
		//	}
		//}

	}
}
