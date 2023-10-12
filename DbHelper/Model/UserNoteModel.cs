using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class UserNoteModel
    {
        public string? noteId { get; set; }
        public string? userCode { get; set; }
        public string? appCode { get; set; }
        public string? title { get; set; }
        public string? noteDate { get; set; }
        public string? noteSex { get; set; }
        public DateTime? updateTime { get; set; }
    }
}
