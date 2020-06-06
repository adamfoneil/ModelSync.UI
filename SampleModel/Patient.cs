using AO.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hs5.Models
{
    //[TrackChanges(IgnoreProperties = "DateModified,ModifiedBy")]
    public class Patient : BaseTable
	{
		[References(typeof(Clinic))]
		[Key]
		public int ClinicId { get; set; }
		
		[Key]
		public int Number { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[References(typeof(Sex))]
		public SexValue SexId { get; set; }

		[References(typeof(Species))]
		public int SpeciesId { get; set; }

		[References(typeof(Client))]
		public int? OwnerClientId { get; set; }

		[References(typeof(Client))]
		public int? VolumeClientId { get; set; }

		[References(typeof(Breed))]
		public int? PrimaryBreedId { get; set; }

		[References(typeof(Breed))]
		public int? SecondaryBreedId { get; set; }

		public DateTime? BirthDate { get; set; }

		/// <summary>
		/// Age at initial data entry
		/// </summary>
		[References(typeof(TimeUnit))]
		public int? AgeUnitId { get; set; }

		/// <summary>
		/// Age at initial data entry
		/// </summary>
		public int? AgeValue { get; set; }

		[References(typeof(SterilizationStatus))]
		public SterilizationStatusValue SterilizationStatusId { get; set; } = SterilizationStatusValue.NotSet;

		[Column(TypeName = "date")]
		public DateTime? SterilizationDate { get; set; }

		public string Notes { get; set; }

		public Clinic Clinic { get; set; }
	}
}