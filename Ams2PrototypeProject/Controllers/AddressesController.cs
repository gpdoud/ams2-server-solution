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
	public class AddressesController : ApiController {

		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public JsonResponse GetAddresss() {
			return new JsonResponse(db.Addresses.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetAddress(int? id) {
			if (id == null)
				return new JsonResponse { Message = "Parameter id cannot be null" };
			var address = db.Addresses.Find(id);
			if (address == null)
				return new JsonResponse { Message = $"Address id={id} not found" };
			return new JsonResponse(address);
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse AddAddress([FromBody] Address address) {
			if (address == null)
				return new JsonResponse { Message = "Parameter address cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			address.DateCreated = DateTime.Now;
			db.Addresses.Add(address);
			var resp = new JsonResponse { Message = "Address Created", Data = address };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse ChangeAddress([FromBody] Address address) {
			if (address == null)
				return new JsonResponse { Message = "Parameter address cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			address.DateUpdated = DateTime.Now;
			db.Entry(address).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "Address Changed", Data = address };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse RemoveAddress([FromBody] Address address) {
			if (address == null)
				return new JsonResponse { Message = "Parameter address cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			db.Entry(address).State = System.Data.Entity.EntityState.Deleted;
			var resp = new JsonResponse { Message = "Address Removed", Data = address };
			return SaveChanges(resp);
		}

		private JsonResponse SaveChanges(JsonResponse resp = null) {
			try {
				db.SaveChanges();
				return resp ?? JsonResponse.Ok;
			} catch (Exception ex) {
				return new JsonResponse { Message = ex.Message, Error = ex };
			}
		}
	}
}
