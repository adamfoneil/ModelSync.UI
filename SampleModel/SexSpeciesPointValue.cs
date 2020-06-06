using AO.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class SexSpeciesPointValue : BaseTable
	{
		[References(typeof(Sex))]
		[Key]
		public int SexId { get; set; }

		[References(typeof(Species))]
		[Key]
		public int SpeciesId { get; set; }

		public int Points { get; set; }
	}
}