var UserPermissions;

$(".toggleMenu").on('click', function () {
    $("#mainMenu").toggleClass('open');
});

$(document).ready(function () {
    $("#btnMenuOpener").click();

});

var Login = {
    isLoginPage: false,
    showLogin: function (g) {
        $("#loginDiv").show();
        $("#mainMenu").hide();
        $("#loginTitleButton").hide();
        $("#returnurl").val(g);
    },

    hideLogin: function () {
        $("#loginDiv").hide();
        $("#mainMenu").show();
        $("#loginTitleButton").show();
        $("#returnurl").val("");
    },

    openTab: function (url,closeTab) {
        var win = window.open(url, '_blank');
        win.focus();
        if (closeTab == true) {
            //logout user
            document.getElementById('logoutForm').submit();
        }
    },

    showCards: function () {
        //show card loader
        $.ajax({
            type: "POST",
            async: true,
            //data: {
            //    Office_Sequence: UserPermissions.Office_Sequence
            //},
            url: window.location.origin + "/Home/LoadCardInformations",
            success: function (data) {
                
                if (data.Success && data.Data != null) {
                    
                    var records = data.Data;
                    //$("#todayAppointmentScore").html(records.TotalAppointments);
                    //$("#todayRecallAppointmentScore").html(records.TotalRecalls);
                    //$("#todayBillingScore").html(records.TotalCharge);
                    //$("#totalBillingCountScore").html(records.TotalChargeCount);
                    //$("#todayPaymentScore").html(records.TotalPayment);
                    //$("#todayPaymentTotalCountScore").html(records.TotalPaymentCount);

                    $("#scoreCards").show();
                }
                else {
                    $("#scoreCards").hide();
                }
                //show card 
                //hide card loader
            },
            error: function (xhr, data, errorThrown) {
                //show error
                //hide card loader
            }
        });



    },
}