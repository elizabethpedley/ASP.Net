using System.ComponentModel.DataAnnotations;
using System;

namespace RESTauranter.Models{
	public class Rating{
		public int Id { get; set; }

		[Required]
		[Display(Name = "Reviewer Name")]
		[MinLength(3)]
		[MaxLength(255)]
		public string Reviewer{get;set;}

		[Required]
		[Display(Name = "Restaurant Name")]
		[MinLength(3)]
		[MaxLength(255)]
		public string Restaurant{get;set;}

		[Required]
		[MinLength(10)]
		[MaxLength(255)]
		public string Review{get;set;}

		[Required]
		[Display(Name="Date of Visit")]
		[DataType(DataType.Date)]
		[BeforeToday]
		public DateTime? DateOfVisit { get; set;}

		[Required]
		[Range(1,6)]
		public int? Stars{get; set;}

		public DateTime CreatedAt= DateTime.Now;

	}

	//cutom validator to check if date is after today
	public class BeforeTodayAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
		if(value == null)
		{
			return new ValidationResult("Date cannot be empty");
		}
        value = (DateTime)value;
        if (DateTime.Now.AddYears(-100).CompareTo(value) <= 0 && DateTime.Now.CompareTo(value) >= 0)
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("Date cannot be after today");
        }
    }
}
}
