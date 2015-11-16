using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ThunderVip.ViewModel;
using ThunderVip.Model;
using ThunderVip.Util;
using Windows.ApplicationModel.DataTransfer;
//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace ThunderVip
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class VipPage : Page
    {
        public ThunderVipViewModel ThunderVip;
        public VipPage()
        {
            ThunderVip = new ThunderVipViewModel();
            this.DataContext = ThunderVip;
            this.InitializeComponent();
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ThunderVip.VipUsers.Clear();
            var item = e.ClickedItem as VipTitle;
            var url = item.Url;
            var userList = await HtmlAnalysis.GetVipDataAsync(url);
            foreach (var value in userList)
            {
                ThunderVip.VipUsers.Add(HtmlAnalysis.GetVipUser(value));
            }
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var elem = sender as FrameworkElement;
            if (elem != null)
            {
                FlyoutBase.ShowAttachedFlyout(elem);
            }
        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as MenuFlyoutItem;
            var User = item.DataContext as VipUser;
            var dp = new DataPackage();
            if (item.Name == "Account")
            {
                dp.SetText(User.Account);
            }
            else if (item.Name=="Password")
            {
                dp.SetText(User.Password);
            }
            Clipboard.SetContent(dp);
        }
    }
}
