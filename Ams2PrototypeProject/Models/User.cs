using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Ams2PrototypeProject.Models {
	public class User {
		public int Id { get; set; }
		public string Username { get; set; }
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

		public void Copy(User user) {
			this.Username = user.Username;
			this.Password = user.Password;
			this.Firstname = user.Firstname;
			this.Lastname = user.Lastname;
			this.Phone = user.Phone;
			this.Email = user.Email;
			this.Active = user.Active;
		}
	}
}