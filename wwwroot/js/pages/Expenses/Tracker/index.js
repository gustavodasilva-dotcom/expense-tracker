var TrackerIndexPage = function () {
    return {
        init: () => {
            TrackerIndexPage.options.actions.get();
            TrackerIndexPage.options.prepare();
        },
        options: {
            model: {},
            prepare: () => {
                $('.btn-save-income').click(async function () {
                    const data = { ...TrackerIndexPage.options.model };
                    data.monthlyIncome = $('.edt-monthly-icone').val();

                    const response = await HelperAjax.request({
                        type: 'PUT',
                        endpoint: 'Account/Update',
                        data: data
                    });

                    TrackerIndexPage.options.model = response.data;
                });
            },
            actions: {
                get: async () => {
                    const response = await HelperAjax.request({
                        type: 'GET',
                        endpoint: 'Account/Get'
                    });

                    TrackerIndexPage.options.model = response.data;
                }
            }
        }
    };
}();