var PublicSurveyControls = {

    selectedSurveyID: 0,
    formBuilder: null,
    isLoaded: false,

    openSurveyModal: function () {
        $('#PublicSurveyloginModal').modal('show');
        PublicSurveyControls.initializeFormBuilder();
    },

    initializeFormBuilder: function () {

        PublicSurveyControls.initializeSurvey();

        $.get("/Survey/GetPublicTags", function (data) {
            var selectoptions = { "0": "Select Tag" };
            for (var i = 0; i < data.length; i++) {
                selectoptions[data[i].TagID] = data[i].Description;
            }
            PublicSurveyControls.initializeFormBuilderAfterGetTags(selectoptions);
        });
    },

    initializeFormBuilderAfterGetTags: function (selectoptions) {
        fields = [{
            label: 'Star Rating',
            attrs: {
                type: 'starRating'
            },
            icon: '🌟'
        },
        {
            label: 'Line Seperator',
            attrs: {
                type: 'LineSeprator',
                className: "form-control"
            },
            icon: '🔖'
        },
        {
            label: 'New Line',
            attrs: {
                type: 'NewLine',
                className: "form-control"
            },
            icon: '📥'
        }];
        templates = {
            starRating: function (fieldData) {
                return {
                    field: '<span id="' + fieldData.name + '">',
                    onRender: function () {
                        $(document.getElementById(fieldData.name)).rateYo({
                            rating: 3.6
                        });
                    }
                };
            },
            LineSeprator: function () {
                return {
                    field: '<hr>',
                    onRender: function () {
                    }
                };
            },
            NewLine: function () {
                return {
                    field: '<br/>',
                    onRender: function () {
                    }
                };
            }
        };
        typeUserAttrs = {
            autocomplete: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            button: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            "checkbox-group": {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            file: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            header: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            hidden: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            number: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            paragraph: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            "radio-group": {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            select: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            text: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            textarea: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            LineSeprator: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            NewLine: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            starRating: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            },
            date: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            }, text: {
                TagId: {
                    label: 'Tag',
                    options: selectoptions
                }
            }
        };

        $("#fbPublicSurvey-editor").html('');
        var container = (document.getElementById('fbPublicSurvey-editor'));
        PublicSurveyControls.formBuilder = $(container).formBuilder({
            templates,
            fields,
            typeUserAttrs
        });

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
    },

    reInitializeFormBuilder: function () {

    },

    initializeSurvey: function () {
        if (!PublicSurveyControls.isLoaded) {

            PublicSurveyControls.isLoaded = true;

            PublicSurveysTable = $('#tblPublicSurvey').DataTable({
                autoWidth: false,
                select: {
                    style: 'single'
                },
                responsive: true,
                paging: true,
                ordering: true,
                info: true,
                searching: true,
                ajax: {
                    url: window.location.origin + '/Survey/GetPublicSurveys',
                    dataSrc: ""
                },
                dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
                columnDefs: [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    {
                        "targets": [2],
                        "data": null,
                        "render": function (data) {
                            var duplicateButton = '<a href="javascript:;" onclick="PublicSurveyControls.duplicateForm(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                            var viewButton = '<a href="javascript:;" onclick="PublicSurveyControls.renderForm(' + data + ',true);" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                            var editButton = '<a href="javascript:;" onclick="PublicSurveyControls.editForm(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                            var deleteButton = '<a href="javascript:;" onclick="PublicSurveyControls.deleteForm(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click"><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                            var allButtons = viewButton;
                            //if (UserPermissions.CanCreate) allButtons += duplicateButton;
                            //if (UserPermissions.CanEdit) allButtons += editButton;
                            if (UserPermissions.SurveyDelete) allButtons += deleteButton;

                            return allButtons;
                        }
                    }
                ],
                columns: [
                    { data: "FormID", width: "0%" },
                    { data: "Description", width: "80%" },
                    { data: "FormID", width: "20%" },
                ]
            });

            PublicSurveysTable.on('select', function (e, dt, type, indexes) {
                if (type === 'row') {
                    var data = PublicSurveysTable.row('.selected').data();
                    PublicSurveyControls.selectedSurveyID = data.FormID;

                    PublicSurveyControls.renderForm(data.FormID);
                }
            });

        } else {
            PublicSurveysTable.ajax.reload(null, false);
        }
    },

    saveSurveyDesign: function () {
        var content = PublicSurveyControls.formBuilder.actions.getData();
        Layout.showLoader();
        $.ajax({
            type: "POST",
            async: true,
            data: {
                FormID: $('#hdPublicsid').val(),
                Description: $('#txtPublicsurveyname').val(),
                Content: JSON.stringify(content)
            },
            url: window.location.origin + "/Survey/SavePublicDesign",
            success: function (data) {
                Layout.hideLoader();
                if (data.Success) {
                    alert("record saved.");
                    PublicSurveyControls.showSurveyList();
                    PublicSurveyControls.formBuilder.actions.clearFields();
                    $('#txtPublicsurveyname').val('');
                    $('#hdsPublicid').val('-1');
                    PublicSurveysTable.ajax.reload(null, false);

                }
                else {
                }
            },
            error: function () {
                Layout.hideLoader();
            }
        });
    },

    duplicateForm: function (id) {
        if (id > 0) {
            PublicSurveyControls.showDesigner();
            var record = PublicSurveysTable.data().filter(x => x.FormID == id);
            $("#txtPublicsurveyname").val("Copy of -" + record[0].Description);
            $('#hdsPublicid').val("-1");
            PublicSurveyControls.formBuilder.actions.setData(record[0].Content);
        } else {
        }
    },

    editForm: function (id) {
        if (id > 0) {
            PublicSurveyControls.showDesigner();
            var record = PublicSurveysTable.data().filter(x => x.FormID == id);
            PublicSurveyControls.formBuilder.actions.setData(record[0].Content);
            $("#txtPublicsurveyname").val(record[0].Description);
            $('#hdsPublicid').val(record[0].FormID);
            console.log(id);
            console.log(record[0].FormID);
        } else {
        }
    },

    deleteForm: function (id) {
        if (confirm('are you sure to delete this item')) {
            Layout.showLoader();
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    Id: id
                },
                url: window.location.origin + "/Survey/DeletePublicDesign",
                success: function (data) {
                    Layout.hideLoader();
                    PublicSurveysTable.ajax.reload(null, false);
                    alert('Item deleted.');
                },
                error: function (xhr, data, errorThrown) {
                    Layout.hideLoader();
                    alert('There was an error. Please try again later');
                }
            });

        }
    },

    renderForm: function (id) {
        if (id > 0) {
            var record = PublicSurveysTable.data().filter(x => x.FormID == id);

            var content = PublicSurveyControls.removeSelectedTitles(record[0].Content);

            Layout.renderFormDesignerInIframe(content, 'iframePreviewPublicSurvey');
                Layout.renderFormDesignerInIframe(content, 'iframeThePreview');
        }
    },

    createNewForm: function () {
        if (!UserPermissions.SurveyCreate) {
            alert('You do not have permissions to perform this action');
        }
        else {
            $("#txtPublicsurveyname").val('New Design');
            PublicSurveyControls.selectedSurveyID = 0;
            PublicSurveyControls.showDesigner();
            PublicSurveyControls.formBuilder.actions.clearFields();
        }
    },

    showSurveyList: function () {

        PublicSurveyControls.formBuilder.actions.clearFields();
        $(".publicSurveyList").show();
        $(".publicSurveyCreate").hide();
        //this.closeFullScreenPreview();
    },

    showDesigner: function () {

        PublicSurveyControls.formBuilder.actions.clearFields();
        $(".publicSurveyList").hide();
        $(".publicSurveyCreate").show();

    },

    removeSelectedTitles: function (content) {
        var json = JSON.parse(content);
        for (i = 0; i < json.length; i++)
            if (json[i].type == 'LineSeprator' || json[i].type == 'NewLine')
                json[i].label = '';
        return JSON.stringify(json)
    },
}

$(document).ready(function () {

    $(".publicSurveyCreate").hide();
    $(".panel-left").panelResizable({
        handleSelector: ".splitter",
        resizeHeight: false
    });


});
