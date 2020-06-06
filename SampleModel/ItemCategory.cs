using AO.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class ItemCategory : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		public bool IsActive { get; set; } = true;
	}
}