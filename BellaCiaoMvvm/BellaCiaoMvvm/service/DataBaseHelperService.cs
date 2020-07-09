using BellaCiaoMvvm.interfaces;
using BellaCiaoMvvm.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BellaCiaoMvvm.service
{
    
    public class DataBaseHelperService  
    {
        private static IFirestore firestore = DependencyService.Get<IFirestore>();
        public static bool InsertSubscription(Subscription subs)
        {
            return firestore.InsertSubscription(subs);
        }

        public static Task<bool> DeleteSubscription(Subscription subs)
        {
            return firestore.DeleteSubscription(subs);
        }

        public static Task<bool> UpdateSubscription(Subscription subs)
        {
            return firestore.UpdateSubscription(subs);
        }

        public static Task<IList<Subscription>> GetSubscription()
        {
            return firestore.GetSubscription();
        }
    }
}
