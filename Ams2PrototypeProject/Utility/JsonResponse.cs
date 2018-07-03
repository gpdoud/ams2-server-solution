using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ams2.Utility {
	public class JsonResponse {

		public static JsonResponse Ok = new JsonResponse("Ok");

		public int Code { get; set; } = 0;
		public string Message { get; set; } = "Success";
		public object Data { get; set; } = null;
		public object Error { get; set; } = null;

		public JsonResponse() { }

		public JsonResponse(int Code, string Message) {
			this.Code = Code;
			this.Message = Message;
		}
		public JsonResponse(string Message) {
			this.Message = Message;
		}
		public JsonResponse(object Data) {
			this.Data = Data;
		}
	}
}