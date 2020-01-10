var PrivateTag = {
    selectedID: 0,
    selectedPublicTag: 0,
    categories: null,
    isLoaded: false,
    PublicDuplicationTagTable: null,

    savePrivateTag: function () {
        if (PrivateTag.validatePrivateTag()) {
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    TagID: $("#hdnPrivateTagId").val(),
                    CategoryID: $("#ddlPrivateTagCategories").val(),
                    Description: $("#txtPrivateTagDescription").val(),
                    Office_Sequence: $("#ddlFormOffice").val()
                },
                url: window.location.origin + "/Form/SavePrivateTag",
                success: function (data) {
                    if (data.Success) {
                        $("#PrivateTagModal").modal('hide');
                        PrivateTagTable.ajax.reload(null, false);

                    }
                    else {
                        //show error
                    }
                },
                error: function (xhr, data, errorThrown) {
                    //show error
                }
            });
        }
    },

    editPrivateTag: function (id) {
        if (id > 0) {
            var record = PrivateTagTable.data().filter(x => x.TagID == id)[0];
            $("#hdnPrivateTagId").val(record.TagID);
            $("#ddlPrivateTagCategories").val(record.CategoryID);
            $("#txtPrivateTagDescription").val(record.Description);

            // move back to table view
            // show message that saved successfully for now add alert
        } else {
            //show alert $('#renderFormForm').html('Select a form from table.');
        }
    },

    deletePrivateTag: function (id) {
        if (confirm('are you sure to delete this item')) {
            //DeletePublicDesign
            Layout.showLoader();
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    Id: id
                },
                url: window.location.origin + "/Form/DeletePrivateTag",
                success: function (data) {
                    Layout.hideLoader();
                    PrivateTagTable.ajax.reload(null, false);
                    alert('Item deleted.');
                },
                error: function (xhr, data, errorThrown) {
                    Layout.hideLoader();
                    alert('There was an error. Please try again later');
                }
            });
        }
    },

    validatePrivateTag: function () {
        var isValid = true;
        if ($("#ddlPrivateTagCategories").val() == "-1") {
            isValid = false;
        }
        if ($("#txtPrivateTagDescription").val() == "") {
            isValid = false;
        }

        if (!isValid) {
            alert('please fill the required fields');
        }

        return isValid;
    },

    createNewPrivateTag: function () {
        if (UserPermissions.FormTagCreate) {
            $('#PrivateTagModal').modal('show');

            $("#hdnPrivateTagId").val(0);
            $("#ddlPrivateTagCategories").val('-1');
            $("#txtPrivateTagDescription").val('');
        }
        else
            alert('You do not have permissions to perform this action');
    },

    openCopyPrivateTag: function () {
        PrivateTag.selectedPublicTag = 0;
        if (PrivateTag.PublicDuplicationTagTable == null) {
            PrivateTag.PublicDuplicationTagTable = $('#tblPublicDuplicateTag').DataTable({
                select: {
                    style: 'single'
                },
                responsive: true,
                paging: true,
                ordering: true,
                info: true,
                searching: true,
                ajax: {
                    url: window.location.origin + '/Form/GetPublicTags',
                    dataSrc: ""
                },
                //dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l><"datatableLeftInfoDiv"i>p><"clear">',
                dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
                columnDefs: [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    }
                ],
                columns: [
                    { data: "TagID", width: "0" },
                    { data: "Description", width: "85%" },
                    { data: "CategoryDescription", width: "15%" },
                ]
            });

            PrivateTag.PublicDuplicationTagTable.on('select', function (e, dt, type, indexes) {
                if (type === 'row') {
                    var data = PrivateTag.PublicDuplicationTagTable.row('.selected').data();
                    PrivateTag.selectedPublicTag = data.TagID;
                }
            });
        }
        else {
            PrivateTagTable.clear();
            PrivateTagTable.ajax.reload(null, false);
        }
    },

    copyPrivateTag: function () {
        var id = PrivateTag.selectedPublicTag;
        if (id > 0) {
            var record = PrivateTag.PublicDuplicationTagTable.data().filter(x => x.TagID == id)[0];
            $("#hdnPrivateTagId").val('0');
            $("#ddlPrivateTagCategories").val(record.CategoryID);
            $("#txtPrivateTagDescription").val(record.Description);
            $("#PublicDuplicationModel").modal('hide');
            $("#PrivateTagModal").modal('show');
            // move back to table view
            // show message that saved successfully for now add alert
        } else {
            //show alert $('#renderFormForm').html('Select a form from table.');
        }
    },

    loadCategories: function () {
        $.ajax({
            type: "POST",
            async: true,
            data: {
            },
            url: window.location.origin + "/Form/GetCategories",
            success: function (data) {
                if (data.Success) {
                    PrivateTag.categories = data.data;
                    var currData = '';
                    $.each(PrivateTag.categories, function (i, data) {
                        currData += "<option value='" + data.CategoryID + "'>" + data.Form_Tag_Type + "</option>";
                    });
                    $('#ddlPrivateTagCategories').append(currData);
                }
                else {
                    //show error
                }
            },
            error: function (xhr, data, errorThrown) {
                //show error
            }
        });
    },

    onChangeddlPrivateTagCategories: function () {
        var id = $("#ddlPrivateTagCategories").val();
        var categoryDetail = PrivateTag.categories.filter(x => x.CategoryID == id)[0];
        if (categoryDetail.Description != null && categoryDetail.Description != '')
            $("#txtPrivateTagDescription").val(categoryDetail.Description);
    },

    defaultLoad: function () {
        if (Form.OfficeId > 0) {
            if (!PrivateTag.isLoaded) {
                PrivateTag.isLoaded = true;
                PrivateTag.loadCategories();
                PrivateTagTable = $('#tblTag').DataTable({
                    select: {
                        style: 'single'
                    },
                    responsive: true,
                    paging: true,
                    ordering: true,
                    info: true,
                    searching: true,
                    ajax: {
                        url: window.location.origin + '/Form/GetPrivateTags',
                        type: "POST",
                        data: {
                            "OfficeId": function (d) {
                                return $("#ddlFormOffice").val();
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
                            "targets": [3],
                            "data": null,
                            "render": function (data, type, row) {

                                var copyButton = '<a href="javascript:;" onclick="PrivateTag.openCopyPrivateTag(' + data + ');" data-type="text" data-pk="1" data-placement="right" class="editable editable-click editable-empty" data-toggle="modal" data-target="#PublicDuplicationModel"><i class="fa fa-clone" title="Duplicate"></i></a> &nbsp;&nbsp;&nbsp;';
                                var editButton = '<a href="javascript:;" onclick="PrivateTag.editPrivateTag(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click" data-toggle="modal" data-target="#PrivateTagModal"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                                var deleteButton = '<a href="javascript:;" onclick="PrivateTag.deletePrivateTag(' + data + ');" data-type="text" data-pk="1" data-title="Enter username" class="editable editable-click" data-toggle="modal"><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                                var allButtons = '';
                                if (UserPermissions.FormEdit) allButtons = editButton;
                                if (UserPermissions.FormDuplicate) allButtons += copyButton;
                                if (UserPermissions.FormDelete) allButtons += deleteButton;

                                return allButtons;
                            }
                        }
                    ],
                    columns: [
                        { data: "TagID", width: "0" },
                        { data: "Description", width: "60%" },
                        { data: "CatagoryDescription", width: "25%" },
                        { data: "TagID", width: "15%" },
                    ]
                });

                PrivateTagTable.on('select', function (e, dt, type, indexes) {
                    if (type === 'row') {
                        var data = PrivateTagTable.row('.selected').data();
                        PrivateTag.selectedFormID = data.FormID;

                    }
                });
            } else {
                PrivateTagTable.clear();
                PrivateTagTable.ajax.reload(null, false);
            }
        }

    }
}

$(document).ready(function () {


});
