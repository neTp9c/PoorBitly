using Bitly.Business;
using Bitly.Entities;
using Bitly.Web.Controllers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bitly.Web.Tests
{
    [TestFixture]
    public class RedirectControllerTests
    {
        [Test]
        public void CanRedirect()
        {
            var link = GetLink();
            var mockLinkManager = new Mock<ILinkManager>();
            mockLinkManager.Setup(m => m.GetWithVisitIncremention(It.IsAny<string>()))
                .Returns((string shortPath) => { return link; });
            var redirectController = new RedirectController(mockLinkManager.Object);

            var result = redirectController.Index("aaaaab");
            var redirectResult = result as RedirectResult;

            Assert.IsInstanceOf<RedirectResult>(redirectResult);
            Assert.That(redirectResult.Url, Is.EqualTo(link.OriginalUrl));
        }

        [Test]
        public void IncrementVisitIsCalledWithRightParameters()
        {
            var mockLinkManager = new Mock<ILinkManager>();
            var redirectController = new RedirectController(mockLinkManager.Object);
            var linkId = "aaaaab";

            var result = redirectController.Index(linkId);

            mockLinkManager.Verify(m => m.GetWithVisitIncremention(It.Is<string>(v => v == linkId)), Times.Once());
        }

        private Link GetLink()
        {
            var link = new Link
            {
                Id = 1,
                ShortenPath = "aaaaab",
                CreatedUtc = DateTime.Parse("2010-03-04T14:15:16Z"),
                OriginalUrl = "https://www.google.ru/maps/search/%D1%82%D0%B8%D0%BD%D1%8C%D0%BA%D0%BE%D1%84%D1%84+%D0%B2%D0%BE%D0%B4%D0%BD%D1%8B%D0%B9+%D1%81%D1%82%D0%B0%D0%B4%D0%B8%D0%BE%D0%BD/@55.8468184,37.4271234,12z/data=!3m1!4b1",
                VisitCount = 10
            };

            return link;
        }
    }
}
