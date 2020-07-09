using BellaCiaoMvvm.model;
using BellaCiaoMvvm.service;
using System;
using System.ComponentModel;
using System.Windows.Input;

using Xamarin.Forms;
using System.Linq;

namespace BellaCiaoMvvm.viewmodel
{
    public class NewSubscriptionViewModel : INotifyPropertyChanged
    {
        //init props
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged("Name");
            }
        }
        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value;
                OnPropertyChanged("IsActive");
            }
        }
        //end props

        public ICommand SaveSubsCommand { get; set; }
        public NewSubscriptionViewModel()
        {
            SaveSubsCommand = new Command(SaveSubs,SaveSubsCanExe);
        }

        private bool SaveSubsCanExe(object arg)
        {
            return !string.IsNullOrEmpty(Name);
        }
      

        private void SaveSubs(object obj)
        {
            Subscription x = new Subscription();

            bool req = DataBaseHelperService.InsertSubscription(new Subscription{
                    IsActive = IsActive,
                    Name = Name,
                    UserId = Auth.GetCurrentId(),
                    SubscribedDate = DateTime.Now,
                    Id = Auth.GetCurrentId()
            });

            if (req)
            {
                App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error","Intente en otro momento","ok");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        private void OnPropertyChanged(string v)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(v));
        }
    } 

    
    
}
