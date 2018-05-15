using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;

namespace Planner.Controllers{
	 public class LoginController:Controller{
        private PlannerContext _context;
 
        public LoginController(PlannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id != null)
            {
                return RedirectToAction("Index", "Planner");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("")]
        public IActionResult Register(RegistrationVM Register){
            
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id != null)
            {
                System.Console.WriteLine("in not null id");
                return RedirectToAction("Index", "Planner");
                
            }
            
            if(ModelState.IsValid)
            {
                System.Console.WriteLine("in valid model");
                var checkEmail = _context.Users.Where(e=> e.Email == Register.Email).SingleOrDefault();
                if(checkEmail != null)
                {
                    ModelState.AddModelError("Email", "A user with this email already exists.");
                    return View("Index");
                }
                PasswordHasher<RegistrationVM> Hasher = new PasswordHasher<RegistrationVM>();
                Register.Password = Hasher.HashPassword(Register, Register.Password);
                
                User NewUser = new User{
                    FirstName = Register.FirstName,
                    LastName = Register.LastName,
                    Email = Register.Email,
                    Password = Register.Password

                };

                _context.Users.Add(NewUser);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("Id", NewUser.UserId);
                
                return RedirectToAction("Index", "Planner");
            }

            return View("Index");
            
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginVM Log){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id != null)
            {
                return RedirectToAction("Index", "Planner");
            }
            if(ModelState.IsValid)
            {
                User User = _context.Users.SingleOrDefault(u => u.Email == Log.LEmail);
                if(User == null)
                {
                    System.Console.WriteLine("Invalid email");
                    ModelState.AddModelError("LEmail", "Email/Password is Invalid");
                    ModelState.AddModelError("LPassword", "Email/Password is Invalid");
                    return View("Index");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                if(0 == Hasher.VerifyHashedPassword(User, User.Password, Log.LPassword))
                {
                    System.Console.WriteLine("Invalid password");
                    ModelState.AddModelError("LEmail", "Email/Password is Invalid");
                    ModelState.AddModelError("LPassword", "Email/Password is Invalid");
                    return View("Index");
                }
                HttpContext.Session.SetInt32("Id", User.UserId);
                return RedirectToAction("Index", "Planner");
            }

            return View("Index");
        }

        [HttpGet]
        [Route("logoff")]
        public IActionResult Logoff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

     }
}
