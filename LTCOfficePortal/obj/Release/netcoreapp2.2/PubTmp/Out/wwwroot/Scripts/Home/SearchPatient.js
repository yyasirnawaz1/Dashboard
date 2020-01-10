  var OTable = null; 
var SearchPatient = {

    defaultLoad: function () {
      
        OTable = $('#tblSearchPatient').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/Home/GetPatients",
            "sServerMethod": "POST",
            "fnDrawCallback": function (oSettings) {
                Layout.hideLoader();
            },
            "aoColumns": [
                { mData: "Account", width: "6%" },
                { mData: "Doctor", width: "6%" },
                { mData: "Name", width: "24%" },
                { mData: "Sex_Age", width: "6%" },
                { mData: "HomePhone", width: "12%" },
                { mData: "CellPhone", width: "12%" },
                { mData: "Birthdate", width: "12%" },
                { mData: "LastVisit", width: "12%" },
                { mData: "Account", width: "10%" },
                { mData: "PatientNumber", width: "0%" }
            ],
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
                    "searchable": true
                },
                {
                    "targets": [5],
                    "visible": true,
                    "searchable": true
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

                        var btnSelectForm = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.patientForm(' + 0 + ',' + full.PatientNumber + ',' + full.Office_Sequence + ')"><i class="fa fa-hand-pointer-o" title="Select Form"></i></a> &nbsp;&nbsp;&nbsp; ';
                        return btnSelectForm;
                    }
                }
            ],
            "ordering": false,
            "searching": true,
            "serverSide": true,
            "processing": true,
            "language": {
                "processing": "loading ... "
            }

        });  
        //SearchPatientTable = $('#tblSearchPatient').DataTable({
        //    autoWidth: false,
        //    select: {
        //        style: 'single'
        //    },
        //    responsive: true,
        //    paging: true,
        //    ordering: true,
        //    info: true,
        //    searching: true,
        //    "bServerSide": true,
        //    ajax: {
        //        url: window.location.origin + '/Home/GetPatients',
        //        type: "POST",
        //        dataSrc: ''
        //    },
        //    dom: '<"top">frt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
        //    columnDefs: [
        //        {
        //            "targets": [0],
        //            "visible": true,
        //            "searchable": true
        //        },
        //        {
        //            "targets": [1],
        //            "visible": true,
        //            "searchable": false
        //        },
        //        {
        //            "targets": [2],
        //            "visible": true,
        //            "searchable": true
        //        },
        //        {
        //            "targets": [3],
        //            "visible": true,
        //            "searchable": false
        //        },
        //        {
        //            "targets": [4],
        //            "visible": true,
        //            "searchable": true
        //        },
        //        {
        //            "targets": [5],
        //            "visible": true,
        //            "searchable": true
        //        },
        //        {
        //            "targets": [6],
        //            "visible": true,
        //            "searchable": false
        //        },
        //        {
        //            "targets": [7],
        //            "visible": true,
        //            "searchable": false
        //        },
        //        {
        //            "targets": [9],
        //            "visible": false,
        //            "searchable": true
        //        },
        //        {
        //            "targets": [8],
        //            "data": null,
        //            "render": function (data, type, full, row) {

        //                var btnSelectForm = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.patientForm(' + 0 + ',' + full.PatientNumber + ',' + full.Office_Sequence + ')"><i class="fa fa-hand-pointer-o" title="Select Form"></i></a> &nbsp;&nbsp;&nbsp; ';
        //                return btnSelectForm;
        //            }
        //        }
        //    ],
        //    columns: [
        //        { data: "Account", width: "6%" },
        //        { data: "Doctor", width: "6%" },
        //        { data: "Name", width: "24%" },
        //        { data: "Sex_Age", width: "6%" },
        //        { data: "HomePhone", width: "12%" },
        //        { data: "CellPhone", width: "12%" },
        //        { data: "Birthdate", width: "12%" },
        //        { data: "LastVisit", width: "12%" },
        //        { data: "Account", width: "10%" },
        //        { data: "PatientNumber", width: "0%" }
        //    ]
        //});
    }
}
$(function () {
    SearchPatient.defaultLoad();
});