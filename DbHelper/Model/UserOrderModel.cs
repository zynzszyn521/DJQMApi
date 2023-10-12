using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class UserOrderModel
    {
        public string? orderId { get; set; }
        public string? userCode { get; set; }
        public string? orderType { get; set; }
        public string? productId { get; set; }
        public string? price { get; set; }
        public string? payType { get; set; }
        public DateTime? createTime { get; set; }
    }
}
