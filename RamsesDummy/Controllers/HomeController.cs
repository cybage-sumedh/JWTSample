using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using RamsesDummy.Models;

namespace RamsesDummy.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           SetAuthTokenForContentPush();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        private void SetAuthTokenForContentPush()
        {
            var baseUrl = ConfigurationManager.AppSettings["RakutenJwtServerBaseUrl"];
            var relativeUrl = ConfigurationManager.AppSettings["RakutenJwtServerTokenApiRelativeUrl"];

            var data = new Dictionary<string, string>
            {
                {"username", "amar"},
                {"password", "whatever"},
                {"grant_type", "password"},
                {"client_id", ConfigurationManager.AppSettings["DefaultClientId"]}
            };

            using (var httpClient = new HttpClient())
            {
                using (var content = new FormUrlEncodedContent(data))
                {
                    content.Headers.Clear();
                    content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

                    var response = httpClient.PostAsync(baseUrl + relativeUrl, content);

                    var responseData = response.Result.Content.ReadAsStringAsync();

                    var authData = JsonConvert.DeserializeObject<AuthToken>(responseData.Result);

                    Session["authKey"] = authData.AuthTokenKey;

                }
            }
        }
    }
}