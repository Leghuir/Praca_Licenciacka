console.log("zaladowalo sie");
$('#myDropDown').change(function () {

     /* Get the selected value of dropdownlist */
     var selectedID = $(this).val();

    /* Request the partial view with .get request. */
    $.get('/TeacherOptions/PartialAddMakrs/?' + "Klasa=" + selectedID, function (data) {

         /* data is the pure html returned from action method, load it to your page */
         if(data!=null)
         $('#partialPlaceHolder').html(data);
         /* little fade in effect */
         $('#partialPlaceHolder').fadeIn('fast');
     });

});