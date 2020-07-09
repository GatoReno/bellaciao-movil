using BellaCiaoMvvm.model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BellaCiaoMvvm.interfaces
{
    public interface IFirestore
    {
        bool InsertSubscription(Subscription subs);
        Task<bool> DeleteSubscription(Subscription subs);
        Task<bool> UpdateSubscription(Subscription subs);

        Task<IList<Subscription>> GetSubscription();

    }
}
