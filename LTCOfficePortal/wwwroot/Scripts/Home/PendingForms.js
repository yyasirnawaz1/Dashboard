PendingForms = {
    defaultLoad: function () {
        PendingFormsTable = $('#tblPendingForms').DataTable({
            "order": [[ 1, "desc" ]],
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
                url: window.location.origin + '/Home/GePendingtForms',
                type: "GET",
                dataSrc: ''
            },
            dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
            columnDefs: [
                {
                    "targets": [2],
                    "data": null,
                    "render": function (data, type, row) {

                        var btnPreview = '<a class="editable editable-click apply-Cursur-Pointer" onclick="PendingForms.preview(' + data + ')"><i class="fa fa-eye" title="Preview"></i></a>&nbsp;&nbsp;&nbsp;';
                        var btnProcessed = '<a class="editable editable-click apply-Cursur-Pointer" onclick="PendingForms.processed(' + data + ')"><i class="fa fa-clipboard" title="Processed"></i></a> &nbsp;&nbsp;&nbsp;';

                        return btnPreview + btnProcessed;
                    }
                }
            ],
            columns: [
                { data: "PatientName", width: "60%" },
                { data: "SystemDate", width: "30%" },
                { data: "SavedFormID", width: "10%" }
            ]
        });
    },

    preview: function (id) {
        if (id > 0) {
            var record = PendingFormsTable.data().filter(x => x.SavedFormID == id);
            var content = HomeLayout.removeSelectedTitles(record[0].Content);

            //PendingSurveys.renderFormDesignerInIframe(content, 'iframePreviewPrivateSurvey');
            HomeLayout.renderFormDesignerInIframe(content, 'iframeThePreview');
        }
    },

    processed: function () {
        alert('Comming Soon');
    }
}

$(function () {
    PendingForms.defaultLoad();
});