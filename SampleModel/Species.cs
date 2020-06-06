using AO.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class Species : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Key]
		public string BaseName { get; set; }

		[MaxLength(50)]
		[Key]
		public string SubCategory { get; set; }

		/// <summary>
		/// Minimum weight to be in this sub-category, if applicable
		/// </summary>
		public int MinWeight { get; set; }

		public bool IsActive { get; set; } = true;

		public Clinic Clinic { get; set; }
	}

	/*
	public class SpeciesSeedData : SeedData<Species, int>
	{
		public override string ExistsCriteria => "[dbo].[Species] WHERE [ClinicId]=@clinicId AND [BaseName]=@baseName AND [SubCategory]=@subCategory";

		public override IEnumerable<Species> Records => new Species[]
		{
			new Species() { BaseName = "Dog", SubCategory = "Small" },
			new Species() { BaseName = "Dog", SubCategory = "Medium" },
			new Species() { BaseName = "Dog", SubCategory = "Large" },
			new Species() { BaseName = "Cat", SubCategory = "Owned" },
			new Species() { BaseName = "Cat", SubCategory = "Community" }
		};
	}
	*/
}