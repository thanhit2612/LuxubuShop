$(".wishlist-btn").on("click", function () {
   $(".wishlist-box").toggleClass("active")
})
$(".account-btn").on("click", function () {
   $(".account-box").toggleClass("active")
})
$(".search-input").on("keypress", function () {
   $(".search-btn").addClass("active")
})

$(".search-btn").on("click", function () {
   $(this).removeClass("active")
})

$(".btn-like").on("click", function () {
   $(this).toggleClass("active")
})

$(window).scroll(function () {
   if ($(this).scrollTop() >= 300) {
      $(".top-btn").fadeIn()
   } else {
      $(".top-btn").fadeOut()
   }
})
$(".top-btn").click(function () {
   $("html, body").animate({ scrollTop: 0 }, 1000)
})

$(".navbar-btn").on("click", function () {
   $(".main-left").addClass("active")
   $(".overlay").addClass("active")
   $(".overlay").on("click", function () {
      $(this).removeClass("active")
      $(".main-left").removeClass("active")
   })
})

$(window).on("load", function (e) {
    $(".loading").delay(2000).fadeOut("lows");
});