using BellaCiaoMvvm.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BellaCiaoMvvm.viewmodel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        //props

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("CanRegister");
            }

        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value;
                OnPropertyChanged("Email");
                OnPropertyChanged("CanRegister");
            }

        }
        private string pass;
        public string Pass
        {
            get { return pass; }
            set { pass = value;
                OnPropertyChanged("Pass");
                OnPropertyChanged("CanLogin");
                OnPropertyChanged("CanRegister");
            }

        }
        private string confirmpass;
        public string ConfirmPass
        {
            get { return confirmpass; }
            set { confirmpass = value;
                OnPropertyChanged("ConfirmPass");
                OnPropertyChanged("CanRegister");
            }

        }

        public bool CanLogin
        {
            get { return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Pass); }

        }

        public bool CanRegister
        {
            get { return !string.IsNullOrEmpty(Email) &&
                    !string.IsNullOrEmpty(Pass) &&
                    !string.IsNullOrEmpty(Name); }

        }


        //Commands

        public ICommand LoginCommand{ get; set; }
        public ICommand RegistCommand { get; set; }



        public event PropertyChangedEventHandler PropertyChanged;
        public LoginViewModel()
        {
            LoginCommand = new Command(Login, LoginCanExe);
            RegistCommand = new Command(Register, RegistCanExe);
        }

        private async void Login(object param)
        {
            bool result = await Auth.AuthenUser(Email, Pass);
             
            if (result)
            {
                await App.Current.MainPage.Navigation.PopAsync();
            }
        }

        private async void Register(object param)
        {
            if(ConfirmPass != Pass)
            {
               await App.Current.MainPage.DisplayAlert("Error","Passwords do not match!","ok");
            }
            else { 
                bool result = await Auth.RegisterUser(Name,Email, Pass);
                if (result)
                {
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private bool RegistCanExe(object param)
        {


            return CanRegister;
        }
        private bool LoginCanExe(object param) {
            
            
            return CanLogin;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
