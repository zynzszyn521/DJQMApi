using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class VipPriceModel
    {
        public string? typeId { get; set; }
        public string? typeName { get; set; }
        public string? typePrice { get; set; }
        public DateTime? updateTime { get; set; }
        public int? monthLenth { get; set; }
    }
}
