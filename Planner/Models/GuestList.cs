using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Planner.Models{
	public class GuestList{
        [Key]
		public int GuestListId {get;set;}

        public int AttendeeId {get;set;}

        public User Attendee {get;set;}

        public int EventId {get;set;}

        public Event Event {get;set;}

	}
}