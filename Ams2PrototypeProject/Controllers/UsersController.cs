using Ams2PrototypeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ams2PrototypeProject.Controllers {

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class UsersController : ApiController {

		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("Login")]
		public User LoginUser(string username, string password) {
			if (username == null || password == null) return null;
			var user = db.Users.SingleOrDefault(u => u.Username == username && u.Password == password);
			return user;
		}

		[HttpGet]
		[ActionName("List")]
		public IEnumerable<User> GetUsers() {
			return db.Users.ToList();
		}

		[HttpGet]
		[ActionName("Get")]
		public User GetUser(int? id) {
			if (id == null) return null;
			var user = db.Users.Find(id);
			return user;
		}

		[HttpPost]
		[ActionName("Create")]
		public bool AddUser([FromBody] User user) {
			if (!ModelState.IsValid) return false;
			user.DateCreated = DateTime.Now;
			db.Users.Add(user);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Change")]
		public bool ChangeUser([FromBody] User user) {
			if (!ModelState.IsValid) return false;
			var dbuser = db.Users.Find(user.Id);
			if (dbuser == null) return false;
			dbuser.Copy(user);
			dbuser.DateUpdated = DateTime.Now;
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Remove")]
		public bool RemoveUser([FromBody] User user) {
			if (!ModelState.IsValid) return false;
			var dbuser = db.Users.Find(user.Id);
			if (dbuser == null) return false;
			db.Users.Remove(dbuser);
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
