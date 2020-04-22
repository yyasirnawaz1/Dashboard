var SurveyControls = {

    selectedSurveyID: 0,
    isLoaded: false,
    isMenuOpen: false,

    initializeFormBuilder: function () {
        //$.post("/Survey/GetPrivateTags", { OfficeId: $("#ddlSurveyOffice").val() }, function (data) {
        //    var selectoptions = { "0": "Select Tag" };
        //    for (var i = 0; i < data.length; i++) {
        //        selectoptions[data[i].TagID] = data[i].Description;
        //    }
        //    SurveyControls.initializeFormBuilderAfterGetTags(selectoptions);
        //});

        $.get("/Survey/GetPublicTags", function (data) {
            var selectoptions = { "0": "Select Tag" };
            for (var i = 0; i < data.length; i++) {
                selectoptions[data[i].TagID] = data[i].Description;
            }
            SurveyControls.initializeFormBuilderAfterGetTags(selectoptions);
        });
    },

    initializeFormBuilderAfterGetTags: function (selectoptions) {
        fields = [{
            label: 'Signature',
            attrs: {
                type: 'signature'
            },
            icon: '✍️'
        },
{
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
            }, signature: function (fieldData) {
                return {
                    field: '<hr>',
                    onRender: function () {
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
            }, signature: {
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

        $("#fb-editor").html('');
        var container = (document.getElementById('fb-editor'));
        privateFormBuilder = $(container).formBuilder({
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
                            input.val(signature.signature('toSVG'));
                        }
                    });

                    var signature = $('#' + this.config.name).signature();
                    $('#clear' + this.config.name).click(function () {
                        signature.signature('clear');
                    });

                }

            }
            controlClass.register('signature', controlSignature);
            return controlSignature;
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

    saveSurveyDesign: function () {
        var content = privateFormBuilder.actions.getData();
        Layout.showLoader();
        $.ajax({
            type: "POST",
            async: true,
            data: {
                //data: JSON.stringify(content)
                FormID: $('#hdsid').val(),
                Description: $('#txtsurveyname').val(),
                Content: JSON.stringify(content),
                Office_Sequence: $("#ddlSurveyOffice").val()
            },
            url: window.location.origin + "/Survey/SaveDesign",
            success: function (data) {
                Layout.hideLoader();
                if (data.Success) {
                    privateFormBuilder.actions.clearFields();
                    $('#txtsurveyname').val('');
                    $('#hdsid').val('-1');
                    SurveyTable.ajax.reload(null, false);
                    SurveyControls.showSurveyList();
                }
                else {
                    //show error
                }
            },
            error: function (xhr, data, errorThrown) {
                Layout.hideLoader();
                //show error
            }
        });
    },

    duplicateForm: function (id) {
        if (id > 0) {
            SurveyControls.showDesigner();

            var record = SurveyTable.data().filter(x => x.FormID == id);
            $("#txtsurveyname").val("Copy of -" + record[0].Description);
            $('#hdsid').val("-1");
            privateFormBuilder.actions.setData(record[0].Content);
        } else {
            //show alert $('#renderSurveyForm').html('Select a survey from table.');
        }
    },

    editForm: function (id) {

        if (id > 0) {
            SurveyControls.showDesigner();

            var record = SurveyTable.data().filter(x => x.FormID == id);
            privateFormBuilder.actions.setData(record[0].Content);
            $("#txtsurveyname").val(record[0].Description);
            $('#hdsid').val(record[0].FormID);
            console.log(id);
            console.log(record[0].FormID);
            // move back to table view
            // show message that saved successfully for now add alert
        } else {
            //show alert $('#renderSurveyForm').html('Select a survey from table.');
        }
    },

    deleteForm: function (id) {
        if (confirm('are you sure to delete this item')) {
            //DeletePublicDesign
            Layout.showLoader();
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    Id: id
                },
                url: window.location.origin + "/Survey/DeleteDesign",
                success: function (data) {
                    Layout.hideLoader();
                    SurveyTable.ajax.reload(null, false);
                    alert('Item deleted.');
                },
                error: function (xhr, data, errorThrown) {
                    Layout.hideLoader();
                    alert('There was an error. Please try again later');
                }
            });
        }
    },

    duplicatePublicForm: function () {
        if (UserPermissions.SurveyDuplicate) {
            if (SurveyControls.selectedPublicSurveyID > 0) {
                SurveyControls.showDesigner();
                $("#publicSurveyModal").modal("hide");
                var record = PublicSurveyTable.data().filter(x => x.FormID == SurveyControls.selectedPublicSurveyID);
                $("#txtsurveyname").val("Copy of -" + record[0].Description);
                privateFormBuilder.actions.setData(record[0].Content);
            } else {
                alert('Select a survey from table.');
            }
        } else alert('You do not have permissions to perform this action');
    },

    renderForm: function (id, isFromPreviewButton) {
        if (id > 0) {
            var record = SurveyTable.data().filter(x => x.FormID == id);
            var content = SurveyControls.removeSelectedTitles(record[0].Content);

            Layout.renderFormDesignerInIframe(content, 'iframePreviewPrivateSurvey');
            Layout.renderFormDesignerInIframe(content, 'iframeThePreview');
        }
    },

    //closeFullScreenPreview: function () {
    //    $('#privateSurveyListToggleParent').addClass('toggleDivHidden');
    //    $('#privateSurveyListToggleParent .panel-left').show();
    //    $('#privateSurveyListToggleParent .panel-right').removeClass('col-lg-6 col-md-6 col-sm-6 col-xs-6');
    //    $('#btnPrivateSurveyToogle').show();
    //    $('#btnPrivateSurveyCreateNew').show();
    //    $('#btnPrivateSurveyShowTemplates').show();
    //    $('#btnPrivateSurveyCloseFullScreenPreview').hide();
    //},

    renderPublicForm: function (isFromPreviewButton) {
        if (SurveyControls.selectedPublicSurveyID > 0) {

            var record = PublicSurveyTable.data().filter(x => x.FormID == SurveyControls.selectedPublicSurveyID);
            var content = SurveyControls.removeSelectedTitles(record[0].Content);

            Layout.renderFormDesignerInIframe(content, 'iframePreviewPrivateSurveyPublicList');
            Layout.renderFormDesignerInIframe(content, 'iframeThePreview');
        }
    },

    createNewForm: function () {
        if (UserPermissions.SurveyCreate) {
            $("#txtsurveyname").val('New Design');
            SurveyControls.selectedSurveyID = 0;
            SurveyControls.showDesigner();
            privateFormBuilder.actions.clearFields();
        } else alert('You do not have permissions to perform this action');
    },

    showSurveyList: function () {
        privateFormBuilder.actions.clearFields();
        $("#divSurveyList").show();
        $("#divSurveyDesigner").hide();
    },

    showDesigner: function () {
        privateFormBuilder.actions.clearFields();
        $("#divSurveyList").hide();
        $("#divSurveyDesigner").show();

    },

    showPublicTemplates: function () {

    },

    defaultLoad: function () {
        if (Survey.OfficeId > 0) {
            if (!SurveyControls.isLoaded) {
                SurveyControls.isLoaded = true;
                SurveyControls.initializeFormBuilder();

                $(".panel-left").panelResizable({
                    handleSelector: ".splitter",
                    resizeHeight: false
                });

                SurveyTable = $('#tblPrivateSurveyForms').DataTable({
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
                        url: window.location.origin + '/Survey/GetPrivateSurveys',
                        type: "POST",
                        data: {
                            "OfficeId": function (d) {
                                return $("#ddlSurveyOffice").val();
                            }
                        },
                        dataSrc: ''
                    },
                    //dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l><"datatableLeftInfoDiv"i>p><"clear">',
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
                            "render": function (data, type, row) {

                                if (row.IsInUsed) {
                                    var duplicateButton = '<a href="javascript:;" onclick="SurveyControls.duplicateForm(' + data + ');" class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                                    var viewButton = '<a href="javascript:;" onclick="SurveyControls.renderForm(' + data + ',true);" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                                    var editButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                                    var deleteButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                                } else {
                                    var duplicateButton = '<a href="javascript:;" onclick="SurveyControls.duplicateForm(' + data + ');"  class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                                    var viewButton = '<a href="javascript:;" onclick="SurveyControls.renderForm(' + data + ',true);" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                                    var editButton = '<a href="javascript:;" onclick="SurveyControls.editForm(' + data + ');"  class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                                    var deleteButton = '<a href="javascript:;" onclick="SurveyControls.deleteForm(' + data + ');"  class="editable editable-click"><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';
                                }

                                var allButtons = viewButton;
                                if (UserPermissions.SurveyDuplicate) allButtons += duplicateButton;
                                if (UserPermissions.SurveyEdit) allButtons += editButton;
                                if (UserPermissions.SurveyDelete) allButtons += deleteButton;

                                return allButtons;
                            }
                        }
                    ],
                    columns: [
                        { data: "FormID", width: "0" },
                        { data: "Description", width: "80%" },
                        { data: "FormID", width: "20%" },
                    ]
                });

                SurveyTable.on('select', function (e, dt, type, indexes) {
                    if (type === 'row') {
                        var data = SurveyTable.row('.selected').data();
                        SurveyControls.selectedSurveyID = data.FormID;

                        SurveyControls.renderForm(data.FormID);
                        //AppEditor.textboxSetValue('hdnExistingResidentId', data.ID);
                        //AppEditor.dateSetTodayDate('txtInActiveDate');
                    }
                });


                //public survey
                PublicSurveyTable = $('#tblPublicSurveyForms').DataTable({
                    select: {
                        style: 'single'
                    },
                    responsive: true,
                    paging: true,
                    ordering: true,
                    info: true,
                    searching: true,
                    ajax: {
                        url: '/Survey/GetPublicSurveys',
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
                            "render": function (data, type, row) {
                                //var duplicateButton = '<button type="button" class="btn btn-primary btn-glow margin-right-5 pull-right" onclick="SurveyControls.duplicateForm(' + data + ');">Duplicate</button> ';
                                //var viewButton = '<button type="button" class="btn btn-primary btn-glow margin-right-5 pull-right" onclick="SurveyControls.renderForm(' + data + ');"  data-toggle="modal" data-target="#renderSurveyModal">Preview</button> ';
                                //var editButton = '<button type="button" class="btn btn-primary btn-glow margin-right-5 pull-right" onclick="SurveyControls.renderForm(' + data + ');"  data-toggle="modal" data-target="#renderSurveyModal">Edit</button> ';



                                var viewButton = '<a href="javascript:;" onclick="SurveyControls.renderPublicForm(' + data + ');" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';

                                return viewButton;
                            }
                        }

                    ],
                    columns: [
                        { data: "FormID", width: "0" },
                        { data: "Description", width: "90%" },
                        { data: "FormID", width: "10%" },

                    ]
                });

                PublicSurveyTable.on('select', function (e, dt, type, indexes) {
                    if (type === 'row') {
                        var data = PublicSurveyTable.row('.selected').data();
                        SurveyControls.selectedPublicSurveyID = data.FormID;
                        SurveyControls.renderPublicForm();
                        //AppEditor.textboxSetValue('hdnExistingResidentId', data.ID);
                        //AppEditor.dateSetTodayDate('txtInActiveDate');
                    }
                });

                //$('#renderSurveyModal').on('hidden.bs.modal', function () {
                //    //SurveyControls.selectedSurveyID = 0;
                //    //SurveyControls.selectedPublicSurveyID = 0;
                //})

            }
            else {
                SurveyTable.clear();
                SurveyTable.ajax.reload(null, false);
            }
        }

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


});
