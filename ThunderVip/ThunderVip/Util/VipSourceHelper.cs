using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThunderVip.Util
{
    public static class VipSourceHelper
    {
        public static string ThunderVipSourceRegex = "http://www.vipfenxiang.com/xunlei/";
        public static string ThunderVipUrlRegex = "<a href=\"http://www.vipfenxiang.com/xunlei/\\w*.html\"";
        public static string ThunderVipTitleSourceRegex = ThunderVipUrlRegex + " title=\"[\\w\\s -|\"]*>[\\w\\s]*前";
        public static string ThunderVipTitleRegex = ">[\\w\\s]*";
        public static string LoadingString = "正在加载中,请稍候....";
        public static string ThunderVipTitleTimeRegex = "发布于[\\s\\w]*";
        public static ObservableCollection<string> ThunderVipTitleFilter = new ObservableCollection<string>
        {
            "迅雷会员vip账号分享 ",
            "白金",
            "子",
            ">",
        };
        public static ObservableCollection<string> ThunderVipUrlFilter = new ObservableCollection<string>
        {
            "<a href=",
            "\"",
            " "
        };
        public static string ThunderVipRegex = "V\\w.*<br />";
        public static string ThunderVipAccountRegex = "\\w*:\\d";
        public static string ThunderVipPasswordRegex = ":\\w.*";
        public static ObservableCollection<string> ThunderVipAccountFilter = new ObservableCollection<string>
        {
            "<br />",
            "VIP分享网独家迅雷vip账号",
            "VIP分享网",
            "迅",
            "雷",
            "账号",
            "号"
        };
        public static ObservableCollection<string> ThunderVipPasswordFilter = new ObservableCollection<string>
        {
            "密",
            "码",
            ":1",
            ":2",
            "|"
        };
        public static string GetFilter(string value, ObservableCollection<string> filters)
        {
            foreach (var filter in filters)
            {
                value = value.Replace(filter, "");
            }
            return value;
        }
    }
}
