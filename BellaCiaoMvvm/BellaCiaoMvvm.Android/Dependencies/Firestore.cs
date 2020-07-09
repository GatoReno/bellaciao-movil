using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Provider;
using BellaCiaoMvvm.interfaces;
using BellaCiaoMvvm.model;
using Java.Util;
using Android.Gms.Tasks;
using Firebase.Firestore;

[assembly: Xamarin.Forms.Dependency(typeof(BellaCiaoMvvm.Droid.Dependencies.Firestore))]
namespace BellaCiaoMvvm.Droid.Dependencies
{
    public class Firestore : Java.Lang.Object,IFirestore , IOnCompleteListener
    {
        List<Subscription> subscriptions;

        bool hasReadSubs = false;
        public Firestore() {
            subscriptions = new List<Subscription>(); 
        }
        private static Date DateTimeToNatDate(DateTime date)
        {
            long dt = (long)date.ToUniversalTime().Subtract(new DateTime
                (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            return new Date(dt);
        }

        private static DateTime DateToNatDateTime(Date date)
        {                                
            DateTime reference = System.TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return reference.AddMilliseconds(date.Time);
        }


     

        public async Task<IList<Subscription>> GetSubscription()
        {
            hasReadSubs = false;
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
            var query = collection.WhereEqualTo("author", Firebase.Auth.FirebaseAuth.Instance.Uid);
            query.Get().AddOnCompleteListener(this);

            for (int i = 0; i < 20; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (hasReadSubs) break;
            }
            return subscriptions;
        }
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful) {
                var docs = (QuerySnapshot)task.Result;
                //List<Subscription> subscriptions = new List<Subscription>();
                subscriptions.Clear();
                foreach (var doc in docs.Documents)
                {
                    Subscription sb = new Subscription { 
                        Id = doc.Id,
                        IsActive = (bool)doc.Get("isActive"),
                        Name = doc.Get("name").ToString(),
                        UserId = doc.Get("author").ToString(),
                        SubscribedDate = DateToNatDateTime(doc.Get("subscribedDate") as Date),

                    };

                    subscriptions.Add(sb);
                }

                
            }
            else
            {
                subscriptions.Clear();
            }

            hasReadSubs = true;
        }

        public void OnDispose()
        {
            throw new NotImplementedException();
        }
        public bool InsertSubscription(Subscription subs)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                var subscriptionDoc = new Dictionary<string, Java.Lang.Object> {
                { "author" , Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                { "name" , subs.Name },
                { "isActive" , subs.IsActive },
                {"subscribedDate", DateTimeToNatDate(subs.SubscribedDate) }
            };

                collection.Add(subscriptionDoc);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("!Error ----------------"+ex.ToString());
                return false;
            }
        }

        public async Task<bool> DeleteSubscription(Subscription subs)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                collection.Document(subs.Id).Delete();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<bool> UpdateSubscription(Subscription subs)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                 collection.Document(subs.Id).Update("name", subs.Name, "isActive", subs.IsActive);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}   