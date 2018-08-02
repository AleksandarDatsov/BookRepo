$(document).ready(function () {
    $('#home-index-clear-fields-button-id').click(function (event) {
        event.preventDefault();

        $('#home-index-text-box-id').val("");
        $('#js-search-result').html("");
        $('#home-index-drop-down-list-id').get(0).selectedIndex = 0;
        $('#home-index-check-box-id').prop('checked', false);
    });

    $("#myLink").click(function (e) {
        e.preventDefault();
        $.ajax({

            url: $(this).attr("href"),
            success: function () {
                alert("Value Added");
            }

        });
    });

    $('#home-index-search-button-id').click(function (event) {
        event.preventDefault();
        var button = $(this),
            form = button.closest('form'),
            action = form.attr('action');

        $.get(action, form.serialize(), function (result) {
            $("#js-search-result").html(result);
        });
    });

    $('#layout-button-id').click(function (event) {
    });
    $('#books-create-date-picker-id').datepicker();

    $('#books-create-is-in-stock-checkbox-id').change(function () {
        $('#books-create-numbers-in-stock-div-id').slideToggle('slow');
        $('#books-create-textbox-numbers-in-stock-id').val("0");
    });
});