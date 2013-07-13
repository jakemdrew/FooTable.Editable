using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ASP.net_Editable_Demo
{
    /// <summary>
    /// Summary description for FooTableHandler
    /// </summary>
    public class FooTableHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var contextData = context.Request;
            var sr = new StreamReader(contextData.InputStream);
            var stream = sr.ReadToEnd();
  
            var javaScriptSerializer = new JavaScriptSerializer();
            var data = javaScriptSerializer.Deserialize<Dictionary<string, string>>(stream);

            string command = data["command"];
            string table = data["table"];

            string response = "";
            string message = "";
            object responseData = null;

            try
            {

                //employee table operations
                if (table == "Employees")
                {
                    if (command == "Add")
                    {
                        response = "Update";
                        responseData = "{\"EmpId\":" + FakeDatabase.add(data) + "}";
                    }

                    if (command == "Delete") FakeDatabase.delete(data);
                    if (command == "Update") FakeDatabase.update(data);

                    if (command == "Load")
                    {
                        response = "Load";
                        responseData = FakeDatabase.select(data);
                    }
                }

                response = response == "" ? "Success" : response;
                message = "Hell Yeah!";
            }
            catch (Exception e)
            {
                response = "Error";
                message = e.Message;
            }
            
            context.Response.ContentType = "application/json; charset=utf-8";

            Dictionary<string, object> responseItems = new Dictionary<string, object>();
            if (response != null) responseItems.Add("response", response);
            if (message != null) responseItems.Add("message", message);
            if (responseData != null) responseItems.Add("responseData", responseData);
            //string test = javaScriptSerializer.Serialize(responseItems);
            context.Response.Write(javaScriptSerializer.Serialize(responseItems));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}