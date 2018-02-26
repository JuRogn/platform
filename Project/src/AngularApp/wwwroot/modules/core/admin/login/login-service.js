/*global angular*/
(function () {
    angular
        .module('AppAdmin.core')
        .factory('loginService', loginService);

    /* @ngInject */
    function loginService($http) {
        var service = {
            token: token,
            refreshToken: refreshToken,
            getProfile: getProfile
        };
        return service;

        function token(params) {
            return $http.post('connect/token', params, {
                headers: {'Content-Type': 'application/x-www-form-urlencoded'}
            });
        }

        function getProfile() {
            return $http.get('connect/profile');
        }

        function refreshToken(params) {
            return $http.post('connect/token', params);
        }
    }
})();