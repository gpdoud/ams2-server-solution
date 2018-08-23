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
	public class CategoriesController : AmsWebApiController {

		// private AmsDbContext db = new AmsDbContext(); included in AmsWebApiController

		[HttpGet]
		[ActionName("List")]
		public JsonResponse GetCategories() {
			return new JsonResponse(db.Categories.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetCategory(int? id) {
			if (id == null)
				return new JsonResponse { Code = -2, Message = "Parameter id cannot be null" };
			var Category = db.Categories.Find(id);
			if (Category == null)
				return new JsonResponse { Code = -2, Message = $"Category id={id} not found" };
			return new JsonResponse(Category);
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse AddCategory([FromBody] Category Category) {
			if (Category == null)
				return new JsonResponse { Code = -2, Message = "Parameter Category cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			db.Categories.Add(Category);
			var resp = new JsonResponse { Message = "Category Created", Data = Category };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse ChangeCategory([FromBody] Category Category) {
			if (Category == null)
				return new JsonResponse { Code = -2, Message = "Parameter Category cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			db.Entry(Category).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "Category Changed", Data = Category };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse RemoveCategory([FromBody] Category Category) {
			if (Category == null)
				return new JsonResponse { Code = -2, Message = "Parameter Category cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			db.Entry(Category).State = System.Data.Entity.EntityState.Deleted;
			var resp = new JsonResponse { Message = "Category Removed", Data = Category };
			return SaveChanges(resp);
		}

		//private JsonResponse SaveChanges(JsonResponse resp = null) {
		//	try {
		//		db.SaveChanges();
		//		return resp ?? JsonResponse.Ok;
		//	} catch (Exception ex) {
		//		return new JsonResponse { Message = ex.Message, Error = ex };
		//	}
		//}
	}
}
