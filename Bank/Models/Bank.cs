using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Bank.Models{
	public class User{
		public int UserId {get;set;}

		public string FirstName {get; set;}

		public string LastName {get; set;}

		public string Email {get;set;}

		public string Password {get;set;}

		public Account Account {get; set;}


	}

	public class Account{
		public int AccountId {get;set;}
		public int UserId {get;set;}
		public User User {get;set;}

		public List<Transaction> Transactions {get;set;}

		public double Balance {get;set;}

		public Account(){
			Transactions = new List<Transaction>();
		}
	}
	public class Transaction{
		public int TransactionId {get;set;}
		public DateTime Date = DateTime.Now;
		public double Amount {get;set;}
		public int AccountId{get;set;}
		public Account Account{get; set;}

	}

	public class Registration{

		[Required]
		[Display(Name = "First Name")]
		[MinLength(3)]
		[MaxLength(255)]
		public string FirstName {get; set;}

		[Required]
		[Display(Name = "Last Name")]
		[MinLength(3)]
		[MaxLength(255)]
		public string LastName {get; set;}

		[Required]
		[RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Not a valid email address.")]
		[EmailAddress]
		public string Email {get;set;}

		[Required]
		[MinLength(8)]
		[MaxLength(18)]
		[DataType(DataType.Password)]
		public string Password {get;set;}

		[Required]
		[Display(Name = "Confirm Password")]
		[MinLength(8)]
		[MaxLength(18)]
		[DataType(DataType.Password)]
		public string ConfirmPassword {get;set;}

	}

	public class Login{

		[Required]
		[EmailAddress]
		[MinLength(1)]
		[RegularExpression(@"^[a-zA-Z0-9.+_-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]+$", ErrorMessage="Not a valid email address.")]
		public string Email {get;set;}

		[Required]
		[DataType(DataType.Password)]
		[MinLength(1)]
		public string Password {get;set;}

	}

	public class Withdraw{
		[Required]
		public double Amount {get;set;}
	}

}
