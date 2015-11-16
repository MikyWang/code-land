using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderVip.Model
{
    public class VipUser : EntityBase
    {
        private string _account;
        private string _password;
        private string _copy = "复制";
        public string Copy
        {
            get { return _copy; }
        }
        public String Account
        {
            get { return _account; }
            set { SetProperty(ref _account, value); }
        }
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
    }
}
