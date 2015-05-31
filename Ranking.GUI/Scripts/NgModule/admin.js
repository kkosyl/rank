(function () {
    var myModule = angular.module('myModule');

    myModule.controller('adminCtrl', ['$scope', '$http', function ($scope, $http) {
        $http.get('/Admin/AllPlaces')
        .success(function (data) {
            $scope.allPlaces = data;
        })
        .error(function (message) {
            alert("Błąd przy pobieraniu danych");
        });

        $scope.toggleEdit = function () {
            this.place.editMode = !this.place.editMode;
        }

        $scope.save = function () {
            var place = this.place;
            var response = $http({
                method: "post",
                url: "/Admin/SaveChanges",
                data: JSON.stringify(place),
                dataType: "json"
            })
            this.place.editMode = !this.place.editMode;
        };

        $scope.verify = function () {   
            var placeId = this.place.PlaceId;
            var response = $http({
                method: "post",
                url: "/Admin/Verify",
                params: {
                    id: JSON.stringify(placeId)
                }
            });
            this.place.Verified = !this.place.Verified;
        };
            

        $scope.delete = function () {
            var placeId = this.place.PlaceId;
            var response = $http({
                method: "post",
                url: "/Admin/Delete",
                params: {
                    id: JSON.stringify(placeId)
                }
            });
            $.each($scope.allPlaces, function (i) {
                if ($scope.allPlaces[i].PlaceId === placeId) {
                    $scope.allPlaces.splice(i, 1);
                    return false;
                }
            });
        };
    }]);
})();