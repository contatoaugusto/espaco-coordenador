using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EC.Common
{
    public sealed class JsonResponse
    {
        public string Status { get; set; }

        public JsonError Error { get; set; }

        public JsonSerializer Response { get; set; }

        public JsonResponse(JsonStatus status, JsonError error, JsonSerializer response)
        {
            Status = status.ToString().ToLower();
            Error = error;
            Response = response;
        }

        public JsonResponse(string status, JsonError error, JsonSerializer response)
        {
            Status = status;
            Error = error;
            Response = response;
        }

        public string Serialize()
        {
            return Serialize(false);
        }
        public string Serialize(bool responseIsArray)
        {
            if (string.IsNullOrEmpty(Status))
                throw new ArgumentNullException("Status");

            StringBuilder json = new StringBuilder();

            json.Append("{");

            json.AppendFormat(@"""status"":""{0}"",", Status);

            if (Error != null)
            {
                JsonSerializer error = new JsonSerializer();

                error.Add(new JsonItem("id", Error.ID),
                    new JsonItem("description", Error.Description));

                json.AppendFormat(@"""error"":{0},", error.Serialize());
            }
            else
                json.Append(@"""error"":""null"",");

            if (Response != null)
            {
                if (responseIsArray)
                    json.AppendFormat(@"""response"":{0}", Response.Serialize(true));
                else
                    json.AppendFormat(@"""response"":{0}", (Response.Count > 0 ? Response.Serialize(false) : "null"));
            }
            else
                json.Append(@"""response"":""null""");

            json.Append("}");

            return json.ToString();
        }
    }
}
