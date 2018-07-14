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
	public class PropertiesController : AmsWebApiController {

		[HttpGet]
		[ActionName("List")]
		public JsonResponse GetProperty() {
			return new JsonResponse(db.Properties.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetProperty(int? id) {
			if (id == null)
				return new JsonResponse { Message = "Parameter id cannot be null" };
			var property = db.Properties.Find(id);
			if (property == null)
				return new JsonResponse { Message = $"Property id={id} not found" };
			return new JsonResponse(property);
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse CreateProperty([FromBody] Property property) {
			if (property == null)
				return new JsonResponse { Message = "Parameter property cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			// add the asset first
			// needs all the asset data entered already
			var asset = db.Assets.Add(property.Asset);
			var recsAffected = db.SaveChanges(); // so the asset exists for the property
			if (recsAffected != 1)
				return new JsonResponse("Create asset failed while attempting to add property");
			property.AssetId = asset.Id; // this gets the generated PK
			property.DateCreated = DateTime.Now;
			db.Properties.Add(property);
			var resp = new JsonResponse { Message = "Property Created", Data = property };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse ChangeProperty([FromBody] Property property) {
			if (property == null)
				return new JsonResponse { Message = "Parameter property cannot be null" };
			// issue #11
			// If the addressId in the asset is set to null (clears the address dropdown)
			// set the Asset instance to null also.
			if (property.Asset.AddressId == null)
				property.Asset.Address = null;
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			property.DateUpdated = DateTime.Now;
			db.Entry(property.Asset).State = System.Data.Entity.EntityState.Modified;
			db.Entry(property).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "Property Changed", Data = property };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse RemoveProperty([FromBody] Property property) {
			if (property == null)
				return new JsonResponse { Message = "Parameter property cannot be null" };
			db.Entry(property.Asset).State = System.Data.Entity.EntityState.Deleted;
			// the related property record will be deleted also because
			// of cascading delete
			var resp = new JsonResponse { Message = "Property Removed", Data = property };
			return SaveChanges(resp);
		}
	}
}
