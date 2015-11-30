using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum NotifyType
{
    Progress,
    Data
}
namespace GTA5Net.IPSource
{

    public static class IPHelper
    {
        public static string[] IP =
        {
           "conductor-prod.ros.rockstargames.com",
           "conductor-prod.ros.rockstargames.com",
           "patches.rockstargames.com",
           "prod.cloud.rockstargames.com",
           "prod.cs.ros.rockstargames.com",
           "prod.p01sjc.pod.rockstargames.com",
           "prod.p02sjc.pod.rockstargames.com",
           "prod.ros.rockstargames.com",
           "prod.telemetry.ros.rockstargames.com",
        };
        public static string Script = @"
var host = document.getElementById('host');
 var subBtns = document.getElementsByTagName('input');
   var j = 0;
      for (var i = 0; i < subBtns.length; i++) {
          if (subBtns[i].value == ""检测"") {
                          j = i;
                            break;
                                   }
                             }" +
                "host.setAttribute('value', '" + IP[0] + "');" + @"
                   subBtns[j].click();
                 
  
";
        public static string Script2 = @"
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
        public static string Script3 = @"
var progress = document.getElementsByTagName('em')[0].innerText;
progress.onclick = window.external.notify('Progress,'+progress.toString());
";
    }
}
