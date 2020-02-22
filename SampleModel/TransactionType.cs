using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class TransactionType : AppTable
	{
		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		/// <summary>
		/// Impact on the cash drawer (is money coming in or out?)
		/// </summary>
		public int BankDepositMulitplier { get; set; }

		/// <summary>
		/// Impact on the client's balance (is balance increased or decreased?)
		/// </summary>
		public int ClientBalanceMulitiplier { get; set; }

		/// <summary>
		/// Used with the Refund type to change the balance multiplier on the transaction dynamically to prevent a credit
		/// </summary>
		public bool ClientAutoBalance { get; set; }

		/// <summary>
		/// Can this type be selected manually?
		/// </summary>
		public bool AllowUserSelection { get; set; }

		[MaxLength(255)]
		public string Description { get; set; }
	}

	/*
	public class TransactionTypeSeedData : SeedData<TransactionType, int>
	{
		public override string ExistsCriteria => "[app].[TransactionType] WHERE [Name]=@name";

		public override IEnumerable<TransactionType> Records => new TransactionType[]
		{
			new TransactionType() { Name = "Invoice", AllowUserSelection = false, ClientBalanceMulitiplier = 1, BankDepositMulitplier = 0, Description = "Record of money charged to a client's account." },
			new TransactionType() { Name = "Payment", AllowUserSelection = true, ClientBalanceMulitiplier = -1, BankDepositMulitplier = 1, Description = "Money received from a client" },
			new TransactionType() { Name = "Refund", AllowUserSelection = false, ClientAutoBalance = true, BankDepositMulitplier = -1, Description = "Money returned to a client" },
			new TransactionType() { Name = "Deposit", AllowUserSelection = true, ClientBalanceMulitiplier = -1, BankDepositMulitplier = 1, Description = "Money received for a future appointment." },
			new TransactionType() { Name = "Credit Memo", AllowUserSelection = true, ClientBalanceMulitiplier = -1, BankDepositMulitplier = 0, Description = "Record of a reduction of a client's balance." },
			new TransactionType() { Name = "Debit Memo", AllowUserSelection = true, ClientBalanceMulitiplier = 1, BankDepositMulitplier = 0, Description = "Record of money charged to a client's balance." }
		};
	}
	*/
}