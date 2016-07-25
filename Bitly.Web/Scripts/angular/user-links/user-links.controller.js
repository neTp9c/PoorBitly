angular
    .module('userLinks')
    .controller('UserLinksController',
        ['$scope', '$http', 'shortLnks', function ($scope, $http, shortLnks) {

            var ids = shortLnks.getUserLinkIds();

            $scope.totalItems = ids.length;
            $scope.currentPage = 1;
            $scope.itemsPerPage = 8;

            $scope.loadUserLinks = function (skip, take) {
                var loadIds = ids.slice(skip, skip + take);
                shortLnks.loadLinks(loadIds, function (data) {
                    $scope.links = data;
                });
            };

            $scope.pageChanged = function () {
                $scope.loadUserLinks(($scope.currentPage - 1) * $scope.itemsPerPage, $scope.itemsPerPage);
            };
            $scope.pageChanged();
        }
    ]);