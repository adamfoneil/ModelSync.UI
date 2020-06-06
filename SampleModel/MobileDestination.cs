using AO.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class MobileDestination : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		[MaxLength(100)]
		public string Address { get; set; }

		[MaxLength(50)]
		public string City { get; set; }

		[MaxLength(2)]
		public string State { get; set; }

		[MaxLength(20)]
		public string PostalCode { get; set; }

		[MaxLength(500)]
		public string DirectionsUrl { get; set; }

		public bool IsActive { get; set; } = true;
	}
}