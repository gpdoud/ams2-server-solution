using System;
using System.Collections;
using System.Collections.Generic;
using Ams2.Controllers;
using Ams2.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ams2TestProject {

	[TestClass]
	public class TestUsersController {

		static UsersController controller = new UsersController();
		static List<User> testUsers = new List<User>();
		int rnd = new Random().Next(1000);

		[ClassCleanup]
		public static void ClassCleanup() {
			foreach (var user in testUsers) {
				controller.RemoveUser(user);
			}
		}

		[TestMethod]
		public void TestCreate() {
			var user = new User {
				Id = 0,
				Username = "Testusername" + rnd,
				Password = "Textuserpass",
				Firstname = "Testfirstname",
				Lastname = "Testlastname",
				Phone = "123-456-7890",
				Email = "test@user.com",
				Active = true
			};
			var jr = controller.AddUser(user);
			Assert.IsNotNull(jr.Data, "User add failed");
			testUsers.Add(user);
		}

		[TestMethod]
		public void TestChange() {
			if (testUsers.Count == 0)
				Assert.Fail("No users to test change");
			var user = testUsers[0];
			user.Firstname = "FirstUpdated";
			user.Lastname = "LastUpdated";
			var jr = controller.ChangeUser(user);
			Assert.IsNotNull(jr.Data, "User change failed");
		}

		[TestMethod]
		public void TestList() {
			var jr = controller.GetUsers();
			Assert.IsNotNull(jr.Data, "Data is null");
		}

		[TestMethod]
		public void TestGet() {
			if (testUsers.Count == 0)
				Assert.Fail("No users have been added - error!");
			var id = testUsers[0].Id;
			var jr = controller.GetUser(id);
			Assert.AreEqual(id, ((User)jr.Data).Id, "Get user failed");
		}

		[TestMethod]
		public void TestLogin() {
			if (testUsers.Count == 0)
				Assert.Fail("No users to test login");
			var user = testUsers[0];
			// check invalid login
			var username = user.Username; // force it to be correct
			var password = user.Password;
			var jr = controller.LoginUser(username, password);
			Assert.IsNotNull(jr.Data, "Valid user login failed");
			jr = controller.LoginUser(username + "X", password);
			Assert.IsNull(jr.Data, "Invalid user login failed");
		}

		[TestMethod]
		public void TestRemove() {
			if (testUsers.Count == 0)
				Assert.Fail("No users to test remove");
			var user = testUsers[0];
			var jr = controller.RemoveUser(user);
			Assert.IsNotNull(jr.Data, "User remove failed");
		}

	}
}
