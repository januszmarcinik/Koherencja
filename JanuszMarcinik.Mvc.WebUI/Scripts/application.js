$(document).ready(function () {

    $(".clicable-action").click(function () {
        window.location = $(this).data("href");
    });

})