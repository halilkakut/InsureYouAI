using Microsoft.AspNetCore.Mvc;

namespace InsureYouAINew.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult SendChatWithAI()
        {
            return View();
        }
    }
}
