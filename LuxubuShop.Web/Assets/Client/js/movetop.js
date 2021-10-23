$(window).scroll(function() {
    if ($(this).scrollTop() >= 300) {
        $('#toTop').fadeIn();
    }else{
        $('#toTop').fadeOut();
    }
});
$('#toTop').click(function() {
  $('html, body').animate({scrollTop: 0}, 800);
});