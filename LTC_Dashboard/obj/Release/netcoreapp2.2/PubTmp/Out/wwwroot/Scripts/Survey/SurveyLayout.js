var Survey = {
    OfficeId: null,

    loadOfficeDropdown: function () {
        var officeDATAURL = window.location.origin + "/Home/GetOffices";
        
        $.getJSON(officeDATAURL, function (data) {
        
            var items = '<option>Select an Office</option>';
            $.each(data, function (i, offices) {
                items += "<option value='" + offices.Id + "'>" + offices.ClinicName + "</option>";
            });
            $('#ddlSurveyOffice').html(items);
            $('#ddlSurveyOffice').selectpicker('refresh');
        });
    },
    loadUserData: function () {
        var DATAURL = window.location.origin + "/Account/GetUserdata";
        $.ajax({
            type: "GET",
            url: DATAURL,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                debugger;
                if (response !== null) {
                    $('#txtSalutation').val(response.Salutation);
                    $('#txtInitials').val(response.Initials);
                    $('#txtFirstName').val(response.FirstName);
                    $('#txtLastName').val(response.LastName);
                    $('#txtPhone').val(response.Phone);
                    $('#txtFax').val(response.Fax);
                    $('#txtAddressLine1').val(response.AddressLine1);
                    $('#txtAddressLine2').val(response.AddressLine2);
                    $('#txtAddressLine3').val(response.AddressLine3);
                    $('#txtCity').val(response.City);
                    $('#txtProvince').val(response.Province);
                    $('#txtCountry').val(response.Country);
                    $('#txtPostalCode').val(response.PostalCode);
                } else {
                    //alert("Something went wrong");
                }
            },
            failure: function (response) {
                //alert(response.responseText);
            },
            error: function (response) {
                //alert(response.responseText);
            }
        });
    },

    loadMenuItem: function (id) {
        if (Survey.OfficeId != null && Survey.OfficeId > -1) {
            if (id == 'surveyDesigner') {

                $("#nodataDiv").hide();
                SurveyControls.defaultLoad(); //private survey.js file
                $("#tab-surveyDesigner").show();
                $("#surveyDesigner").css('display', '');
                $("#tab-surveyDesigner > a").click();

            }
            else if (id == 'definedTag') {
                $("#nodataDiv").hide();
                PrivateTag.defaultLoad();
                $("#tab-definedTag").show();
                $("#definedTag").css('display', '');
                $("#tab-definedTag > a").click();
            }
            else if (id == 'surveys') {
                $("#nodataDiv").hide();
                SurveyAnswers.defaultLoad();
                $("#tab-surveys").show();
                $("#surveys").css('display', '');
                $("#tab-surveys > a").click();
            }
            else if (id == 'reports') {
                $("#nodataDiv").hide();
                SurveyReport.defaultLoad();
                $("#tab-reports").show();
                $("#reports").css('display', '');
                $("#tab-reports > a").click();
            }
        }
        else {
            alert('please select an office first');
        }
    },

    surveyOfficeDropDownChange: function () {

        var officeId = $("#ddlSurveyOffice").val();
        if (officeId == '-1') {
            //show layout
            $("#nodataDiv").show();
            $("#surveyDesigner").hide();
            $("#definedTag").hide();
            $("#surveys").hide();
            $("#reports").hide();


        } else {
            $("#nodataDiv").hide();
            Survey.OfficeId = officeId;

            //
            $("#tab-surveyDesigner").show();
            $("#tab-surveyDesigner > a").click();

            SurveyControls.defaultLoad();

            if (SurveyAnswers.isMenuOpen)
                SurveyAnswers.defaultLoad();

            //if (PrivateTag.isLoaded)
            //    PrivateTag.defaultLoad();


            if (SurveyReport.isLoaded)
                SurveyReport.defaultLoad();


        }
    }
}

$(document).ready(function () {
    Survey.loadOfficeDropdown();
    Survey.loadUserData();
});