using Microsoft.AspNetCore.Mvc;

namespace InsureYouAINew.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/404")]
        public IActionResult Page404()
        {
            return View();
        }
    }
}
