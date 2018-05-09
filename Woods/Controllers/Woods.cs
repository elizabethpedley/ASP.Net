using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Woods.Models;
using Woods.Factory;

namespace Woods.Controllers{
    public class WoodsController:Controller{
        private readonly TrailFactory trailFactory;
        public WoodsController()
        {
            //Instantiate a UserFactory object that is immutable (READONLY)
            //This establishes the initial DB connection for us.
            trailFactory = new TrailFactory();
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            ViewBag.all = trailFactory.FindAll();
            return View("Index");
        }

        [HttpGet]
        [Route("/trails/{id}")]
        public IActionResult trail(int id){
            ViewBag.trail = trailFactory.FindByID(id);
            return View();
        }

        [HttpGet]
        [Route("/newtrail")]
        public IActionResult newTrail(){
            return View();
        }

        [HttpPost]
        [Route("newTrailCreate")]
        public IActionResult newTrailCreate(Trail trail){
            System.Console.WriteLine("top of create");
            if(ModelState.IsValid)
            {
                trailFactory.Add(trail);
                System.Console.WriteLine("here");
                return RedirectToAction("Index");
            }
            System.Console.WriteLine("bottom of create");
            return View("newTrail");
        }

        
    }
}
