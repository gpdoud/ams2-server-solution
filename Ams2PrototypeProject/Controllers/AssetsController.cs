using Ams2.Models;
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
	public class AssetsController : ApiController {

		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public IEnumerable<Asset> GetAssets() {
			return db.Assets.ToList();
		}

		[HttpGet]
		[ActionName("Get")]
		public Asset GetAssets(int? id) {
			if (id == null) return null;
			var asset = db.Assets.Find(id);
			if (asset == null) return null;
			return asset;
		}

		[HttpPost]
		[ActionName("Create")]
		public bool PutAsset([FromBody] Asset asset) {
			if (asset == null) return false;
			if (!ModelState.IsValid) return false;
			db.Assets.Add(asset);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Change")]
		public bool PostAsset([FromBody] Asset asset) {
			if (asset == null) return false;
			if (!ModelState.IsValid) return false;
			var asset2 = db.Assets.Find(asset.Id);
			if (asset2 == null) return false;
			asset2.Copy(asset);
			return SaveChanges();
		}

		[HttpPost]
		[ActionName("Remove")]
		public bool DeleteAsset([FromBody] Asset asset) {
			if (asset == null) return false;
			var asset2 = db.Assets.Find(asset.Id);
			if (asset2 == null) return false;
			db.Assets.Remove(asset2);
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
