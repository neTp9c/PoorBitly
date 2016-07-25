angular.module('core.shortLnks', []);
angular
    .module('core.shortLnks')
    .factory('shortLnks', ['$http',
        function ($http) {
            return {
                loadLinks: function (ids, callback) {

                    var idsSegment = ids.join('-');
                    var loadUrl = '/api/links/list/' + idsSegment;

                    $http.get(loadUrl).then(function (response) {
                        var sortedData = [];
                        for (var i = 0; i < ids.length; i++) {
                            for (var j = 0; j < response.data.length; j++) {
                                if (ids[i] == response.data[j].Id) {
                                    sortedData.push(response.data[j]);
                                    break;
                                }
                            }
                        }
                        callback(sortedData);
                    });
                },

                addLink: function(url, callback) { 
                    $http
                        .put('/api/links', $.param({ url: url }), {
                            headers: {
                                'Content-Type': 'application/x-www-form-urlencoded'
                            }
                        })
                        .then(function (response) {
                            callback(response.data);
                        });
                },

                getUserLinkIds: function() { 
                    var storagedIds = localStorage.getItem("linkIds");
                    return storagedIds != null ? JSON.parse(storagedIds) : [];
                },

                setUserLinkIds: function (ids) {
                    localStorage.setItem("linkIds", JSON.stringify(ids));
                }
            }
        }
    ]);