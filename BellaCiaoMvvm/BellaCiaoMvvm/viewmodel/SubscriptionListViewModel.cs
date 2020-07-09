using BellaCiaoMvvm.model;
using BellaCiaoMvvm.service;
using BellaCiaoMvvm.view;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace BellaCiaoMvvm.viewmodel
{
    public class SubscriptionListViewModel : INotifyPropertyChanged
    {
        private Subscription selectedSubscription;
        public Subscription SelectedSubscription
        {
            get { 
                return selectedSubscription; 
            }
            set {
                selectedSubscription = value;
                OnPropertyChanged("SelectedSubscription");
                if (selectedSubscription != null)
                {
                    App.Current.MainPage.Navigation.PushAsync(new SubscriptionDetailsPage(selectedSubscription));
                }
            }
        }


        public ObservableCollection<Subscription> Subscriptions { get; set; }
        public SubscriptionListViewModel() {
            Subscriptions = new ObservableCollection<Subscription>();
            SelectedSubscription = selectedSubscription;
            //ReadSubscriptions();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async void ReadSubscriptions() {
            var subs = await DataBaseHelperService.GetSubscription();

            Subscriptions.Clear();
            foreach (var s in subs)           
            {
                Subscriptions.Add(s);
            }

        } 

        private void OnPropertyChanged(string propName) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
