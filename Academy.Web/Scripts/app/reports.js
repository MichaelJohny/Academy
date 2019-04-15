var app = angular.module("reportsApp", ["ngSanitize", "ngCsv", "ngMaterialDatePicker"]);

app.controller("ReportsFormCtrl", ["$scope", "$http", "$q",
    function ($scope, $http, $q) {
        $scope.initReports = function () {
            getgroupNumbers();
            getBatches();
        }

        function getData() {
            $http.get('/student/getDDLs')
                .then(function (response) {
                    const data = response.data;
                    if (data.Success) {
                        console.log(response);
                        $scope.nationalities = data.Result.Nationalities;
                        $scope.collages = data.Result.Collages;
                        $scope.qualifiations = data.Result.Qualifiations;
                        $scope.cities = data.Result.Cities;
                        $scope.areas = data.Result.Areas;
                        $scope.specializations = data.Result.Specializations;
                    }
                });
        }

        function getgroupNumbers() {
            $http.get('/reports/groupNumbers')
                .then(function(response) {
                    const data = response.data;
                    if (data.Success) {
                        $scope.groupNumbers = data.Result;
                    }
                });
        }
        function getBatches() {
            $http.get('/reports/batches')
                .then(function(response) {
                    const data = response.data;
                    if (data.Success) {
                        $scope.batches = data.Result;
                    }
                });
        }
        $scope.SearchInput = {
         
        }

        $scope.test = function() {
            var sss = $scope.SearchInput;
        }
        $scope.asd = 'hello angular';

        $scope.MyCSV = function () {
            $scope.reportPagedInput.Page = 0;
            let deferred = $q.defer();
            $http.post('/Reports/GetData', $scope.reportPagedInput)
                .then(function (res) {
                    $q.when(res).then(function () {
                        const data = res.data.Result.Items.map(x => ({
                            Name: x.Name,
                            Quantity: x.Quantity,
                            TotalPrice: x.Total
                        }));
                        deferred.resolve(data);
                    });
                }, function (res) {
                    deferred.reject();
                });
            return deferred.promise;
        }

        $scope.filename = "test ";

        $scope.getHeaders = function () { return ['Name', 'Quantity', 'Price'] };

    }
]);