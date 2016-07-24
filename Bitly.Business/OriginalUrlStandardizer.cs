using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitly.Business
{
    public interface IOriginalUrlStandardizer
    {
        string Standardize(string originalUrl);
    }

    public class OriginalUrlStandardizer : IOriginalUrlStandardizer
    {
        public string Standardize(string originalUrl)
        {
            return originalUrl;
        }
    }
}
