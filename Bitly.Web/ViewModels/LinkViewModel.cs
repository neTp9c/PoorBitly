using System;

namespace Bitly.Web.ViewModels
{
    public class LinkViewModel
    {
        public long Id { get; set; }
        public string ShortUrl { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime CreatedUtc { get; set; }
        public int VisitCount { get; set; }
    }
}