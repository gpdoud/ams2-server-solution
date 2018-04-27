using System.Web;
using System.Web.Mvc;

namespace Ams2PrototypeProject {
	public class FilterConfig {
		public static void RegisterGlobalFilters(GlobalFilterCollection filters) {
			filters.Add(new HandleErrorAttribute());
		}
	}
}
