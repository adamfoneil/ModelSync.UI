using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class TimeUnit : AppTable
	{
		[Key]
		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(255)]
		public string Hints { get; set; }

		public int WeekMultiplier { get; set; }
	}

	/*
	public class TimeUnitSeedData : SeedData<TimeUnit, int>
	{
		public override string ExistsCriteria => "[app].[TimeUnit] WHERE [Name]=@name";

		public override IEnumerable<TimeUnit> Records => new TimeUnit[]
		{
			new TimeUnit() { Name = "Weeks", WeekMultiplier = 1, Hints = "wk,wks,w" },
			new TimeUnit() { Name = "Months", WeekMultiplier = 4, Hints = "m,mon" },
			new TimeUnit() { Name = "Years", WeekMultiplier = 52, Hints = "yr,y" }
		};
	}
	*/
}