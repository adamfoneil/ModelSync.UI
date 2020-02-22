using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	public class PatientAppointment : BaseTable
	{
		[References(typeof(Patient))]
		[Key]
		public int PatientId { get; set; }

		[References(typeof(Appointment))]
		[Key]
		public int AppointmentId { get; set; }

		[Column(TypeName = "decimal(5,2)")]
		public decimal Weight { get; set; }

		public bool InHeat { get; set; }

		public bool IsPregnant { get; set; }

		public bool Cryptorchid { get; set; }

		[References(typeof(Veterinarian))]
		public int VeterinarianId { get; set; }

		[References(typeof(DeclinedSurgeryReason))]
		public int DeclinedSurgeryReasonId { get; set; }

		public string Notes { get; set; }
	}
}