var app = angular.module('app', ['toastr', 'ngAnimate'], function ($locationProvider) {
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
});

