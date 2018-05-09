using System.ComponentModel.DataAnnotations;

namespace Woods.Models{
	public class Trail{

		public int id {get;set;}

		[Required]
		[Display(Name = "Trail Name")]
		[MinLength(3)]
		[MaxLength(255)]
        public string Name { get; set; }

		[Required]
		[Display(Name = "Trail Description")]
		[MinLength(10)]
		[MaxLength(255)]
		public string Description {get; set;}

		[Required]
		[Display(Name = "Elevation Change")]
		public int Elevation {get; set;}

		[Required]
		[Range(0.0, double.MaxValue, ErrorMessage = "Your trail length cannot be less than 0.0.")]
		public double Length {get; set;}


		[Required]
		public double Longitude {get; set;}

		[Required]
		public double Latitude {get; set;}

		public Trail(string title, string desc, int vation, double len, double longi, double lat)
		{
			Name = title;
			Description = desc;
			Elevation = vation;
			Length = len;
			Longitude = longi;
			Latitude = lat;
		}
		public Trail()
		{
			
		}

/*
	Useful Annotations and Examples:

	[Required] - Makes a field required.
	[RegularExpression(@"[0-9]{0,}\.[0-9]{2}", ErrorMessage = "error Message")] - Put a REGEX string in here.
	[MinLength(100)] - Field must be at least 100 characters long.
	[MaxLength(1000)] - Field must be at most 1000 characters long.
	[Range(5,10)] - Field must be between 5 and 10 characters.
	[EmailAddress] - Field must contain an @ symbol, followed by a word and a period.
	[DataType(DataType.Password)] - Ensures that the field conforms to a specific DataType
*/
	}
}
