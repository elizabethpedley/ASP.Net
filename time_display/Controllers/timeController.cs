using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace YourNamespace.Controllers
{
    public class timeController : Controller
    {
        [Route("")]
        public IActionResult index()
        {
            return View();
        }
    }
}