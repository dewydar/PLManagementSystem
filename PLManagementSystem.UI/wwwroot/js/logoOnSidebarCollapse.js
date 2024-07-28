let navbarLogo = $('.brand-link-navbar');
let sidebarLogo = $('.brand-link-sidebar');
$(document).on('collapsed.lte.pushmenu', function () {

    if (sidebarLogo.is(":visible")) {
        sidebarLogo.hide()
        navbarLogo.show(200)
    }

});

$(document).on('shown.lte.pushmenu', function () {

    if (navbarLogo.is(":visible")) {
        navbarLogo.hide()
        sidebarLogo.show(200)
    }

});

$(".menu-btn").on("click", function () {

    if (navbarLogo.is(":hidden")) {
        navbarLogo.show(200);
        sidebarLogo.hide();
    } else {
        navbarLogo.hide();
        sidebarLogo.show(200);
    }

});