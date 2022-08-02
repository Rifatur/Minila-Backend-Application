// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    var dataElement = $('#dataElement');
    $('button[data-bs-toggle="modal"]').click(function (event) {
        var url = $(this).data('url');
        $.get(url).done(function (data) {
            dataElement.html(data);
            dataElement.find('.modal').modal('show');
        })
    } )
})