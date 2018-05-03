using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ams2.Models {
	/// <summary>
	/// Equipment are things like shovels, rakes, etc.
	/// </summary>
	public class Equipment {
		public int Id { get; set; }
		public int AssetId { get; set; }
		public Asset Asset { get; set; }
		public string Name { get; set; }
		public bool Active { get; set; }
		public DateTime DateCreated { get; set; } = DateTime.Now;
		public DateTime? DateUpdated { get; set; }

		public Equipment() { }

		public Equipment(int AssetId, Asset Asset, string Name) {
			this.AssetId = AssetId;
			this.Asset = Asset;
			this.Name = Name;
			this.Active = true;
			this.DateCreated = DateTime.Now;
		}

		public void Copy(Equipment e) {
			this.AssetId = e.AssetId;
			this.Asset = e.Asset;
			this.Name = e.Name;
			this.Active = e.Active;
			this.DateCreated = e.DateCreated;
			this.DateUpdated = e.DateUpdated;
		}
	}
}