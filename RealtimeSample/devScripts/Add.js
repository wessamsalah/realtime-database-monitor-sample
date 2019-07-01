

(function () {
    "use strict";

    function add(newObj, $http) {
        return $http.post("/api/DevTest/", newObj);
    }

    function controller($http, toastr) {
        var model = this;
        model.newObj = {};

        model.$onInit = function () { }
        model.Add = function (newObj) {
            add(newObj, $http).then(function success() {
                toastr.success('Added ', 'Success!');
            }, function error() {
                toastr.success('Error ', 'Faild!');
            })
        }
    }

    app.component("addCtrl", {
        templateUrl: "/Templates/add.template.html",
        controllerAs: "vw",
        controller: ['$http','toastr', controller]
    })

}());