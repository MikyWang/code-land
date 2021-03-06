﻿using GTA5Net.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public enum NotifyType
{
    Progress,
    Data
}
public enum WebViewType
{
    NetWeb0,
    NetWeb1,
    NetWeb2,
    NetWeb3,
    NetWeb4,
    NetWeb5,
    NetWeb6,
    NetWeb7,
}
namespace GTA5Net.IPSource
{

    public static class IPHelper
    {
        public const string Filter = @"\[[\u4E00-\u9FA5]*\s(\]|[\u4E00-\u9FA5]*\w*(\]|[\u4E00-\u9FA5]*\])|(\w*(\]|[\u4E00-\u9FA5]*)(\]|\w*(\]|[\u4E00-\u9FA5]*\]))))";
        public static string[] IP =
        {
           "conductor-prod.ros.rockstargames.com",
           "patches.rockstargames.com",
           "prod.cloud.rockstargames.com",
           "prod.cs.ros.rockstargames.com",
           "prod.p01sjc.pod.rockstargames.com",
           "prod.p02sjc.pod.rockstargames.com",
           "prod.ros.rockstargames.com",
           "prod.telemetry.ros.rockstargames.com",
        };
        public static string GetDomain()
        {
            var script = @"
 
var host = document.getElementById('host');
 var subBtns = document.getElementsByTagName('input');
   var j = 0;
      for (var i = 0; i < subBtns.length; i++) {
          if (subBtns[i].value == ""检测"") {
                          j = i;
                            break;
                                   }
                             }
" +
                "host.setAttribute('value', '" + IP[Net5.Count] + "');" + @"
                   subBtns[j].click();
                 
  
";
            return script;
        }
        public static string GetData()
        {
            var script = @"
var table = document.getElementById('tablesumary');
            if (table == null) {
table.onclick=window.external.notify('Data,table');
}
else {
    var p = table.getElementsByTagName('p');
    var data = """";
    for (var i = 0; i < p.length; i ++) {
        data += p[i].innerText;
        if (i < p.length - 1)
            data += ""&"";
    }
    table.onclick = window.external.notify('Data,'+data.toString());
}
";
            return script;
        }
        public static string GetProgress()
        {
            var script = @"
var progress = document.getElementsByTagName('em')[0].innerText;
progress.onclick = window.external.notify('Progress,'+progress.toString());
";
            return script;
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
                source = replace(value);
            }
            return source;
        }
    }
}
