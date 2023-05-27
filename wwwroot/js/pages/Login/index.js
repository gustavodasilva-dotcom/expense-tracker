var LoginIndexJs = function () {
    return {
        init: () => {
            LoginIndexJs._prepare()
        },
        _prepare: async () => {
            $('.btn-log-in').click(async function (e) {
                e.preventDefault();
                await LoginIndexJs.actions._login();
            });
        },
        actions: {
            _login: async () => {
                const obj = {
                    Email: $('.edt-user-email').val(),
                    Password: $('.edt-user-password').val()
                };

                const response = await HelperAjax.request({
                    type: 'POST',
                    endpoint: 'Login/SignUp',
                    data: obj
                });
            }
        }
    };
}();
