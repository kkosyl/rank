(function () {
    var myModule = angular.module('myModule', ['ui.bootstrap']);

    myModule.controller('searchCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
        $scope.selected = "";

        $http.get('/Place/GetPlaces')
        .success(function (data) {
            $scope.places = data;
        });

        $scope.onSelect = function ($item) {
            $window.location.href = '/Place/Details/' + $item.PlaceId;
        };

        $scope.imageSource = function (item) {
            return item.Picture['medium'];
        };
    }]);

    myModule.controller('collapseCtrl', function ($scope) {
        $scope.isCollapsed = true;
    });
})();