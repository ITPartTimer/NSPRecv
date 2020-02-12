using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NSPRecv
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ConfPage : ContentPage
	{
		public ConfPage (string msg, string tags)
		{
			InitializeComponent ();

            Conf.Text = msg;          

            TagsSent.Text = tags;           
		}
	}
}