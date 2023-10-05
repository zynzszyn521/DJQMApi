using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class SMSModel
    {
        public string? phoneNumber { get; set; }
        public string? smsCode { get; set; }
        public string? devManufacturer { get; set; }
        public string? devModel { get; set; }
        public string? devId { get; set; }
        public string? appVersion { get; set; }
    }
}
