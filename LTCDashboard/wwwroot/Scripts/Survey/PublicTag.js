var PublicTag = {

    selectedID: 0,
    categories: null,
    isLoaded: false,

    openPublicTagModal: function () {
        $('#PublicTagloginModal').modal('show');
        PublicTag.initializePublicTag();
    },

    initializePublicTag: function () {

        if (!PublicTag.isLoaded) {

            PublicTag.isLoaded = true;

            PublicTagTable = $('#tblPublicTag').DataTable({
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
                    url: window.location.origin + '/Survey/GetPublicTags',
                    dataSrc: ""
                },
                dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
                columnDefs: [
                    {
                        "targets": [0],
                        "visible": false,
                        "searchable": false
                    },
                    //{
                    //    "targets": [3],
                    //    "data": null,
                    //    "render": function (data, type, row) {
                    //        var editButton = '<a href="javascript:;" onclick="PublicTag.editPublicTag(' + data + ');" data-type="text" data-pk="1" class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                    //        var deleteButton = '<a href="javascript:;" onclick="PublicTag.deletePublicTag(' + data + ');" data-type="text" data-pk="1" class="editable editable-click" ><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                    //        var allButtons = '';

                    //        if (UserPermissions.SurveyEdit) allButtons += editButton;
                    //        if (UserPermissions.SurveyDelete) allButtons += deleteButton;

                    //        return allButtons;
                    //    }
                    //}
                ],
                columns: [
                    { data: "TagID", width: "0" },
                    { data: "Description", width: "65%" },
                    { data: "CategoryDescription", width: "25%" },
                    //{ data: "TagID", width: "15%" },
                ]
            });

            //PublicTagTable.on('select', function (e, dt, type, indexes) {
            //    if (type === 'row') {
            //        var data = PublicTagTable.row('.selected').data();
            //        PublicTag.selectedSurveyID = data.FormID;
            //    }
            //});
        }
        else {
            PublicTagTable.ajax.reload(null, false);
        }

    },

    savePublicTag: function () {
        if (PublicTag.validatePublicTag()) {
            Layout.showLoader();
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    TagID: $("#hdnPublicTagId").val(),
                    CategoryID: $("#ddlPublicTagCategories").val(),
                    Description: $("#txtPublicTagDescription").val(),
                    TagType: $("#ddlPublicTagTypes").val(),
                    Caption: $("#txtPublicTagCaption").val(),
                    DataField: $("#txtPublicTagDataField").val(),
                    MaxSize: $("#txtPublicTagMaxSize").val(),
                },
                url: window.location.origin + "/Survey/SavePublicTag",
                success: function (data) {
                    Layout.hideLoader();
                    if (data.Success) {
                        PublicTag.showPublicTagList();
                        PublicTagTable.ajax.reload(null, false);

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
        }
    },

    editPublicTag: function (id) {

        if (id > 0) {

            var record = PublicTagTable.data().filter(x => x.TagID == id)[0];
            $("#hdnPublicTagId").val(record.TagID);
            $("#ddlPublicTagCategories").val(record.CategoryID);
            $("#txtPublicTagDescription").val(record.Description);
            $("#ddlPublicTagTypes").val(record.TagType);
            $("#txtPublicTagCaption").val(record.Caption);
            $("#txtPublicTagDataField").val(record.DataField);
            $("#txtPublicTagMaxSize").val(record.MaxSize);
            PublicTag.showPublicCreateTag();
            // move back to table view
            // show message that saved successfully for now add alert
        } else {
            //show alert $('#renderSurveyForm').html('Select a survey from table.');
        }
    },

    deletePublicTag: function (id) {
        if (confirm('are you sure to delete this item')) {
            //DeletePublicDesign
            Layout.showLoader();
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    Id: id
                },
                url: window.location.origin + "/Survey/DeletePublicTag",
                success: function (data) {
                    Layout.hideLoader();
                    PublicTagTable.ajax.reload(null, false);
                    alert('Item deleted.');
                },
                error: function (xhr, data, errorThrown) {
                    Layout.hideLoader();
                    alert('There was an error. Please try again later');
                }
            });

        }
    },

    validatePublicTag: function () {
        var isValid = true;
        if ($("#ddlPublicTagCategories").val() == "-1") {
            isValid = false;
        }
        if ($("#ddlPublicTagTypes").val() == "-1") {
            isValid = false;
        }

        if ($("#txtPublicTagDescription").val() == "") {
            isValid = false;
        }

        if (!isValid) {
            alert('please fill the required fields');
        }

        return isValid;
    },

    createNewPublicTag: function () {
        if (!UserPermissions.SurveyTagCreate) {
            alert('You do not have permissions to perform this action');
        }
        else {
            $("#hdnPublicTagId").val(0);
            $("#ddlPublicTagCategories").val('-1');
            $("#txtPublicTagDescription").val('');
            $("#ddlPublicTagTypes").val('-1');
            $("#txtPublicTagCaption").val('');
            $("#txtPublicTagDataField").val('');
            $("#txtPublicTagMaxSize").val('');
            PublicTag.showPublicCreateTag();
        }

    },

    showPublicTagList: function () {
        $(".publicTagList").show();
        $(".publicTagCreate").hide();
    },

    showPublicCreateTag: function () {
        $(".publicTagList").hide();
        $(".publicTagCreate").show();
    },

    loadCategories: function () {
        $.ajax({
            type: "POST",
            async: true,
            data: {
            },
            url: window.location.origin + "/Survey/GetCategories",
            success: function (data) {
                if (data.Success) {
                    PublicTag.categories = data.data;
                    var currData = '';
                    $.each(PublicTag.categories, function (i, data) {

                        currData += "<option value='" + data.CategoryID + "'>" + data.Description + "</option>";
                    });
                    $('#ddlPublicTagCategories').append(currData);
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

    onChangeddlPublicTagCategories: function () {
        var id = $("#ddlPublicTagCategories").val();
        var categoryDetail = PublicTag.categories.filter(x => x.CategoryID == id)[0];
        if (categoryDetail.Description != null && categoryDetail.Description != '')
            $("#txtPublicTagDescription").val(categoryDetail.Description);
    },

}

$(document).ready(function () {
    PublicTag.showPublicTagList();
    PublicTag.loadCategories();


});
