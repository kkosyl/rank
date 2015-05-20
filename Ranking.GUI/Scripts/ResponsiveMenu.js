function responsiveMobileMenu() {
    $('.top-menu').each(function () {

        $(this).children('ul').addClass('top-menu-main-list');	// mark main menu list


        var $style = $(this).attr('data-menu-style');	// get menu style
        if (typeof $style == 'undefined' || $style == false) {
            $(this).addClass('graphite'); // set graphite style if style is not defined
        }
        else {
            $(this).addClass($style);
        }


        /* 	width of menu list (non-toggled) */

        var $width = 0;
        $(this).find('ul li').each(function () {
            $width += $(this).outerWidth();
        });

        // if modern browser

        if ($.support.leadingWhitespace) {
            $(this).css('max-width', $width * 1.05 + 'px');
        }
            // 
        else {
            $(this).css('width', $width * 1.05 + 'px');
        }

    });
}
function getMobileMenu() {

    /* 	build toggled dropdown menu list */

    $('.top-menu').each(function () {
        var menutitle = $(this).attr("data-menu-title");
        if (menutitle == "") {
            menutitle = "Menu";
        }
        else if (menutitle == undefined) {
            menutitle = "Menu";
        }
        var $menulist = $(this).children('.top-menu-main-list').html();
        var $menucontrols = "<div class='top-menu-toggled-controls'><div class='top-menu-toggled-title'>" + menutitle + "</div><div class='top-menu-button'><span>&nbsp;</span><span>&nbsp;</span><span>&nbsp;</span></div></div>";
        $(this).prepend("<div class='top-menu-toggled top-menu-closed'>" + $menucontrols + "<ul>" + $menulist + "</ul></div>");

    });
}

function adaptMenu() {

    /* 	toggle menu on resize */

    $('.top-menu').each(function () {
        var $width = $(this).css('max-width');
        $width = $width.replace('px', '');
        if ($(this).parent().width() < $width * 1.05) {
            $(this).children('.top-menu-main-list').hide(0);
            $(this).children('.top-menu-toggled').show(0);
        }
        else {
            $(this).children('.top-menu-main-list').show(0);
            $(this).children('.top-menu-toggled').hide(0);
        }
    });

}

$(function () {

    responsiveMobileMenu();
    getMobileMenu();
    adaptMenu();

    /* slide down mobile menu on click */

    $('.top-menu-toggled, .top-menu-toggled .top-menu-button').click(function () {
        if ($(this).is(".top-menu-closed")) {
            $(this).find('ul').stop().show(300);
            $(this).removeClass("top-menu-closed");
        }
        else {
            $(this).find('ul').stop().hide(300);
            $(this).addClass("top-menu-closed");
        }

    });

});
/* 	hide mobile menu on resize */
$(window).resize(function () {
    adaptMenu();
});