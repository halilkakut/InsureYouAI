using Microsoft.AspNetCore.Mvc;

namespace InsureYouAINew.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
