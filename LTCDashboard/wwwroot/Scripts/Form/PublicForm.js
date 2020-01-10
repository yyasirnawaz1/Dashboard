var PublicFormControls = {

    selectedFormID: 0,
    formBuilder: null,
    isLoaded: false,

    openFormModal: function () {
        $('#PublicFormloginModal').modal('show');
        PublicFormControls.initializeForms();
    },

    initializeForms: function () {
        if (!PublicFormControls.isLoaded) {
            PublicFormControls.isLoaded = true;

            PublicFormsTable = $('#tblPublicForm').DataTable({
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
                    url: window.location.origin + '/Form/GetPublicForms',
                    dataSrc: ""
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
                            var duplicateButton = '<a href="javascript:;" onclick="PublicFormControls.duplicateForm(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                            var viewButton = '<a href="javascript:;" onclick="PublicFormControls.renderForm(' + data + ',true);" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                            var editButton = '<a href="javascript:;" onclick="PublicFormControls.editForm(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                            var deleteButton = '<a href="javascript:;" onclick="PublicFormControls.deleteForm(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click"><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';


                            var allButtons = viewButton;
                            //if (UserPermissions.CanCreate) allButtons += duplicateButton;
                            //if (UserPermissions.CanEdit) allButtons += editButton;
                            if (UserPermissions.FormDelete) allButtons += deleteButton;

                            return allButtons;


                            //return viewButton + deleteButton;
                        }
                    }
                ],
                columns: [
                    { data: "FormID", width: "0" },
                    { data: "Description", width: "80%" },
                    { data: "FormID", width: "20%" },
                ]
            });

            PublicFormsTable.on('select', function (e, dt, type, indexes) {
                if (type === 'row') {
                    var data = PublicFormsTable.row('.selected').data();
                    PublicFormControls.selectedFormID = data.FormID;

                    PublicFormControls.renderForm(data.FormID);

                    //AppEditor.textboxSetValue('hdnExistingResidentId', data.ID);
                    //AppEditor.dateSetTodayDate('txtInActiveDate');
                }
            });

        } else {
            PublicFormsTable.ajax.reload(null, false);
        }
    },

    initializeFormBuilder: function () {
        $.get("/Form/GetPublicTags", function (data) {
            var selectoptions = { "0": "Select Tag" };
            for (var i = 0; i < data.length; i++) {
                selectoptions[data[i].TagID] = data[i].Description;
            }
            PublicFormControls.initializeFormBuilderAfterGetTags(selectoptions);
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

        $("#fbPublicForm-editor").html('');
        var container = (document.getElementById('fbPublicForm-editor'));
        PublicFormControls.formBuilder = $(container).formBuilder({
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
        //if (formBuilder.action.clearFields == null) {

        //    $("#fbPublicForm-editor").html('');
        //    var container = (document.getElementById('fbPublicForm-editor'));
        //    formBuilder = $(container).formBuilder();
        //}

    },

    saveFormDesign: function () {
        var content = PublicFormControls.formBuilder.actions.getData();
        Layout.showLoader();
        $.ajax({
            type: "POST",
            async: true,
            data: {
                //data: JSON.stringify(content)
                FormID: $('#hdPublicsid').val(),
                Description: $('#txtPublicformname').val(),
                Content: JSON.stringify(content)
            },
            url: window.location.origin + "/Form/SavePublicDesign",
            success: function (data) {
                Layout.hideLoader();
                if (data.Success) {
                    alert('record saved.');
                    PublicFormControls.showFormList();
                    PublicFormControls.formBuilder.actions.clearFields();
                    $('#txtPublicformname').val('');
                    $('#hdsPublicid').val('-1');
                    PublicFormsTable.ajax.reload(null, false);

                }
                else {
                    //show error
                }
            },
            error: function (xhr, data, errorThrown) {
                //show error
                Layout.hideLoader();
            }
        });
    },

    duplicateForm: function (id) {

        if (id > 0) {
            PublicFormControls.showDesigner();

            var record = PublicFormsTable.data().filter(x => x.FormID == id);
            $("#txtPublicformname").val("Copy of -" + record[0].Description);
            $('#hdsPublicid').val("-1");
            PublicFormControls.formBuilder.actions.setData(record[0].Content);
        } else {
            //show alert $('#renderFormForm').html('Select a form from table.');
        }
    },

    editForm: function (id) {

        if (id > 0) {
            PublicFormControls.showDesigner();

            var record = PublicFormsTable.data().filter(x => x.FormID == id);
            PublicFormControls.formBuilder.actions.setData(record[0].Content);
            $("#txtPublicformname").val(record[0].Description);
            $('#hdsPublicid').val(record[0].FormID);
            console.log(id);
            console.log(record[0].FormID);
            // move back to table view
            // show message that saved successfully for now add alert
        } else {
            //show alert $('#renderFormForm').html('Select a form from table.');
        }
    },

    deleteForm: function (id) {
        if (confirm('are you sure to delete this item')) {
            Layout.showLoader();
            //DeletePublicDesign
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    Id: id
                },
                url: window.location.origin + "/Form/DeletePublicDesign",
                success: function (data) {
                    Layout.hideLoader();
                    PublicFormsTable.ajax.reload(null, false);
                    alert('Item deleted.');
                },
                error: function (xhr, data, errorThrown) {
                    Layout.hideLoader();
                    alert('There was an error. Please try again later');
                }
            });

        }
    },

    //private form view
    renderForm: function (id, isFromPreviewButton) {

        if (id > 0) {
            var record = PublicFormsTable.data().filter(x => x.FormID == id);

            var content = PublicFormControls.removeSelectedTitles(record[0].Content);


            Layout.renderFormDesignerInIframe(content, 'iframePreviewPublicForm');
            Layout.renderFormDesignerInIframe(content, 'iframeThePreview');
        }
    },

    createNewForm: function () {
        if (!UserPermissions.FormCreate) {
            alert('You do not have permissions to perform this action');
        }
        else {
            $("#txtPublicformname").val('New Design');
            PublicFormControls.selectedFormID = 0;
            PublicFormControls.showDesigner();
            PublicFormControls.formBuilder.actions.clearFields();
        }
    },

    showFormList: function () {

        PublicFormControls.formBuilder.actions.clearFields();
        $(".divPublicFormList").show();
        $(".divPublicFormDesigner").hide();
        //this.closeFullScreenPreview();
    },

    showDesigner: function () {

        PublicFormControls.formBuilder.actions.clearFields();
        $(".divPublicFormList").hide();
        $(".divPublicFormDesigner").show();

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
    $(".divPublicFormDesigner").hide();
    PublicFormControls.initializeFormBuilder();

    $(".panel-left").panelResizable({
        handleSelector: ".splitter",
        resizeHeight: false
    });
    
    //$('#renderPublicFormModal').on('hidden.bs.modal', function () {
    //    //FormControls.selectedFormID = 0;
    //    //FormControls.selectedPublicFormID = 0;
    //});

});
