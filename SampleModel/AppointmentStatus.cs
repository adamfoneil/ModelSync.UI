using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class AppointmentStatus : AppTable
	{
		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		public bool AffectsCapacity { get; set; }
	}

	/*
	public class AppointmentStatusSeedData : SeedData<AppointmentStatus, int>
	{
		public override string ExistsCriteria => "[app].[AppointmentStatus] WHERE [Name]=@name";

		public override IEnumerable<AppointmentStatus> Records => new AppointmentStatus[]
		{
			new AppointmentStatus() { Name = "Scheduled", AffectsCapacity = true },
			new AppointmentStatus() { Name = "Confirmed", AffectsCapacity = true },
			new AppointmentStatus() { Name = "Canceled" }
		};
	}
	*/
}