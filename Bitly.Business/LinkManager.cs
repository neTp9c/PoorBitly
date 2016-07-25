using Bitly.Data;
using Bitly.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bitly.Business
{
    public interface ILinkManager
    {
        Link Get(long linkId);
        Link GetByShortenPath(string shortenPath);
        Link GetByOriginalUrl(string originalUrl);
        IEnumerable<Link> GetLinks(IEnumerable<long> linkIds);
        void Create(Link link);

        Link GetWithVisitIncremention(string shortPath);
    }

    public class LinkManager : ILinkManager
    {
        private BitlyContext _bitlyContext = new BitlyContext();
        private IShortenPathConverter _shortenPathConverter;
        private IOriginalUrlStandardizer _originalUrlStandardizer;

        public LinkManager(BitlyContext bitlyContext, IShortenPathConverter shortenPathConverter, IOriginalUrlStandardizer originalUrlStandardizer)
        {
            _bitlyContext = bitlyContext;
            _shortenPathConverter = shortenPathConverter;
            _originalUrlStandardizer = originalUrlStandardizer;
        }

        public Link Get(long linkId)
        {
            return _bitlyContext.Links.Find(linkId);
        }

        public Link GetByShortenPath(string shortenPath)
        {
            var linkId = _shortenPathConverter.PathToId(shortenPath);
            return Get(linkId);
        }

        public Link GetByOriginalUrl(string originalUrl)
        {
            var standardizedOriginalUrl = _originalUrlStandardizer.Standardize(originalUrl);
            return _bitlyContext.Links.FirstOrDefault(l => l.OriginalUrl == standardizedOriginalUrl);
        }

        public IEnumerable<Link> GetLinks(IEnumerable<long> linkIds)
        {
            return _bitlyContext.Links.Where(link => linkIds.Contains(link.Id)).ToList();
        }

        public void Create(Link link)
        {
            link.CreatedUtc = DateTime.UtcNow;
            link.OriginalUrl = _originalUrlStandardizer.Standardize(link.OriginalUrl);

            _bitlyContext.Links.Add(link);
            _bitlyContext.SaveChanges();

            // may be should move this step that generated short path to the database
            link.ShortenPath = _shortenPathConverter.IdToPath(link.Id);
            _bitlyContext.SaveChanges();
        }



        public Link GetWithVisitIncremention(string shortPath)
        {
            // there should be an optimized version with one query
            var link = GetByShortenPath(shortPath);

            if (link != null)
            {
                link.VisitCount++;
                _bitlyContext.SaveChanges();
            }

            return link;
        }
    }
}
