using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Planner.Models{
	public class EventVM{
        [Required]
        [MinLength(3)]
		[MaxLength(255)]
        public string WedderOne {get;set;}

        [Required]
        [MinLength(3)]
		[MaxLength(255)]
        public string WedderTwo {get;set;}

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Date {get;set;}
        
        [Required]
        [MinLength(8)]
		[MaxLength(255)]
        public string Address {get;set;}

	}
}