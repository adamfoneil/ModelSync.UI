using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Hs5.Models
{
	public class Clinic : BaseTable
	{
		[Key]
		[MaxLength(100)]
		public string Name { get; set; }

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
		public string Phone1 { get; set; }

		[MaxLength(50)]
		public string Phone2 { get; set; }

		[MaxLength(50)]
		public string Email1 { get; set; }

		[MaxLength(50)]
		public string Email2 { get; set; }

		[MaxLength(50)]
		public string Url { get; set; }

		[MaxLength(500)]
		public string DirectionsUrl { get; set; }

		public int NextPatientNumber { get; set; } = 1000;

		public int NextInvoiceNumber { get; set; } = 1000;
		
		public byte[] Logo { get; set; }

		[References(typeof(FeeSchedule))]
		public int? DefaultFeeScheduleId { get; set; }

		[Column(TypeName = "money")]
		public decimal SalesTaxRate { get; set; }

		public DateTime RenewalDate { get; set; } = DateTime.Today.AddDays(30);

		[Column(TypeName = "money")]
		public decimal Price { get; set; }
	}
}