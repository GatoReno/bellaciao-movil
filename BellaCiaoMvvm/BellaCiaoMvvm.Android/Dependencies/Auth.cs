using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BellaCiaoMvvm.interfaces;
using BellaCiaoMvvm.service;
using Firebase.Auth;
using Xamarin.Forms;

    [assembly: Xamarin.Forms.Dependency(typeof(BellaCiaoMvvm.Droid.Dependencies.Auth))]
namespace BellaCiaoMvvm.Droid.Dependencies
{


    public class Auth : IAuth
    {
        public Auth() { }
        public async Task<bool> AuthenUser(string email, string pass)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, pass);

                return true;

            }
            catch (FirebaseAuthWeakPasswordException x)
            {
                throw new Exception(x.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException x)
            {
                throw new Exception(x.Message);
            }
            catch (FirebaseAuthUserCollisionException x)
            {
                throw new Exception(x.Message);
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }

        }

        public string GetCurrentId()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public bool ISAuthenticated()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null; 
        }

        public async Task<bool> RegisterUser(string name, string email, string pass)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, pass);
                var profileObjects = new Firebase.Auth.UserProfileChangeRequest.Builder();
                var build = profileObjects.Build();

                var user = Firebase.Auth.FirebaseAuth.Instance.CurrentUser;
                await user.UpdateProfileAsync(build);

                return true;

            }
            catch(FirebaseAuthWeakPasswordException x)
            {
                throw new Exception(x.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException x)
            {
                throw new Exception(x.Message);
            }
            catch (FirebaseAuthUserCollisionException x)
            {
                throw new Exception(x.Message);
            }
            catch (Exception x)
            {
                throw new Exception(x.Message);
            }

        }
    }
}