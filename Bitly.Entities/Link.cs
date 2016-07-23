using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitly.Entities
{
    public class Link
    {
        public int Id { get; set; }
        public string ShortenPath { get; set; }
        public string OriginalUrl { get; set; }
        public DateTime CreatedUtc { get; set; }
        public int VisitCount { get; set; }
    }
}
