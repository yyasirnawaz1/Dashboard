var FormAnswers = {

    selectedFormID: 0,
    isLoaded: false,

    //private form view
    renderForm: function (id) {
        if (id > 0) {
            $('#renderForms').html('');
            var record = FormsTable.data().filter(x => x.FormID == id);
            $("#renderFormsModal-header").html(record[0].FormDescription);
            $('#renderForms').formRender({
                dataType: 'json',
                formData: record[0].Content
            });
        }
        else {
            $('#renderForms').html('Select a form from table.');
        }
    },

    defaultLoad: function () {
        if (Form.OfficeId > 0) {
            if (!FormAnswers.isLoaded) {
                FormAnswers.isLoaded = true;
                FormsTable = $('#tblForms').DataTable({
                    select: {
                        style: 'single'
                    },
                    responsive: true,
                    paging: true,
                    ordering: true,
                    info: true,
                    searching: true,
                    ajax: {
                        url: window.location.origin + '/Form/GetFormsAnswers',
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

                                var viewButton = '<a href="javascript:;" onclick="FormAnswers.renderForm(' + data + ');" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#renderFormsModal"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';

                                if (UserPermissions.FormAnsweredPreview)

                                return viewButton;
                            }
                        }
                    ],
                    columns: [
                        { data: "FormID", width: "0" },
                        //{ data: "FormDescription", width: "70%" },
                        { data: "FormID", width: "70%" },
                        { data: "PatientNumber", width: "20%" },
                        { data: "FormID", width: "10%" },
                    ]
                });
                FormsTable.on('select', function (e, dt, type, indexes) {
                    if (type === 'row') {
                        var data = FormsTable.row('.selected').data();
                        FormAnswers.selectedFormID = data.FormID;
                    }
                });

            } else {
                FormsTable.clear();;
                FormsTable.ajax.reload(null, false);
            }
        }
    }
}

$(document).ready(function () {

});
