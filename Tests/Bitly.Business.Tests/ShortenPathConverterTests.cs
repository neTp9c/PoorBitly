using Autofac;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitly.Business.Tests
{
    [TestFixture]
    public class ShortenPathConverterTests
    {
        private IContainer _container;

        [SetUp]
        public void SetUp()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ShortenPathConverter>().As<IShortenPathConverter>();
            _container = builder.Build();
        }

        [Test]
        public void CanConvertIdToPath()
        {
            var shortenPathConverter = _container.Resolve<IShortenPathConverter>();
            var shortenPaths = GetShortenPaths();

            foreach(var shortenPath in shortenPaths)
            {
                var id = shortenPath.Key;
                var path = shortenPath.Value;

                var actualPath = shortenPathConverter.IdToPath(id);

                Assert.That(actualPath, Is.EqualTo(path));
            }
        }

        [Test]
        public void CanConvertPathToId()
        {
            var shortenPathConverter = _container.Resolve<IShortenPathConverter>();
            var shortenPaths = GetShortenPaths();

            foreach (var shortenPath in shortenPaths)
            {
                var id = shortenPath.Key;
                var path = shortenPath.Value;

                var actualId = shortenPathConverter.PathToId(path);

                Assert.That(actualId, Is.EqualTo(id));
            }
        }

        private IDictionary<long, string> GetShortenPaths()
        {
            var alphabetLength = "abcdefghijklmnopqrstuvwxyzABCDIFGHIJKLMNOPQRSTUVWXYZ0123456789".Length;
            var shortenPathLength = 6;

            var shortenPaths = new Dictionary<long, string>
            {
                { 1, "aaaaab" },
                { 2, "aaaaac" },

                { alphabetLength - 1, "aaaaa9" },
                { alphabetLength, "aaaaba" },
                { alphabetLength + 1, "aaaabb" },

                { (long)Math.Pow(alphabetLength, shortenPathLength) - 2, "999998" },
                { (long)Math.Pow(alphabetLength, shortenPathLength) - 1, "999999" },
            };

            return shortenPaths;
        }
    }
}
