using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bank.Controllers{
	[Route("/Bank")]
    public class BankController:Controller{

        private BankContext _context;
 
        public BankController(BankContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Login", "Login");
            }
            User user = _context.Users.Include(u => u.Account).ThenInclude(a => a.Transactions).ToList().SingleOrDefault(e => e.UserId == Id);
            ViewBag.User = user;
            return View("Index");
        }

        [HttpPost]
        [Route("")]
        public IActionResult Withdraw(Withdraw amt){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Login", "Login");
            }
            User user = _context.Users.Include(u => u.Account).ThenInclude(a => a.Transactions).SingleOrDefault(e => e.UserId == Id);
    
            if(amt.Amount>0)
            {
                user.Account.Balance += amt.Amount;
                Transaction trans = new Transaction{
                AccountId = user.Account.AccountId,
                Amount = amt.Amount
                };
                _context.Transactions.Add(trans);
            } else if(amt.Amount < 0)
            {
                if(user.Account.Balance - amt.Amount < 0)
                {
                    ModelState.AddModelError("Amount", "You do not have enough money to make that Withdraw.");
                    
                }else{
                    user.Account.Balance += amt.Amount;
                    Transaction trans = new Transaction{
                    AccountId = user.Account.AccountId,
                    Amount = amt.Amount
                    };
                    _context.Transactions.Add(trans);
                    // user.Account.Transactions.Add(trans);
                }
                
            }
            
            _context.SaveChanges();
            System.Console.WriteLine(user.Account.Transactions.Count);
            return RedirectToAction("Index");
        }
    }

    public class LoginController:Controller{
        private BankContext _context;
 
        public LoginController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id != null)
            {
                return RedirectToAction("Index", "Bank");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("")]
        public IActionResult Register(Registration Register){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id != null)
            {
                return RedirectToAction("Index", "Bank");
            }
            
            if(ModelState.IsValid)
            {
                var checkEmail = _context.Users.Where(e=> e.Email == Register.Email).SingleOrDefault();
                if(checkEmail != null)
                {
                    ModelState.AddModelError("Email", "A user with this email already exists.");
                    return View("Index");
                }
                PasswordHasher<Registration> Hasher = new PasswordHasher<Registration>();
                Register.Password = Hasher.HashPassword(Register, Register.Password);
                
                User NewUser = new User{
                    FirstName = Register.FirstName,
                    LastName = Register.LastName,
                    Email = Register.Email,
                    Password = Register.Password

                };

                Account Account = new Account{
                    Balance = 0.00,
                    User = NewUser
                };
                _context.Users.Add(NewUser);
                _context.Accounts.Add(Account);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("Id", NewUser.UserId);
                
    
            }
            return RedirectToAction("Index", "Bank");
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id != null)
            {
                return RedirectToAction("Index", "Bank");
            }
            return View("Login");
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Log(Login Log){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id != null)
            {
                return RedirectToAction("Index", "Bank");
            }
            if(ModelState.IsValid)
            {
                User User = _context.Users.SingleOrDefault(u => u.Email == Log.Email);
                if(User == null)
                {
                    ModelState.AddModelError("Email", "Email/Password is Invalid");
                    ModelState.AddModelError("Password", "Email/Password is Invalid");
                    return View("Login");
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                if(0 == Hasher.VerifyHashedPassword(User, User.Password, Log.Password))
                {
                    System.Console.WriteLine("Password Failed");
                    ModelState.AddModelError("Email", "Email/Password is Invalid");
                    ModelState.AddModelError("Password", "Email/Password is Invalid");
                    return View("Login");
                }
                HttpContext.Session.SetInt32("Id", User.UserId);
            }

            return RedirectToAction("Index", "Bank");
        }

        [HttpGet]
        [Route("logoff")]
        public IActionResult Logoff(){
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

    }
}
