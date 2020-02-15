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
            Tags.Text = "";
        }

        private void Send_Clicked(object sender, EventArgs e)
        {
            string msg = "";

            try
            {
                // Get Preferences
                var hst = Preferences.Get("Host", "");
                var frm = Preferences.Get("From", "");
                var tos = Preferences.Get("Tos", "");
                var Cc = Preferences.Get("Ccopy", "");
                var prt = Preferences.Get("Port", 0);
                var pwd = Preferences.Get("Pwd", "");
                var tls = Preferences.Get("TLS", true);

                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient(hst);
                mail.From = new MailAddress(frm);

                List<string> lstTos = tos.Split(',').ToList<string>();

                foreach (string s in lstTos)
                {
                    mail.To.Add(s);
                }

                mail.CC.Add(Cc);

                mail.Subject = "NSP Receiving Scans";
                mail.Body = DateTime.Today.ToString("d") + "\n\n" + Tags.Text;

                SmtpServer.Port = Convert.ToInt32(prt);
                SmtpServer.Host = hst;
                SmtpServer.Credentials = new System.Net.NetworkCredential(frm, pwd);
                SmtpServer.EnableSsl = tls;

                SmtpServer.Send(mail);

                msg = "SUCCESS";
            }
            catch (Exception ex)
            {
                Msg.Text = ex.Message.ToString();
                return;
            }

            Navigation.PushAsync(new ConfPage(msg, Tags.Text));

        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
