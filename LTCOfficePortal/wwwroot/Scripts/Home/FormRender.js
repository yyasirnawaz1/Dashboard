var FormRender = {
    renderedForm: null,
    renderData: null,
    Patient: null,
    Address: null,
    PatientPhone: null,
    Tags: null,
    FormContent: null,

    loadRendering: function () {
        var officeSequence = $('#hid_os').val();
        var formId = $('#hid_fid').val();
        var AP = $('#hid_ac').val();
        var PID = $('#hid_pid').val();

        Common.showLoader();

        $.when(
            FormRender.loadTags()
            , FormRender.loadDesgin(officeSequence, formId)
            , FormRender.loadPatientDetail(officeSequence, PID)
            , FormRender.loadAddress(officeSequence, PID)
            , FormRender.loadPhoneDetail(officeSequence, PID))
            .done(function (a1, a2, a3, a4, a5) {
                Common.hideLoader();

                for (var i = 0; i < FormRender.FormContent.length; i++) {
                    var item = FormRender.FormContent[i];
                    var tag = FormRender.Tags.filter(x => x.TagID == item.TagId);

                    if (tag != null && tag != '' && tag.length > 0) {
                        if (tag[0].DataField != "" && tag[0].DataField != null) {

                            if (tag[0].TagType == 6)
                                FormRender.TagToData(item.name, tag[0].DataField);

                        }
                    }
                }
            });
    },

    loadTags: function () {
        return $.get("/Home/GetFormsPublicTags", function (data) {
            FormRender.Tags = data;
        });
    },

    loadDesgin: function (officeSequence, formId) {
        var DATAURL = "/Home/GetPrivateDesignForm?oid=" + officeSequence + "&fid=" + formId;
        return $.ajax({
            type: "GET",
            url: DATAURL,
            success: function (response) {
                Common.hideLoader();
                if (response !== null) {
                    var designdata = response.Content;
                    try {
                        FormRender.FormContent = JSON.parse(designdata);
                    } catch (e) {

                    }
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
                Common.hideLoader();
                alert("Request Not Completed");
            },
            error: function (response) {
                Common.hideLoader();
                alert("Invalid Form Request");
            }
        });
    },

    loadPatientDetail: function (officeSequence, PID) {
        return $.ajax({
            type: "GET",
            url: "/Home/GetPatientDetail?OfficeSequence=" + officeSequence + "&PatientNumber=" + PID,
            success: function (response) {
                FormRender.Patient = response;

            },
            failure: function (response) {

                alert("Request Not Completed");
            },
            error: function (response) {
                //alert(response.responseText);
                alert("Invalid Form Request");
            }
        });
    },

    loadAddress: function (officeSequence, PID) {
        return $.ajax({
            type: "GET",
            url: "/Home/GetPatientAddress?OfficeSequence=" + officeSequence + "&PatientNumber=" + PID,
            success: function (response) {
                FormRender.Address = response;

            },
            failure: function (response) {
                alert("Request Not Completed");
            },
            error: function (response) {
                //alert(response.responseText);
                alert("Invalid Form Request");
            }
        });
    },

    loadPhoneDetail: function (officeSequence, PID) {
        return $.ajax({
            type: "GET",
            url: "/Home/GetPatientPhone?OfficeSequence=" + officeSequence + "&PatientNumber=" + PID,
            success: function (response) {
                FormRender.PatientPhone = response;

            },
            failure: function (response) {
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

        var result = $('#render-container').formRender('userData');
        $.each(result, function (index, item) {
            if (item.type == "signature") {
                var list = [];
                list.push($('#input-' + item.name).val());
                item.userData = list;
            }
        });
        //Common.showLoader();
        var content = JSON.stringify(result);
        console.log(content);
        var FID = $('#hid_fid').val();
        var PID = $('#hid_pid').val();
        var OS = $('#hid_os').val();
        var type = 0;

        //Common.showLoader();
        $.post("/Home/SaveSurveyAndForm", { FormID: FID, PatientNumber: PID, Office_Sequence: OS, Type: type, Content: content, IsSurveyForm:false }, function (result) {
            $("btnSubmit").hide();
            // Common.hideLoader();

            alert('Data Successfully Added');
            $("#logoutButton").click();
            //  window.location.href = '/account/logout';
            //$('#modalFormSubmit').modal('hide');
        });
    },

    TagToData: function (id, content) {
        try {
            var data = content.split('.');
            if (data.length > 1) {

                var tableName = data[0];
                var columnName = data[1];

                var value = '';
                if (tableName.toLowerCase().indexOf('patient') > -1 && FormRender.Patient.length > 0) {
                    value = FormRender.Patient[0][columnName];

                    if (columnName.toLowerCase() == 'birthdate') {
                        var extractDate = value.split(' ');
                        var dateSplit = extractDate[0].split('/');

                        if (dateSplit[0].length > 2) {
                            if (dateSplit[1].length == 1) {
                                dateSplit[1] = "0" + dateSplit[1];
                            }
                            if (dateSplit[2].length == 1) {
                                dateSplit[2] = "0" + dateSplit[1];
                            }
                            value = dateSplit[0] + '-' + dateSplit[2] + '-' + dateSplit[1];
                        } else {
                            if (dateSplit[1].length == 1) {
                                dateSplit[1] = "0" + dateSplit[1];
                            }
                            if (dateSplit[0].length == 1) {
                                dateSplit[0] = "0" + dateSplit[0];
                            }
                            value = dateSplit[2] + '-' + dateSplit[1] + '-' + dateSplit[0];
                        }
                    }
                } else if (tableName.toLowerCase().indexOf('address') > -1 && FormRender.Address.length > 0) {
                    value = FormRender.Address[0][columnName];
                } else if (tableName.toLowerCase().indexOf('patientphone') > -1 && FormRender.PatientPhone.length > 0) {
                    value = FormRender.PatientPhone[0][columnName];
                } else if (tableName.toLowerCase().indexOf('code') > -1 && FormRender.Address.length > 0) {
                    value = FormRender.Address[0][columnName];
                }
                $("#" + id).val(value);
            }
        } catch (e) {

        }
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
                var el = this;

                $('#' + el.config.name).rateYo({

                    onSet: function (rating, rateYoInstance) {

                        $(rateYoInstance).next().val(rating);
                        el.config.ratingData = rating;
                        el.rawConfig.ratingData = rating;
                        el.config.value = rating;
                    },
                    ratingData: 5,
                    rating: el.config.ratingData || 5,
                    numStars: el.config.noStars || 5,

                    fullStar: true,
                });

            }
            //onRender() {
            //    let value = this.config.value || 3.6;
            //    $('#' + this.config.name).rateYo({ rating: value });
            //}
        }
        controlClass.register('starRating', controlStarRating);
        return controlStarRating;
    });
    window.fbControls.push(function (controlClass) {
        class controlSignature extends controlClass {
            build() {
                return '<div><input style="display:none" id= "input-' + this.config.name + '" /><div id="' + this.config.name + '"></div></div><p style="clear: both;"><button id="clear' + this.config.name + '">Clear</button></p>';
            }
            onRender() {
                
                if (this.config.userData) {
                    $('#' + this.config.name).val(this.config.userData[0]);
                }
                var signature = $('#' + this.config.name);
                var input = $('#input-' + this.config.name);

                signature.signature({
                    change: function (event, ui) {
                        
                        //signature.signature('toJSON');
                        input.val( signature.signature('toSVG') );
                    }
                });

                var signature = $('#' + this.config.name).signature();
                $('#clear' + this.config.name).click(function () {
                    signature.signature('clear');
                });
                $('#json' + this.config.name).click(function () {
                    alert(signature.signature('toJSON'));
                });
                $('#svg' + this.config.name).click(function () {
                    alert(signature.signature('toSVG'));
                });
            }

        }
        controlClass.register('signature', controlSignature);
        return controlSignature;
    });

});