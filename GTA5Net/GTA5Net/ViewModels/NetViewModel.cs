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

        private ObservableCollection<IPMod> _ipMods;
        public NetViewModel()
        {
            IpMods = new ObservableCollection<IPMod>()
            {
                new IPMod{Domain="conductor-prod.ros.rockstargames.com"},
                new IPMod{Domain="patches.rockstargames.com"},
                new IPMod {Domain= "prod.cloud.rockstargames.com"},
                new IPMod  {Domain="prod.cs.ros.rockstargames.com"},
                new IPMod{Domain="prod.p01sjc.pod.rockstargames.com"},
                new IPMod {Domain="prod.p02sjc.pod.rockstargames.com"},
                new IPMod{Domain="prod.ros.rockstargames.com"},
                new IPMod{Domain="prod.telemetry.ros.rockstargames.com"},
            };
            foreach (var ipMod in IpMods)
            {
                ipMod.IsPopUp = true;
                ipMod.IP = "            loading......";
                ipMod.TTL = string.Empty;
                ipMod.Progress = "0%";
                ipMod.ProgressValue = "0";
            }
        }

        public ObservableCollection<IPMod> IpMods
        {
            get { return _ipMods; }
            set { SetProperty(ref _ipMods, value); }
        }
    }
}
