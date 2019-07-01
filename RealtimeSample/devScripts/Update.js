

(function () {
    "use strict";

    function getObjById(Id, $http) {
        return $http.get("/api/DevTest/" + Id);
    }
    function updateObj(obj, $http) {
        return $http.put("/api/DevTest/" + obj.ID, obj);
    }
    function controller($http, $location, toastr) {
        var model = this;
        model.newObj = {};

        model.$onInit = function () {
            var path = $location.path().split("/")
            var len = path.length
            var id = path[len - 1];
            if (id != undefined)
                getObjById(id, $http).then(function (response) {
                    model.newObj = response.data;
                });
        }
        model.Update = function (newObj) {
            updateObj(newObj, $http).then(function success() {
                toastr.success( 'Updated','Success! ');
            }, function error() {
                toastr.success('Error ', 'Faild!');
            })
        }
    }

    app.component("updateCtrl", {
        templateUrl: "/Templates/update.template.html",
        controllerAs: "vm",
        controller: ['$http', '$location','toastr', controller]
    })

}());