using GTA5Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTA5Net.ViewModels
{
    public class NetViewModel : EntityBase
    {
        private string _progress;
        private string _progressValue;
        private bool _isPopUp;
        private ObservableCollection<IPMod> _ipMods;
        public NetViewModel()
        {
            Progress = "0%";
            IpMods = new ObservableCollection<IPMod>();
        }
        public bool IsPopUp
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
            get
            {
                _progressValue = _progress.Split('%')[0];
                return _progressValue;
            }
            set { SetProperty(ref _progressValue, value); }
        }
        public ObservableCollection<IPMod> IpMods
        {
            get { return _ipMods; }
            set { SetProperty(ref _ipMods, value); }
        }
    }
}
