const HelperFunctions = function () {
    return {
        setDropdown: async (options) => {
            const response = await HelperAjax.request({
                type: options.request.type,
                endpoint: options.request.endpoint
            });

            if (response.status_code !== 200) {
                alert(response.status_message);
                return;
            }

            const data = response.data;

            if (data.length > 0) {
                if (options.element?.defaultSelected) {
                    $(`${options.element.input}`)
                        .append($('<option>', {
                            selected: true,
                            disabled: true,
                            text: options.element?.defaultMessage
                        }));
                }

                const fieldId = options.element.fieldId;
                const fieldName = options.element.fieldName;

                data.forEach(item => {
                    $(`${options.element.input}`)
                        .append($('<option>', {
                            value: item[fieldId],
                            text: item[fieldName]
                        }));
                });
            }
        }
    };
}();