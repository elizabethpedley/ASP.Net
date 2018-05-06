using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
 
namespace passcode.Controllers
{
    public class passcodeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult index()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[14];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var path = new String(stringChars);

            int? count = HttpContext.Session.GetInt32("count");

            if(count == null)
            {
                HttpContext.Session.SetInt32("count", 1);
                count = 1;
            }else
            {
                count++;
                int num = count.Value;
                HttpContext.Session.SetInt32("count", num);
            }
            
            
            ViewBag.passcode = path;
            ViewBag.num = count;

            return View();
        }

        
    }
}