var TrackerIndexPage = function () {
    return {
        init: () => {
            TrackerIndexPage.options.prepare();
        },
        options: {
            prepare: async () => {
                await TrackerIndexPage.options.actions.income.get();
                await TrackerIndexPage.options.actions.expenses.get();
                TrackerIndexPage.options.form.account.set();
                TrackerIndexPage.options.form.expense.table.generate();

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
                    await TrackerIndexPage.options.actions.expenses.save();
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
                    model: null,
                    get: async () => {
                        const response = await HelperAjax.request({
                            type: 'GET',
                            endpoint: 'Account/Get'
                        });

                        if (response.status_code !== 200) {
                            alert(response.status_message);
                            return;
                        }

                        TrackerIndexPage.options.actions.income.model = response.data;
                        $('.edt-monthly-income').disable();
                    },
                    save: async () => {
                        const data = { ...TrackerIndexPage.options.actions.income.model };
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

                        TrackerIndexPage.options.actions.income.model = response.data;
                    }
                },
                expenses: {
                    model: null,
                    get: async () => {
                        const response = await HelperAjax.request({
                            type: 'GET',
                            endpoint: 'Expenses/GetExpenses'
                        });

                        if (response.status_code !== 200) {
                            alert(response.status_message);
                            return;
                        }

                        TrackerIndexPage.options.actions.expenses.model = response.data;
                    },
                    save: async () => {
                        const obj = {};

                        obj.date = $('.edt-expense-date').val();
                        obj.value = $('.edt-expense-value').unmaskCurrency();
                        obj.comments = $('.txt-comments').val();
                        obj.expenseType = {
                            id: $('.cmb-expenses-type').val()
                        };
                        obj.user = {
                            id: TrackerIndexPage.options.actions.income.model?.id
                        };

                        const response = await HelperAjax.request({
                            type: 'POST',
                            endpoint: 'Expenses/AddExpense',
                            data: obj
                        });

                        if (response.status_code !== 200) {
                            alert(response.status_message);
                            return;
                        }

                        const model = TrackerIndexPage.options.actions.expenses.model;
                        TrackerIndexPage.options.actions.expenses.model = [...model, response.data];

                        TrackerIndexPage.options.form.expense.clear();
                        TrackerIndexPage.options.form.expense.table.generate();
                    },
                    delete: async (id) => {
                        const data = TrackerIndexPage.options.form.expense.table.content.find(c => c.internalId === id);

                        const response = await HelperAjax.request({
                            type: 'DELETE',
                            endpoint: 'Expenses/DeleteExpense',
                            data: {
                                id: data.model.id
                            }
                        });

                        if (response.status_code !== 200) {
                            alert(response.status_message);
                            return;
                        }

                        const deleted = TrackerIndexPage.options.actions.expenses.model?.find(e => e.id === data.model.id);
                        const index = TrackerIndexPage.options.actions.expenses.model?.indexOf(deleted);

                        if (index > -1) {
                            TrackerIndexPage.options.actions.expenses.model.splice(index, 1);
                            TrackerIndexPage.options.form.expense.table.generate();
                        }
                    }
                }
            },
            form: {
                account: {
                    set: () => {
                        const model = TrackerIndexPage.options.actions.income.model;

                        $('.edt-monthly-income').disable();
                        $('.edt-monthly-income').val(model?.monthlyIncome ?? '')
                    }
                },
                expense: {
                    table: {
                        content: [],
                        generate: () => {
                            TrackerIndexPage.options.form.expense.table._clear();

                            const model = [...TrackerIndexPage.options.actions.expenses.model];
                            const $tableRows = $('.tb-expenses-rows');

                            model?.map(expense => {
                                const tableContent = {
                                    internalId: Math.floor(Math.random() * 100),
                                    order: 1,
                                    model: expense
                                };

                                TrackerIndexPage.options.form.expense.table.content.push(tableContent);
                            });

                            const tableContent = TrackerIndexPage.options.form.expense.table.content;

                            tableContent?.map(row => {
                                var htmlRow = $(`<tr>
                                    <th scope="row">${row.order}</th>
                                    <td>${HelperFunctions.formatters.usDollar(row.model.value)}</td>
                                    <td>${moment(row.model.date?.valueOf()).format('MM-DD-YYYY')}</td>
                                    <td>${row.model.expenseType.description}</td>
                                    <td>${row.model.comments}</td>`);

                                $('<td><a><i class="fa fa-edit table-icon pencil-icon"></i></a></td>')
                                    .click(() => {
                                        TrackerIndexPage.options.form.expense.set(row.internalId);
                                    })
                                    .appendTo(htmlRow);

                                $('<td><a><i class="fa fa-trash table-icon trash-icon"></i></a></tr>')
                                    .click(() => {
                                        TrackerIndexPage.options.actions.expenses.delete(row.internalId);
                                    })
                                    .appendTo(htmlRow);

                                $(htmlRow).appendTo($tableRows);
                            });
                        },
                        _clear: () => {
                            TrackerIndexPage.options.form.expense.table.content = [];
                            $('.tb-expenses-rows').find('tr').remove();
                        }
                    },
                    set: (id) => {
                        console.log(id);
                    },
                    clear: () => {
                        $('.cmb-expenses-type').val('');
                        $('.edt-expense-date').val('');
                        $('.edt-expense-value').val('');
                        $('.txt-comments').val('');
                    }
                }
            }
        }
    }
}();