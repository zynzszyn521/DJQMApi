using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class ReturnResult
    {
        public bool successed { get; set; }
        public string msg { get; set; }
        public dynamic data { get; set; }
    }
}
