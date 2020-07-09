using BellaCiaoMvvm.model;
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
    public partial class SubscriptionDetailsPage : ContentPage
    {
        SubscriptionDetailsViewModel vm;

        public SubscriptionDetailsPage()
        {
            InitializeComponent();

            vm = Resources["vm"] as SubscriptionDetailsViewModel;
        }

        public SubscriptionDetailsPage(Subscription selectedSubs)
        {
            InitializeComponent();
            vm = Resources["vm"] as SubscriptionDetailsViewModel;
            vm.Subscription = selectedSubs;

        }
       


    }
}