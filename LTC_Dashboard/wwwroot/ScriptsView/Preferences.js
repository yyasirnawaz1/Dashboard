$.ajaxSetup({ cache: false });
var Preferences = function () {


    return {


        clearChangePasswordForm: function () {

            $("#formchangepassword div").removeClass("error");
            $("#formchangepassword label").removeClass("error");
            $("#formchangepassword")[0].reset();

        }





    };

}();

function passwordChangeSuccess(data) {
    var statusCode = data.StatusCode;
    switch (statusCode) {
        case 1:
            {

                ltcApp.successMessage(null, data.StatusMessage);
                Preferences.clearChangePasswordForm();
                break;
            }
        case 0:
            {
                ltcApp.errorMessage(data.StatusMessage);
                break;
            }
        default:
            {

                ltcApp.errorMessage(data.StatusMessage);
                break;
            }
    }
}

function userdetailSuccess(data) {
    var statusCode = data.StatusCode;
    switch (statusCode) {
        case 1:
            {

                ltcApp.successMessage(null, data.StatusMessage);
                Preferences.clearChangePasswordForm();
                break;
            }
        case 0:
            {
                ltcApp.errorMessage(data.StatusMessage);
                break;
            }
        default:
            {

                ltcApp.errorMessage(data.StatusMessage);
                break;
            }

    }

}


function officedetailSuccess(data) {
    var statusCode = data.StatusCode;
    switch (statusCode) {
        case 1:
            {

                ltcApp.successMessage(null, data.StatusMessage);
                Preferences.clearChangePasswordForm();
                break;
            }
        case 0:
            {
                ltcApp.errorMessage(data.StatusMessage);
                break;
            }
        default:
            {

                ltcApp.errorMessage(data.StatusMessage);
                break;
            }

    }

}


