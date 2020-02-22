using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	//[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
	public class ItemPrice : BaseTable
	{
		[References(typeof(Item))]
		[Key]
		public int ItemId { get; set; }

		[References(typeof(FeeSchedule))]
		[Key]
		public int FeeScheduleId { get; set; }

		[Column(TypeName = "money")]
		public decimal Price { get; set; }
	}
}