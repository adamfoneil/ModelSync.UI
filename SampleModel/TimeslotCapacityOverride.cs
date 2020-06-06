using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	public class TimeslotCapacityOverride : BaseTable
	{
		[References(typeof(Calendar))]
		[Key]
		public int CalendarId { get; set; }

		[Column(TypeName = "date")]
		[Key]
		public DateTime Date { get; set; }

		public TimeSpan StartTime { get; set; }

		public int MaxPatients { get; set; }
	}
}