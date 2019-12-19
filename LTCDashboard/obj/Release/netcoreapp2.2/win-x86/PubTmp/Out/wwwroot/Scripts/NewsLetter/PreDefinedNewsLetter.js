var PreNewsLetterControls = {

    defaultLoad: function () {

        if (typeof (PreNewsLetterTable) != 'undefined') {
            PreNewsLetterTable.clear();
            PreNewsLetterTable.destroy();
        }
        else {
            $(".panel-left").panelResizable({
                handleSelector: ".splitter",
                resizeHeight: false
            });
        }

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
                            var duplicateButton = '<a href="javascript:;" onclick="PreNewsLetterControls.duplicateForm(' + data + ');" class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                            var viewButton = '<a href="javascript:;" onclick="PreNewsLetterControls.viewTemplate(' + data + ',true);" class="editable editable-click"  data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                            var editButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                            var deleteButton = '<a href="javascript:;" class="editable editable-click" disabled><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                        } else {
                            var duplicateButton = '<a href="javascript:;" onclick="PreNewsLetterControls.duplicateForm(' + data + ');"  class="editable editable-click editable-empty"><i class="fa fa-clone" title="Duplicate"></i></a>&nbsp;&nbsp;&nbsp;';
                            var viewButton = '<a href="javascript:;" onclick="PreNewsLetterControls.viewTemplate(' + data + ',true);" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#thePreviewPanel"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';
                            var editButton = '<a href="javascript:;" onclick="PreNewsLetterControls.editTemplate(' + data + ');"  class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                            var deleteButton = '<a href="javascript:;" onclick="PreNewsLetterControls.deleteTemplate(' + data + ');"  class="editable editable-click"><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';
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
                { data: "IndustryId", width: "0%" },
                { data: "Title", width: "80%" },
                { data: "ID", width: "20%" },
            ],
            drawCallback: function () {
                setTimeout(function () { $('#rightDiv ul').css('height', $('#tblPrivateNewsLetterForms').height() + 'px'); }, 2000)
            }
        });

        PreNewsLetterTable.on('select', function (e, dt, type, indexes) {
            if (type === 'row') {
                var data = PreNewsLetterTable.row('.selected').data();

                PreNewsLetterControls.viewTemplate(data.ID);
            }
        });



        this.getIndustriesInDDL();
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
        if (!UserPermissions.NewsLetterCreate) {
            alert('You do not have permissions to perform this action');
        }
        else {
            Newsletter.loadTemplateDetailInEditor(false, 0);
            //$('.newsletter-industry-ddl').hide();
            //$('#publicNewsLetterCreateNew').hide();
            $(".templateEditordiv").show();
            $(".divNewsLetterList").hide();
        }
    },

    cancelCreateNewModel: function () {
        $(".templateEditordiv").hide();
        $(".divNewsLetterList").show();
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

            var html = '';
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

            Layout.renderContentInIframe(html, 'livePreview');


        }

        this.initializeLivePreview();
    },
}

$(document).ready(function () {
    $(".templateEditordiv").hide();
    $('#newsletterPreviewModal').on('shown.bs.modal', function (e) {
        $("#PreDefinedNewsLetter").modal('hide');
    });
    $('#newsletterPreviewModal').on('hidden.bs.modal', function (e) {
        $("#PreDefinedNewsLetter").modal('show');
    });

    $('#newsletterSaveModel').on('shown.bs.modal', function (e) {
        $("#PreDefinedNewsLetter").modal('hide');
    });
    $('#newsletterSaveModel').on('hidden.bs.modal', function (e) {
        $("#PreDefinedNewsLetter").modal('show');
    });


    PreNewsLetterControls.initializeLivePreview();

});
