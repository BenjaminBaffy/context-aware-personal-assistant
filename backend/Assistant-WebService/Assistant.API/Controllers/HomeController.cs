using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace Assistant.API.Controllers
{
    [Route("")]
    [OpenApiIgnore]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
