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
	public class DepartmentsController : ApiController {
		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public JsonResponse GetDepartments() {
			return new JsonResponse(db.Departments.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetDepartment(int? id) {
			if (id == null)
				return new JsonResponse { Message = "Parameter id cannot be null" };
			var department = db.Departments.Find(id);
			if (department == null)
				return new JsonResponse { Message = $"Department id={id} not found" };
			return new JsonResponse(department);
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse AddDepartment([FromBody] Department department) {
			if (department == null)
				return new JsonResponse { Message = "Parameter department cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			department.DateCreated = DateTime.Now;
			db.Departments.Add(department);
			var resp = new JsonResponse { Message = "Department Created", Data = department };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse ChangeDepartment([FromBody] Department department) {
			if (department == null)
				return new JsonResponse { Message = "Parameter department cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			department.DateUpdated = DateTime.Now;
			db.Entry(department).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "Department Changed", Data = department };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse RemoveDepartment([FromBody] Department department) {
			if (department == null)
				return new JsonResponse { Message = "Parameter department cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Message = "ModelState invalid", Error = ModelState };
			db.Entry(department).State = System.Data.Entity.EntityState.Deleted;
			var resp = new JsonResponse { Message = "Department Removed", Data = department };
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
