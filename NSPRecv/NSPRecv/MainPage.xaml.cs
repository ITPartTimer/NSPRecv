using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
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
            bool sent = true;
            string msg = "";

            try
            {
                MailMessage mail = new MailMessage();

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("nsp.recv@gmail.com");
                mail.To.Add("pmorales@calstripsteel.com");
                mail.To.Add("tlaster@calstripsteel.com");
                //mail.To.Add("scott.clemons317@outlook.com");
                mail.CC.Add("sclemons@calstripsteel.com");
                mail.Subject = "NSP Receiving Scans";
                mail.Body = DateTime.Today.ToString("d") + "\n\n" + Tags.Text;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.Credentials = new System.Net.NetworkCredential("nsp.recv@gmail.com","A8dg2h8q");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                msg = "SUCCESS";
            }
            catch (Exception ex)
            {
                sent = false;
                msg = "FAILED" + "\n" + ex.InnerException.ToString();
            }

            Navigation.PushAsync(new ConfPage(msg, Tags.Text));

        }

        private async void Settings_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
