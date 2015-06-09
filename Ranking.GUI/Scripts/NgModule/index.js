(function () {
    var myModule = angular.module('myModule');

    myModule.controller('citiesCountriesCtrl', ['$scope', '$http', '$window', function ($scope, $http, $window) {
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

        $scope.onPlaceSelect = function ($name) {
            $http({
                url: "/Place/Popular",
                method: "GET",
                params: { name: $name }
            });
            //$window.location.href = '/Place/Popular/' + $name;
        };
    }]);
})();