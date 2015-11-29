using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA5Net.Model
{
    public class IPMod : EntityBase
    {
        private string _ttl;
        private string _ip;
        public string TTL
        {
            get { return _ttl; }
            set { SetProperty(ref _ttl, value); }
        }
        public string IP
        {
            get { return _ip; }
            set { SetProperty(ref _ip, value); }
        }
    }
}
