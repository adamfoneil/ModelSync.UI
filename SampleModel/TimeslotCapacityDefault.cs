using AO.DbSchema.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class TimeslotCapacityDefault : BaseTable
	{
		[References(typeof(Calendar))]
		[Key]
		public int CalendarId { get; set; }

		[Key]
		public DayOfWeek DayOfWeek { get; set; }

		public TimeSpan StartTime { get; set; }

		public int MaxPatients { get; set; }
	}
}