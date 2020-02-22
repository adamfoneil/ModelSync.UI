  
  

namespace Hs5.Models
{
	public enum PermissionValue : long
	{
				ManageClinic = 1,
		ManageUsers = 2,
		ManageCapacity = 4,
		ManageItems = 8,
		ManageVolumeClients = 16,
		ManageAppointments = 32,
		PostInvoices = 64,
		RollbackInvoices = 128,
		EditDrugLog = 256,
		VolumeClientAppointments = 512,
	}
}