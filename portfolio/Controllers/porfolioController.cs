using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace YourNamespace.Controllers
{
    public class portfolioController : Controller
    {
        [Route("")]
        public IActionResult index()
        {
            return View();
        }

        [Route("projects")]
        public IActionResult projects()
        {
            return View();
        }

        [Route("contact")]
        public IActionResult contact()
        {
            return View();
        }
    }
}