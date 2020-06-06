using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	//[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
	//[TrackDeletions]
	public class RabiesVaccine : BaseTable
	{
		[Key]
		[References(typeof(Patient))]
		public int PatientId { get; set; }

		[Column(TypeName = "date")]
		public DateTime DateGiven { get; set; }

		public int Years { get; set; }

		[Column(TypeName = "date")]
		public DateTime ExpirationDate { get; set; }

		[MaxLength(50)]
		[Required]
		public string LotNumber { get; set; }

		[Column(TypeName = "date")]
		public DateTime LotExpirationDate { get; set; }

		[MaxLength(50)]
		[Required]
		public string Manufacturer { get; set; }

		[References(typeof(Veterinarian))]
		public int VeterinarianId { get; set; }

		public int? SourcePatientItemId { get; set; }
	}
}