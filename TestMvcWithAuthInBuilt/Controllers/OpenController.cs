using System.Web.Mvc;
using TestMvcWithAuthInBuilt.Models;

namespace TestMvcWithAuthInBuilt.Controllers
{
    public class OpenController : Controller
    {
        // GET: Open

        [System.Web.Http.HttpPost]
        public void Post(string token)
        {
            JwtModel.Instance.Token = token;
        }
    }
}