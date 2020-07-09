
using BellaCiaoMvvm.interfaces;
using BellaCiaoMvvm.service;
using BellaCiaoMvvm.viewmodel;
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
    public partial class MainPage : ContentPage
    {
        SubscriptionListViewModel vm;
        public MainPage()
        {
            InitializeComponent();
                vm = Resources["vm"] as SubscriptionListViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!Auth.ISAuthenticated()) {

                await Task.Delay(2000);
                await DisplayAlert("No Account data", "Please log in or regist", "ok"); 
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                vm.ReadSubscriptions();
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewSubscriptionPage());
        }
    }
}