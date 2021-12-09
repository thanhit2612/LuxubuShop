﻿var common = {
	init: function () {
		common.registerEvent();
	},
	registerEvent: function () {

		$('#search-input').autocomplete({
			minLength: 0,
			source: function (request, response) {
				$.ajax({
					url: "/Product/ListName",
					dataType: "json",
					data: {
						q: request.term
					},
					success: function (res) {
						response(res.data);
					}
				});
			},
			focus: function (event, ui) {
				$("#search-input").val(ui.item.label);
				return false;
			},
			select: function (event, ui) {
				$("#search-input").val(ui.item.label);

				return false;
			}
		})
			.autocomplete("instance")._renderItem = function (ul, item) {
				return $("<li>")
					.append("<a>" + item.label + "</a>")
					.appendTo(ul);
			};
	}
}
common.init();