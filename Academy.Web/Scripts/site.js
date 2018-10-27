$(function() {
    $('datePicker').datetimepicker();

    $('.datePick').each(function () {
        var a = $(this).datepicker({
            dateFormat: "dd/ww/yy",
            defaultDate: new Date($(this).val())
        });
    });

    $('#acceptApplicant').attr("disbled", true);
    $('#rejectApplicant').attr("disbled", true);
});


