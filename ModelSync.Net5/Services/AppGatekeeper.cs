using AOLicensing.Desktop;
using AOLicensing.Desktop.Models;
using ModelSync.App.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelSync.App.Services
{
    public class AppGatekeeper : Gatekeeper
    {
        public AppGatekeeper() : base("https://aolicensing.azurewebsites.net")
        {
        }

        public override string CompanyName => "Adam O'Neil";
        
        public override string ProductId => "ModelSync";

        public override int TrialDays => 30;

        public override string PurchaseUrl => "https://aosoftware.net/modelsync/";

        public override async Task<decimal> GetPriceAsync() => await Task.FromResult(50m);        

        protected override bool ShowActivationDialog(LicenseInfo licenseInfo)
        {
            frmActivate dlg = new frmActivate()
            {
                LicenseInfo = licenseInfo,
                Gatekeeper = this
            };

            return (dlg.ShowDialog() == DialogResult.OK);
        }
    }
}
