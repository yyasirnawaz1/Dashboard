var NewsLetter = {
    OfficeId: null,

    loadOfficeDropdown: function () {
        var officeDATAURL = window.location.origin + "/Home/GetOffices";
        $.getJSON(officeDATAURL, function (data) {
            var items = '<option>Select an Office</option>';
            $.each(data, function (i, offices) {
                items += "<option value='" + offices.Id + "'>" + offices.ClinicName + "</option>";
            });
            $('#ddlNewsLetterOffice').html(items);
            $('#ddlNewsLetterOffice').selectpicker('refresh');
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
        if (NewsLetter.OfficeId != null && NewsLetter.OfficeId > -1) {
            if (id == 'newsLetterTemplates') {
                $("#nodataDiv").hide();
                PreNewsLetterControls.defaultLoad(); //private newsLetter.js file
                $("#tab-newsLetterTemplates").show();
                $("#newsLetterDesigner").css('display', '');
                $("#tab-newsLetterDesigner > a").click();
            }
            else if (id == 'newsLetterUserTemplates') {
                $("#nodataDiv").hide();
                UserNewsLetterControls.defaultLoad();
                Newsletter.loadSystemTemplates();
                $("#tab-newsLetterUserTemplates").show();
                $("#newsLetterUserTemplates").css('display', '');
                $("#tab-newsLetterUserTemplates > a").click();
            }
        }
        else {
            alert('please select an office first');
        }
    },

    newsLetterOfficeDropDownChange: function () {

        var officeId = $("#ddlNewsLetterOffice").val();
        if (officeId == '-1') {
            //show layout
            $("#nodataDiv").show();
            $("#newsLetterDesigner").hide();
            $("#definedTag").hide();
            $("#newsLetters").hide();
            $("#reports").hide();


        } else {
            $("#nodataDiv").hide();
            NewsLetter.OfficeId = officeId;

            $("#tab-newsLetterUserTemplates").show();
            $("#tab-newsLetterUserTemplates > a").click();

            //PreNewsLetterControls.defaultLoad();
            UserNewsLetterControls.defaultLoad();
            Newsletter.loadSystemTemplates();
        }
    }
}

$(document).ready(function () {
    NewsLetter.loadOfficeDropdown();
    //NewsLetter.loadUserData();
});