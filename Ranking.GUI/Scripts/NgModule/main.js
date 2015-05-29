(function () {
    var myModule = angular.module('myModule', ['ui.bootstrap']);

    myModule.controller('searchCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
        $scope.selected = "";

        $http.get('/Place/GetPlaces')
        .success(function (data) {
            $scope.places = data;
        });

        $scope.onSelect = function ($item, $model, $label) {
            $scope.$item = $item;
            $scope.$model = $model;
            $scope.$label = $label;
            $window.location.href = '/Place/Details/' + $item.PlaceId;
        };
    }]);

    myModule.controller('collapseCtrl', function ($scope) {
        $scope.isCollapsed = true;
    });
})();