using AO.Models;
using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class MobileUnit : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		public bool IsActive { get; set; } = true;

		public Clinic Clinic { get; set; }
	}
}