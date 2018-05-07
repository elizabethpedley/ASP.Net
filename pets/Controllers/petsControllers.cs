using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
 
namespace pets.Controllers
{
    public class petsController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult index()
        {
            int? fullness = HttpContext.Session.GetInt32("full");
            if(fullness == null)
            {
                HttpContext.Session.SetInt32("full", 20);
                ViewBag.full = 20;
                HttpContext.Session.SetInt32("happy", 20);
                ViewBag.happy = 20;
                HttpContext.Session.SetInt32("energy", 50);
                ViewBag.energy = 50;
                HttpContext.Session.SetInt32("meals", 3);
                ViewBag.meals = 3;
                ViewBag.check = true;
                ViewBag.line = "Do something with your new Dojodachi!";
            }else
            {
                int? happiness = HttpContext.Session.GetInt32("happy");
                int? energyNum = HttpContext.Session.GetInt32("energy");
                int? mealsNum = HttpContext.Session.GetInt32("meals");
                if(happiness > 100 && energyNum > 100 && fullness > 100)
                {
                    ViewBag.line = "Congratulations, You win!";
                    ViewBag.check = false;

                }else if(fullness <= 0 || happiness <= 0)
                {
                    ViewBag.line = "Your Dojodachi has passed away.";
                    ViewBag.check = false;
                }
                else
                {
                    ViewBag.check = true;
                    if(TempData["line"] == null)
                    {
                        ViewBag.line = "Try Playing with your Dojodachi";
                    }else
                    {
                        ViewBag.line = TempData["line"];
                    }
                }
                ViewBag.full = fullness;
                ViewBag.happy = happiness;
                ViewBag.energy = energyNum;
                ViewBag.meals = mealsNum;
            }
            return View();
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult feed()
        {
            int? fullness = HttpContext.Session.GetInt32("full");
            int? mealsNum = HttpContext.Session.GetInt32("meals");
            Random rand = new Random();
            if(fullness != null)
            {
                if(mealsNum > 0)
                {
                    int chance = rand.Next(1,5);
                    if(chance != 4)
                    {
                        int increase = rand.Next(5,11);
                        fullness += increase;
                        HttpContext.Session.SetInt32("full", fullness.Value);
                        TempData["line"] = $" You fed your Dojodachi. Meals - 1 and fullness + {increase}";
                    }else
                    {
                        TempData["line"] = " You fed your Dojodachi and they didn't like it. Meals - 1"; 
                    }
                    mealsNum --;
                    HttpContext.Session.SetInt32("meals", mealsNum.Value);
                }
                else
                {
                    TempData["line"] = "You don't have any meals to feed your Dojodachi";
                }
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("play")]
        public IActionResult play()
        {
            Random rand = new Random();
            int? happiness = HttpContext.Session.GetInt32("happy");
            int? energyNum = HttpContext.Session.GetInt32("energy");
            if(happiness != null && energyNum != null)
            {
                int chance = rand.Next(1,5);
                if(energyNum >= 5)
                {
                    if(chance != 4)
                    {
                        int up = rand.Next(5,11);
                        happiness += up;
                        HttpContext.Session.SetInt32("happy", happiness.Value);
                        TempData["line"] = $" You played with your Dojodachi. Energy - 5 and Happiness + {up}";
                    }
                    else
                    {
                        TempData["line"] = " You played with your Dojodachi and they didn't like it. Energy - 5";
                    }
                    energyNum -= 5;
                    HttpContext.Session.SetInt32("energy", energyNum.Value);
                }else
                {
                    TempData["line"] = "Your Dojodachi does not have enough energy to play.";
                }
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("work")]
        public IActionResult work()
        {
            int? energyNum = HttpContext.Session.GetInt32("energy");
            int? mealsNum = HttpContext.Session.GetInt32("meals");
            Random rand = new Random();
            if(mealsNum != null && energyNum != null)
            {
                if(energyNum >= 5)
                {
                    energyNum -= 5;
                    int earned = rand.Next(1,4);
                    mealsNum += earned;
                    HttpContext.Session.SetInt32("energy", energyNum.Value);
                    HttpContext.Session.SetInt32("meals", mealsNum.Value);
                    TempData["line"] = $" You worked your Dojodachi. Energy - 5 and Meals + {earned}";
                }else
                {
                    TempData["line"] = "Your Dojodachi does not have enough energy to work.";
                }
                

            }
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult sleep()
        {
            int? energyNum = HttpContext.Session.GetInt32("energy");
            int? happiness = HttpContext.Session.GetInt32("happy");
            int? fullness = HttpContext.Session.GetInt32("full");
            if(happiness != null && energyNum != null && fullness != null)
            {
                energyNum += 15;
                fullness -= 5;
                happiness -= 5;
                HttpContext.Session.SetInt32("energy", energyNum.Value);
                HttpContext.Session.SetInt32("full", fullness.Value);
                HttpContext.Session.SetInt32("happy", happiness.Value);
                TempData["line"] = "Your Dojodachi slept. Energy + 15, Fullness -5, Happiness - 5.";
            }
            return RedirectToAction("index");
        }

        [HttpGet]
        [Route("restart")]
        public IActionResult restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }
}