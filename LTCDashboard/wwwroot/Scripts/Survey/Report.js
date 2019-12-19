var SurveyReport = {
    isLoaded: false,

    displayComingSoon: function () {
        alert('Coming Soon');
    },

    defaultLoad: function () {
        if (Survey.OfficeId > 0) {
            if (!SurveyReport.isLoaded) {
                SurveyReport.isLoaded = true;
                SurveyReportTable = $('#tblSurveyReportForms').DataTable({
                    select: {
                        style: 'single'
                    },
                    responsive: true,
                    paging: true,
                    ordering: true,
                    info: true,
                    searching: true,
                    ajax: {
                        url: window.location.origin + '/Survey/GetSurveysReport',
                        type: "POST",
                        data: {
                            "OfficeId": function (d) {
                                return $("#ddlSurveyOffice").val();
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

                                var viewButton = '<a href="javascript:;" onclick="SurveyReport.displayComingSoon();" data-type="text" data-pk="1" data-placement="right" class="editable editable-click"  data-target="#renderPublicSurveyModal"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';

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

                SurveyReportTable.clear();;
                SurveyReportTable.ajax.reload(null, false);
            }
        }

    }
}

$(document).ready(function () {

});