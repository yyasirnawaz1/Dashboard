var appointmentDateVisible = false;


var TodayAppointmentParent = {
    defaultLoad: function (fromDate, toDate) {
        try {
            if (fromDate == undefined || toDate == undefined) {
                var dateRange = $("#todayRangeControl").val().split('-');
                var fromDate = dateRange[0].replace(/\s+$/, '');
                var toDate = dateRange[1].replace(/\s+$/, '');
            }

        } catch (e) {

        }

        if (typeof (TodayAppointment) != 'undefined') {
            TodayAppointment.destroy();

            $('#ddlProviders').val(["Everyone"]);
            $("#ddlProviders option:not(:first-child)").attr('disabled', 'true');
            $('#ddlProviders').selectpicker('refresh');
            $('#ddlShowHideColumn').val([]);
            $('#ddlShowHideColumn').selectpicker('refresh');
        }
        else {

            $('#ddlProviders').val(["Everyone"]);
            $("#ddlProviders option:not(:first-child)").attr('disabled', 'true');
            $('#ddlProviders').selectpicker('refresh');
            $('#ddlShowHideColumn').val([]);
            $('#ddlShowHideColumn').selectpicker('refresh');
        }


        TodayAppointment = $('#tblTodayAppointment').DataTable({
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
                url: window.location.origin + '/Home/GetTodayAppointment?fromDate=' + fromDate + '&toDate=' + toDate,
                type: "GET",
                dataSrc: ''
            },
            dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
            columnDefs: [
                {
                    "targets": [1],
                    "visible": appointmentDateVisible,
                    "searchable": true
                },
                {
                    "targets": [3],
                    "render": function (data, type, row) {
                        var arrivedIcon = '<i class="fa ' + arrivedDynamicIcon + '" title="Arrived"></i>';
                        var checkInIcon = '<i class="fa ' + checkInDynamicIcon + '" title="Checkin"></i>';

                        var returnString = '';
                        if (!appointmentDateVisible && row.ArrivedTime != '01/01/0001') returnString += arrivedIcon;
                        if (!appointmentDateVisible && row.TakenInTime != '01/01/0001') returnString += checkInIcon;
                        returnString += data;

                        return returnString;
                    }
                },
                {
                    "targets": [5],
                    "visible": false,
                    "searchable": true
                },
                {
                    "targets": [6],
                    "visible": false,
                    "searchable": true
                },
                {
                    "targets": [7],
                    "visible": false,
                    "searchable": true
                },
                {
                    "targets": [8],
                    "data": null,
                    "render": function (data, type, full, row) {
                        var btnArrived = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.patientArrived(' + data + ')"><i class="fa ' + arrivedDynamicIcon + '" title="Arrived"></i></a>&nbsp;&nbsp;&nbsp;';
                        var btnSurvey = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.patientSurvey(' + data + ',' + full.PatientNumber + ',' + full.Office_Sequence + ')"><i class="fa fa-clipboard" title="Survey"></i></a> &nbsp;&nbsp;&nbsp;';
                        var btnCheckIn = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.patientCheckIn(' + data + ')"><i class="fa ' + checkInDynamicIcon + '" title="Checkin"></i></a> &nbsp;&nbsp;&nbsp; ';
                        var btnSelectForm = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.patientForm(' + data + ',' + full.PatientNumber + ',' + full.Office_Sequence + ')"><i class="fa fa-hand-pointer-o" title="Select Form"></i></a> &nbsp;&nbsp;&nbsp; ';
                        //Arrived, Checkin, Select Form

                        var allButtons = '';
                        if (!appointmentDateVisible && (full.ArrivedTime == '01/01/0001' || full.ArrivedTime == '' || full.ArrivedTime == null)) allButtons += btnArrived;
                        if (!appointmentDateVisible && (full.TakenInTime == '01/01/0001' || full.TakenInTime == '' || full.TakenInTime == null)) allButtons += btnCheckIn;
                        if (true) allButtons += btnSelectForm;
                        if (true) allButtons += btnSurvey;

                        return allButtons;
                    }
                }
            ],
            columns: [
                //{ data: "Account", width: "7%" },
                { data: "provider", width: "3%" },
                { data: "AppointDate", width: "7%" },
                { data: "TimeSLot", width: "7%" },
                { data: "Name", width: "16%" },
                { data: "job", width: "3%" },
                { data: "DESCRIPTION", width: "20%" },
                { data: "HomePhone", width: "15%" },
                { data: "CellPhone", width: "15%" },
                { data: "AppointCounter", width: "14%" },
            ]
        });
    },

    showHideColumn: function () {
        var items = $('#ddlShowHideColumn').val();

        if (items.indexOf('7') > -1)
            TodayAppointment.column(7).visible(true);
        else
            TodayAppointment.column(7).visible(false);

        if (items.indexOf('5') > -1)
            TodayAppointment.column(5).visible(true);
        else
            TodayAppointment.column(5).visible(false);

        if (items.indexOf('6') > -1)
            TodayAppointment.column(6).visible(true);
        else
            TodayAppointment.column(6).visible(false);
    },

    changeDate: function (fromDate, toDate) {
        //appointmentDateVisible = true;
        ////$('#txtDateSpan').html($('#daterangepicker_start').val() + ' - ' + $('#daterangepicker_end').val());

        //var dateRange = $("#todayRangeControl").val().split('-');
        //var fromDate = dateRange[0].replace(/\s+$/, '');
        //var toDate = dateRange[1].replace(/\s+$/, '');


        this.defaultLoad(fromDate, toDate);
    },

    loadDefaultDates: function () { 
        //old method. need to remove this
        var curDate = new Date();
        var dateString = (curDate.getMonth() + 1 < 10 ? '0' + (curDate.getMonth() + 1) : (curDate.getMonth() + 1)) + '/'
            + (curDate.getDate() < 10 ? '0' + curDate.getDate() : curDate.getDate()) + '/' + curDate.getFullYear();
        
        //$('#todayRangeControl').html(dateString + ' - ' + dateString);

        $('#todayRangeControl').data('daterangepicker').setStartDate(dateString);
        $('#todayRangeControl').data('daterangepicker').setEndDate(dateString);
        //$('#daterangepicker_start').val(dateString);
        //$('#daterangepicker_end').val(dateString);
    },

    refreshTable: function () {
        this.loadDefaultDates();
        appointmentDateVisible = false;
        this.defaultLoad();
    },

    onProvidersDDLChange: function () {
        var items = $('#ddlProviders').val();

        if (items.indexOf('Everyone') > -1) {
            $("#ddlProviders option:not(:first-child)").attr('disabled', 'true');
            $('#ddlProviders').selectpicker('refresh');
            TodayAppointment.column(0).search('').draw();
        }
        else {
            $("#ddlProviders option:not(:first-child)").removeAttr('disabled');
            $('#ddlProviders').selectpicker('refresh');

            var searchQuery = '';

            if (items.indexOf('Doctors only') > -1) {
                for (var i = 0; i < allProviders.length; i++)
                    if (!allProviders[i].hygienist)
                        searchQuery += '^' + allProviders[i].Provider + '$|';
            }

            if (items.indexOf('hygienists only') > -1) {
                for (var i = 0; i < allProviders.length; i++)
                    if (allProviders[i].hygienist)
                        searchQuery += '^' + allProviders[i].Provider + '$|';
            }

            for (var i = 0; i < items.length; i++) {
                if (items[i] != 'Everyone' && items[i] != 'Doctors only' && items[i] != 'hygienists only')
                    searchQuery += '^' + items[i] + '$|';
            }

            searchQuery = searchQuery.substr(0, searchQuery.length - 1)

            TodayAppointment.column(0).search(searchQuery, true, false).draw();
        }
    },

    patientCheckIn: function (Id) {
        Common.showLoader();
        $.post("/Home/AddPortalStatus", { AppId: Id, PortalAction: 1, Type: 0 }, function (result) {
            Common.hideLoader();
            alert('Status Successfully Added');
            TodayAppointmentParent.defaultLoad();
        });
    },

    patientArrived: function (Id) {
        Common.showLoader();
        $.post("/Home/AddPortalStatus", { AppId: Id, PortalAction: 1, Type: 1 }, function (result) {
            Common.hideLoader();
            alert('Status Successfully Added');
            TodayAppointmentParent.defaultLoad();
        });
    },

    patientForm: function (Id, PatientNumber, OfficeSequence) {
        globalFormVariables.AppointCounter = Id;
        globalSurveyVariables.PatientNumber = PatientNumber;
        globalSurveyVariables.OfficeSequence = OfficeSequence;
        $('#modalPrivateForms').modal('show');
    },

    patientSurvey: function (Id, PatientNumber, OfficeSequence) {
        globalSurveyVariables.AppointCounter = Id;
        globalSurveyVariables.PatientNumber = PatientNumber;
        globalSurveyVariables.OfficeSequence = OfficeSequence;
        $('#modalPrivateSurveys').modal('show');
    },

    populatePrivateSurveys: function () {
        PrivateSurveysTable = $('#tblPrivateSurveys').DataTable({
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
                url: window.location.origin + '/Home/GetPrivateSurveys',
                type: "GET",
                dataSrc: ''
            },
            dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
            columnDefs: [
                {
                    "targets": [2],
                    "data": null,
                    "render": function (data, type, row) {

                        var btnProcessed = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.selectSurvey(' + data + ')"><i class="fa fa-hand-pointer-o" title="Select"></i></a> &nbsp;&nbsp;&nbsp;';

                        return btnProcessed;
                    }
                },
                {
                    "targets": [1],
                    "render": function (data) {
                        var date = new Date(parseInt(data.substr(6, 13)));
                        return (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                    }
                }
            ],
            columns: [
                { data: "Description", width: "60%" },
                { data: "SystemDate", width: "30%" },
                { data: "FormID", width: "10%" }
            ]
        });
    },

    populatePrivateForms: function () {
        PrivateFormsTable = $('#tblPrivateForms').DataTable({
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
                url: window.location.origin + '/Home/GetPrivateForms',
                type: "GET",
                dataSrc: ''
            },
            dom: '<"top">rt<"bottom"<"datatableLeftLengthDiv"l>p><"clear">',
            columnDefs: [
                {
                    "targets": [2],
                    "data": null,
                    "render": function (data, type, row) {

                        var btnProcessed = '<a class="editable editable-click apply-Cursur-Pointer" onclick="TodayAppointmentParent.selectForm(' + data + ')"><i class="fa fa-hand-pointer-o" title="Select"></i></a> &nbsp;&nbsp;&nbsp;';

                        return btnProcessed;
                    }
                },
                {
                    "targets": [1],
                    "render": function (data) {
                        var date = new Date(data);
                        return (date.getMonth() + 1) + '/' + date.getDate() + '/' + date.getFullYear();
                    }
                }
            ],
            columns: [
                { data: "Description", width: "60%" },
                { data: "SystemDate", width: "30%" },
                { data: "FormID", width: "10%" }
            ]
        });
    },

    selectForm: function (FormId) {
        globalFormVariables.FormID = FormId;
        //if (FormId > 0) {
        //    var record = PrivateFormsTable.data().filter(x => x.FormID == FormId);
        //    var content = HomeLayout.removeSelectedTitles(record[0].Content);

        //    HomeLayout.renderFormDesignerFormSubmit(content, 'iframeFormSubmit');
        //}
        //$('#btnFormSubmit').attr('onclick', 'TodayAppointmentParent.submitFormData(0);');
        $('#modalPrivateForms').modal('hide');
        this.addFormQueueEntry(0, '/Home/FormRender?');
    },

    selectSurvey: function (FormId) {
        globalSurveyVariables.FormID = FormId;
        //if (FormId > 0) {
        //    var record = PrivateSurveysTable.data().filter(x => x.FormID == FormId);
        //    var content = HomeLayout.removeSelectedTitles(record[0].Content);

        //    HomeLayout.renderFormDesignerFormSubmit(content, 'iframeFormSubmit');
        //}
        //$('#btnFormSubmit').attr('onclick', 'TodayAppointmentParent.submitFormData(1);');
        $('#modalPrivateSurveys').modal('hide');
        this.addSurveyQueueEntry(1, '/Home/SurveyRender?');
    },

    addSurveyQueueEntry: function (type, navigateURL) {
        var AC, FID;
        if (type == 0) {
            AC = globalFormVariables.AppointCounter;
            FID = globalFormVariables.FormID;
            PN = globalSurveyVariables.PatientNumber;
            OS = globalSurveyVariables.OfficeSequence;
        } else {
            AC = globalSurveyVariables.AppointCounter;
            FID = globalSurveyVariables.FormID;
            PN = globalSurveyVariables.PatientNumber;
            OS = globalSurveyVariables.OfficeSequence;
        }

        Common.showLoader();
        $.post("/Home/AddSurveyQueueEntry", { AppointmentCounter: AC, FormID: FID, Type: type, Office_Sequence: OS, PatientNumber: PN }, function (result) {
            Common.hideLoader();
            //alert('Status Successfuly Added');
            //var win = window.open(navigateURL + 'os=' + AP + '&fi=' + FID + '&pn=' + PN, '_blank');
            //win.focus();

            //window.location = '/account/logout';

            window.location.href = navigateURL + 'ac=' + AC + '&fi=' + FID + '&pn=' + PN + '&os=' + OS;

        });
    },

    addFormQueueEntry: function (type, navigateURL) {
        var AC, FID;
        if (type == 0) {
            AC = globalFormVariables.AppointCounter; // remove this
            FID = globalFormVariables.FormID;
            PN = globalSurveyVariables.PatientNumber;
            OS = globalSurveyVariables.OfficeSequence;
        } else {
            AC = globalSurveyVariables.AppointCounter; // remove this
            FID = globalSurveyVariables.FormID;
            PN = globalSurveyVariables.PatientNumber;
            OS = globalSurveyVariables.OfficeSequence;
        }

        Common.showLoader();
        $.post("/Home/AddFormQueueEntry", { AppointmentCounter: AC, FormID: FID, Type: type, Office_Sequence: OS, PatientNumber: PN }, function (result) {
            Common.hideLoader();
            //alert('Status Successfuly Added');
            //var win = window.open(navigateURL + 'os=' + AP + '&fi=' + FID + '&pn=' + PN, '_blank');
            //win.focus();

            //window.location = '/account/logout';
            window.location.href = navigateURL + 'ac=' + AC + '&fi=' + FID + '&pn=' + PN + '&os=' + OS;
        });
    },

    submitFormData: function (type) {

        var content = $('#iframeFormSubmit')[0].contentWindow.getFormBuilderData();

        if (type == 0) {
            AC = globalFormVariables.AppointCounter;
            FID = globalFormVariables.FormID;
            PN = globalSurveyVariables.PatientNumber;
            OS = globalSurveyVariables.OfficeSequence;
        } else {
            AC = globalSurveyVariables.AppointCounter;
            FID = globalSurveyVariables.FormID;
            PN = globalSurveyVariables.PatientNumber;
            OS = globalSurveyVariables.OfficeSequence;
        }

        Common.showLoader();
        $.post("/Home/SaveSurveyAndForm", { AppointmentCounter: AC, FormID: FID, Type: type, Content: content, Office_Sequence: OS, PatientNumber: PN }, function (result) {
            Common.hideLoader();
            //remove submit button
            $("btnSubmit").hide();
            alert('Data Successfuly Added');
            $('#modalFormSubmit').modal('hide');
        });

    }
}
$(document).ready(function () {
    TodayAppointmentParent.defaultLoad();
    //TodayAppointmentParent.loadDefaultDates();
    TodayAppointmentParent.populatePrivateForms();
    TodayAppointmentParent.populatePrivateSurveys();


    //$("#applyBtn .applyBtn").bind({
    //    click: function () {
    //        TodayAppointmentParent.changeDate();
    //    }
    //});
   
    $('input[id="todayRangeControl"]').on('apply.daterangepicker', function (ev, picker) {
       TodayAppointmentParent.changeDate(picker.startDate.format('MM/DD/YYYY'), picker.endDate.format('MM/DD/YYYY'));
    });
});