

var HomeView = function () {

    return {

        init: function () {

        },
        selectSubscriber: function () {
            $('#entryModel').modal('show');
        },
        newSubscription: function () {
            $('#subscriberModel').modal('show');
        },
        saveForm: function () {

            var form = new Object();
            var screenName = $("input[name=UName]:checked").val();
            if (screenName == "userName") {
                form.StaffScreener = 'userName';
            } else {
                if ($("#staffScreener").val() == "") {
                    ltcApp.warningMessage(null, "Please enter staff screener");
                    return;
                } else {
                    form.StaffScreener = $("#staffScreener").val();
                }

            }

          
            if ($("#patientName").val() == "") {
                ltcApp.warningMessage(null, "Please enter patient name");
                return;
            } else {
                form.PatientName = $("#patientName").val();
            }

            if ($("#patientAge").val() == "") {
                ltcApp.warningMessage(null, "Please enter patient age");
                return;
            } else {
                form.PatientAge = $("#patientAge").val();
            }
            if ($("#whoAns").val() == "") {
                ltcApp.warningMessage(null, "Please provide name");
                return;
            } else {
                form.WhoAnswer = $("#whoAns").val();
            }

            form.IsPreScreen = $('input[name="prescreen"]').is(":checked");
            form.IsPreScreenDate = '10/06/2020';
            form.InPerson = $('input[name="inperson"]').is(":checked");
            form.InPersonDate = '10/06/2020';


            if (form.IsPreScreen) {
                if ($("input[name=PreScreenAnswer1]:checked").val() == null ||
                    $("input[name=PreScreenAnswer2]:checked").val() == null ||
                    $("input[name=PreScreenAnswer3]:checked").val() == null ||
                    $("input[name=PreScreenAnswer4]:checked").val() == null ||
                    $("input[name=PreScreenAnswer5]:checked").val() == null ||
                    $("input[name=PreScreenAnswer6]:checked").val() == null ||
                    $("input[name=PreScreenAnswer7]:checked").val() == null ||
                    $("input[name=PreScreenAnswer8]:checked").val() == null ||
                    $("input[name=PreScreenAnswer9]:checked").val() == null) {
                    ltcApp.warningMessage(null, "Please select the options for Pre-Screen");
                    return;
                }
                form.PreScreen = new Object();
                form.PreScreen.Answer1 = $("input[name=PreScreenAnswer1]:checked").val();
                form.PreScreen.Answer2 = $("input[name=PreScreenAnswer2]:checked").val();
                form.PreScreen.Answer3 = $("input[name=PreScreenAnswer3]:checked").val();
                form.PreScreen.Answer4 = $("input[name=PreScreenAnswer4]:checked").val();
                form.PreScreen.Answer5 = $("input[name=PreScreenAnswer5]:checked").val();
                form.PreScreen.Answer6 = $("input[name=PreScreenAnswer6]:checked").val();
                form.PreScreen.Answer7 = $("input[name=PreScreenAnswer7]:checked").val();
                form.PreScreen.Answer8 = $("input[name=PreScreenAnswer8]:checked").val();
                form.PreScreen.Answer9 = $("input[name=PreScreenAnswer9]:checked").val();

            }

            if (form.InPerson) {
                if ($("input[name=InPersonScreenAnswer1]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer2]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer3]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer4]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer5]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer6]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer7]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer8]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer9]:checked").val() == null) {
                    ltcApp.warningMessage(null, "Please select the options for InPerson-Screen");
                    return;
                }
                form.InPersonScreen = new Object();
                form.InPersonScreen.Answer1 = $("input[name=InPersonScreenAnswer1]:checked").val();
                form.InPersonScreen.Answer2 = $("input[name=InPersonScreenAnswer2]:checked").val();
                form.InPersonScreen.Answer3 = $("input[name=InPersonScreenAnswer3]:checked").val();
                form.InPersonScreen.Answer4 = $("input[name=InPersonScreenAnswer4]:checked").val();
                form.InPersonScreen.Answer5 = $("input[name=InPersonScreenAnswer5]:checked").val();
                form.InPersonScreen.Answer6 = $("input[name=InPersonScreenAnswer6]:checked").val();
                form.InPersonScreen.Answer7 = $("input[name=InPersonScreenAnswer7]:checked").val();
                form.InPersonScreen.Answer8 = $("input[name=InPersonScreenAnswer8]:checked").val();
                form.InPersonScreen.Answer9 = $("input[name=InPersonScreenAnswer9]:checked").val();

            }

            form.AdditionalInformation = $("#txtAdditionalInformation").val();

            //convert object to json string
            var string = JSON.stringify(form);

            //convert string to Json Object

            console.log(string);


            //   if (template != null && article != null) {
            var IdValue = $("#IdValue").val();

            var data = {
                ID: IdValue,
                StorageInJson: string,
                IsPreScreen: form.IsPreScreen,
                IsInPersonScreen: form.InPerson,
            };


            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Home/Upsert',
                success: function (data) {
                    ltcApp.successMessage("Success", 'Form has been saved');
                   // Subscription.refresh();
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error saving the form');

                },
                complete: function () {

                    //$('#subscriberModel').modal('hide');

                }
            })



        },

    };

}();
 
$("input[name=prescreen]").change(function () {
    $(".PreScreen").attr('disabled', false);
    $("#inPerson").attr('disabled', false);
    var d = new Date();
    $("#PreScreenDate").html((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear());
    
})
$("input[name=inperson]").change(function () {
    $(".InPerson").attr('disabled', false);
    var d = new Date();
    $("#InPersonDate").html((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear());
 
})
HomeView.init();

$(document).ready(function () {


});


