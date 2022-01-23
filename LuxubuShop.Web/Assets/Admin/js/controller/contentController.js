var content = {
	init: function () {
		content.registerEvents();
	},
	registerEvents: function () {
		$('.input-active').off('change').on('change', function (e) {
			e.preventDefault();
			var btn = $(this);
			var id = btn.data('id');
			$.ajax({
				url: "/Admin/Content/ChangeTopHot",
				data: { id: id },
				dataType: "json",
				type: "POST",
			});
		});
	}
}
content.init();