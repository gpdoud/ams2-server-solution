using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ams2.Models {
	public class Property {
		public int Id { get; set; }

		public string Code { get; set; }
		public string Description { get; set; }
		public int AssetId { get; set; }
		public int? AddressId { get; set; }

		public bool Active { get; set; } = true;
		public DateTime DateCreated { get; set; } = DateTime.Now;
		public DateTime? DateUpdated { get; set; } = null;

		public virtual Asset Asset { get; set; }
		public virtual Address Address { get; set; }

		public Property() { }
	}
}