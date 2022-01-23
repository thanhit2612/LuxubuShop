$("#slider").owlCarousel({
   items: 1,
   loop: true,
   center: false,
   autoplay: true,
   autoplayTimeout: 4000,
   autoplayHoverPause: true,
   animateOut: "fadeOut",
   nav: true,
   navText: ["<span class='fal fa-angle-left  slider-icon'>", "<span class='fal fa-angle-right  slider-icon'>"],
})

$("#posts").owlCarousel({
   items: 3,
   loop: true,
   dots: false,
   autoplay: true,
   autoplayTimeout: 4000,
   autoplayHoverPause: true,
   animateOut: "fadeOut",
   margin: 10,
   nav: true,
   navText: ["<span class='fal fa-angle-left  posts-icon'>", "<span class='fal fa-angle-right  posts-icon'>"],
})

$("#brand").owlCarousel({
   items: 5,
   loop: true,
   margin: 15,
   dots: false,
   center: true,
   autoplay: true,
   autoplayTimeout: 3000,
   autoplayHoverPause: true,
})
