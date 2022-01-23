var content = {
	init: function () {
		content.registerEvents();
	},
	registerEvents: function () {
		$('#btn-remove').off('click').on('click', function (e) {
			e.preventDefault();
			var btn = $(this);
			var id = btn.data('id');
			$.ajax({
				url: "/Wishlist/Delete",
				data: { id: id },
				dataType: "json",
				type: "POST",
				success: function(res) {
					if(res.status == true) {
					window.location.href = "/yeu-thich";
					}
				}
			});
		});
	}
}
content.init();