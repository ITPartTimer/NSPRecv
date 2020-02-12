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

            // Set values, if Preference Keys exist
            if (Preferences.ContainsKey("Inits"))
                Initials.Text = Preferences.Get("Inits", "");

            if (Preferences.ContainsKey("Tos"))
                Tos.Text = Preferences.Get("Tos", "cscottierun@gmail.com;scott.clemons317@outlook.com");

            if (Preferences.ContainsKey("From"))
                From.Text = Preferences.Get("From", "sw.scanck65@gmail.com");

            if (Preferences.ContainsKey("Host"))
                Host.Text = Preferences.Get("Host", "smtp.gmail.com");

            if (Preferences.ContainsKey("Port"))
                Port.Text = Preferences.Get("Port", "587");

            if (Preferences.ContainsKey("Port"))
                TLS.IsToggled = Preferences.Get("TLS", true);
        }

        private void Save_Clicked(object sender, EventArgs e)
        {

            if (Preferences.ContainsKey("Inits"))
                Initials.Text = Preferences.Get("Inits", "CSC;PM");

            // Set Preferences from values entered
            Preferences.Set("Initials", Initials.Text.ToString());
            Preferences.Set("Tos", Tos.Text.ToString());
            Preferences.Set("From", From.Text.ToString());
            Preferences.Set("Host", Host.Text.ToString());
            Preferences.Set("Pwd", Pwd.Text.ToString());
            Preferences.Set("Port", Convert.ToInt32(Port.Text));
            Preferences.Set("TLS", Convert.ToBoolean(TLS));

        }
    }
}