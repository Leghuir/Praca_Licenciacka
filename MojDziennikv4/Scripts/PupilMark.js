console.log("zaladowalo sie pupil");
$('#Classpicker').change(function () {
    console.log("zmiana klasy");
    if ($('#Subjectpicker').val() != '')
    {
        console.log("zmiana obu");
        var subject = $('#Subjectpicker').val();
        var schoolClass = $(this).val();

        /* Request the partial view with .get request. */
        $.get('/TeacherOptions/PartialPupilMarks/?' + "SchoolClass=" + schoolClass + "&Subject=" + subject, function (data) {

            /* data is the pure html returned from action method, load it to your page */
            if (data != null)
                $('#partialPlaceHolder').html(data);
            /* little fade in effect */
            $('#partialPlaceHolder').fadeIn('fast');
        });
    }
});
$('#Subjectpicker').change(function () {
    console.log("zmiana przedmiotu");
    if ($('#Classpicker').val() != '')
    {
        console.log("zmiana obu");
    /* Get the selected value of dropdownlist */
        var subject = $(this).val();
        var schoolClass = $('#Classpicker').val();

    /* Request the partial view with .get request. */
        $.get('/TeacherOptions/PartialPupilMarks/?' + "SchoolClass=" + schoolClass + "&Subject=" + subject, function (data) {

        /* data is the pure html returned from action method, load it to your page */
        if (data != null)
            $('#partialPlaceHolder').html(data);
        /* little fade in effect */
        $('#partialPlaceHolder').fadeIn('fast');
    
    });
    }
});