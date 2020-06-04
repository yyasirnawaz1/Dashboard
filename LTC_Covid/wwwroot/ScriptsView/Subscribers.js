$.ajaxSetup({ cache: false });
var Subscription = function () {
    var dt = null;
    var initSubscriptionTable = function () {
        Layout.showLoader();


        var table = $('#subscriberTable');
        if (dt != null) {
            dt.destroy();
        }
        // begin first table
        dt = table.DataTable({

            "serverSide": true,
            "processing": true,
            "ajax": {
                "url": "/Subscribers/Get"
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
                    "title": "Last Update At",
                    "data": "LastSubscriptionStatusUpdated",
                    "searchable": true,
                   
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
            "pageLength": 20,
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

            initSubscriptionTable();

        },
        refresh: function () {

            initSubscriptionTable();
        },
        disableButton: function () {
            Layout.showLoader();
        },


        toggleSubscription: function (actionURL) {
            Layout.showLoader();




            $.ajax({
                url: actionURL,
                type: 'Post',
                async: false,
                cache: false,
                success: function (data) {

                    Subscription.refresh();


                },

                error: function (xhr) {
                    ltcApp.errorMessage("Error", 'error');

                }


            });

        },

        newSubscription: function (actionURL) {
            $.get(actionURL, function (data) {
                $('#modifySubscriptionContainer').html(data);
                jQuery.validator.unobtrusive.parse('#modifySubscriptionContainer');
                $('#modifySubscription').modal('show');
            });
            $("#modificationArea").addClass("hide");

        },

        modifySubscription: function (actionURL) {


            $.get(actionURL, function (data) {
                $('#modifySubscriptionContainer').html(data);
                jQuery.validator.unobtrusive.parse('#modifySubscriptionContainer');
                $('#modifySubscription').modal('show');
            });


            $("#modificationArea").removeClass("hide");

        },

        deleteSubscription: function (actionURL) {

            //
            swal({
                title: "Are you sure you want to delete?",
                text: "Record will be deleted permanently",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes",
                cancelButtonText: "No",
                closeOnConfirm: false,
                closeOnCancel: true
            },
                function (isConfirm) {
                    if (isConfirm) {
                        Layout.showLoader();

                        $.ajax({
                            url: actionURL,
                            type: 'Post',
                            async: false,
                            cache: false,
                            contentType: 'application/json',
                            success: function (data) {

                                Subscription.refresh();

                                ltcApp.successMessage("Deleted!", "Record has been deleted.", "success");


                            },

                            error: function (xhr) {

                                ltcApp.errorMessage("Error!", "Error", "error");
                            }


                        });

                    }
                });
            //


        },



        saveSubscription: function () {



        }
    };

}();


function createSubscriptionSuccess(data) {

    var statusCode = data.StatusCode;
    switch (statusCode) {
        case 1:
            {
                $('#btnSubmitAdd').removeAttr('disabled');

                Subscription.refresh();
                $('#modifySubscription').modal('hide');
                ltcApp.successMessage(null, data.StatusMessage);

                break;
            }
        case 0:
            {
                $('#btnSubmitAdd').removeAttr('disabled');
                ltcApp.errorMessage(data.StatusMessage);
                break;
            }
        default:
            {
                ltcApp.successMessage(data.StatusMessage);
                break;
            }
    }
}





jQuery(document).ready(function () {

    Subscription.init();


});