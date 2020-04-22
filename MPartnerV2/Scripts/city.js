

        $(document).ready(function () {
            $("#Button1city").on("click", function (e) {
                var email = $("#Countries").val();
                if (email == 0) {
                    e.preventDefault();
                    alert("Please Select State Name.");

                }
                else {

                    var email = $("#Text1").val();
                    if (email == "") {
                        e.preventDefault();
                        alert("Please enter your City Name.");

                    }
                    else {

                        savenew();
                    }
                }
            });
        });

    function editcall() {
        $(document).ready(function () {
            $("#btnUpdate").on("click", function (e) {
                var id = $(this).attr("value")


                var email = $("#Select1").val();
                if (email == 0) {
                    e.preventDefault();
                    alert("Please Select State Name.");

                }
                else {

                    var email = $("#txtEmpName1").val();
                    if (email == "") {
                        e.preventDefault();
                        alert("Please enter your City Name.");

                    }
                    else {

                        updateRecord(id);
                    }
                }





            });
        });
    }
