angular
  .module('addLink')
  .component('addLink', {
      templateUrl: 'scripts/angular/add-link/add-link.template.html',
      controller: ['$http', function AddLinkController($http) {
          var that = this;
          this.links = [];
          this.linkUrl = '';

          this.addLink = function () {
              var postBody = $.param({ url: this.linkUrl });
              $http
                  .put('/api/links', postBody, {
                      headers: {
                          'Content-Type': 'application/x-www-form-urlencoded'
                      }
                  })
                  .then(function (response) {
                      var index = ids.indexOf(response.data.Id);
                      if (index > -1) {
                          ids.splice(index, 1);
                      }

                      ids.unshift(response.data.Id);
                      localStorage.setItem("linkIds", JSON.stringify(ids));
                      that.links.unshift(response.data);
                      if (that.links.length > 3) {
                          that.links.pop();
                      }

                      linkUrl = response.data.ShortUrl;
                  });
          };

          var storagedIds = localStorage.getItem("linkIds");
          var ids = storagedIds != null ? JSON.parse(storagedIds) : [];

          this.loadUserLinks = function (count) {
              var idsSegment = ids.slice(0, count).join('-');
              if (!idsSegment) {
                  return;
              }
              var loadUrl = '/api/links/list/' + idsSegment;

              $http
                  .get(loadUrl)
                  .then(function (response) {
                      that.links = response.data.reverse();
                  });
          };

          this.loadUserLinks(3);
      }]
  });