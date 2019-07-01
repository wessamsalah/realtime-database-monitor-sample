
(function () {
    "use strict";

    function getData($http) {
        return $http.get("api/DevTest").then(function (response) {
            return response.data;
        })
    }

    function getNewAddedData($http, Date) {
        return $http.get("api/DevTest?lastupdate=" + Date).then(function (response) {
            return response.data;
        })
    }

    function deleteData(id, $http) {
        return $http.delete("api/DevTest/" + id);
    }

    function getLastAddedDate(list) {
        var maxDate;
        maxDate = list[0].Date;
        angular.forEach(list, function (value, key) {
            if (maxDate < value.Date)
                maxDate = value.Date;
        });
        return maxDate;
    }

    function getUpdatedData($http, Date) {
        var maxDate;
        maxDate = list[0].Date;
        angular.forEach(list, function (value, key) {
            if (maxDate < value.Date)
                maxDate = value.Date;
        });
        return maxDate;
    }

    function getDeletedData()
    {
        return $http.get("api/DevTest/deleted");
    }
    function controller($http, $timeout, $filter) {
        var model = this;
        model.Dev = [];

        var appendToArray = function (maxDate) {
            var newLst = getNewAddedData($http, maxDate).then(function (response) {
                model.Dev = model.Dev.concat(response);
            });
        }

        var updataArray = function (maxDate) {
            var updatedObj = getNewAddedData($http, maxDate).then(function (response) {
                if (response.length == 1) {
                    var single_object = $filter('filter')(model.Dev, function (d) { return d.ID === response[0].ID; })[0];
                    var objIndex = model.Dev.indexOf(single_object);
                    if (objIndex > -1) {
                        model.Dev[objIndex] = response[0];
                    }
                } else {
                    angular.forEach(response, function (value, key) {
                        var single_object = $filter('filter')(model.Dev, function (d) { return d.ID === value.ID; })[0];
                        var objIndex = model.Dev.indexOf(single_object);
                        if (objIndex > -1) {
                            model.Dev[objIndex] = value;
                        }
                    });
                }
            });
        }

        var dataHub = $.connection.dataHub;

        $.connection.hub.start().done(
            function () {

                console.log("connection work")

            }
        ).fail(
        function () {
            console.log("fail")
        }
        );

        getData($http).then(function (response) {
            model.Dev = response;
        })

        model.$onInit = function () {
        }

        dataHub.client.newMessage = function (message, info) {

            console.log(message + " " + info);
            var maxDate = getLastAddedDate(model.Dev)
            switch (info) {
                case "Insert":
                    appendToArray(maxDate);
                    break;

                case "Delete":
                    // getDeletedData($http)
                    getData($http).then(function (response) {
                        model.Dev = response;
                    });
                    break;

                case "Update":
                    updataArray(maxDate);
                    break;
            }
        }
        model.$onDestroy = function () {
            $.connection.hub.stop();
        }

        model.delete = function (id) {
            deleteData(id, $http);
        }


    }

    app.component("mainCtrl", {
        templateUrl: "/Templates/devData.template.html",
        controllerAs: "vm",
        controller: ['$http', '$timeout', '$filter', controller]
    })
}());