const HelperAjax = function () {
    return {
        getPath: (url) => {
            return `${window.location.origin}/${url}`;
        },
        request: async (options) => {
            const data = await $.ajax({
                type: options.type,
                async: true,
                timeout: 50000,
                url: HelperAjax.getPath(options.endpoint),
                data: options?.data
            });

            return data;
        }
    }
}();