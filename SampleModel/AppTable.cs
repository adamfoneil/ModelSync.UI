using AO.Models;

namespace Hs5.Models
{
	[Schema("app")]
	[Identity(nameof(Id))]
	public abstract class AppTable
	{
		public int Id { get; set; }
	}
}