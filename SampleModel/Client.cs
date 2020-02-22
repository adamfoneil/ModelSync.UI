using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	public class Client : BaseTable
	{
		[References(typeof(Clinic))]
		public int ClinicId { get; set; }

		[References(typeof(FeeSchedule))]
		public int FeeScheduleId { get; set; }

		/// <summary>
		/// Individual full name or volume client name
		/// </summary>
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }

		[MaxLength(50)]
		public string FirstName { get; set; }

		[MaxLength(50)]
		public string LastName { get; set; }

		[MaxLength(50)]
		public string Address1 { get; set; }

		[MaxLength(50)]
		public string Address2 { get; set; }

		[MaxLength(50)]
		public string City { get; set; }

		[MaxLength(2)]
		public string State { get; set; }

		[MaxLength(20)]
		public string PostalCode { get; set; }

		[MaxLength(50)]
		public string County { get; set; }

		[MaxLength(50)]
		public string HomePhone { get; set; }

		[MaxLength(50)]
		public string WorkPhone { get; set; }

		[MaxLength(50)]
		public string MobilePhone { get; set; }

		[MaxLength(50)]
		public string Email { get; set; }

		public string Notes { get; set; }

		public bool IsTaxExempt { get; set; }

		[Column(TypeName = "money")]
		public decimal Balance { get; set; }

		public Clinic Clinic { get; set; }
	}
}