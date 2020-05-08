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
    $(".headerDate").hide();
});