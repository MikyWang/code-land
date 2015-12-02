using GTA5Net.Model;
using GTA5Net.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上提供

namespace GTA5Net.View
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Net5 : Page
    {
        private NotifyType _notifyType;
        public NetViewModel NetViewModel { get; set; }
        public static int Count =0;
        public Net5()
        {
            NetViewModel = new NetViewModel();
            this.DataContext = NetViewModel;
            this.InitializeComponent();
        }
        private async void NetWeb_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            await NetWeb.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.GetDomain() });
            NetWeb.DOMContentLoaded -= NetWeb_DOMContentLoaded;
            await Task.Delay(2000);
            await NetWeb.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.Script3 });
        }
        private async void NetWeb_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var datas = e.Value.Split(',');
            _notifyType = (NotifyType)Enum.Parse(typeof(NotifyType), datas[0]);
            var data = datas[1];
            switch (_notifyType)
            {
                case NotifyType.Progress:
                    if (data != "100%")
                    {
                        NetViewModel.IpMods[Count].Progress = data;
                        NetViewModel.IpMods[Count].ProgressValue = data.Split('%')[0];
                        await Task.Delay(100);
                        await NetWeb.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.Script3 });
                    }
                    if (data == "100%")
                    {
                        NetViewModel.IpMods[Count].Progress = data;
                        NetViewModel.IpMods[Count].ProgressValue = data.Split('%')[0];
                        NetViewModel.IpMods[Count].IsPopUp = false;
                        await NetWeb.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.Script2 });

                    }
                    break;
                case NotifyType.Data:
                    var ipTTL = data.Split('&');
                    data = string.Empty;
                    var i = 0;
                    while (i < ipTTL.Length - 1)
                    {
                        if ((ipTTL[i].Length > 4 && ipTTL[i + 1].Length > 4) || (ipTTL[i].Length <= 4 && ipTTL[i + 1].Length <= 4))
                        {
                            data += ipTTL[i];
                            i += 2;
                        }
                        else
                        {
                            data += ipTTL[i];
                            i++;
                            if (i == ipTTL.Length - 1)
                            {
                                data += ":";
                                data += ipTTL[i];
                            }
                        }
                        if (i <= ipTTL.Length - 2)
                        {
                            data += ":";
                        }

                    }
                    var list = data.Split(':');
                    int min = 1;
                    for (int j = 1; j < list.Length; j = j + 2)
                    {
                        if (int.Parse(list[j]) < int.Parse(list[min]))
                        {
                            min = j;
                        }
                    }
                    var ipMod = new IPMod
                    {
                        IP = list[min - 1],
                        TTL = list[min]
                    };
                    NetViewModel.IpMods[Count].IP = list[min-1];
                    NetViewModel.IpMods[Count].TTL = list[min];
                    Count++;
                    if (Count<8)
                    {
                        NetWeb.Navigate(new Uri("http://tool.chinaz.com/dns/"));
                        NetWeb.DOMContentLoaded += NetWeb_DOMContentLoaded;
                    }
                    break;
                default:
                    break;
            }
        }

        private void NetWeb_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            NetWeb.Refresh();
            
        }
    }
}