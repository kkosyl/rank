(function () {
    var myModule = angular.module('myModule', ['ui.bootstrap']);

    myModule.controller('searchCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
        $scope.selected = "";
        $scope.search = { Name: '', City: '', Country: '' }

        $scope.getMatchingPlaces = function ($viewValue) {
            var matchingPlaces = [];

            for (var i = 0; i < $scope.places.length; i++) {

                if (
                  $scope.places[i].Name.toLowerCase().indexOf($viewValue.toLowerCase()) != -1 ||
                  $scope.places[i].City.toLowerCase().indexOf($viewValue.toLowerCase()) != -1 ||
                  $scope.places[i].Country.toLowerCase().indexOf($viewValue.toLowerCase()) != -1) {

                    matchingPlaces.push($scope.places[i]);
                }
            }
            return matchingPlaces;
        };

        $http.get('/Place/GetPlaces')
        .success(function (data) {
            $scope.places = data;
        });

        $scope.imageSource = function (item) {
            return item.Picture['medium'];
        };

        $http.get('/Place/PolishCities')
        .success(function (data) {
            $scope.polishCities = data;
        })
        .error(function (message) {
            alert("Błąd przy pobieraniu danych");
        });

        $http.get('/Place/PopularCountries')
        .success(function (data) {
            $scope.popularCountries = data;
        })
        .error(function (message) {
            alert("Błąd przy pobieraniu danych");
        });
    }]);

    myModule.controller('collapseCtrl', function ($scope) {
        $scope.isCollapsed = true;
    });


    myModule.filter('firstPage', function () {
        return function (input, start) {
            start = +start;
            return input.slice(start);
        }
    })

    myModule.controller('Pagination', ['$scope', '$window', '$http', function ($scope, $window, $http) {
        $scope.currentPage = 0,
        $scope.numPerPage = 5,
        $scope.maxSize = 5;


        $http.get('/Place/GetPlaces')
            .success(function (data) {
                $scope.places = data;

                $scope.numberOfPages = function () {
                    return Math.ceil($scope.places.length / $scope.numPerPage);
                }
            });

        $scope.onSelect = function ($item) {
            $window.location.href = '/Place/Details/' + $item.PlaceId;
        };
    }]);
})();