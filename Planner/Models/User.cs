using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Planner.Models{
	public class User{
		[Key]
		public int UserId {get;set;}

		public string FirstName {get; set;}

		public string LastName {get; set;}

		public string Email {get;set;}

		public string Password {get;set;}

		public List<Event> Created {get;set;}

		public List<GuestList> Attending {get;set;}

		public User()
		{
			Created = new List<Event>();
			Attending = new List<GuestList>();
		}


	}
}
