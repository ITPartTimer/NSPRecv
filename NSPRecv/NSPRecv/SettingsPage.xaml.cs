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

            try
            {
                // Try to get Keys.  If there are no values, defaults are used
                if (Preferences.ContainsKey("Inits"))
                {
                    var v = Preferences.Get("Inits", "ABC,XYZ");
                    Initials.Text = v.ToString();
                }

                if (Preferences.ContainsKey("Tos"))
                {
                    var v = Preferences.Get("Tos", "cscottierun@gmail.com,scott.clemons317@outlook.com");
                    Tos.Text = v.ToString();
                }

                if (Preferences.ContainsKey("From"))
                {
                    var v = Preferences.Get("From", "nsp.scan@calstripsteel.com");
                    From.Text = v.ToString();
                }

                if (Preferences.ContainsKey("Ccopy"))
                {
                    var v = Preferences.Get("Ccopy", "sclemons@calstripsteel.com");
                    Ccopy.Text = v.ToString();
                }

                if (Preferences.ContainsKey("Host"))
                {
                    var v = Preferences.Get("Host", "smtp.office365.com");
                    Host.Text = v.ToString();
                }

                //if (Preferences.ContainsKey("Pwd"))
                //{
                //    var v = Preferences.Get("Pwd", "EDL8ywDDePiT");
                //    Pwd.Text = v.ToString();
                //}

                if (Preferences.ContainsKey("TLS"))
                {
                    var v = Preferences.Get("TLS", true);
                    TLS.IsTabStop = Convert.ToBoolean(v);
                }

                if (Preferences.ContainsKey("Port"))
                {
                    var v = Preferences.Get("Port", 587);
                    Port.Text = v.ToString();
                }

                try
                {
                    var pwd = await SecureStorage.GetAsync("pwd");
                    Pwd.Text = pwd.ToString();
                }
                catch (Exception ex)
                {
                    Msg.Text = ex.Message.ToString();
                }
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            // Set Preferences from values entered
            // Trap for empty fields
            try
            {
                Preferences.Set("Inits", Initials.Text.ToString().ToUpper());
                Preferences.Set("Tos", Tos.Text.ToString());
                Preferences.Set("From", From.Text.ToString());
                Preferences.Set("Ccopy", Ccopy.Text.ToString());
                Preferences.Set("Host", Host.Text.ToString());
                //Preferences.Set("Pwd", Pwd.Text.ToString());
                Preferences.Set("Port", Convert.ToInt32(Port.Text));
                Preferences.Set("TLS", Convert.ToBoolean(TLS.IsToggled));
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }

            try
            {
                await SecureStorage.SetAsync("pwd", Pwd.Text.ToString());
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }

            // Return to main page
            await Navigation.PushAsync(new MainPage());
        }

        private void Clear_Clicked(object sender, EventArgs a)
        {
            Preferences.Clear();

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