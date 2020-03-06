using AO.DbSchema.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hs5.Models
{
	public class PendingPatientItem : BaseTable
	{
		[References(typeof(PatientAppointment))]
		public int PatientAppointmentId { get; set; }

		[References(typeof(Item))]
		public int ItemId { get; set; }

		[References(typeof(Client))]
		public int ClientId { get; set; }

		[Column(TypeName = "money")]
		public decimal UnitPrice { get; set; }

		[Column(TypeName = "decimal(6,2)")]
		public decimal Quantity { get; set; }

		/// <summary>
		/// Clinic.SalesTaxRate * UnitPrice * Quantity
		/// </summary>
		[Column(TypeName = "money")]
		public decimal SalesTax { get; set; }

		[Calculated("[UnitPrice]*[Quantity]+[SalesTax]", true)]
		public decimal ExtPrice { get; set; }
	}
}