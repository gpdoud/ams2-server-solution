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
	public class AddressesController : ApiController {

		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public IEnumerable<Address> GetAddresses() {
			return db.Addresses.ToList();
		}

		[HttpGet]
		[ActionName("Get")]
		public Address GetAddress(int? id) {
			if (id == null) return null;
			var address = db.Addresses.Find(id);
			return address;
		}

		[HttpPost]
		[ActionName("Create")]
		public bool AddAddress([FromBody] Address address) {
			if (address == null) return false;
			if (!ModelState.IsValid) return false;
			db.Addresses.Add(address);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Change")]
		public bool ChgAddress([FromBody] Address address) {
			if (address == null) return false;
			if (!ModelState.IsValid) return false;
			var address2 = db.Addresses.Find(address.Id);
			if (address2 == null) return false;
			address2.Copy(address);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Remove")]
		public bool RemAddress([FromBody] Address address) {
			if (address == null) return false;
			if (!ModelState.IsValid) return false;
			var address2 = db.Addresses.Find(address.Id);
			if (address2 == null) return false;
			db.Addresses.Remove(address2);
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
