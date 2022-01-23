var product = {
	init: function () {
		product.registerEvents();
	},
	registerEvents: function () {
		$('.product-counter').off('click').on('click', function () {
			var btn = $(this);
			var id = btn.data('id');
			$.ajax({
				url: "/Product/ClickCount",
				data: { id: id },
				dataType: "json",
				type: "POST",
			});
		});
	}
}
product.init();