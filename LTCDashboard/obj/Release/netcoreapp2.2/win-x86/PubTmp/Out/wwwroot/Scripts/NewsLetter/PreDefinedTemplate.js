var PreNewsLetterControls = {

    isLoaded: false,

    defaultLoad: function () {
        //load data also in a variable
        Newsletter.loadSystemTemplatesForDuplication();

        //if (typeof (PreNewsLetterTable) != 'undefined') {
        //    PreNewsLetterTable.clear();
        //    PreNewsLetterTable.destroy();
        //}

        PreNewsLetterControls.initializeNewsLetter();

        this.getIndustriesInDDL();
    },

    initializeNewsLetter: function () {
        if (!PreNewsLetterControls.isLoaded) {
            PreNewsLetterControls.isLoaded = true;

            PreNewsLetterTable = $('#tblPrivateNewsLetterForms').DataTable({
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
                    url: window.location.origin + '/NewsLetter/GetPreDefinedTemplates',
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
                        "searchable": true
                    },
                    {
                        "targets": [2],
                        "data": null,
                        "render": function (data, type, row) {
                            if (row.IsInUsed) {
                                var duplicateButton = '<a href="javascript:;" onclick="PreNewsLetterControls.duplicateTemplate(' + data + ');" class="editable editable-click editable-empty"><i class="fa fa-clone" title="Use this template"></i></a>&nbsp;&nbsp;&nbsp;';
                                var viewButton = '<a href="javascript:;" onclick="PreNewsLetterControls.viewTemplate(' + data + ',true);" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                                var editButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                                var deleteButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                            } else {
                                var duplicateButton = '<a href="javascript:;" onclick="PreNewsLetterControls.duplicateTemplate(' + data + ');"  class="editable editable-click editable-empty"><i class="fa fa-clone" title="Use this template"></i></a>&nbsp;&nbsp;&nbsp;';
                                var viewButton = '<a href="javascript:;" onclick="PreNewsLetterControls.viewTemplate(' + data + ',true);" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                                var editButton = '<a href="javascript:;" onclick="PreNewsLetterControls.editTemplate(' + data + ');"  class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                                var deleteButton = '<a href="javascript:;" onclick="PreNewsLetterControls.deleteTemplate(' + data + ');"  class="editable editable-click"><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';
                            }

                            var allButtons = viewButton;
                            if (UserPermissions.NewsLetterDuplicate) allButtons += duplicateButton;
                            //if (UserPermissions.CanEdit) allButtons += editButton;
                            //if (UserPermissions.CanDelete) allButtons += deleteButton;

                            return allButtons;
                        }
                    }
                ],
                columns: [
                    { data: "IndustryId", width: "0%" },
                    { data: "Title", width: "80%" },
                    { data: "ID", width: "20%" },
                ]
            });

            PreNewsLetterTable.on('select', function (e, dt, type, indexes) {
                if (type === 'row') {
                    var data = PreNewsLetterTable.row('.selected').data();

                    PreNewsLetterControls.viewTemplate(data.ID);
                }
            });

        } else {
            PreNewsLetterTable.ajax.reload(null, false);
        }
    },

    searchPredifinedNLOnIndustry: function () {
        var IndID = $("#ddlNewsLetterIndustries").val();

        if (IndID == 0)
            PreNewsLetterTable.column(0).search('').draw();
        else
            PreNewsLetterTable.column(0).search('^' + IndID + '$', true, false).draw();
    },

    getIndustriesInDDL: function () {
        //get industreis and populate ddl
        $.ajax({
            type: "GET",
            url: '/Newsletter/GetIndustries',
            success: function (data) {
                if (data != null) {
                    $("#ddlNewsLetterIndustries").html('<option value="0"> --All Industries--</option>');
                    $.each(NewsLetter_Industries, function (index, value) {
                        var item = NewsLetter_Industries[index];
                        $("#ddlNewsLetterIndustries").append('<option value="' + item.ID + '">' + item.Title + '</option>');
                    });
                    $('#ddlNewsLetterIndustries').selectpicker('refresh');
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                //ltcApp.errorMessage("Error", 'Error loading industries');
            },
            complete: function () {
            }
        });
    },

    loadFromHomePage: function () {
        $('#PreDefinedNewsLetter').modal('show');
        this.defaultLoad();
    },

    showCreateNewModal: function () {
        Newsletter.loadTemplateDetailInEditor(false, 0);
        //$('#PreDefinedNewsLetter').modal('hide');
        //$('#templateEditorWindow').modal('show');

        $(".templateEditordiv").show();
        $("#divNewsLetterList").hide();
    },

    duplicateTemplate: function (id) {
        Newsletter.loadTemplateDetailInEditor(true, id, true);
        $('#PreDefinedNewsLetter').modal('hide');
        //$('#templateEditorWindow').modal('show');
        $(".templateEditordiv").show();
        $("#divNewsLetterList").hide();
        //alert('comming soon');
    },

    cancelCreateNewModel: function () {
        $(".templateEditordiv").hide();
        $("#divNewsLetterList").show();
    },

    viewTemplate: function (id, isFromPreviewButton) {
        var newsLetterPriviewTem = NewsLetter_UserDefinedTemplates.find(x => x.ID === id);
        var newsLetterPriviewShellTem = NewsLetter_ShellTemplates.find(x => x.ID === newsLetterPriviewTem.ShellTemplateId);

        var html = '';
        if (newsLetterPriviewShellTem != undefined) {
            html = newsLetterPriviewShellTem.Markup.replace('[maincontent]', newsLetterPriviewTem.Markup);
        }

        Layout.renderContentInIframe(html, 'previewPreNewsLetter');
        Layout.renderContentInIframe(html, 'iframeThePreview');
    },

    editTemplate: function (id) {
        Newsletter.modifyTemplate(id);
    },

    deleteTemplate: function (id) {
        if (confirm('are you sure, you want to delete this template.'))
            Layout.showLoader();
        $.post("/NewsLetter/DeletePredefinedTemplate", { Id: id }, function (data, status) {
            Layout.hideLoader();
                PreNewsLetterControls.defaultLoad();
            });
    },

    initializeLivePreview: function () {
        setTimeout(function () { PreNewsLetterControls.showLivePreview(); }, 2000);
    },

    showLivePreview: function () {

        if ($("#templateEditor").data("kendoEditor") != undefined) {

            var html;
            if ($('#ddlShellTemplates').val() == '-1' || $('#ddlShellTemplates').val() == null) {
                html = $("#templateEditor").data("kendoEditor").value();
            }
            else {
                if (NewsLetter_ShellTemplates != null && NewsLetter_ShellTemplates != undefined) {
                    var shellTemplateView = NewsLetter_ShellTemplates.find(x => x.ID == $('#ddlShellTemplates').val());
                    html = shellTemplateView.Markup.replace('[maincontent]', $("#templateEditor").data("kendoEditor").value());
                } else {
                    html = $("#templateEditor").data("kendoEditor").value();
                }

            }

            //var html = $("#templateEditor").data("kendoEditor").value();

            var iframe = document.getElementById('livePreview');
            iframe = iframe.contentWindow || (iframe.contentDocument.document || iframe.contentDocument);

            iframe.document.open();
            iframe.document.write(html);
            iframe.document.close();
        }

        this.initializeLivePreview();
    },
}

$(document).ready(function () {
    PreNewsLetterControls.initializeLivePreview();

});
