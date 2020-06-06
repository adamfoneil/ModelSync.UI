using AO.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Hs5.Models
{
	public class PaymentMethod : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }

		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		public bool IsActive { get; set; } = true;
	}

	/*
	public class PaymentMethodSeedData : SeedData<PaymentMethod, int>
	{
		public override string ExistsCriteria => "[dbo].[PaymentMethod] WHERE [ClinicId]=@clinicId AND [Name]=@name";

		public override IEnumerable<PaymentMethod> Records => new PaymentMethod[]
		{
			new PaymentMethod() { Name = "Cash" },
			new PaymentMethod() { Name = "Check" },
			new PaymentMethod() { Name = "Credit" },
			new PaymentMethod() { Name = "Debit" }
		};
	}
	*/
}