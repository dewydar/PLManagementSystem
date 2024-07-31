let navbarLogo = $('.brand-link-navbar');
let sidebarLogo = $('.brand-link-sidebar');
$(document).on('collapsed.lte.pushmenu', function () {

    if (sidebarLogo.is(":visible")) {
        $('#sideLogo').css('display','none');
        sidebarLogo.hide()
        navbarLogo.show(200)
        $('#nvLogo').css('display','flex');
    }

});

$(document).on('shown.lte.pushmenu', function () {

    if (navbarLogo.is(":visible")) {
        $('#nvLogo').css('display', 'none');
        navbarLogo.hide()
        sidebarLogo.show(200)
        $('#sideLogo').css('display', 'flex');
    }

});

$(".menu-btn").on("click", function () {

    if (navbarLogo.is(":hidden")) {
        navbarLogo.show(200);
        $('#nvLogo').css('display', 'flex');
        $('#sideLogo').css('display', 'none');
        sidebarLogo.hide();
    } else {
        $('#nvLogo').css('display', 'none');
        navbarLogo.hide();
        sidebarLogo.show(200);
        $('#sideLogo').css('display', 'flex');
    }

});