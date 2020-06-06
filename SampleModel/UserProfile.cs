using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	[Identity(nameof(UserId))]
	public class UserProfile
	{
		public int UserId { get; set; }

		[MaxLength(50)]
		public string UserName { get; set; }

		[References(typeof(Clinic))]
		public int? ClinicId { get; set; }

		public bool IsActive { get; set; }

		public int TimeZoneOffset { get; set; }
		
		public DateTime LocalTime
		{
			get { return DateTime.UtcNow; }
		}

		public long Permissions { get; set; }

		public static bool HasPermission(IDbConnection connection, string action)
		{
			throw new NotImplementedException();
		}

		public static void SetPermissions(IDbConnection connection, string userName, long permissions)
		{
			throw new NotImplementedException();
		}
	}
}