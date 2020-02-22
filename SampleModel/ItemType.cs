using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class ItemType : AppTable
	{
		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		[MaxLength(255)]
		public string Description { get; set; }
	}

	/*
	public class ItemTypeSeedData : SeedData<ItemType, int>
	{
		public override string ExistsCriteria => "[app].[ItemType] WHERE [Name]=@name";

		public override IEnumerable<ItemType> Records => new ItemType[]
		{
			new ItemType() { Name = "Service", Description = "Services are not taxable, have no inventory, and always a quantity of 1." },
			new ItemType() { Name = "Product", Description = "Products have inventory and may be taxable." },
			new ItemType() { Name = "Package", Description = "Packages combine one or more other items to add together for rapid data entry." },
			new ItemType() { Name = "Controlled Substance", Description = "Medications that require drug log tracking." }
		};
	}
	*/
}