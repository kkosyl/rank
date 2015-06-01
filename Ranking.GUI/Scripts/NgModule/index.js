(function () {
    var myModule = angular.module('myModule');

    myModule.controller('citiesCountriesCtrl', ['$scope', '$http', '$window', '$rootScope', function ($scope, $http, $window, $rootScope) {
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

        //$scope.onCitySelect = function ($city) {
        //    $rootScope.$broadcast("search", $scope.query = $city);
        //    $window.location.href = '/Place/Search';
        //};

        //$scope.onCountrySelect = function ($country) {
        //    $rootScope.$broadcast("search", $scope.query = $country);
        //    console.log($country);
        //    $window.location.href = '/Place/Search';
        //};
    }]);
})();