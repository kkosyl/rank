﻿$(document).ready(function () {
    var str = location.href.toLowerCase();

    $(".nav li a").each(function () {
        if (str.indexOf(this.href.toLowerCase()) > -1) {
            $("li.highlight").removeClass("highlight");
            $(this).parent().addClass("highlight");
        }

    });
})