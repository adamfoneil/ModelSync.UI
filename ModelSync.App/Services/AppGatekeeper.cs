using CloudLicensing.Desktop;
using CloudLicensing.Server.Models;
using ModelSync.App.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public override string PurchaseUrl => "http://www.aosoftware.net/modelSync.html";

        public override async Task<decimal> GetPriceAsync()
        {
            var response = await HttpClient.GetAsync("http://www.aosoftware.net/products.json");
            
            string json = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var products = JsonConvert.DeserializeObject<List<Product>>(json);
                var productDictionary = products.ToDictionary(item => item.ItemNumber);
                return productDictionary[ProductId].Price;
            }

            throw new Exception($"Couldn't get price: {json}");
        }

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
