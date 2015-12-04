using GTA5Net.Model;
using GTA5Net.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
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
        public static int Count = 0;
        private WebViewType _webViewType;
        public Net5()
        {
            NetViewModel = new NetViewModel();
            this.DataContext = NetViewModel;
            this.InitializeComponent();
        }
        private async void NetWeb_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            sender.DOMContentLoaded -= NetWeb_DOMContentLoaded;
            _webViewType = (WebViewType)Enum.Parse(typeof(WebViewType), sender.Name);
            switch (_webViewType)
            {
                case WebViewType.NetWeb0:
                    Count = 0;
                    break;
                case WebViewType.NetWeb1:
                    Count = 1;
                    break;
                case WebViewType.NetWeb2:
                    Count = 2;
                    break;
                case WebViewType.NetWeb3:
                    Count = 3;
                    break;
                case WebViewType.NetWeb4:
                    Count = 4;
                    break;
                case WebViewType.NetWeb5:
                    Count = 5;
                    break;
                case WebViewType.NetWeb6:
                    Count = 6;
                    break;
                case WebViewType.NetWeb7:
                    Count = 7;
                    break;
                default:
                    break;
            }
            await sender.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.GetDomain() });
            await Task.Delay(2500);
            await sender.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.GetProgress() });
        }
        private async void NetWeb_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var webview = sender as WebView;
            _webViewType = (WebViewType)Enum.Parse(typeof(WebViewType), webview.Name);
            switch (_webViewType)
            {
                case WebViewType.NetWeb0:
                    Count = 0;
                    break;
                case WebViewType.NetWeb1:
                    Count = 1;
                    break;
                case WebViewType.NetWeb2:
                    Count = 2;
                    break;
                case WebViewType.NetWeb3:
                    Count = 3;
                    break;
                case WebViewType.NetWeb4:
                    Count = 4;
                    break;
                case WebViewType.NetWeb5:
                    Count = 5;
                    break;
                case WebViewType.NetWeb6:
                    Count = 6;
                    break;
                case WebViewType.NetWeb7:
                    Count = 7;
                    break;
                default:
                    break;
            }
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
                        await webview.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.GetProgress() });
                    }
                    if (data == "100%")
                    {
                        NetViewModel.IpMods[Count].Progress = data;
                        NetViewModel.IpMods[Count].ProgressValue = data.Split('%')[0];
                        NetViewModel.IpMods[Count].IsPopUp = Visibility.Collapsed;
                        await webview.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.GetData() });

                    }
                    break;
                case NotifyType.Data:
                    NetViewModel.IpMods[Count].IP = getData(getDataFilter(data))[0];
                    NetViewModel.IpMods[Count].TTL = getData(getDataFilter(data))[1];
                    break;
                default:
                    break;
            }
        }
        private string getDataFilter(string data)
        {
            var ipTTL = data.Split('&');
            data = string.Empty;
            var i = 0;
            while (i < ipTTL.Length - 1)
            {
                data += ipTTL[i];
                if (i < ipTTL.Length)
                {
                    data += ":";
                }
                if (ipTTL[i].Length > 4)
                {
                    i++;
                    while (ipTTL[i].Length > 4)
                    {
                        i++;
                    }
                }
                else if (ipTTL[i].Length <= 4 && i < ipTTL.Length - 1)
                {
                    i++;
                    while (ipTTL[i].Length <= 4 && i < ipTTL.Length - 1)
                    {
                        i++;
                    }
                }
                if (i == ipTTL.Length - 1)
                {
                    data += ipTTL[i];
                    i++;
                }
            }
            return data;
        }
        private string[] getData(string data)
        {
            var list = data.Split(':');
            int min = 1;
            if (int.Parse(list[min])==0)
            {
                min += 2;
            }
            for (int j = 1; j < list.Length; j = j + 2)
            {
                if (int.Parse(list[j]) == 0)
                {
                    j += 2;
                }
                if (int.Parse(list[j]) < int.Parse(list[min]))
                {
                    min = j;
                }
            }
            return new string[] { list[min - 1], list[min] };
        }
        private void NetWeb_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            (sender as WebView).Refresh();
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            var hosts = (item.DataContext as NetViewModel).Hosts;
            var dp = new DataPackage();
            dp.SetText(hosts);
            Clipboard.SetContent(dp);
        }

        private void Hosts_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var elem = sender as FrameworkElement;
            if (elem != null)
            {
                FlyoutBase.ShowAttachedFlyout(elem);
            }
        }
    }
}
