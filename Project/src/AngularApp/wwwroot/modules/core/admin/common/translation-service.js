﻿/*global angular*/
(function () {
    angular
        .module('AppAdmin.common')
        .service('translateService', translateService);

    /* @ngInject */
    function translateService($http) {
        var data = {},
            isDataLoaded = false,
            service = {};

        $http.get('api/localization/get-translation').then(function (result) {
            data = result.data;
            isDataLoaded = true;
        })

        service.get = function (key) {
            if (isDataLoaded) {
                if (data[key]) {
                    return data[key];
                }

                return key;
            }
        };

        return service;
    }
}());