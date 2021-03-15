using CloudLicensing.Desktop;
using ModelSync.App.Forms;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModelSync.App.Services
{
    public class AppGatekeeper : Gatekeeper
    {
        public override string CompanyName => "Adam O'Neil";

        protected override string KeyValidationEndpoint => "https://aosoftwarelicensing.azurewebsites.net/api/ValidateKey";

        public override string ProductId => "model-sync";

        public override int TrialDays => 30;

        protected override string ValidKeyResponse => "valid";

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
