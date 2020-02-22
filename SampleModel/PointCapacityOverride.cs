using AO.DbSchema.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	public class PointCapacityOverride : BaseTable
	{
		[References(typeof(Calendar))]
		[Key]
		public int CalendarId { get; set; }

		[Column(TypeName = "date")]
		[Key]
		public DateTime Date { get; set; }

		public int Maximum { get; set; }
	}
}