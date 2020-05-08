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
});