using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTauranter.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace RESTauranter.Controllers{
    public class RESTauranterController:Controller{
        private RESTauranterContext _context;
 
        public RESTauranterController(RESTauranterContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(){

            return View("Index");
        }

        [HttpPost]
        [Route("/new")]
        public IActionResult addNew(Rating rating){
            if(ModelState.IsValid)
            {
                _context.Add(rating);
                _context.SaveChanges();
                return RedirectToAction("Reviews");
            }
            return View("Index");
        }

        [HttpGet]
        [Route("/reviews")]
        public IActionResult Reviews(){

            ViewBag.all = _context.reviews.OrderBy(r => r.CreatedAt).ToList();
            return View();
        }
    }
}
