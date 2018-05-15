using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Planner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Planner.Controllers{
	[Route("/planner")]
    public class PlannerController:Controller{
        private PlannerContext _context;
 
        public PlannerController(PlannerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index(){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<Event> wedding =  _context.Events.Include(e => e.Creator).Include(e => e.Attendees).ToList();
            

            ViewBag.User = _context.Users.SingleOrDefault(u => u.UserId == Id);
            ViewBag.Events = _context.Events.Include(e => e.Creator).Include(e => e.Attendees).ToList();
            
            return View("Index");
        }

        [HttpGet]
        [Route("new")]
        public IActionResult New(){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            return View();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult Create(EventVM wedding){
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if(ModelState.IsValid)
            {
                if(wedding.Date < DateTime.Now)
                {
                    ModelState.AddModelError("Date", "Date Cannot be before today.");
                    return View("New");
                }
                User user = _context.Users.SingleOrDefault(u => u.UserId == Id);
                Event Wedding = new Event{
                    WedderOne = wedding.WedderOne,
                    WedderTwo = wedding.WedderTwo,
                    Date = wedding.Date.Value,
                    Address = wedding.Address,
                    Creator = user
                };
                _context.Events.Add(Wedding);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View("New");
        }

        [HttpGet]
        [Route("{id}/rsvp")]
        public IActionResult Rsvp(int id)
        {
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Index", "Login");
            }
            User user = _context.Users.SingleOrDefault(u => u.UserId == Id );
            Event wedding = _context.Events.SingleOrDefault(e => e.EventId == id);
            GuestList check = _context.GuestList.SingleOrDefault(g => g.AttendeeId == user.UserId && g.EventId == wedding.EventId);

            if(check != null || wedding.CreatorId == user.UserId)
            {
                return RedirectToAction("Index");
            }

            GuestList guest = new GuestList{
                Attendee = user,
                Event = wedding

            };

            _context.GuestList.Add(guest);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/unrsvp")]
        public IActionResult unRsvp(int id)
        {
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Index", "Login");
            }
            User user = _context.Users.SingleOrDefault(u => u.UserId == Id );
            Event wedding = _context.Events.SingleOrDefault(e => e.EventId == id);
            GuestList check = _context.GuestList.SingleOrDefault(g => g.AttendeeId == user.UserId && g.EventId == wedding.EventId);

            if(check == null)
            {
                return RedirectToAction("Index");
            }

            _context.GuestList.Remove(check);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}/delete")]
        public IActionResult delete(int id)
        {
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Index", "Login");
            }
            User user = _context.Users.SingleOrDefault(u => u.UserId == Id );
            Event wedding = _context.Events.SingleOrDefault(e => e.EventId == id);
            

            if(wedding == null)
            {
                return RedirectToAction("Index");
            }else if(wedding.CreatorId != user.UserId)
            {
                return RedirectToAction("Index");
            }

            _context.Events.Remove(wedding);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Wedding(int id)
        {
            int? Id = HttpContext.Session.GetInt32("Id");
            if(Id == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.user = _context.Users.SingleOrDefault(u => u.UserId == Id );
            ViewBag.wedding = _context.Events.Include(e => e.Attendees).ThenInclude(g => g.Attendee).SingleOrDefault(e => e.EventId == id);
            
            return View();
        }




    }
}
