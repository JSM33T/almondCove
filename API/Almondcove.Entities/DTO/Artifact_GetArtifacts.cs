﻿using Almondcove.Entities.Dedicated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Almondcove.Entities.DTO
{
    public class Blog_GetBlogs
    {
        public int Id { get; set; }
        public string BlogName { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public string Type { get; set; }
        public string CategoryName { get; set; }
        public string CategorySlug { get; set; }
        public int SeriesId { get; set; }
        public string Markdown { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
