using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NSPRecv
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SettingsPage : ContentPage
	{
		public SettingsPage ()
		{
			InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Clear message
            Msg.Text = "";

            try
            {
                // Try to get Keys.  If there are no values, defaults are used
                Initials.Text = Preferences.Get("Inits", "ABC,XYZ").ToString();               
                Tos.Text = Preferences.Get("Tos", "tlaster@calstripsteel.com,pmorales@calstripsteel.com").ToString();
                From.Text = Preferences.Get("From", "nsp.scan@calstripsteel.com").ToString();                
                Ccopy.Text = Preferences.Get("Ccopy", "sclemons@calstripsteel.com").ToString();
                Host.Text = Preferences.Get("Host", "smtp.office365.com").ToString();
                TLS.IsToggled = Convert.ToBoolean(Preferences.Get("TLS", true));
                Port.Text = Preferences.Get("Port", 587).ToString();
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }

            // Pwd is secure storage
            try
            {
                var pwd = await SecureStorage.GetAsync("pwd");

                // On 1st load, it will e null, so create a default
                if(pwd != null)
                    Pwd.Text = pwd.ToString();
                else
                    Pwd.Text = string.Empty;
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            // Set Preferences from values entered
            try
            {
                Preferences.Set("Inits", Initials.Text.ToString().ToUpper());
                Preferences.Set("Tos", Tos.Text.ToString());
                Preferences.Set("From", From.Text.ToString());
                Preferences.Set("Ccopy", Ccopy.Text.ToString());
                Preferences.Set("Host", Host.Text.ToString());
                Preferences.Set("Port", Convert.ToInt32(Port.Text));
                Preferences.Set("TLS", Convert.ToBoolean(TLS.IsToggled));
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }

            // Pwd is secure storage
            try
            {
                await SecureStorage.SetAsync("pwd", Pwd.Text.ToString());
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }

            // Return to main page
            await Navigation.PopAsync();
        }

        private void Clear_Clicked(object sender, EventArgs a)
        {
            // Remove items from Xamarin Essentials
            try
            {
                Preferences.Clear();
                SecureStorage.Remove("pwd");
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }

            /*
             * Clear screen.  If you leave without SAVE
             * fields will be popluated by defaults.
             * Password will be empty
             */
            Initials.Text = "";
            Tos.Text = "";
            From.Text = "";
            Ccopy.Text = "";
            Host.Text = "";
            Pwd.Text = "";
            Port.Text = "";
            TLS.IsToggled = true;
        }

    }
}