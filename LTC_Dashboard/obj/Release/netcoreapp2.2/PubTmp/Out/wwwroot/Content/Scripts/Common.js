////https://lipis.github.io/bootstrap-sweetalert/
var ltcApp = function () {
    return {
        showLoader: function (id) {

        },

        hideLoader: function (id) {

        },

        successMessage: function (title, msg) {
            if (title == null) {
                title = "Success";
            }

            swal(title, msg, "success");
            //swal({
            //    title: "Are you sure?",
            //    text: "Your will not be able to recover this imaginary file!",
            //    type: "warning",
            //    showCancelButton: true,
            //    confirmButtonClass: "btn-danger",
            //    confirmButtonText: "Yes, delete it!",
            //    closeOnConfirm: false
            //},
            //function () {
            //    swal("Deleted!", "Your imaginary file has been deleted.", "success");
            //});
        },

        errorMessage: function (title, msg) {
            if (title == null) {
                title = "Error";
            }

            swal(title, msg, "error");
        },

        warningMessage: function (title, msg) {
            if (title == null) {
                title = "Warning";
            }

            swal(title, msg, "warning");
        },

        validateEmail: function ($email) {
            var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
            return emailReg.test($email);
        },

        promptConfirmation: function (title, msg) {
            if (title == null) {
                title = "Are you sure?";
            }

            swal({
                    title: title,
                    text: msg,
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Yes, Close it!",
                    closeOnConfirm: true
                },
                function(isConfirm) {
                    if (isConfirm) {
                        $('#templateEditorWindow').modal('hide');
                    }
                });
        }


    };
}();