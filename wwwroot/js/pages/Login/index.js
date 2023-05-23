var LoginIndexJs = function () {
    return {
        script: {},
        _prepare: () => {
            $('.btn-log-in').click(function (e) {
                e.preventDefault();
                alert('Ok');
            })
        },
        init: (options) => {
            script = options.script;
            script._prepare()
        }
    };
}();
