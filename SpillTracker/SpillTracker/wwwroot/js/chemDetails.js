$("dt").hover(
    function () {
        $(this).append($("<span>*</span>"));
        $(this).prepend($("<span>*</span>"));
    }, function () {
        $(this).find("span").first().remove();
        $(this).find("span").last().remove();
    }
);

$("dt.fa").hover(function () {
    $(this).fadeOut(100);
    $(this).fadeIn(500);
});