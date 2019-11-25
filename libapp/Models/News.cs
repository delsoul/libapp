using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace libapp.Models
{
    public class News
    {
        // id новости
        public int NewsId { get; set; }
        // заголовок новости
        public string Title { get; set; }
        // содержание новости
        public string Content { get; set; }
        // фотография с события
        public byte[] Picture { get; set; }
    }
}