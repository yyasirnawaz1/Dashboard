var FormRender = {
    renderedForm: null,
    renderData:null,

    loadRendering: function () {
        var officeId = $('#hid_os').val();
        var formId = $('#hid_fid').val();
        var DATAURL = "/Form/GetPrivateDesign?oid=" + officeId + "&fid=" + formId;
        $.ajax({
            type: "GET",
            url: DATAURL,
            success: function (response) {
                if (response !== null) {
                    var designdata = response.Content;
                    console.log(designdata);
                    FormRender.renderedForm = $('#render-container').formRender({
                        formData: FormRender.removeSelectedTitles(response.Content)
                    });

                    FormRender.renderData = response.Content;
                } else {
                    alert("Something went wrong");
                }
            },
            failure: function (response) {
                //alert(response.responseText);
                alert("Request Not Completed");
            },
            error: function (response) {
                //alert(response.responseText);
                alert("Invalid Form Request");
            }
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
        Layout.showLoader();
        $.ajax({
            type: "POST",
            url: "/Form/SaveAnswer",
            async: true,
            data: {
                //data: JSON.stringify(content)
                Office_Sequence: $('#hid_os').val(),
                PatientNumber: $('#hid_pid').val(),
                FormID: $('#hid_fid').val(),
                Description: $('#txtformname').val(),
                Content: JSON.stringify($('#render-container').formRender('userData'))
            },
            success: function (response) {
                Layout.hideLoader();
                if (response !== null) {

                    alert(response);

                } else {
                    alert("Something went wrong");
                }
            },
            failure: function (response) {
                //alert(response.responseText);
                Layout.hideLoader();
                alert("Request Not Completed");
            },
            error: function (response) {
                //alert(response.responseText);
                Layout.hideLoader();
                alert("Invalid Form Request");
            }
        });

    }
}


jQuery(document).ready(function ($) {
    //var code = window.location.search.split('v=')[1];
    FormRender.loadRendering();


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