(function () {
    var myModule = angular.module('myModule', ['ui.bootstrap']);

    myModule.controller('searchCtrl', ['$scope', '$http', '$window', '$rootScope', function ($scope, $http, $window, $rootScope) {
        $scope.selected = "";
        $scope.search = { Name: '', City: '', Country: '' }

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

        $scope.onCitySelect = function ($city) {
            $scope.search = { Name: '', City: $city, Country: '' };
            $window.location.href = '/Place/Search';
        };

        $scope.onCountrySelect = function ($country) {
            $scope.search = { Name: '', City: '', Country: $country };
            
            $window.location.href = '/Place/Search';
        };
    }]);

    myModule.controller('collapseCtrl', function ($scope) {
        $scope.isCollapsed = true;
    });
})();