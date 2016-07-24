using Bitly.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitly.Data
{
    public class BitlyInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<BitlyContext>
    {
        protected override void Seed(BitlyContext context)
        {
            var links = new List<Link>
            {
                new Link
                {
                    OriginalUrl = "https://www.google.ru/maps/search/%D1%82%D0%B8%D0%BD%D1%8C%D0%BA%D0%BE%D1%84%D1%84+%D0%B2%D0%BE%D0%B4%D0%BD%D1%8B%D0%B9+%D1%81%D1%82%D0%B0%D0%B4%D0%B8%D0%BE%D0%BD/@55.8468184,37.4271234,12z/data=!3m1!4b1",
                    ShortenPath = "aaaaab",
                    CreatedUtc = DateTime.Parse("2010-03-04T14:15:16Z")
                },
                new Link
                {
                    OriginalUrl = "https://market.yandex.ru/product/12859250",
                    ShortenPath = "aaaaac",
                    CreatedUtc = DateTime.Parse("2011-04-05T12:13:20Z")
                },
                new Link
                {
                    OriginalUrl = "https://lenta.ru/articles/2016/07/20/brain/",
                    ShortenPath = "aaaaad",
                    CreatedUtc = DateTime.Parse("2012-06-07T09:11:40Z")
                },
                new Link
                {
                    OriginalUrl = "http://xn--h1akeme.xn--d1abbgf6aiiy.xn--p1ai/%D0%B1%D0%B8%D0%BE%D0%B3%D1%80%D0%B0%D1%84%D0%B8%D1%8F",
                    ShortenPath = "aaaaae",
                    CreatedUtc = DateTime.Parse("2014-01-02T23:33:50Z")
                }
            };
            links.ForEach(link => context.Links.Add(link));
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
