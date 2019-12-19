var FormReport = {
    isLoaded: false,

    displayComingSoon: function () {
        alert('Coming Soon');
    },

    defaultLoad: function () {
        if (Form.OfficeId > 0) {
            if (!FormReport.isLoaded) {
                FormReport.isLoaded = true;
                FormReportTable = $('#tblFormReportForms').DataTable({
                    select: {
                        style: 'single'
                    },
                    responsive: true,
                    paging: true,
                    ordering: true,
                    info: true,
                    searching: true,
                    ajax: {
                        url: window.location.origin + '/Form/GetFormsReport',
                        type: "POST",
                        data: {
                            "OfficeId": function (d) {
                                return $("#ddlFormOffice").val();
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
                            "targets": [3],
                            "data": null,
                            "render": function (data, type, row) {

                                var viewButton = '<a href="javascript:;" onclick="FormReport.displayComingSoon();" data-type="text" data-pk="1" data-placement="right" class="editable editable-click"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';

                                return viewButton;
                            }
                        }
                    ],
                    columns: [
                        { data: "Count", width: "0" },
                        { data: "Description", width: "60%" },
                        { data: "Count", width: "20%" },
                        { data: "Count", width: "20%" },
                    ]
                });
            } else {

                FormReportTable.clear();;
                FormReportTable.ajax.reload(null, false);
            }
        }

    }
}

$(document).ready(function () {

});