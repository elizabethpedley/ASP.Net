using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace YourNamespace.Controllers
{
    public class dojoController : Controller
    {
        [Route("")]
        public IActionResult index()
        {
            return View();
        }

        [HttpPost]
        [Route("result")]
        public IActionResult result(string name, string location, string language, string comments )
        {
            ViewBag.name = name;
            ViewBag.language = language;
            ViewBag.location = location;
            ViewBag.comments = comments;
            return View();
        }
    }
}