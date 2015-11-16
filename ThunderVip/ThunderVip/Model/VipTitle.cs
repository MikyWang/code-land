using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderVip.Model
{
    public class VipTitle : EntityBase
    {
        private string _title;
        private string _url;
        private string _afterTime;
        public string AfterTime
        {
            get { return _afterTime; }
            set { SetProperty(ref _afterTime, value); }
        }
        public string Url
        {
            get { return _url; }
            set { SetProperty(ref _url, value); }
        }
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
