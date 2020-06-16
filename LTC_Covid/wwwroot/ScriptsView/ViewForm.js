$.ajaxSetup({ cache: false });
var ViewForm = function () {
    var dt = null;
    var initViewFormTable = function () {
        Layout.showLoader();


        var table = $('#viewFormTable');
        if (dt != null) {
            dt.destroy();
        }
        dt = table.DataTable({

            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/Home/GetForms"
            }
            , drawCallback: function () {


            },
            "columns": [
                {
                    "title": "Name",
                    "data": "FullName",
                    "searchable": true
                },
                {
                    "title": "Form Name",
                    "data": "FormName",
                    "searchable": true
                },
                {
                    "title": "Pre-Screen",
                    "data": "IsPreScreen",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, full, meta) {
                      
                        var res = '';
                        if (type === 'display') {
                         
                            if (data == true) {
                                res = '<input type="radio" name="InPre-' + full.Id +'"   disabled="disabled" checked="checked"  value="' + data + '">';
                            } else {
                                res = '<input type="radio"  name="InPre-' + full.Id +'"    value="' + data + '">';
                            }
                        }

                        return res;
                    }
                } ,
                {
                    "title": "Pre-ScreenDate",
                    "data": "PreScreenDate",
                    "searchable": true,

                },
                {
                    "title": "In-Person",
                    "data": "IsInPersonScreen",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, full, meta) {
                        if (type === 'display') {
                            if (data == true) {
                                res = '<input type="radio" name="InPerson-'+full.Id+'"  disabled="disabled" checked="checked"  value="' + data + '">';
                            } else {
                                res = '<input type="radio"  disabled="disabled"   name="InPerson-' + full.Id +'"   value="' + data + '">';
                            }
                        }

                        return res;
                    }
                },
                {
                    "title": "In-Person Date",
                    "data": "InPersonScreenDate",
                    "searchable": true,

                }, {
                    "title": "Actions",
                    "data": "Id",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, full, meta) {

                        var actionLinks = $('#div_grid_actions').html();
                        actionLinks = actionLinks.replace(/__prm_id__/gi, data);
                        //, __prm_formid__
                        actionLinks = actionLinks.replace(/__prm_subid__/gi, full.SubscriberID);
                        actionLinks = actionLinks.replace(/__prm_formid__/gi, full.FormID);
                        return actionLinks;
                    }
                }

            ],
            "language": {
                "aria": {
                    "sortAscending": ": activate to sort column ascending",
                    "sortDescending": ": activate to sort column descending"
                },
                "emptyTable": "No data available in table",
                "info": "Showing _START_ to _END_ of _TOTAL_ records",
                "infoEmpty": "No records found",
                "infoFiltered": "(filtered1 from _MAX_ total records)",
                "lengthMenu": "Show _MENU_",
                "zeroRecords": "No matching records found",

            },



            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.

            "lengthMenu": [
                [5, 15, 20, 100],
                [5, 15, 20, 100] // change per page values here
            ],

            // set the initial value
            "pageLength": 10,
            "pagingType": "numbers",
            "columnDefs": [
                {  // set default column settings
                    'orderable': false,
                    'targets': [0]
                },
                {
                    "searchable": false,
                    "targets": [0]
                },
                {
                    "className": "dt-right",
                    //"targets": [2]
                }
            ],

            "order": [
                [1, "asc"]
            ], // set first column as a default sort by asc

            initComplete: function () {
                Layout.hideLoader();
            }
        });


    }


    return {

        //main functionto initiate the module
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }

            initViewFormTable();

        },
        refresh: function () {
            initViewFormTable();
        },
        disableButton: function () {
            Layout.showLoader();
        },









    };

}();






jQuery(document).ready(function () {

    ViewForm.init();


});