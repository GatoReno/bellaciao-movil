using BellaCiaoMvvm.model;
using BellaCiaoMvvm.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace BellaCiaoMvvm.viewmodel
{
    public class SubscriptionDetailsViewModel : INotifyPropertyChanged
    {
        private Subscription subscription;

        public Subscription Subscription
        {
            get { return subscription; }
            set
            {
                subscription = value;
                Name = subscription.Name;
                IsActive = subscription.IsActive;
                OnPropertyChanged("Subscription");

            }
        }

        private string name;
        public  string Name
        {
            get { return name; }
            set { name = value;
                Subscription.Name = name;
                OnPropertyChanged("Name");
                OnPropertyChanged("Subscription");
            }
            
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value;
                Subscription.IsActive = isActive;
                OnPropertyChanged("IsAvtive");
                OnPropertyChanged("Subscription");
            }
        }

        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public SubscriptionDetailsViewModel()
        {
            UpdateCommand = new Command(Update, UpdateCanExecute);
            DeleteCommand = new Command(Delete);
        }

        private bool UpdateCanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Name);
        }
        private async void Update(object parameter)
        {
            bool result = await DataBaseHelperService.UpdateSubscription(Subscription);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "There was an error, please try again", "Ok");
        }

        private async void Delete(object parameter)
        {
            bool result = await DataBaseHelperService.DeleteSubscription(Subscription);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "There was an error, please try again", "Ok");
        }
        private void OnPropertyChanged(string propName)
        {
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
