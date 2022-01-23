var content = {
	init: function () {
		content.registerEvents();
	},
	registerEvents: function () {
		$('.content-counter').off('click').on('click', function () {
			var btn = $(this);
			var id = btn.data('id');
			$.ajax({
				url: "/Content/ClickCount",
				data: { id: id },
				dataType: "json",
				type: "POST",
			});
		});
	}
}
content.init();