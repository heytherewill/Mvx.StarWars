namespace StarWars.Core.Models
{
	using System.Collections.Generic;
	
	public class ResponseEntity<T>
	{
		public int Count { get; set; } 

		public string Next { get; set; }

		public List<T> Results { get; set; }
	}
}