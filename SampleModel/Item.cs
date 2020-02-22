using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class Item : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(100)]
		[Key]
		public string Name { get; set; }

		[References(typeof(ItemType))]
		public int TypeId { get; set; }

		[References(typeof(ItemCategory))]
		public int? CategoryId { get; set; }

		public bool IsTaxed { get; set; }

		public bool IsSurgery { get; set; }

		public bool IsActive { get; set; } = true;

		public Clinic Clinic { get; set; }

	}
}