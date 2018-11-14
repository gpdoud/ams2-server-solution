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
	public class UsersController : AmsWebApiController {

		//private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("Login")]
		public JsonResponse LoginUser(string username, string password) {
			if (username == null || password == null) return null;
			var user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
            if(user == null) {
                return new JsonResponse {
                    Code = -3,
                    Message = "Username/Password combination not found"
                };
            }
			return new JsonResponse(user);
		}

		[HttpGet]
		[ActionName("List")]
		public JsonResponse GetUsers() {
			return new JsonResponse(db.Users.ToList());
		}

		[HttpGet]
		[ActionName("Get")]
		public JsonResponse GetUser(int? id) {
			if (id == null)
				return new JsonResponse { Code = -2, Message = "Parameter id cannot be null" };
			var user = db.Users.Find(id);
			if (user == null)
				return new JsonResponse { Code = -2, Message = $"User id={id} not found" };
			return new JsonResponse(user);
		}

		[HttpPost]
		[ActionName("Create")]
		public JsonResponse AddUser([FromBody] User user) {
			if (user == null)
				return new JsonResponse { Code = -2, Message = "Parameter user cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			user.DateCreated = DateTime.Now;
			db.Users.Add(user);
			var resp = new JsonResponse { Message = "User Created", Data = user };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Change")]
		public JsonResponse ChangeUser([FromBody] User user) {
			if (user == null)
				return new JsonResponse { Code = -2, Message = "Parameter user cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			user.DateUpdated = DateTime.Now;
			db.Entry(user).State = System.Data.Entity.EntityState.Modified;
			var resp = new JsonResponse { Message = "User Changed", Data = user };
			return SaveChanges(resp);
		}

		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse RemoveUser([FromBody] User user) {
			if (user == null)
				return new JsonResponse { Code = -2, Message = "Parameter user cannot be null" };
			if (!ModelState.IsValid)
				return new JsonResponse { Code = -1, Message = "ModelState invalid", Error = ModelState };
			db.Entry(user).State = System.Data.Entity.EntityState.Deleted;
			var resp = new JsonResponse { Message = "User Removed", Data = user };
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
