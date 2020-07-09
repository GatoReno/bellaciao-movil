using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BellaCiaoMvvm.interfaces
{
      public interface IAuth
        {
            Task<bool> RegisterUser(string name, string email, string pass);
            Task<bool> AuthenUser(string email, string pass);
            bool ISAuthenticated();
            string GetCurrentId();
        }
}
