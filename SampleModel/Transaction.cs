using AO.DbSchema.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hs5.Models
{
	//[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
	//[TrackDeletions]
	public class Transaction : BaseTable
	{
		[References(typeof(Client))]
		public int ClientId { get; set; }

		[References(typeof(TransactionType))]
		public int TypeId { get; set; }

		[References(typeof(PaymentMethod))]
		public int? PaymentMethodId { get; set; }

		[Column(TypeName = "date")]
		public DateTime PostDate { get; set; }

		[Column(TypeName = "money")]
		public decimal Amount { get; set; }

		[MaxLength(255)]
		public string Description { get; set; }

		/// <summary>
		/// Set by the refund type, then by TransactionType.ClientBalanceMultiplier
		/// </summary>
		public int? BalanceMultiplier { get; set; }
	}
}