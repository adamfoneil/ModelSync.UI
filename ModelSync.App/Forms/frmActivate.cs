using CloudLicensing.Desktop;
using ModelSync.App.Services;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace ModelSync.App.Forms
{
	public partial class frmActivate : Form
	{	
		public LicenseInfo LicenseInfo { get; set; }
		public AppGatekeeper Gatekeeper { get; set; }

		public frmActivate()
		{
			InitializeComponent();
		}

		private async void frmMain_Load(object sender, EventArgs e)
		{
			try
			{
				webUrlLinkLabel1.Url = Gatekeeper.PurchaseUrl;
				btnContinue.Enabled = LicenseInfo.AllowContinue;								
				label2.Text = $"This is a fully-functional {Gatekeeper.TrialDays}-day trial with {LicenseInfo.TrialPeriodRemaining:n0} days left.";
				var price = await Gatekeeper.GetPriceAsync();
				label5.Text = $"A license key is {price:c2}";
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private async void btnRegister_Click(object sender, EventArgs e)
		{
			try
			{
				var task = Gatekeeper.ActivateAsync(tbEmail.Text, tbLicenseKey.Text);
				btnRegister.Enabled = false;
				progressBar1.Visible = true;

				var result = await task;
				if (result.IsSuccessful)
				{
					MessageBox.Show("Successfully registered. Thank you.");
					DialogResult = DialogResult.OK;
				}
				else
				{
					MessageBox.Show("Key and email combination was not recognized.");
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
			finally
			{
				progressBar1.Visible = false;
				btnRegister.Enabled = true;
			}
		}

		private void btnPurchase_Click(object sender, EventArgs e)
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo(Gatekeeper.PurchaseUrl);
				Process.Start(psi);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}

		private void btnContinue_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo(Gatekeeper.PurchaseUrl);
				Process.Start(psi);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}
	}
}