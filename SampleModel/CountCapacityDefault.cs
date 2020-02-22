using AO.DbSchema.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class CountCapacityDefault : BaseTable
	{
		[References(typeof(Calendar))]
		[Key]
		public int CalendarId { get; set; }

		[Key]
		public DayOfWeek DayOfWeek { get; set; }

		[References(typeof(Sex))]
		[Key]
		public int SexId { get; set; }

		[References(typeof(Species))]
		[Key]
		public int SpeciesId { get; set; }

		public int Maximum { get; set; }
	}
}