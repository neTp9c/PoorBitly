using Bitly.Entities;
using Bitly.Web.ViewModels;
using System;
using System.Web;

namespace Bitly.Web.Services
{
    public class LinkToLinkViewModelConverter : IConverter<Link, LinkViewModel>
    {
        private HttpContextBase _httpContextBase;
        public LinkToLinkViewModelConverter(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }

        public LinkViewModel Convert(Link link)
        {
            var linkViewModel = new LinkViewModel
            {
                Id = link.Id,
                CreatedUtc = link.CreatedUtc,
                VisitCount = link.VisitCount,
                OriginalUrl = link.OriginalUrl,
                ShortUrl = ConstractShortUrl(link.ShortenPath)
            };

            return linkViewModel;
        }

        private string ConstractShortUrl(string shortPath)
        {
            var leftPart = _httpContextBase.Request.Url.GetLeftPart(UriPartial.Authority);
            return leftPart.EndsWith("/")
                ? leftPart + shortPath
                : leftPart + "/" + shortPath;
        }
    }
}