angular
    .module('userLinks')
    .controller('UserLinksController',
        ['$scope', '$http', function ($scope, $http) {
            
            var storagedIds = localStorage.getItem("linkIds");
            var ids = storagedIds != null ? JSON.parse(storagedIds) : [];

            $scope.loadUserLinks = function (count) {
                var idsSegment = ids.slice(0, count).join('-');
                if (!idsSegment) {
                    return;
                }
                var loadUrl = '/api/links/list/' + idsSegment;

                $http
                    .get(loadUrl)
                    .then(function (response) {
                        $scope.links = response.data.reverse();
                    });
            };

            $scope.loadUserLinks(20);

        }
    ]);