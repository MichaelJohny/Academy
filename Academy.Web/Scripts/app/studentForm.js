var app = angular.module("baseApp", ["ngSanitize", "ngCsv"]);

app.controller("StudentFormCtrl", ["$scope", "$http",
  function ($scope, $http) {
      $scope.initStudentForm = function () {
          getNationlities();
      }

      function getNationlities() {
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

      $scope.asd = 'hello angular';
      $scope.student = {
          FirstName: "micho",
          SecondName: "adel",
          LastName: "johny",
          Code: "2010",
          Mobile1: "01276945242",
          Mobile2: "01006655982",
          Email: "micho@yahoo.com",
          University: "aun",
          GraduationYear: "2019",
          AddressDescrption: "assuit 2olta",
          JobTitle: "senior",
          Experience: "4",
          Employer: "nabila",
          CollageId: 1
      };
  }
]);