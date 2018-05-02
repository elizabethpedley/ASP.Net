using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
 
namespace YourNamespace.Controllers
{
    public class CardController : Controller
    {
        [Route("{first}/{last}/{age}/{color}")]
        public JsonResult Index(string first,string last,int age,string color)
        {
            var obj = new{
                FirstName = first,
                LastName = last,
                Age = age,
                FavoriteColor = color
            };
            return Json(obj);
        }
    }
}