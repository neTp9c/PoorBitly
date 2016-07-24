using Autofac;
using Bitly.Data;
using Bitly.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Bitly.Business.Tests
{
    // this is an example of testing some sort of methods using mock
    // better but longer way is to create test doubles https://msdn.microsoft.com/en-US/data/dn314431

    [TestFixture]
    public class LinkManagerTests
    {
        [Test]
        public void CanGetLinksByIds()
        {
            var links = GetLinks().AsQueryable();
            var mockSetLink = new Mock<DbSet<Link>>();
            mockSetLink.As<IQueryable<Link>>().Setup(m => m.Provider).Returns(links.Provider);
            mockSetLink.As<IQueryable<Link>>().Setup(m => m.Expression).Returns(links.Expression);
            mockSetLink.As<IQueryable<Link>>().Setup(m => m.ElementType).Returns(links.ElementType);
            mockSetLink.As<IQueryable<Link>>().Setup(m => m.GetEnumerator()).Returns(links.GetEnumerator());

            var mockContext = new Mock<BitlyContext>();
            mockContext.Setup(c => c.Links).Returns(mockSetLink.Object);

            var linkManager = new LinkManager(
                mockContext.Object,
                new Mock<IShortenPathConverter>().Object,
                new Mock<IOriginalUrlStandardizer>().Object);

            var retrivedLinks = linkManager.GetLinks(new long[] { 1, 3 });

            Assert.That(retrivedLinks.Count(), Is.EqualTo(2));
        }

        public IEnumerable<Link> GetLinks()
        {
            var links = new List<Link>
            {
                new Link
                {
                    Id = 1,
                    ShortenPath = "aaaaab",
                    CreatedUtc = DateTime.Parse("2010-03-04T14:15:16Z"),
                    OriginalUrl = "https://www.google.ru/maps/search/%D1%82%D0%B8%D0%BD%D1%8C%D0%BA%D0%BE%D1%84%D1%84+%D0%B2%D0%BE%D0%B4%D0%BD%D1%8B%D0%B9+%D1%81%D1%82%D0%B0%D0%B4%D0%B8%D0%BE%D0%BD/@55.8468184,37.4271234,12z/data=!3m1!4b1",
                    VisitCount = 10
                },
                new Link
                {
                    Id = 2,
                    ShortenPath = "aaaaac",
                    CreatedUtc = DateTime.Parse("2011-04-05T12:13:20Z"),
                    OriginalUrl = "https://market.yandex.ru/product/12859250",
                    VisitCount = 20
                },
                new Link
                {
                    Id = 3,
                    ShortenPath = "aaaaad",
                    CreatedUtc = DateTime.Parse("2012-06-07T09:11:40Z"),
                    OriginalUrl = "https://lenta.ru/articles/2016/07/20/brain/",
                    VisitCount = 30
                },
                new Link
                {
                    Id = 4,
                    ShortenPath = "aaaaae",
                    CreatedUtc = DateTime.Parse("2012-06-07T09:11:40Z"),
                    OriginalUrl = "http://xn--h1akeme.xn--d1abbgf6aiiy.xn--p1ai/%D0%B1%D0%B8%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D0%B8%D1%8F",
                    VisitCount = 0
                }
            };

            return links;
        }
    }
}
