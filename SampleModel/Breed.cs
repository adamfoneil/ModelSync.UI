using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	public class Breed : AppTable
	{
		[MaxLength(50)]
		[Key]
		public string Name { get; set; }
	}
}