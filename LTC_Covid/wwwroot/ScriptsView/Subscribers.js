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
            "responsive": true,

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
                    "title": "Last Updated",
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

            initSubscriptionTable();

        },
        refresh: function () {
            $("#email").attr("disabled", false);
            
            $("#fname").val('');
            $("#lname").val('');
            $("#mname").val('');
            $("#ddlSalutation").val('-1');
            $("#email").val('');
            $("#IdValue").val('0');
            
            $("#customID").val('');

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
        newSubscription: function () {
            $("#fname").val('');
            $("#lname").val('');
            $("#mname").val('');
            $("#ddlSalutation").val('-1');
            $("#email").val('');
            $("#IdValue").val('0');

            $("#customID").val('');

            $('#subscriberModel').modal('show');
            
        },
        saveSubscription: function () {
            debugger;

            var firstName = '';
            var lname = '';
            var mname = '';
            var Salutation = '';
            var email = '';

            if ($("#fname").val() == "") {
                ltcApp.warningMessage(null, "Please provide first name");
                return;
            } else {
                firstName = $("#fname").val();
            }
            if ($("#lname").val() == "") {
                ltcApp.warningMessage(null, "Please provide last name");
                return;
            } else {
                lname = $("#lname").val();
            }

            //if ($("#mname").val() == "") {
            //    ltcApp.warningMessage(null, "Please provide middle name");
            //    return;
            //} else {
            //    mname = $("#mname").val();
            //}
            mname = $("#mname").val();
            Salutation = $("#ddlSalutation").val();

            //if ($("#ddlSalutation").val() == "-1") {
            //    ltcApp.warningMessage(null, "Please provide salutation");
            //    return;
            //} else {
            //}

            if (ltcApp.validateEmail($("#email").val())) {
                email = $("#email").val();
            } else {
                ltcApp.warningMessage(null, "Invalid email address.");
                return;
            }


            //   if (template != null && article != null) {
            var IdValue = $("#IdValue").val();
            $("#btnSaveSub").attr("disabled", true);
            var data = {
                ID : IdValue,
                FirstName: firstName,
                LastName: lname,
                MiddleInitial: mname,
                EmailAddress: email,
                Salutation: Salutation,
            };


            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Subscribers/Upsert',
                success: function (data) {
                    
                    if (data.success) {
                        ltcApp.successMessage("Success", 'Subscriber has been added into the system.');
                        //window.location.href = "../Subscribers/Index";

                        Subscription.refresh();
                    } else {
                        ltcApp.errorMessage("Error", data.Message);

                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading preview');
                    $("#btnSaveSub").attr("disabled", false);

                },
                complete: function () {
                   
                    $('#subscriberModel').modal('hide');
                    $("#btnSaveSub").attr("disabled", false);
 
                }
            })
 


        },

        modifySubscription: function (actionURL) {


            $.get(actionURL, function (data) {
           
                if (data.success) {
                    
                    $("#fname").val(data.obj.FirstName);
                    $("#lname").val(data.obj.LastName);
                    $("#mname").val(data.obj.MiddleInitial);
                    $("#ddlSalutation").val(data.obj.Salutation);
                    $("#email").val(data.obj.EmailAddress);
                    $("#IdValue").val(data.obj.ID);
                    $("#customID").val(data.obj.CustomID);
                  //  $("#email").attr("disabled",true);
                    $('#subscriberModel').modal('show');
                }


            });



        },

        deleteSubscription: function (actionURL) {

            var result = confirm("Are you sure, you want to delete?");
            if (result) {

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




        },
       




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