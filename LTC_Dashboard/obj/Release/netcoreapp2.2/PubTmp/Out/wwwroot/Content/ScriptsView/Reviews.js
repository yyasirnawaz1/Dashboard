$.ajaxSetup({ cache: false });
var Reviews = function () {
   
    var initReviewTable = function () {
        Layout.showLoader();

         var table = $('#reviewTable');
        if (dt != null) {
            dt.destroy();
        }
        // begin first table
        dt = table.DataTable({

            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/Review/Get"
            }
            , drawCallback: function () {


            },
            "columns": [
                {
                    "title": "Name",
                    "data": "FirstName",
                    "searchable": true
                },
                {
                    "title": "Email",
                    "data": "EmailAddress",
                    "searchable": true
                },
                {
                    "title": "Status",
                    "data": "SubscriptionStatus",
                    "searchable": true,
                    "render": function (data, type, full, meta) {
                        var strstatus = data;
                        if (data == 1) {
                            strstatus = '<span class="badge badge-success"> Subscribed </span>';
                        } else {
                            strstatus = ' <span class="badge badge-danger"> Unsubscribed </span>';
                        }
                        return strstatus;
                    }
                },
                {
                    "title": "Actions",
                    "data": "Id",
                    "searchable": false,
                    "sortable": false,
                    "render": function (data, type, full, meta) {
                        var actionLinks = $('#div_grid_actions').html();
                        actionLinks = actionLinks.replace(/__prm_id__/gi, data);
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

            initReviewTable();

        },
     
    };

}();
 




jQuery(document).ready(function () {

    Subscription.init();


});