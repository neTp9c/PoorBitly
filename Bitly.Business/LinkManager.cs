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
    }

    public class LinkManager : ILinkManager
    {
        private BitlyContext _db = new BitlyContext();
        private IShortenPathConverter _shortenPathConverter;
        private IOriginalUrlStandardizer _originalUrlStandardizer;

        public LinkManager(IShortenPathConverter shortenPathConverter, IOriginalUrlStandardizer originalUrlStandardizer)
        {
            _shortenPathConverter = shortenPathConverter;
            _originalUrlStandardizer = originalUrlStandardizer;
        }

        private BitlyContext _bitlyContext = new BitlyContext();

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
            return _db.Links.First(l => l.OriginalUrl == standardizedOriginalUrl);
        }

        public IEnumerable<Link> GetLinks(IEnumerable<long> linkIds)
        {
            var query = "SELECT * FROM Link WHERE Id In (@p0)";
            return _db.Links.SqlQuery(query, string.Join(",", linkIds)).ToList();
        }

        public void Create(Link link)
        {
            link.CreatedUtc = DateTime.UtcNow;
            link.OriginalUrl = _originalUrlStandardizer.Standardize(link.OriginalUrl);

            _db.Links.Add(link);
            _db.SaveChanges();

            // may be should move this step that generated short path to the database
            link.ShortenPath = _shortenPathConverter.IdToPath(link.Id);
            _db.SaveChanges();
        }
    }
}
