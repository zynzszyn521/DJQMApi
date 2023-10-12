using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class UserModel
    {
        public string? userCode { get; set; }
        public string? userName { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? phoneNumber { get; set; }
        public DateTime? createTime { get; set; }
        public DateTime? lastTime { get; set; }
        public int? vipFlag { get; set; }
        public DateTime? expirationTime { get; set; }
    }
}
