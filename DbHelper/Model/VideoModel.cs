﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbHelper.Model
{
    public class VideoModel
    {
        public string? videoId { get; set; }
        public string? price { get; set; }
        public string? title { get; set; }
        public string? videoUrl { get; set; }
        public string? author { get; set; }
        public int? viewTimes { get; set; }
        public DateTime createTime { get; set; }
    }
}