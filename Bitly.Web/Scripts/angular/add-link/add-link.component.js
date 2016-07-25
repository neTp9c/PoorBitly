angular
  .module('addLink')
  .component('addLink', {
      templateUrl: 'scripts/angular/add-link/add-link.template.html',
      controller: ['$http', 'shortLnks', function AddLinkController($http, shortLnks) {
          var that = this;
          this.links = [];
          this.linkUrl = '';

          this.addLink = function () {
              shortLnks.addLink(this.linkUrl, function (link) {
                  var index = ids.indexOf(link.Id);
                  if (index > -1) {
                      ids.splice(index, 1);
                  }

                  ids.unshift(link.Id);
                  shortLnks.setUserLinkIds(ids);

                  that.links.unshift(link);
                  if (that.links.length > 3) {
                      that.links.pop();
                  }

                  that.linkUrl = response.data.ShortUrl;
              });
          };

          var ids = shortLnks.getUserLinkIds();

          this.loadUserLinks = function (skip, take) {
              var loadIds = ids.slice(skip, skip + take);
              shortLnks.loadLinks(loadIds, function (data) {
                  that.links = data;
              });
          };

          this.loadUserLinks(0, 3);
      }]
  });