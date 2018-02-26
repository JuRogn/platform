 /*global angular*/
(function () {
    angular
        .module('AppAdmin.core')
        .controller('LoginFormCtrl', LoginFormCtrl);

    /* @ngInject */
    function LoginFormCtrl($state, $stateParams, loginService, translateService) {
        var vm = this;
        vm.translate = translateService;
        vm.loginform = {
            client_id:'app',
            client_secret:'secret',
            grant_type:'password',
            password : $stateParams.passowrd,
            username : $stateParams.username
        };
        vm.refreshtokendata = {
            client_id: 'app',
            client_secret: 'secret',
            grant_type: 'refresh_token',
            token_type: 'Bearer',
            refresh_token: ''
        };
        vm.accesstoken = {
            access_token: '',
            expires_in: '',
            token_type: '',
            refresh_token: ''
        };
        vm.authorized = false;


        vm.login = function login() {
            var promise;
            if (vm.authorized) {
                promise = loginService.refreshToken(vm.refreshtokendata);
            } else {
                promise = loginService.token(vm.loginform);
            }

            promise
                .then(function (result) {
                    vm.accesstoken = result;
                    $state.go('users');
                })
                .catch(function (response) {
                    var error = response.data;
                    vm.validationErrors = [];
                    if (error && angular.isObject(error)) {
                        for (var key in error) {
                            vm.validationErrors.push(error[key][0]);
                        }
                    } else {
                        vm.validationErrors.push('authrize err.');
                    }
                });
        };
        

        function init() {
            if (vm.authorized) {
                //检查令牌是否过期，如果过期需要刷新令牌
                console.log('authrized');
                $state.go("dashboard");//跳转到首页
            }
        }

        init();
    }
})();