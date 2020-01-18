$.ajaxSetup({ cache: false });
var ScheduledNewsletters = function () {

    var dt = null;
    var initScheduledNewslettersTable = function () {
        Layout.showLoader();
        var table = $('#scheduledNewsLetterTable');
        if (dt != null) {
            dt.destroy();
        }
        // begin first table
        dt = table.DataTable({

            // Internationalisation. For more info refer to http://datatables.net/manual/i18n
            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/Report/Get"
            },
            "columns": [
                {
                    "title": "Title",
                    "data": "TemplateTitle",
                    "searchable": true
                },
                {
                    "title": "ScheduledDate",
                    "data": "AppointDate",
                    "searchable": true
                    }
                    ,
                {
                    "title": "Status",
                    "data": "Status",
                    "searchable": true,
                    "render": function (data, type, full, meta) {
                        var status = data;
                        switch (status) {
                            case 0:
                                {
                                    status = '<span class="label label-sm label-danger"> Not Sent </span>';
                                    break;
                                }
                            case 1:
                                {
                                    status = '<span class="label label-sm label-warning"> Scheduled </span>';
                                    break;
                                }
                            case 2:
                                {
                                    status = '<span class="label label-sm label-success"> Sent </span>';
                                    break;
                                }
                            default:
                                {
                                    status = '<span class="label label-sm label-success"> Unknown </span>';
                                    break;
                                }
                        }
                       
                        return status;
                    }
                }
                ,
              
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
                "search": "Search:",
                "zeroRecords": "No matching records found",
                 
            },

        
            "bStateSave": false, // save datatable state(pagination, sort, etc) in cookie.

            "lengthMenu": [
                [5, 15, 20, 100],
                [5, 15, 20, 100] // change per page values here
            ],

            // set the initial value
            "pageLength": 100,
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
                 

            }
        });




    }


    return {

        //main functionto initiate the module
        init: function () {
            if (!jQuery().dataTable) {
                return;
            }

            initScheduledNewslettersTable();

        },
        refresh: function () {

            initScheduledNewslettersTable();
        },



    };

}();

jQuery(document).ready(function () {
    ScheduledNewsletters.init();
});