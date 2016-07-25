using Bitly.Business;
using Bitly.Entities;
using Bitly.Web.Services;
using Bitly.Web.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Bitly.Web.Contollers.Api
{
    public class LinksController : ApiController
    {
        private ILinkManager _linkManager;
        private IConverter<Link, LinkViewModel> _linkToViewModelConverter;

        public LinksController(ILinkManager linkManager,
            IConverter<Link, LinkViewModel> linkToViewModelConverter)
        {
            _linkManager = linkManager;
            _linkToViewModelConverter = linkToViewModelConverter;
        }

        [HttpGet]
        [Route("api/links/list/{linkIds}")]
        public IEnumerable<LinkViewModel> GetLinksByIds(string linkIds)
        {
            var ids = linkIds.Split('-').Select(id => long.Parse(id)).ToArray();
            var links = _linkManager.GetLinks(ids);
            var viewModel = links.Select(link => _linkToViewModelConverter.Convert(link)).ToList();
            return viewModel;
        }

        [HttpPut]
        [Route("api/links")]
        public LinkViewModel CreateOrGet(CreateLinkViewModel createLinkViewModel)
        {
            if (!IsValidUrl(createLinkViewModel.Url))
            {
                return null;
            }

            var link = _linkManager.GetByOriginalUrl(createLinkViewModel.Url);
            if (link == null)
            {
                link = new Link
                {
                    OriginalUrl = createLinkViewModel.Url
                };
                _linkManager.Create(link);
            }

            var viewModel = _linkToViewModelConverter.Convert(link);
            return viewModel;
        }

        private bool IsValidUrl(string url)
        {
            return true;
        }


    }
}
