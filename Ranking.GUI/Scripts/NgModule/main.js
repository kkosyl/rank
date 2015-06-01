(function () {
    var myModule = angular.module('myModule', ['ui.bootstrap']);

    myModule.controller('searchCtrl', ['$scope', '$http', '$window', '$rootScope', function ($scope, $http, $window, $rootScope) {
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

        //$rootScope.$on('search', function (event, query) {
        //    console.log(query);
        //    if (query.typ === 'City') {
        //        $scope.search = query.name;
        //    }
        //    else {
        //        $scope.search.Country = query.name;
        //    }
        //});
    }]);

    myModule.controller('collapseCtrl', function ($scope) {
        $scope.isCollapsed = true;
    });
})();