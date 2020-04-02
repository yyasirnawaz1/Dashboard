var PublicFormTag = {

    selectedID: 0,
    categories: null,
    isLoaded: false,

    openPublicTagModal: function () {
        $('#PublicFormTagloginModal').modal('show');
        PublicFormTag.initializeTags();
    },

    initializeTags: function () {
        if (!PublicFormTag.isLoaded) {
            PublicFormTag.isLoaded = true;

            PublicFormTagTable = $('#tblPublicFormTag').DataTable({
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
                    },
                    //{
                    //    "targets": [3],
                    //    "data": null,
                    //    "render": function (data, type, row) {

                    //        var editButton = '<a href="javascript:;" onclick="PublicFormTag.editPublicFormTag(' + data + ');" data-type="text" data-pk="1" class="editable editable-click"><i class="fa fa-edit" title="Edit"></i></a> &nbsp;&nbsp;&nbsp; ';
                    //        var deleteButton = '<a href="javascript:;" onclick="PublicFormTag.deletePublicFormTag(' + data + ');" data-type="text" data-pk="1" class="editable editable-click" ><i class="fa fa-trash" title="Delete"></i></a> &nbsp;&nbsp;&nbsp; ';

                    //        var allButtons = '';

                    //        if (UserPermissions.FormEdit) allButtons += editButton;
                    //        if (UserPermissions.FormDelete) allButtons += deleteButton;

                    //        return allButtons;
                    //    }
                    //}
                ],
                columns: [
                    { data: "TagID", width: "0" },
                    { data: "Description", width: "55%" },
                    { data: "CategoryDescription", width: "30%" },
                    //{ data: "TagID", width: "15%" },
                ]
            });
        }
        else {
            PublicFormTagTable.ajax.reload(null, false);
        }
    },

    savePublicFormTag: function () {
        if (PublicFormTag.validatePublicFormTag()) {
            Layout.showLoader();
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    TagID: $("#hdnPublicFormTagId").val(),
                    CategoryID: $("#ddlPublicFormTagCategories").val(),
                    Description: $("#txtPublicFormTagDescription").val(),
                    TagType: $("#ddlPublicFormTagTypes").val(),
                    Caption: $("#txtPublicFormTagCaption").val(),
                    DataField: $("#txtPublicFormTagDataField").val(),
                    MaxSize: $("#txtPublicFormTagMaxSize").val(),
                },
                url: window.location.origin + "/Form/SavePublicTag",
                success: function (data) {
                    Layout.hideLoader();
                    if (data.Success) {
                        alert('record saved.');
                        PublicFormTag.showList();
                        PublicFormTagTable.ajax.reload(null, false);
                        
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
        }
    },

    editPublicFormTag: function (id) {

        if (id > 0) {
            
            var record = PublicFormTagTable.data().filter(x => x.TagID == id)[0];
            $("#hdnPublicFormTagId").val(record.TagID);
            $("#ddlPublicFormTagCategories").val(record.CategoryID);
            $("#txtPublicFormTagDescription").val(record.Description);
            $("#ddlPublicFormTagTypes").val(record.TagType);
            $("#txtPublicFormTagCaption").val(record.Caption);
            $("#txtPublicFormTagDataField").val(record.DataField);
            $("#txtPublicFormTagMaxSize").val(record.MaxSize);

            PublicFormTag.showCreateEdit();
            // move back to table view
            // show message that saved successfully for now add alert
        } else {
            //show alert $('#renderSurveyForm').html('Select a survey from table.');
        }
    },

    deletePublicFormTag: function (id) {
        if (confirm('are you sure to delete this item')) {
            //DeletePublicDesign
            Layout.showLoader();
            $.ajax({
                type: "POST",
                async: true,
                data: {
                    Id: id
                },
                url: window.location.origin + "/Form/DeletePublicTag",
                success: function (data) {
                    Layout.hideLoader();
                    PublicFormTagTable.ajax.reload(null, false);
                    alert('Item deleted.');
                },
                error: function (xhr, data, errorThrown) {
                    Layout.hideLoader();
                    alert('There was an error. Please try again later');
                }
            });

        }
    },

    validatePublicFormTag: function () {
        var isValid = true;
        if ($("#ddlPublicFormTagCategories").val() == "-1") {
            isValid = false;
        }
        if ($("#ddlPublicFormTagTypes").val() == "-1") {
            isValid = false;
        }

        if ($("#txtPublicFormTagDescription").val() == "") { 
            isValid = false;
        }

        if (!isValid) {
            alert('please fill the required fields');
        }

        return isValid;
    },

    createNewPublicFormTag: function () {
        if (!UserPermissions.FormTagCreate) {
            alert('You do not have permissions to perform this action');
        }
        else {
            $("#hdnPublicFormTagId").val(0);
            $("#ddlPublicFormTagCategories").val('-1');
            $("#txtPublicFormTagDescription").val('');
            $("#ddlPublicFormTagTypes").val('-1');
            $("#txtPublicFormTagCaption").val('');
            $("#txtPublicFormTagDataField").val('');
            $("#txtPublicFormTagMaxSize").val('');

            PublicFormTag.showCreateEdit();
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
                    PublicFormTag.categories = data.data;
                    var currData = '';
                    $.each(PublicFormTag.categories, function (i, data) {
                        
                        currData += "<option value='" + data.CategoryID + "'>" + data.Description + "</option>";
                    });
                    $('#ddlPublicFormTagCategories').append(currData);
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

    onChangeddlPublicFormTagCategories: function () {
        var id = $("#ddlPublicFormTagCategories").val();
        var categoryDetail = PublicFormTag.categories.filter(x => x.CategoryID == id)[0];
        if (categoryDetail.Description != null && categoryDetail.Description != '')
            $("#txtPublicFormTagDescription").val(categoryDetail.Description);
    },

    showList: function () {
        $(".publicFormTagCreate").hide();
        $(".publicFormTagList").show();
    },
    showCreateEdit: function () {
        $(".publicFormTagCreate").show();
        $(".publicFormTagList").hide();
    }
}

$(document).ready(function () {

    PublicFormTag.showList();
    PublicFormTag.loadCategories();

});
