using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	public class CountCapacityOverride : BaseTable
	{
		[References(typeof(Calendar))]
		[Key]
		public int CalendarId { get; set; }

		[Column(TypeName = "date")]
		[Key]
		public DateTime Date { get; set; }

		[References(typeof(Sex))]
		[Key]
		public int SexId { get; set; }

		[References(typeof(Species))]
		[Key]
		public int SpeciesId { get; set; }

		public int Maximum { get; set; }
	}
}