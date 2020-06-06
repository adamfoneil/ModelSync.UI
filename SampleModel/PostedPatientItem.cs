using AO.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hs5.Models
{
	public class PostedPatientItem : BaseTable
	{
		[References(typeof(Transaction))]
		public int TransactionId { get; set; }

		[References(typeof(PatientAppointment))]
		public int PatientAppointmentId { get; set; }

		[References(typeof(Item))]
		public int ItemId { get; set; }

		public string ItemName { get; set; }

		[References(typeof(Client))]
		public int ClientId { get; set; }

		public string ClientName { get; set; }

		[Column(TypeName = "money")]
		public decimal UnitPrice { get; set; }

		[Column(TypeName = "decimal(6,2)")]
		public decimal Quantity { get; set; }

		/// <summary>
		/// Clinic.SalesTaxRate * UnitPrice * Quantity
		/// </summary>
		[Column(TypeName = "money")]
		public decimal SalesTax { get; set; }

		[Column(TypeName = "money")]
		public decimal ExtPrice { get; set; }

		/*
		public override bool AllowSave(IDbConnection connection, SqlDb<int> db, out string message)
		{
			message = "PostedPatientItems cannot be modified";
			return false;
		}

		public override bool AllowDelete(IDbConnection connection, SqlDb<int> db, out string message)
		{
			message = "PostedPatientItems cannot be deleted except by invoice rollback";
			return false;
		}
		*/
	}
}