using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	//[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
	//[TrackDeletions]
	public class RabiesTag : BaseTable
	{
		[References(typeof(Patient))]
		public int PatientId { get; set; }

		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[Key]
		public int Year { get; set; }

		[Key]
		public int TagNumber { get; set; }

		public int? SourcePatientItemId { get; set; }
	}
}