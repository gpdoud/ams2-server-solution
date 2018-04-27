using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ams2PrototypeProject.Models {

	public class Asset {

		public int Id { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public DateTime? AcquiredDate { get; set; }
		public DateTime? DisposedDate { get; set; }
		public string PONumber { get; set; }
		public decimal? Cost { get; set; }
		public DateTime? OutForRepairDate { get; set; }
		public DateTime? ReturnFromRepairDate { get; set; }
		public DateTime? RetiredDate { get; set; }
		public DateTime? SurplusDate { get; set; }
		public decimal? ResidualValue { get; set; }

		public void Copy(Asset asset) {
			this.Code = asset.Code;
			this.Name = asset.Name;
			this.Description = asset.Description;
			this.AcquiredDate = asset.AcquiredDate;
			this.DisposedDate = asset.DisposedDate;
			this.PONumber = asset.PONumber;
			this.Cost = asset.Cost;
			this.OutForRepairDate = asset.OutForRepairDate;
			this.ReturnFromRepairDate = asset.ReturnFromRepairDate;
			this.RetiredDate = asset.RetiredDate;
			this.SurplusDate = asset.SurplusDate;
			this.ResidualValue = asset.ResidualValue;
		}

		public Asset() {
		}
	}
}