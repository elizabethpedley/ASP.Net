using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Planner.Models{
	public class Event{
        [Key]
		public int EventId {get;set;}

        public string WedderOne {get;set;}

        public string WedderTwo {get;set;}

        public DateTime Date {get;set;}

        public string Address {get;set;}

        public int CreatorId {get;set;}
        
        public User Creator {get;set;}

        public List<GuestList> Attendees {get;set;}

        public Event()
        {
            Attendees = new List<GuestList>();
        }

	}
}