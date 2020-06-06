using AO.Models;
using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	[UniqueConstraint(nameof(ClinicId), nameof(Number))]
	[Identity(nameof(Id))]
	public class Invoice
	{
		[Key]
		[References(typeof(Transaction), CascadeDelete = true)]
		public int TransactionId { get; set; }

		[References(typeof(Clinic))]
		public int ClinicId { get; set; }

		public int Number { get; set; }

		/*
		public override bool AllowSave(IDbConnection connection, SqlDb<int> db, out string message)
		{
			message = "Invoice records cannot be modified.";
			return false;
		}

		public override bool AllowDelete(IDbConnection connection, SqlDb<int> db, out string message)
		{
			message = "Invoice records cannot be deleted.";
			return false;
		}
		*/

		public int Id { get; set; }
	}
}