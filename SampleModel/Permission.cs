using AO.Models;
using System.ComponentModel.DataAnnotations;

namespace Hs5.Models
{
	[UniqueConstraint(nameof(Value))]
	public class Permission : AppTable
	{
		[MaxLength(50)]
		[Key]
		public string Name { get; set; }

		public string Description { get; set; }
		
		public long Value { get; set; }

		public const string ManageClinic = "Manage Clinic";
		public const string ManageUsers = "Manage Users";
		public const string ManageCapacity = "Manage Capacity";
		public const string ManageItems = "Manage Items";
		public const string ManageVolumeClients = "Manage Volume Clients";
		public const string ManageAppointments = "Manage Appointments";
		public const string PostInvoices = "Post Invoices";
		public const string RollbackInvoices = "Rollback Invoices";
		public const string EditDrugLog = "Edit Drug Log";
		public const string VolumeClientAppointments = "Volume Client Appointments";

		public static bool HasPermission(long permissions, PermissionValue value)
		{
			return ((permissions & (long)value) == (long)value);
		}

		public static Permission[] GetSeedValues()
		{
			return new Permission[]
			{
				new Permission() { Name = ManageClinic, Value = 1, Description = "Administrator access to all clinic data and settings." },
				new Permission() { Name = ManageUsers, Value = 2, Description = "Create, activate, and deactivate users. Set other users' permissions." },
				new Permission() { Name = ManageCapacity, Value = 4, Description = "Change calendar settings and daily patient limits." },
				new Permission() { Name = ManageItems, Value = 8, Description = "Create items and set pricing." },
				new Permission() { Name = ManageVolumeClients, Value = 16, Description = "Create and modify volume clients." },
				new Permission() { Name = ManageAppointments, Value = 32, Description = "Create, edit, check in, take owner payments, and check out appointments. Can modify patient and client information." },
				new Permission() { Name = PostInvoices, Value = 64, Description = "Post volume client invoices, take volume client payments." },
				new Permission() { Name = RollbackInvoices, Value = 128, Description = "Reverse (rollback) volume client invoices." },
				new Permission() { Name = EditDrugLog, Value = 256, Description = "Edit drug dosages on patients in the drug log, receive controlled substances." },
				new Permission() { Name = VolumeClientAppointments, Value = 512, Description = "May edit appointments for a specific volume client." }
			};
		}
	}

	/*
	public class PermissionSeedData : SeedData<Permission, int>
	{
		public override string ExistsCriteria => "[app].[Permission] WHERE [Name]=@name";

		public override IEnumerable<Permission> Records => Permission.GetSeedValues();
	}
	*/
}