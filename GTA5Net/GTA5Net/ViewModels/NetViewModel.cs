using Commands.Command;
using GTA5Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace GTA5Net.ViewModels
{
    public class NetViewModel : EntityBase
    {
        public Command Convert { get; private set; }
        private ObservableCollection<IPMod> _ipMods;
        private const string _hostsHeader = "# RockStar Start\n";
        private const string _hostsFooter = "# RockStar End";
        private string _hostsBody = string.Empty;
        private Visibility _hostsEnable;
        public Visibility HostsEnable
        {
            get { return _hostsEnable; }
            set { SetProperty(ref _hostsEnable, value); }
        }
        public string Hosts
        {
            get { return _hostsHeader + _hostsBody + _hostsFooter; }
            set { SetProperty(ref _hostsBody, value); }
        }
        public ObservableCollection<IPMod> IpMods
        {
            get { return _ipMods; }
            set { SetProperty(ref _ipMods, value); }
        }
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
                ipMod.IsPopUp = Visibility.Visible;
                ipMod.IP = "            loading......";
                ipMod.TTL = string.Empty;
                ipMod.Progress = "0%";
                ipMod.ProgressValue = "0";
            }
            Convert = new Command(convert, () =>
              {
                  return IpMods.All(ipMod =>
                  {
                      return ipMod.IsPopUp == Visibility.Collapsed;
                  });
              });
            HostsEnable = Visibility.Collapsed;
        }
        private void convert()
        {
            var body = string.Empty;
            foreach (var ipMod in IpMods)
            {
                body += IPSource.IPHelper.GetMatch(IPSource.IPHelper.Filter, ipMod.IP, value =>
                {
                    return ipMod.IP.Replace(value, "");
                }) + "    " + ipMod.Domain + "\n";
            }
            Hosts = body;
            HostsEnable = Visibility.Visible;
        }
    }
}
