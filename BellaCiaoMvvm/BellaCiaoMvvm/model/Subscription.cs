using System;
using System.Collections.Generic;
using System.Text;

namespace BellaCiaoMvvm.model
{
    public class Subscription
    {
        private bool v1;
        private string v2;
        private object p;

        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime SubscribedDate { get; set; }
        public bool IsActive { get; set; }
        public Subscription()
        {

        }

        public Subscription(bool v1, string v2, object p)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.p = p;
        }
    }
}
