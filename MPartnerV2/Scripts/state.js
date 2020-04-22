
 

        $(document).ready(function () {

            $("#Button1state").on("click", function (e) {
                var email = $("#txtstate").val();
                if (email == "") {
                    e.preventDefault();
                    alert("Please enter your State Name.");
                    return false;

                }
                else {
                    var stateN = $('#txtstate').val();


                    $.post('../../state/Savestate', { staten: stateN },
            function (data) { chkSave(data); });
                }


                function chkSave(abc) {
                    alert(abc);
                    if (abc == "session expire") {
                        location.href = '@Url.Action("login", "login")';
                    }
                    else {
                        show();
                        $('#txtstate').val('');
                    }
                }
            });
        });







 
      function editcall() {
          $(document).ready(function () {
              $("#btnUpdate").on("click", function (e) {
                  var id = $(this).attr("value")


                  var email = $("#txtEmpName").val();
                  if (email == "") {
                      e.preventDefault();
                      alert("Please enter state name.");
                      return false;
                  }
                  else {
                     
                      updateRecord(id)
                  }




                 
              });
          });
      }
