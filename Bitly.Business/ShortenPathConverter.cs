using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitly.Business
{
    public interface IShortenPathConverter
    {
        string IdToPath(long linkId);
        long PathToId(string shortenPath);
    }

    // https://gist.github.com/dgritsko/9554733
    public class ShortenPathConverter : IShortenPathConverter
    {
        private const string _alphabet = "abcdefghijklmnopqrstuvwxyzABCDIFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public string IdToPath(long linkId)
        {
            var path = string.Empty;

            while (linkId > 0)
            {
                var remainder = (int)(linkId % _alphabet.Length);
                path = _alphabet[remainder] + path;
                linkId = linkId / _alphabet.Length;
            }

            while (path.Length < 6)
            {
                path = _alphabet[0] + path;
            }

            return path;
        }

        public long PathToId(string shortenPath)
        {
            long number = 0;

            foreach (var shortenPathSymbol in shortenPath)
            {
                number = (number * _alphabet.Length) + _alphabet.IndexOf(shortenPathSymbol);
            }

            return number;
        }
    }
}
