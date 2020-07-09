using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BellaCiaoMvvm.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            RegistStack.IsVisible = true;
            LoginStack.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped_Log(object sender, EventArgs e)
        {
            RegistStack.IsVisible = false;
            LoginStack.IsVisible = true;
        }
    }
}