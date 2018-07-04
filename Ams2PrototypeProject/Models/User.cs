﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ams2.Models {
	public class User {
		public int Id { get; set; }
		[Index("IDX_Username", IsUnique = true)]
		[StringLength(50)]
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		[NotMapped]
		public string Fullname { get { return $"{Firstname} {Lastname}"; } }
		public string Phone { get; set; }
		public string Email { get; set; }
		//
		public Boolean Active { get; set; } = true;
		public DateTime DateCreated { get; set; } = DateTime.Now;
		public DateTime? DateUpdated { get; set; }

	}
}