using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class FeeSchedule : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		public bool IsActive { get; set; } = true;
	}

	/*
	public class FeeScheduleSeedData : SeedData<FeeSchedule, int>
	{
		public override string ExistsCriteria => "[dbo].[FeeSchedule] WHERE [ClinicId]=@clinicId AND [Name]=@name";

		public override IEnumerable<FeeSchedule> Records => new FeeSchedule[]
		{
			new FeeSchedule() { Name = "Standard Fees" }
		};
	}
	*/
}