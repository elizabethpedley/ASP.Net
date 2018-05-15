using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Planner.Models{
		public class LoginVM{

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		[MinLength(1)]
		[RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Not a valid email address.")]
		public string LEmail {get;set;}

		[Required]
		[Display(Name = "Password")]
		[DataType(DataType.Password)]
		[MinLength(1)]
		public string LPassword {get;set;}

	}
}