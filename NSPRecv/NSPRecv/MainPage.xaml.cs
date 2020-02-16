using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NSPRecv
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void Clear_Clicked(object sender, EventArgs e)
        {
            // Blank values of Tags and Veh ID
            Tags.Text = "";
            Ven.Text = "";

            // Clear Qty
            if (pckQty.SelectedIndex != -1)
                pckQty.SelectedIndex = -1;
        }

        private async void Send_Clicked(object sender, EventArgs e)
        {
            string msg = "";

            try
            {
                if (pckInits.SelectedIndex == -1)
                    throw new Exception("No Initials Selected");

                if (pckQty.SelectedIndex == -1)
                    throw new Exception("No Qty Selected");

                if(string.IsNullOrEmpty(Ven.Text.ToString()))
                    throw new Exception("Vehicle ID Required");

                if (string.IsNullOrEmpty(Tags.Text.ToString()))
                    throw new Exception("At Least 1 Tag Required");

                // Get Preferences
                var hst = Preferences.Get("Host", "");
                var frm = Preferences.Get("From", "");
                var tos = Preferences.Get("Tos", "");
                var Cc = Preferences.Get("Ccopy", "");
                var prt = Preferences.Get("Port", 0);
                var tls = Preferences.Get("TLS", true);

                // Secure Storage
                var pwd = await SecureStorage.GetAsync("pwd");

                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient(hst);
                mail.From = new MailAddress(frm);

                List<string> lstTos = tos.Split(',').ToList<string>();

                foreach (string s in lstTos)
                {
                    mail.To.Add(s);
                }

                mail.CC.Add(Cc);

                mail.Subject = "NSP - " + pckInits.Items[pckInits.SelectedIndex] + " - " + pckQty.Items[pckQty.SelectedIndex] + " - " + Ven.Text.ToString().ToUpper();
                mail.Body = DateTime.Today.ToString("d") + "\n" + Tags.Text;

                SmtpServer.Port = Convert.ToInt32(prt);
                SmtpServer.Host = hst;
                SmtpServer.Credentials = new System.Net.NetworkCredential(frm, pwd.ToString());
                SmtpServer.EnableSsl = tls;

                SmtpServer.Send(mail);

                msg = "SUCCESS\n" + mail.Subject + "\n";
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
                return;
            }

            await Navigation.PushAsync(new ConfPage(msg, Tags.Text));
        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            try
            {
                // Clear message
                Msg.Text = "";

                // Get Inits preferences or defauls, if empty
                var v = Preferences.Get("Inits", "ABC,XYZ");


                if (pckInits.Items.Count == 0)
                {
                    // Separate Inits and add to Picker items
                    List<string> lstInits = v.Split(',').ToList<string>();

                    foreach (string s in lstInits)
                    {
                        pckInits.Items.Add(s);
                    }
                }
                
                // Blank values of Tags and Veh ID
                Tags.Text = "";
                Ven.Text = "";

                // Clear Qty
                if (pckQty.SelectedIndex != -1)
                    pckQty.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
            }
        }
    }
}

