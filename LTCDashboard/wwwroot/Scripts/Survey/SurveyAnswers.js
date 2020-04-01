var SurveyAnswers = {

    selectedSurveyID: 0,
    isLoaded: false,

    //private form view
    renderForm: function (id) {
        if (id > 0) {
            $('#renderSurveys').html('');
            var record = SurveysTable.data().filter(x => x.FormID == id);
            $("#renderSurveysModal-header").html(record[0].SurveyDescription);
            $('#renderSurveys').formRender({
                dataType: 'json',
                formData: record[0].Content
            });
        }
        else {
            $('#renderSurveys').html('Select a survey from table.');
        }
    },

    defaultLoad: function () {
        if (Survey.OfficeId > 0) {
            if (!SurveyAnswers.isLoaded) {
                SurveyAnswers.isLoaded = true;
                SurveysTable = $('#tblSurveys').DataTable({
                    select: {
                        style: 'single'
                    },
                    responsive: true,
                    paging: true,
                    ordering: true,
                    info: true,
                    searching: true,
                    ajax: {
                        url: window.location.origin + '/Survey/GetSurveysAnswers',
                        type: "POST",
                        data: {
                            "OfficeId": function (d) {
                                return $("#ddlSurveyOffice").val();
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

                                var viewButton = '<a href="javascript:;" onclick="SurveyAnswers.renderForm(' + data + ');" data-type="text" data-pk="1" data-placement="right" class="editable editable-click" data-toggle="modal" data-target="#renderSurveysModal"><i class="fa fa-eye" title="View"></i></a> &nbsp;&nbsp;&nbsp;';

                                if (UserPermissions.SurveyAnsweredPreview)
                                return viewButton;
                            }
                        }
                    ],
                    columns: [
                        { data: "FormID", width: "0" },
                        //{ data: "SurveyDescription", width: "60%" },
                        { data: "FormID", width: "60%" },
                        { data: "PatientNumber", width: "20%" },
                        { data: "FormID", width: "20%" },
                    ]
                });
                SurveysTable.on('select', function (e, dt, type, indexes) {
                    if (type === 'row') {
                        var data = SurveysTable.row('.selected').data();
                        SurveyAnswers.selectedSurveyID = data.FormID;
                    }
                });

            } else {
                SurveysTable.clear();;
                SurveysTable.ajax.reload(null, false);
            }
        }
    }
}

$(document).ready(function () {

});
