var UserPermissions = null;
var isUserLoggedIn = null;

var Layout = {
    currentModel: '',
    ProfileSave: function () {
        $("#changeuserprofileform").submit(function (e) {
            //$("#btnUpdateProfile").html('<div class="app-spinner loading"></div>');
            e.preventDefault();
            $form = $(this);
            var SUBMITURL = window.location.origin + "/Account/Update";
            console.log(SUBMITURL);
            Layout.showLoader();
            $.ajax({
                url: SUBMITURL,
                method: "POST",
                data: $('#changeuserprofileform').serialize(),
                datatype: "html",
                //data: {
                //    __RequestVerificationToken: token,
                //    profile: pageObj
                //},
                success: function (data) {
                    Layout.hideLoader();
                    $('#userprofileModal').modal('hide');
                    //alert(data);
                },
                error: function () {
                    Layout.hideLoader();
                    //alert(data);
                }
            });
        });
    },

    PasswordSave: function () {
        $("#changepasswordform").submit(function (e) {
            //$("#btnUpdatePassword").html('<div class="app-spinner loading"></div>');
            e.preventDefault();
            $form = $(this);
            var SUBMITURL = window.location.origin + "/Account/ChangePassword";
            $.ajax({
                url: SUBMITURL,
                method: "POST",
                data: $('#changepasswordform').serialize(),
                datatype: "html",
                //data: {
                //    __RequestVerificationToken: token,
                //    profile: pageObj
                //},
                success: function (data) {

                    $('#changePasswordModal').modal('hide');
                    $('#txtCurrentPassword').val('');
                    $('#txtNewPassword').val('');
                    $('#txtConfirmPassword').val('');
                    //alert(data);
                },
                error: function () {
                    //alert("Ajax call failed");
                }
            });
        });
    },

    loadCornerMenuControl: function () {
        $(".trigger").click(function () {
            $(".rotationMenu").toggleClass("active");
        });
    },

    openMenu: function (name) {
        $(".trigger").click();
        var url = '';
        if (name == 'dashboard') {
            url = '/dashboard/index';
        } else if (name == 'report') {
            url = '/Report/index';
        } else if (name == 'survey') {
            url = '/Survey/index';
        } else if (name == 'form') {
            url = '/Form/index';
        } else if (name == 'officemanagement') {
            url = '/OfficeManagement/index';
        } else if (name == 'newsletter') {
            url = '/NewsLetter/index';
        }
        window.open(url, '_blank')
    },

    togglePreviewButton: function (obj, parent) {
        var isPreviewHidden = $(obj).hasClass('isPreviewHidden');
        if (isPreviewHidden) {
            $(obj).removeClass('isPreviewHidden');
            $(obj).html('<span class="fa fa-eye-slash"></span>');
            $(obj).addClass('btn-danger');
            $(obj).removeClass('btn-primary');
            $("#" + parent).removeClass('toggleDivHidden');
            $("#" + parent + " > div:nth(0)").removeClass('col-lg-12 col-md-12 col-sm-12 col-xs-12');
            //$("#" + parent + " > div:nth(2)").removeClass('toggleDivHidden');
            $("#" + parent + " > div:nth(0)").addClass('col-lg-6 col-md-6 col-sm-6 col-xs-6');
            $("#" + parent + " > div:nth(2)").addClass('col-lg-6 col-md-6 col-sm-6 col-xs-6');
        } else {
            $(obj).addClass('isPreviewHidden');
            $(obj).html('<span class="fa fa-eye"></span>');
            $(obj).removeClass('btn-danger');
            $(obj).addClass('btn-primary');
            $("#" + parent).addClass('toggleDivHidden');
            //$("#" + parent + " > div:nth(2)").addClass('toggleDivHidden');
            $("#" + parent + " > div:nth(0)").addClass('col-lg-12 col-md-12 col-sm-12 col-xs-12');
            $("#" + parent + " > div:nth(0)").removeClass('col-lg-6 col-md-6 col-sm-6 col-xs-6');
            $("#" + parent + " > div:nth(2)").removeClass('col-lg-6 col-md-6 col-sm-6 col-xs-6');

        }
    },

    renderFormDesignerInIframe(content, id) {
        var iframe = document.getElementById(id),
            iframeWin = iframe.contentWindow || iframe,
            iframeDoc = iframe.contentDocument || iframeWin.document;

        iframeDoc.open();
        iframeDoc.write('<html><head>');
        iframeDoc.write('</head><body>');
        iframeDoc.write('<link rel="stylesheet" href="/Resources/theme/css/styles.css"><script type="text/javascript" src="/Resources/theme/js/vendor/jquery/jquery.min.js"></script><script type="text/javascript" src="/Resources/theme/js/vendor/jquery/jquery-ui.min.js"></script><script type="text/javascript" src="/Resources/theme/js/vendor/bootstrap/bootstrap.min.js"></script><script src="/Resources/FormDesigner/assets/js/form-render.min.js"></script><link href="/Resources/FormDesigner/assets/css/RateYo/RateYo.css" rel="stylesheet" /><script src="/Resources/FormDesigner/assets/js/Rateyo/jquery.rateyo.min.js"></script>');
        iframeDoc.write('<script src="/Scripts/SharedJS/RenderContent.js"></script>');
        iframeDoc.write('<div id="render-container"  style="padding: 30px;"></div>');
        iframeDoc.write('<script type="text/javascript">$(document).ready(function () {var content = ' + content + ';  RenderContent.RenderContentInIframe(content);});</script>');

        iframeDoc.write('</body></html>');
        iframeDoc.close();
    },

    renderContentInIframe(content, id) {
        var iframe = document.getElementById(id);
        iframe = iframe.contentWindow || (iframe.contentDocument.document || iframe.contentDocument);

        iframe.document.open();
        iframe.document.write(content);
        iframe.document.close();
    },

    updateUserProfile: function () {
        if ($('#txtSalutation').val() == '' ||
            $('#txtInitials').val() == '' ||
            $('#txtFirstName').val() == '' ||
            $('#txtLastName').val() == '' ||
            $('#txtPhone').val() == '' ||
            $('#txtFax').val() == '' ||
            $('#txtAddressLine1').val() == '' ||
            $('#txtCity').val() == '' ||
            $('#txtProvince').val() == '' ||
            $('#txtCountry').val() == '' ||
            $('#txtPostalCode').val() == '') {
            alert('Invalid Data Entered');
        }
        else {
            //$("#btnUpdateProfile").html('<div class="app-spinner loading"></div>');
            var SUBMITURL = window.location.origin + "/Account/Update";
            $.ajax({
                url: SUBMITURL,
                method: "POST",
                data: {
                    Salutation: $('#txtSalutation').val(),
                    Initials: $('#txtInitials').val(),
                    FirstName: $('#txtFirstName').val(),
                    LastName: $('#txtLastName').val(),
                    Phone: $('#txtPhone').val(),
                    Fax: $('#txtFax').val(),
                    AddressLine1: $('#txtAddressLine1').val(),
                    AddressLine2: $('#txtAddressLine2').val(),
                    AddressLine3: $('#txtAddressLine3').val(),
                    City: $('#txtCity').val(),
                    Province: $('#txtProvince').val(),
                    Country: $('#txtCountry').val(),
                    PostalCode: $('#txtPostalCode').val()
                },
                success: function (data) {

                    if (data.Success) {
                        $('#userprofileModal').modal('hide');

                        $('#txtSalutation').val('');
                        $('#txtInitials').val('');
                        $('#txtFirstName').val('');
                        $('#txtLastName').val('');
                        $('#txtPhone').val('');
                        $('#txtFax').val('');
                        $('#txtAddressLine1').val('');
                        $('#txtAddressLine2').val('');
                        $('#txtAddressLine3').val('');
                        $('#txtCity').val('');
                        $('#txtProvince').val('');
                        $('#txtCountry').val('');
                        $('#txtPostalCode').val('');
                    }
                    else {
                        alert(data.Massege);
                    }
                },
                error: function (args) {
                    alert(args);
                }
            });
        }
    },

    updatePassword: function () {
        if ($('#txtCurrentPassword').val() == '' || $('#txtNewPassword').val() == '' || $('#txtNewPassword').val() != $('#txtConfirmPassword').val()) {
            alert('Invalid Data Entered');
        }
        else {
            //$("#btnUpdatePassword").html('<div class="app-spinner loading"></div>');
            var SUBMITURL = window.location.origin + "/Account/ChangePassword";
            $.ajax({
                url: SUBMITURL,
                method: "POST",
                data: {
                    Email: $('#txtCurrentEmail').val(),
                    CurrentPassword: $('#txtCurrentPassword').val(),
                    NewPassword: $('#txtNewPassword').val()
                },
                success: function (data) {

                    if (data.Success) {
                        $('#changePasswordModal').modal('hide');
                        $('#txtCurrentPassword').val('');
                        $('#txtNewPassword').val('');
                        $('#txtConfirmPassword').val('');
                    }
                    else {
                        alert(data.Massege);
                    }
                },
                error: function (args) {
                    alert(args);
                }
            });
        }
    },

    //loadAboutText: function () {
    //    var DATAURL = window.location.origin + "/Home/GetAboutText";
    //    $.ajax({
    //        type: "GET",
    //        url: DATAURL,
    //        contentType: "application/json; charset=utf-8",
    //        dataType: "json",
    //        success: function (response) {
    //            if (response !== null) {
    //                $('#aboutModal .modal-body').html(response.Text);
    //            } else {

    //            }
    //        },
    //        failure: function (response) {

    //        },
    //        error: function (response) {

    //        }
    //    });
    //},

    closeTab: function (id) {
        var tabContentId = $('#tab-' + id + '> a').attr("href");
        $('#tab-' + id).hide(); //remove li of tab
        $(tabContentId).hide(); //remove respective tab content

        if ($('#Tabs a:visible:first').length > 0) {
            setTimeout(function () { $('#Tabs a:visible:first').click(); }, 1000);
        }
        else {
            $("#nodataDiv").show();
        }
    },

    showLoader: function () {
        $("body").block({
            message: '<i class="icon-spinner4 spinner"></i>',
            //timeout: 2000, //unblock after 2 seconds
            overlayCSS: {
                // backgroundColor: '#0E8F92',
                opacity: 0.9,
                cursor: 'wait'
            },
            css: {
                border: 0,
                padding: 0,
                color: '#fff',
                backgroundColor: 'transparent'
            }
        });
    },

    hideLoader: function () {
        $.unblockUI();
    },

    showCardLoader: function (id) {
        $("#" + id).block({
            message: '<i class="icon-spinner9 spinner"></i>',
            overlayCSS: {
                backgroundColor: '#1B2024',
                opacity: 0.85,
                cursor: 'wait'
            },
            css: {
                border: 0,
                padding: 0,
                backgroundColor: 'none',
                color: '#fff'
            }
        })
    },

    hideCardLoader: function (id) {
        $("#" + id).unblock();
        //window.setTimeout(function () {
        //    $("#" + id).unblock();
        //}, 1000);
    },

    storageChange: function (event) {
        if (event.key === 'logged_in') {

            if (event.value == false) {
                $(location).attr('href', '/Account/LogOut');
            }
        }
    },

    checkUserLogin: function () {
        $.ajax({
            type: "POST",
            async: true,
            data: {
                //Office_Sequence: UserPermissions.Office_Sequence
            },
            url: window.location.origin + "/Account/IsUserLoggedIn",
            success: function (data) {
                if (data == true) {
                    window.localStorage.setItem('logged_in', true);
                }
                else {
                    window.localStorage.setItem('logged_in', false);
                }
                //show card 
                //hide card loader
            },
            error: function (xhr, data, errorThrown) {
                window.localStorage.setItem('logged_in', false);
            }
        });
    },

    SelectPicker: function (id, setting) {
        $('#' + id).selectpicker(setting);
    }
}


$(document).ready(function () {
    window.addEventListener('storage', Layout.storageChange, false);

    $.get("/Home/GetUserPermissions", function (data) {
        UserPermissions = data;
        Layout.hideLoader();
        window.localStorage.setItem('logged_in', true);
        try {

            if (Login != null && $("#isLoginPage").val() != "") {
                if (UserPermissions != null && UserPermissions.isDisplaySummary) {
                    Login.showCards();
                }
            }
        } catch (e) {

        }

        try {
            //isUserLoggedIn
        } catch (e) {

        }

    }).fail(function () {
        window.localStorage.setItem('logged_in', false);
        Layout.hideLoader();
    });

    //$.get("/Account/GetUserPermissions", function (data) {
    //    UserPermissions = data;
    //    Layout.hideLoader();
    //});


    $('#thePreviewPanel').on('hidden.bs.modal', function (e) {
        if (Layout.currentModel != '') {
            $("#" + Layout.currentModel).modal('show');
        }
    });

    $('#thePreviewPanel').on('shown.bs.modal', function (e) {
        if ($("#publicSurveyModal").data('bs.modal') && $("#publicSurveyModal").data('bs.modal').isShown) {
            Layout.currentModel = 'publicSurveyModal';
        }
        else
            if ($("#publicFormModal").data('bs.modal') && $("#publicFormModal").data('bs.modal').isShown) {
                Layout.currentModel = 'publicFormModal';
            }
            else
                if ($("#PreDefinedNewsLetter").data('bs.modal') && $("#PreDefinedNewsLetter").data('bs.modal').isShown) {
                    Layout.currentModel = 'PreDefinedNewsLetter';
                }
        //    else
        //        if ($("#publicSurveyModal").data('bs.modal') && $("#publicSurveyModal").data('bs.modal').isShown) {
        //            Layout.currentModel = '';
        //        }
        //        else
        //            if ($("#publicSurveyModal").data('bs.modal') && $("#publicSurveyModal").data('bs.modal').isShown) {
        //                Layout.currentModel = '';
        //            }
        if (Layout.currentModel != '') {
            $("#" + Layout.currentModel).modal('hide');
        }
    });

    //Layout.loadAboutText();

    Layout.loadCornerMenuControl();

});