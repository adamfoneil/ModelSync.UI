using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	[Identity(nameof(Id))]
	public abstract class BaseTable
	{
		public int Id { get; set; }

		[MaxLength(50)]
		[Required]
		public string CreatedBy { get; set; }

		public DateTime DateCreated { get; set; }

		[MaxLength(50)]
		public string ModifiedBy { get; set; }

		public DateTime? DateModified { get; set; }
	}
}