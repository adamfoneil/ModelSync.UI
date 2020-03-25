using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class Sex : AppTable
	{
		[MaxLength(10)]
		[Key]
		public string Name { get; set; }

		[MaxLength(1)]
		[Required]
		public string Abbreviation { get; set; }

		[Required]
		[MaxLength(1)]
		public string SterilizedIndicatorShort { get; set; }

		[Required]
		[MaxLength(20)]
		public string SterilizedIndicatorLong { get; set; }
	}

	/*
	public class SexSeedData : SeedData<Sex, int>
	{
		public override string ExistsCriteria => "[app].[Sex] WHERE [Name]=@name";

		public override IEnumerable<Sex> Records => new Sex[]
		{
			new Sex() { Name = "Female", Abbreviation = "F", SterilizedIndicatorLong = "Spayed", SterilizedIndicatorShort = "S" },
			new Sex() { Name = "Male", Abbreviation = "M", SterilizedIndicatorLong = "Neutered", SterilizedIndicatorShort = "N" }
		};
	}
	*/
}