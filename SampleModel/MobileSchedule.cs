using AO.DbSchema.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	//[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
	public class MobileSchedule : BaseTable
	{
		[References(typeof(MobileUnit))]
		[Key]
		public int UnitId { get; set; }

		[References(typeof(MobileDestination))]
		[Key]
		public int DestinationId { get; set; }

		[Key]
		[Column(TypeName = "date")]
		public DateTime Date { get; set; }
	}
}