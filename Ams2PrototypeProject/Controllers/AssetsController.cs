﻿using Ams2.Models;
using Ams2.Utility;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ams2.Controllers {

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class AssetsController : AmsWebApiController {

		//private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public JsonResponse GetAssets() {
			return new JsonResponse(db.Assets.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetAssets(int? id) {
			if (id == null)
				return new JsonResponse { Code = -2, Message = "Parameter id cannot be null" };
			var asset = db.Assets.Find(id);
			if (asset == null)
				return new JsonResponse { Code = -2, Message = $"Asset id={id} not found" };
			return new JsonResponse(asset);
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse PutAsset([FromBody] Asset asset) {
			if (asset == null)
				return new JsonResponse { Code = -2, Message = "Parameter asset cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			db.Assets.Add(asset);
			var resp = new JsonResponse { Message = "Asset Created", Data = asset };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse PostAsset([FromBody] Asset asset) {
			if (asset == null)
				return new JsonResponse { Code = -2, Message = "Parameter asset cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			db.Entry(asset).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "Asset Changed", Data = asset };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse DeleteAsset([FromBody] Asset asset) {
			if (asset == null)
				return new JsonResponse { Code = -2, Message = "Parameter asset cannot be null" };
			db.Entry(asset).State = System.Data.Entity.EntityState.Deleted;
			var resp = new JsonResponse { Message = "Asset Removed", Data = asset };
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
