using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hs5.Models
{
    //[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
    public class Appointment : BaseTable
	{
		[References(typeof(Calendar))]
		public int CalendarId { get; set; }

		[Column(TypeName = "date")]
		public DateTime Date { get; set; }

		public DateTime? StartTime { get; set; }

		public DateTime? EndTime { get; set; }

		[References(typeof(Client))]
		public int? OwnerClientId { get; set; }

		[References(typeof(Client))]
		public int? VolumeClientId { get; set; }

		[MaxLength(255)]
		public string Comments { get; set; }

		[References(typeof(AppointmentStatus))]
		public int StatusId { get; set; }

		public bool IsScheduledOnline { get; set; }

		/*
		public override bool AllowSave(IDbConnection connection, out string message)
		{
			if (!base.AllowSave(connection, db, out message)) return false;

			if (!OwnerClientId.HasValue && !VolumeClientId.HasValue)
			{
				message = "Patient must have either or both an owner or volume client";
				return false;
			}

			return true;
		}
		*/
	}
}