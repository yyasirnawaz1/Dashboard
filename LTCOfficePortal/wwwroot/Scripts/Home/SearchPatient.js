var SearchPatient = {
    defaultLoad: function () {
        SearchPatientTable = $('#tblSearchPatient').DataTable({
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
                url: window.location.origin + '/Home/GetPatients',
                type: "GET",
                dataSrc: ''
            },
            dom: '<"top">frt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
            columnDefs: [
                {
                    "targets": [0],
                    "visible": true,
                    "searchable": true
                },
                {
                    "targets": [1],
                    "visible": true,
                    "searchable": false
                },
                {
                    "targets": [2],
                    "visible": true,
                    "searchable": true
                },
                {
                    "targets": [3],
                    "visible": true,
                    "searchable": false
                },
                {
                    "targets": [4],
                    "visible": true,
                    "searchable": true//
                },
                {
                    "targets": [5],
                    "visible": true,
                    "searchable": true//
                },
                {
                    "targets": [6],
                    "visible": true,
                    "searchable": false
                },
                {
                    "targets": [7],
                    "visible": true,
                    "searchable": false
                },
                {
                    "targets": [9],
                    "visible": false,
                    "searchable": true
                },
                {
                    "targets": [8],
                    "data": null,
                    "render": function (data, type, full, row) {

                        //var arriveButton = '<a class="editable editable-click apply-Cursur-Pointer"><i class="fa fa-taxi" title="Arrived"></i></a>&nbsp;&nbsp;&nbsp;';
                        ////var viewButton = '<a class="editable editable-click">Checkin</a> &nbsp;&nbsp;&nbsp;';
                        //var checkinButton = '<a class="editable editable-click apply-Cursur-Pointer"><i class="fa fa-check" title="Checkin"></i></a> &nbsp;&nbsp;&nbsp; ';
                        //var formButton = '<a class="editable editable-click apply-Cursur-Pointer"><i class="fa fa-hand-pointer-o" title="Select Form"></i></a> &nbsp;&nbsp;&nbsp; ';
                        var btnSelectForm = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.patientForm(' + 0 + ',' + full.PatientNumber + ',' + full.Office_Sequence + ')"><i class="fa fa-hand-pointer-o" title="Select Form"></i></a> &nbsp;&nbsp;&nbsp; ';

                        //var allButtons = '';
                        //if (true) allButtons += arriveButton;
                        //if (true) allButtons += checkinButton;
                        //if (true) allButtons += formButton;

                        return btnSelectForm;
                    }
                }
            ],
            columns: [
                { data: "Account", width: "6%" },
                { data: "Doctor", width: "6%" },
                { data: "Name", width: "24%" },
                { data: "Sex_Age", width: "6%" },
                { data: "HomePhone", width: "12%" },
                { data: "CellPhone", width: "12%" },
                { data: "Birthdate", width: "12%" },
                { data: "LastVisit", width: "12%" },
                { data: "Account", width: "10%" },
                { data: "PatientNumber", width: "0%" }
            ]
        });
    }
}
$(function () {
    SearchPatient.defaultLoad();
});