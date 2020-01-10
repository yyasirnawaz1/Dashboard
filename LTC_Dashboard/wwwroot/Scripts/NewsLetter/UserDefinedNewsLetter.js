var UserNewsLetterControls = {

    defaultLoad: function () {
        if (true) {
            if (true) {

                if (typeof (UserNewsLetterTable) != 'undefined') {
                    UserNewsLetterTable.clear();
                    UserNewsLetterTable.destroy();
                } else {
                    $(".panel-left").panelResizable({
                        handleSelector: ".splitter",
                        resizeHeight: false
                    });
                }

                UserNewsLetterTable = $('#tblUserNewsLetterForms').DataTable({
                    select: {
                        style: 'single'
                    },
                    responsive: true,
                    paging: true,
                    ordering: true,
                    info: true,
                    searching: true,
                    ajax: {
                        url: window.location.origin + '/NewsLetter/GetUserDefinedTemplates',
                        type: "GET",
                        data: {
                            "OfficeId": function (d) {
                                return $("#ddlNewsLetterOffice").val();
                            }
                        },
                        dataSrc: ''
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
                                if (row.IsInUsed) {
                                    var duplicateButton = '<a href="javascript:;" onclick="UserNewsLetterControls.duplicateTemplate(' + data + ');" class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                                    var viewButton = '<a href="javascript:;" onclick="UserNewsLetterControls.viewTemplate(' + data + ',true);" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                                    var editButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                                    var deleteButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                                } else {
                                    var duplicateButton = '<a href="javascript:;" onclick="UserNewsLetterControls.duplicateTemplate(' + data + ');"  class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                                    var viewButton = '<a href="javascript:;" onclick="UserNewsLetterControls.viewTemplate(' + data + ',true);" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                                    var editButton = '<a href="javascript:;" onclick="UserNewsLetterControls.editTemplate(' + data + ');"  class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                                    var deleteButton = '<a href="javascript:;" onclick="UserNewsLetterControls.deleteTemplate(' + data + ');"  class="editable editable-click"><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';
                                }

                                var allButtons = viewButton;
                                //if (UserPermissions.CanCreate) allButtons += duplicateButton;
                                if (UserPermissions.NewsLetterEdit) allButtons += editButton;
                                if (UserPermissions.NewsLetterDelete) allButtons += deleteButton;

                                return allButtons;
                            }
                        }
                    ],
                    columns: [
                        { data: "ID", width: "0%" },
                        { data: "Title", width: "80%" },
                        { data: "ID", width: "20%" },
                    ]
                });

                UserNewsLetterTable.on('select', function (e, dt, type, indexes) {
                    if (type === 'row') {
                        var data = UserNewsLetterTable.row('.selected').data();

                        UserNewsLetterControls.viewTemplate(data.ID);
                    }
                });
            }
            else {
                NewsLetterTable.clear();
                NewsLetterTable.ajax.reload(null, false);
            }
        }
    },

    //addUserTemplateDataToVAR: function () {
    //    $.ajax({
    //        url: window.location.origin + '/NewsLetter/GetUserDefinedTemplates',
    //        type: "GET",
    //        data: {
    //            "OfficeId": function (d) {
    //                return $("#tblUserNewsLetterForms").val();
    //            }
    //        },
    //        success: function (data) {
    //            if (data != null) {
    //                NewsLetter_UserDefinedTemplates = data;
    //            }
    //        },
    //        error: function (xhr, textStatus, errorThrown) {
    //            //ltcApp.errorMessage("Error", 'Error loading user defined templates');
    //        },
    //        complete: function () {
    //            //if (SelectedUserDefinedTemplateId != null)
    //            //    Newsletter.loadSelectedUserdefinedTemplate(SelectedUserDefinedTemplateId, this)
    //        }
    //    });
    //},

    createNewTemplate: function () {
        if (!UserPermissions.NewsLetterCreate) {
            alert('You do not have permissions to perform this action');
        }
        else {
            SelectedUserDefinedTemplateId = 0;
            Newsletter.loadTemplateDetailInEditor(false, 0, false);
            $(".templateEditordiv").show();
            $("#divNewsLetterList").hide();
        }
    },

    viewTemplate: function (id, isFromPreviewButton) {
        var newsLetterPriviewTem = NewsLetter_SystemTemplates.find(x => x.ID === id);
        var newsLetterPriviewShellTem = NewsLetter_ShellTemplates.find(x => x.ID === newsLetterPriviewTem.ShellTemplateId);

        var html = '';
        if (newsLetterPriviewShellTem != undefined) {
            html = newsLetterPriviewShellTem.Markup.replace('[maincontent]', newsLetterPriviewTem.Markup);
        } else {
            html = newsLetterPriviewTem.Markup;
        }

        Layout.renderContentInIframe(html, 'previewUserNewsLetter');
        Layout.renderContentInIframe(html, 'iframeThePreview');



        //if ($('#userDefinedNewsletterList').hasClass('toggleDivHidden') && isFromPreviewButton) {
        //    $('#userDefinedNewsletterList').removeClass('toggleDivHidden');
        //    $('#userDefinedNewsletterList .panel-left').hide();
        //    $('#userDefinedNewsletterList .panel-right').addClass('col-lg-6 col-md-6 col-sm-6 col-xs-6');
        //    $('#btnUserNewsLetterToggle').hide();
        //    $('#btnUserNewsLetterCreateNew').hide();
        //    $('#btnUserNewsLetterShowTemplate').hide();
        //    $('#btnUserNewsLetterCloseFullPreview').show();
        //}
    },

    duplicateTemplate: function (id) {
        alert('comming soon');
    },

    editTemplate: function (id) {
        Newsletter.loadTemplateDetailInEditor(true, id, false);
        //$('#templateEditorWindow').modal('show');
        $(".templateEditordiv").show();
        $("#divNewsLetterList").hide();
    },

    deleteTemplate: function (id) {
        if (confirm('are you sure, you want to delete this template.'))
            Layout.showLoader();
        $.post("/NewsLetter/DeleteUserdefinedTemplate", { Id: id }, function (data, status) {
            Layout.hideLoader();
            UserNewsLetterControls.defaultLoad();
            Newsletter.loadSystemTemplates();
        });
    },

    showPredefinendTemplate() {
        PreNewsLetterControls.loadFromHomePage();
    },
}

$(document).ready(function () {

});
