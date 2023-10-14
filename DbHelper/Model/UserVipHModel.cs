using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class UserVipHModel
    {
        public string recordId { get; set; }
        public string userCode { get; set; }
        public string vipTypeId { get; set; }
        public string vipTypeName { get; set; }
        public string vipTypePrice { get; set; }
        public int monthLength { get; set; }
        public DateTime? createTime { get; set; }
    }
}
