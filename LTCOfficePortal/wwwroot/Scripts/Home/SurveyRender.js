var SurveyRender = {
    renderedForm: null,
    renderData: null,
    Patient: null,
    Address: null,
    PatientPhone: null,
    Tags: null,

    loadRendering: function () {
        var officeSequence = $('#hid_os').val();
        var formId = $('#hid_fid').val();
        var AC = $('#hid_ac').val();
        var PID = $('#hid_pid').val();

        Common.showLoader();
        var DATAURL = "/Home/GetPrivateDesignSurvey?OfficeSequence=" + officeSequence + "&FormId=" + formId + "&PatientNumber=" + PID + "&AppointmentCounter=" + AC;
        $.ajax({
            type: "GET",
            url: DATAURL,
            success: function (response) {
                Common.hideLoader();
                if (response !== null) {
                    var designdata = response.Content;
                    console.log(designdata);
                    SurveyRender.renderedForm = $('#render-container').formRender({
                        formData: SurveyRender.removeSelectedTitles(response.Content)
                    });

                    SurveyRender.renderData = response.Content;
                } else {
                    alert("Something went wrong");
                }
            },
            failure: function (response) {
                Common.hideLoader();
                alert("Request Not Completed");
            },
            error: function (response) {
                Common.hideLoader();
                alert("Invalid Survey Request");
            }
        });


        $.ajax({
            type: "GET",
            url: "/Home/GetPatientDetail?OfficeSequence=" + officeSequence + "&PatientNumber=" + PID,
            //url: "/Home/GetPatientDetail?OfficeSequence=" + officeSequence + "&FormId=" + formId + "&PatientNumber=" + PID + "&AppointmentCounter=" + AP,
            success: function (response) {
                SurveyRender.Patient = response;

            },
            failure: function (response) {
                alert("Request Not Completed");
            },
            error: function (response) {
                //alert(response.responseText);
                alert("Invalid Survey Request");
            }
        });


        $.ajax({
            type: "GET",
            url: "/Home/GetPatientAddress?OfficeSequence=" + officeSequence + "&PatientNumber=" + PID ,
            success: function (response) {
                SurveyRender.Address = response;

            },
            failure: function (response) {
                alert("Request Not Completed");
            },
            error: function (response) {
                //alert(response.responseText);
                alert("Invalid Survey Request");
            }
        });


        $.ajax({
            type: "GET",
            url: "/Home/GetPatientPhone?OfficeSequence=" + officeSequence + "&PatientNumber=" + PID ,
            success: function (response) {
                SurveyRender.PatientPhone = response;
            },
            failure: function (response) {
                alert("Request Not Completed");
            },
            error: function (response) {
                //alert(response.responseText);
                alert("Invalid Survey Request");
            }
        });

        $.get("/Home/GetSurveyPublicTags", function (data) {
            SurveyRender.Tags = data;
        });
    },

    removeSelectedTitles: function (content) {
        var json = JSON.parse(content);
        for (i = 0; i < json.length; i++)
            if (json[i].type == 'LineSeprator' || json[i].type == 'NewLine')
                json[i].label = '';
        return JSON.stringify(json)
    },

    saveRendering: function () {
        var content = JSON.stringify($('#render-container').formRender('userData'));
        var AP = $('#hid_ap').val();
        var FID = $('#hid_fid').val();
        var PID = $('#hid_pid').val();
        var OS = $('#hid_os').val();
        var type = 1;

        Common.showLoader();
        $.post("/Home/SaveSurveyAndForm", { AppointmentCounter: AP, FormID: FID, PatientNumber: PID, Office_Sequence: OS, Type: type, Content: content }, function (result) {
            $("btnSubmit").hide();
            Common.hideLoader();
            alert('Data Successfully Added');
            window.location.href = '/account/logout';
        });
    }
}


jQuery(document).ready(function ($) {
    //var code = window.location.search.split('v=')[1];
    SurveyRender.loadRendering();

    if (!window.fbControls) window.fbControls = new Array();
    window.fbControls.push(function (controlClass) {
        class controlLineSeprator extends controlClass {
            build() {
                return '<hr>';
            }
        }
        controlClass.register('LineSeprator', controlLineSeprator);
        return controlLineSeprator;
    });
    window.fbControls.push(function (controlClass) {
        class controlNewLine extends controlClass {
            build() {
                return '<br/>';
            }
        }
        controlClass.register('NewLine', controlNewLine);
        return controlNewLine;
    });
    window.fbControls.push(function (controlClass) {
        class controlStarRating extends controlClass {
            build() {
                return this.markup('span', null, { id: this.config.name });
            }
            onRender() {
                let value = this.config.value || 3.6;
                $('#' + this.config.name).rateYo({ rating: value });
            }
        }
        controlClass.register('starRating', controlStarRating);
        return controlStarRating;
    });
});