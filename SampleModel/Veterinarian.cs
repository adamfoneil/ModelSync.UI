using AO.Models;
using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class Veterinarian : BaseTable
	{
		[References(typeof(Clinic))]		
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Required]
		public string FirstName { get; set; }

		[MaxLength(50)]
		[Required]
		public string LastName { get; set; }

		[MaxLength(50)]
		public string LicenseNumber { get; set; }

		public byte[] SignatureImage { get; set; }

		public bool IsActive { get; set; }
	}
}