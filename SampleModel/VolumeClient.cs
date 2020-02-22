using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	//[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
	public class VolumeClient : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[Key]
		[MaxLength(10)]
		public string Code { get; set; }

		/// <summary>
		/// If yes, it means invoices are not receivable
		/// </summary>
		public bool IsGrant { get; set; }

		/// <summary>
		/// Maximum balance allowed before patients cannot be scheduled.
		/// Set this to allow a little bit of grant overspending to allow for no-shows
		/// </summary>
		[Column(TypeName = "money")]
		public decimal? MaxBalance { get; set; }
	}
}