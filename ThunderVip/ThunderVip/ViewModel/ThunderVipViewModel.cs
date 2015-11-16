using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ThunderVip.Command;
using ThunderVip.Model;
using ThunderVip.Util;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ThunderVip.ViewModel
{
    public class ThunderVipViewModel : EntityBase
    {
        private VipTitle _vipTitle;
        private ObservableCollection<VipUser> _vipUsers;
        private ObservableCollection<VipTitle> _vipTitles;
        private bool _isGettingData = true;
        private bool _isNotGettingData = false;
        public ObservableCollection<VipTitle> VipTitles
        {
            get { return _vipTitles; }
            set { SetProperty(ref _vipTitles, value); }
        }
        public bool IsGettingData
        {
            get { return _isGettingData; }
            set { SetProperty(ref _isGettingData, value); }
        }
        public bool IsNotGettingData
        {
            get { return _isNotGettingData; }
            set { SetProperty(ref _isNotGettingData, value); }
        }
        public ObservableCollection<VipUser> VipUsers
        {
            get { return _vipUsers; }
            set { SetProperty(ref _vipUsers, value); }
        }
        public ThunderVipViewModel()
        {
            VipTitle = new VipTitle();
        VipUsers = new ObservableCollection<VipUser>();
            VipTitle.Title = "迅雷VIP";
            VipTitles = new ObservableCollection<VipTitle>();
            VipTitles.Add(new VipTitle() { Title = VipSourceHelper.LoadingString });
            DispatcherTimer timer = new DispatcherTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
             timer.Start();
        }

    private async void Timer_Tick(object sender, object e)
    {
        IsGettingData = true;
        IsNotGettingData = false;
        VipTitles.Clear();
        var titleList = await HtmlAnalysis.GetVipTitleDataAsync();
        foreach (var item in titleList)
        {
            VipTitles.Add(HtmlAnalysis.GetVipTitle(item));
        }
        IsGettingData = false;
        IsNotGettingData = true;
        var timer = sender as DispatcherTimer;
        timer.Tick -= Timer_Tick;
        timer.Stop();
    }


    public VipTitle VipTitle
    {
        get { return _vipTitle; }
        set { SetProperty(ref _vipTitle, value); }
    }
}
}
