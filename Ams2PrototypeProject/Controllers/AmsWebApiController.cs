using Ams2.Models;
using Ams2.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ams2.Controllers {

	public class AmsWebApiController : ApiController {

		protected AmsDbContext db = new AmsDbContext();

		protected JsonResponse SaveChanges(JsonResponse resp = null) {
			try {
				db.SaveChanges();
				return resp ?? JsonResponse.Ok;
			} catch (Exception ex) {
				return new JsonResponse { Message = ex.Message, Error = ex };
			}
		}
	}
}
