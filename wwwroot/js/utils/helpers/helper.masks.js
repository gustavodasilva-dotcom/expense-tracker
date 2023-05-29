const HelperMasks = function () {
	(function ($) {
        $.fn.maskCurrency = function () {
            const pattern = "000.000.000.000.000,00";
            $(this).unmask();
            const value = parseFloat($(this).val()).toFixed(2);
            $(this).val(value).mask(pattern, { reverse: true });
        };
        $.fn.unmaskCurrency = function (value) {
            if (typeof value === 'number')
                return $(this).val(value.toLocaleString('pt-br'));

            const [integer, decimal] = $(this).val().split(',');
            return parseFloat(`${integer.replace(/\./g, '')}.${decimal}`);
        };
	})(jQuery);
}();