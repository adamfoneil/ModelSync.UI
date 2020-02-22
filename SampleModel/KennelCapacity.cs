using AO.DbSchema.Attributes;
using System.Data;

namespace Hs5.Models
{
	public class KennelCapacity : BaseTable
	{
		[References(typeof(Calendar))]
		public int CalendarId { get; set; }

		[References(typeof(Species))]
		public int SpeciesId { get; set; }

		[References(typeof(KennelSize))]
		public int MinKennelSizeId { get; set; }

		public int Count { get; set; }
	}
}