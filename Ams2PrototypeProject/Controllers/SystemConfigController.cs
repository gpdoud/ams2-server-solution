using Ams2PrototypeProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ams2PrototypeProject.Controllers {

	[EnableCors(origins: "*", headers: "*", methods: "*")]
	public class SystemConfigController : ApiController {

		private AmsDbContext db = new AmsDbContext();

		[HttpGet]
		[ActionName("List")]
		public IEnumerable<SystemConfig> GetAll() {
			return db.SystemConfig.ToList();
		}

		[HttpGet]
		[ActionName("GetKey")]
		public SystemConfig GetValueByKey(string syskey) {
			if (syskey == null) return null;
			var systemConfig = getByKey(syskey);
			if (systemConfig == null) return null;
			return systemConfig;
		}

		[HttpGet]
		[ActionName("SetKey")]
		public bool SetValueByKey(string syskey, string sysvalue, string category = null) {
			if (syskey == null) return false;
			SystemConfig syscfg = new SystemConfig(syskey, sysvalue, category);
			var syscfg2 = getByKey(syskey);
			if(syscfg2 == null) { // doesn't exist; add
				db.SystemConfig.Add(syscfg);
			} else { // exists; change
				syscfg2.Category = syscfg.Category;
				syscfg2.SysKey = syscfg.SysKey;
				syscfg2.SysValue = syscfg.SysValue;
			}
			db.SaveChanges();
			return true;
		}

		private SystemConfig getByKey(string syskey) {
			if (syskey == null) return null;
			var systemConfig = db.SystemConfig.SingleOrDefault(sc => sc.SysKey == syskey);
			return systemConfig;
		}
	}
}
