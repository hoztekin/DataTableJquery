using System.ComponentModel.DataAnnotations;

namespace DataTableJquery.Models
{
	public class Customer
	{
		[Key]
		public int id { get; set; }
		public string? firstname { get; set; }
		public string?  lastname { get; set; }
		public string? gender { get; set; }
		public string? country { get; set; }
		public int? age { get; set; }
	}
}
