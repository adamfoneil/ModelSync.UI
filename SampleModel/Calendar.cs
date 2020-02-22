using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class Calendar : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		[MaxLength(500)]
		public string DirectionsUrl { get; set; }

		public bool IsActive { get; set; } = true;

		public Clinic Clinic { get; set; }
	}
}