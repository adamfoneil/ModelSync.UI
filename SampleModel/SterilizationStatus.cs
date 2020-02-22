using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class SterilizationStatus : AppTable
	{
		[Key]
		[MaxLength(50)]
		public string Name { get; set; }
	}

	/*
	public class SterilizationStatusSeedData : SeedData<SterilizationStatus, int>
	{
		public override string ExistsCriteria => "[app].[SterilizationStatus] WHERE [Name]=@name";

		public override IEnumerable<SterilizationStatus> Records => new SterilizationStatus[]
		{
			new SterilizationStatus() { Name = "This Clinic" },
			new SterilizationStatus() { Name = "Somewhere Else" },
			new SterilizationStatus() { Name = "Not Set" }
		};
	}
	*/
}