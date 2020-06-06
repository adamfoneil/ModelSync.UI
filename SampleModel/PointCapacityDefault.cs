using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class PointCapacityDefault : BaseTable
	{
		[References(typeof(Calendar))]
		[Key]
		public int CalendarId { get; set; }

		[Key]
		public DayOfWeek DayOfWeek { get; set; }

		public int Maximum { get; set; }
	}
}