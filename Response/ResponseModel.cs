using System.Net;

namespace AltaProject.Response
{
    public class ResponseModel
    {
        public HttpStatusCode code { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public ResponseModel(HttpStatusCode code, string message, object data)
        {
            this.code = code;
            this.message = message;
            this.data = data;
        }
    }
}
