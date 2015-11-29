using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using GTA5Net.Model;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace GTA5Net
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private NotifyType _notifyType;
        public IPMod Data { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
        }


        private async void Gta5NetWeb_ScriptNotify(object sender, NotifyEventArgs e)
        {
            var datas = e.Value.Split(',');
            _notifyType = (NotifyType)Enum.Parse(typeof(NotifyType), datas[0]);
            var data = datas[1];
            switch (_notifyType)
            {
                case NotifyType.Progress:
                    if (data != "100%")
                    {
                        await Task.Delay(100);
                        await Gta5NetWeb.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.Script3 });
                    }
                    if (data == "100%")
                    {
                        await Gta5NetWeb.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.Script2 });

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
                            data += ipTTL[i] + "," + ipTTL[i + 1];
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
                    await new MessageDialog(data).ShowAsync();
                    break;
                default:
                    break;
            }



        }
        private async void Gta5NetWeb_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            await sender.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.Script });
            Gta5NetWeb.DOMContentLoaded -= Gta5NetWeb_DOMContentLoaded;
            await Task.Delay(3000);
            await Gta5NetWeb.InvokeScriptAsync("eval", new string[] { IPSource.IPHelper.Script3 });
        }
    }
}
