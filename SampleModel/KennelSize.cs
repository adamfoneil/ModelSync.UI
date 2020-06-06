using AO.Models;
using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class KennelSize : BaseTable
	{
		[References(typeof(Calendar))]
		[Key]
		public int CalendarId { get; set; }

		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		public int Size { get; set; }

		public int Count { get; set; }
	}
}