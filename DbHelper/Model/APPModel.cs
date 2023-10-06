using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class APPModel
    {
        public string? appName { get; set; }
        public string? versionCode { get; set; }
        public string? versionName { get; set; }
        public string? updateLog { get; set; }
        public string? apkUrl { get; set; }
        public int? active { get; set; }
        public DateTime createTime { get; set; }
    }
}
