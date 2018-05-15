using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Planner.Models{
	public class RegistrationVM{

		[Required]
		[Display(Name = "First Name")]
		[MinLength(3,ErrorMessage="Names must be atleast 3 letters.")]
		[MaxLength(255)]
		public string FirstName {get; set;}

		[Required]
		[Display(Name = "Last Name")]
		[MinLength(3,ErrorMessage="Names must be atleast 3 letters.")]
		[MaxLength(255)]
		public string LastName {get; set;}

		[Required]
		[RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Not a valid email address.")]
		[EmailAddress]
		public string Email {get;set;}

		[Required]
		[MinLength(8,ErrorMessage="Passwords must be atleast 8 letters.")]
		[MaxLength(18)]
		[DataType(DataType.Password)]
		[Compare("ConfirmPassword", ErrorMessage="Passwords must match.")]
		public string Password {get;set;}

		[Required]
		[Display(Name = "Confirm Password")]
		[MinLength(8,ErrorMessage="Passwords must be atleast 8 letters.")]
		[MaxLength(18)]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage="Passwords must match.")]
		public string ConfirmPassword {get;set;}

	}
}