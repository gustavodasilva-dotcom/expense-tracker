var TrackerIndexPage = function () {
    return {
        init: () => {
            TrackerIndexPage.options.prepare();
        },
        options: {
            model: null,
            prepare: async () => {
                await TrackerIndexPage.options.actions.income.get();
                TrackerIndexPage.options.form.set();

                await HelperFunctions.setDropdown({
                    request: {
                        type: 'GET',
                        endpoint: 'Expenses/GetExpenseTypes'
                    },
                    element: {
                        input: '.cmb-expenses-type',
                        fieldId: 'id',
                        fieldName: 'description',
                        defaultSelected: true,
                        defaultMessage: 'Select a expense type'
                    }
                });

                $('.btn-save-income').click(async function () {
                    await TrackerIndexPage.options.actions.income.save();
                });

                $('.btn-add-expense').click(async function (e) {
                    await TrackerIndexPage.options.actions.expense.save();
                });

                $('.btn-edit-monthly-income').click(function () {
                    const el = $('.edt-monthly-income');

                    if (el.is(':disabled')) {
                        el.enable();
                    } else {
                        el.disable();
                    }
                });

                $('.edt-monthly-income').maskCurrency();
                $('.edt-expense-value').maskCurrency();
            },
            actions: {
                income: {
                    get: async () => {
                        const response = await HelperAjax.request({
                            type: 'GET',
                            endpoint: 'Account/Get'
                        });
    
                        if (response.status_code !== 200) {
                            alert(response.status_message);
                            return;
                        }
    
                        TrackerIndexPage.options.model = response.data;
                        $('.edt-monthly-income').disable();
                    },
                    save: async () => {
                        const data = { ...TrackerIndexPage.options.model };
                        data.monthlyIncome = $('.edt-monthly-income').unmaskCurrency();

                        const response = await HelperAjax.request({
                            type: 'PUT',
                            endpoint: 'Account/Update',
                            data: data
                        });

                        if (response.status_code !== 200) {
                            alert(response.status_message);
                            return;
                        }

                        TrackerIndexPage.options.model = response.data;
                    }
                },
                expense: {
                    save: async () => {

                    }
                }
            },
            form: {
                set: () => {
                    const model = TrackerIndexPage.options.model;

                    $('.edt-monthly-income').disable();
                    $('.edt-monthly-income').val(model?.monthlyIncome ?? '')
                }
            }
        }
    }
}();