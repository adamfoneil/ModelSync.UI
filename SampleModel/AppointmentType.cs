using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class AppointmentType : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[Key]
		[MaxLength(50)]
		public string Name { get; set; }

		[MaxLength(50)]
		public string ForeColor { get; set; }

		[MaxLength(50)]
		public string BackColor { get; set; }

		public bool UsePointCapacity { get; set; }

		public bool UseCountCapacity { get; set; }

		public bool UseKennelCapacity { get; set; }

		public bool UseTimeslotCapacity { get; set; }

		/// <summary>
		/// Length of the last timeslot in a day (since there's no later one to set a boundary)
		/// </summary>
		public int LastTimeslotLength { get; set; }

		public bool IsActive { get; set; } = true;
	}

	/*
	public class AppointmentTypeSeedData : SeedData<AppointmentType, int>
	{
		public override string ExistsCriteria => "[dbo].[AppointmentType] WHERE [ClinicId]=@clinicId AND [Name]=@name";

		public override IEnumerable<AppointmentType> Records => new AppointmentType[]
		{
			new AppointmentType() { Name = "Spay/Neuter", UsePointCapacity = true, BackColor = "#0e0238", ForeColor = "white" },
			new AppointmentType() { Name = "Wellness", UseTimeslotCapacity = true, BackColor = "#4ce08e", ForeColor = "black" },
			new AppointmentType() { Name = "Recheck", BackColor = "#e0c74c", ForeColor = "black" }
		};
	}
	*/
}