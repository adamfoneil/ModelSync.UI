using AO.DbSchema.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hs5.Models
{
	//[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
	public class RabiesVaccineItem : BaseTable
	{
		[Key]
		[References(typeof(Item))]
		public int ItemId { get; set; }

		/// <summary>
		/// Use 0 for all calendars
		/// </summary>
		[Key]
		public int CalendarId { get; set; }

		public int Years { get; set; }

		[MaxLength(50)]
		[Required]
		public string LotNumber { get; set; }

		[Column(TypeName = "date")]
		public DateTime LotExpirationDate { get; set; }

		[MaxLength(50)]
		[Required]
		public string Manufacturer { get; set; }

		public int? NextTagNumber { get; set; }

		public Item Item { get; set; }
	}
}