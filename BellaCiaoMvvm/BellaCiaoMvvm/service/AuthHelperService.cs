using BellaCiaoMvvm.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BellaCiaoMvvm.service
{                 
        public class Auth
        {

        private static IAuth auth = DependencyService.Get<IAuth>();
            public static async Task<bool> RegisterUser(string name, string email, string pass)
            {
                try 
                {
                    return await auth.RegisterUser(name, email, pass);                        
                }
                catch (Exception x) 
                {
                    await App.Current.MainPage.DisplayAlert("",x.Message,"ok");
                    return false;
                }
            }
           public static async Task<bool> AuthenUser(string email, string pass)
           {
            try { bool hasLoggedIn = await auth.AuthenUser(email, pass);
                return hasLoggedIn;
                }
             catch (Exception x) 
            {
                await App.Current.MainPage.DisplayAlert("",x.Message,"ok");
                return false;
            }
           }

            public static bool ISAuthenticated()
            {
                return auth.ISAuthenticated();
            }

            public static string GetCurrentId() 
            {
                return auth.GetCurrentId();
            }
        }

}

