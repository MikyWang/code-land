using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GTA5Net.Model
{
    public class IPMod : EntityBase
    {
        private string _ttl;
        private string _ip;
        private string _domain;
        private string _progress;
        private string _progressValue;
        private Visibility  _isPopUp;
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
        public string Domain
        {
            get { return _domain; }
            set { SetProperty(ref _domain, value); }
        }
        public Visibility IsPopUp
        {
            get { return _isPopUp; }
            set { SetProperty(ref _isPopUp, value); }
        }
        public string Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }
        public string ProgressValue
        {
            get { return _progressValue; }
            set { SetProperty(ref _progressValue, value); }
        }
    }
}
