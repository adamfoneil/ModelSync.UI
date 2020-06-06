using AO.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class MicrochipItem : BaseTable
	{
		[Key]
		[References(typeof(Item))]
		public int ItemId { get; set; }

		public Item Item { get; set; }

		[References(typeof(MicrochipProvider))]
		public int ProviderId { get; set; }

		public bool IsActive { get; set; } = true;
	}
}