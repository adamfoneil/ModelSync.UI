using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class MicrochipProvider : AppTable
	{
		[MaxLength(50)]
		public string Name { get; set; }
	}

	/*
	public class MicrochipProviderSeedData : SeedData<MicrochipProvider, int>
	{
		public override string ExistsCriteria => "[app].[MicrochipProvider] WHERE [Name]=@name";

		public override IEnumerable<MicrochipProvider> Records => new MicrochipProvider[]
		{
			new MicrochipProvider() { Name = "24PetWatch" },
			new MicrochipProvider() { Name = "Found Animals" },
			new MicrochipProvider() { Name = "PetLink" }
		};
	}
	*/
}