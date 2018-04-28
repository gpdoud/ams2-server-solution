﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ams2PrototypeProject.Models {

	public class Vehicle {

		public int Id { get; set; }
		public string Code { get; set; }
		public int AssetId { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		public int? Year { get; set; }

		public virtual Asset Asset { get; set; }

		public void Copy(Vehicle vehicle) {
			this.Code = vehicle.Code;
			this.AssetId = vehicle.AssetId;
			this.Make = vehicle.Make;
			this.Model = vehicle.Model;
			this.Year = vehicle.Year;
		}

		public Vehicle() {
		}
	}
}