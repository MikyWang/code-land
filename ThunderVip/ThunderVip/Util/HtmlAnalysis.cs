using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ThunderVip.Model;

namespace ThunderVip.Util
{
    public static class HtmlAnalysis
    {
        private static HttpClient client = new HttpClient();
        public static async Task<string> GetVipStringAsync()
        {
            var htmlString = await client.GetStringAsync(VipSourceHelper.ThunderVipSourceRegex);
            string value = GetMatchs(VipSourceHelper.ThunderVipUrlRegex, htmlString, (data) => VipSourceHelper.GetFilter(data, VipSourceHelper.ThunderVipUrlFilter))[0];
            return value;
        }
        public static async Task<ObservableCollection<string>> GetVipDataAsync(string value)
        {
            var htmlstring = await client.GetStringAsync(value);
            var values = GetMatchs(VipSourceHelper.ThunderVipRegex, htmlstring, (data) => VipSourceHelper.GetFilter(data, VipSourceHelper.ThunderVipAccountFilter));
            return values;
        }
        public static VipUser GetVipUser(string value)
        {
            var vipUser = new VipUser();
            vipUser.Account = GetMatch(VipSourceHelper.ThunderVipAccountRegex, value);
            vipUser.Password = GetMatch(VipSourceHelper.ThunderVipPasswordRegex, value, (data) => VipSourceHelper.GetFilter(data, VipSourceHelper.ThunderVipPasswordFilter));
            return vipUser;
        }
        public static async Task<ObservableCollection<string>> GetVipTitleDataAsync()
        {
            var htmlString = await client.GetStringAsync(VipSourceHelper.ThunderVipSourceRegex);
            var values = GetMatchs(VipSourceHelper.ThunderVipTitleSourceRegex, htmlString);
            return values;
        }
        public static VipTitle GetVipTitle(string value)
        {
            var vipTitle = new VipTitle();
            vipTitle.Title = GetMatch(VipSourceHelper.ThunderVipTitleRegex, value, (data) => VipSourceHelper.GetFilter(data, VipSourceHelper.ThunderVipTitleFilter));
            vipTitle.Url = GetMatch(VipSourceHelper.ThunderVipUrlRegex, value, (data) => VipSourceHelper.GetFilter(data, VipSourceHelper.ThunderVipUrlFilter));
            vipTitle.AfterTime = GetMatch(VipSourceHelper.ThunderVipTitleTimeRegex, value, (data) => VipSourceHelper.GetFilter(data, VipSourceHelper.ThunderVipUrlFilter));
            return vipTitle;
        }
        public static string GetMatch(string rule, string source, Func<string, string> replace = null)
        {
            var reg = new Regex(rule);
            var match = reg.Match(source);
            if (match.Groups.Count == 0)
            {
                return "";
            }
            var value = match.Value;
            if (replace != null)
            {
                value = replace(value);
            }
            return value;
        }
        public static ObservableCollection<string> GetMatchs(string rule, string source, Func<string, string> replace = null)
        {
            var reg = new Regex(rule);
            var matches = reg.Matches(source);
            var values = new ObservableCollection<string>();
            for (int i = 0; i < matches.Count; i++)
            {
                values.Add(matches[i].Value);
                if (replace != null)
                {
                    values[i] = replace(values[i]);
                }
            }
            return values;
        }
    }
}
