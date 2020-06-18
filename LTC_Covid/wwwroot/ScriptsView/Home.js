

var HomeView = function () {

    return {

        init: function () {

        },
        selectSubscriber: function () {
            $('#entryModel').modal('show');
        },
        newSubscription: function () {
            $('#subscriberModel').modal('show');
        },
        saveFormOld: function (formId) {
            var form = new Object();
            var screenName = $("input[name=UName]:checked").val();
            if (screenName == "userName") {
                form.StaffScreener = 'loggedIn User';
                form.StaffScreenerType = "UserName";
            } else {
                if ($("#staffScreener").val() == "") {
                    ltcApp.warningMessage(null, "Please enter staff screener");
                    return;
                } else {
                    form.StaffScreener = $("#staffScreener").val();
                    form.StaffScreenerType = "Other";

                }

            }

        
            if ($("#patientName").val() == "") {
                ltcApp.warningMessage(null, "Please enter patient name");
                return;
            } else {
                form.PatientName = $("#patientName").val();
            }

            if ($("#patientAge").val() == "") {
                ltcApp.warningMessage(null, "Please enter patient age");
                return;
            } else {
                form.PatientAge = $("#patientAge").val();
            }

            var screenNameWhoAns = $("input[name=UNameWhoAns]:checked").val();
            if (screenNameWhoAns == "userNameWhoAns") {
                form.WhoAnswer = 'UserNameWhoAnswer';
                form.WhoAnsType = "UserNameWhoAnswer";
            } else {
                if ($("#whoAns").val() == "") {
                    ltcApp.warningMessage(null, "Please let us know,who filled the form");
                    return;
                } else {
                    form.WhoAnswer = $("#staffScreener").val();
                    form.WhoAnsType = "otherWhoAns";

                }

            }



            if ($("#txtTemperature").val() == "") {
                ltcApp.warningMessage(null, "Please provide temperature");
                return;
            } else {
                form.Temperature = $("#txtTemperature").val();
            }

            form.IsPreScreen = $('input[name="prescreen"]').is(":checked");
            form.InPerson = $('input[name="inperson"]').is(":checked");

            if (form.IsPreScreen) {
                if ($("input[name=PreScreenAnswer1]:checked").val() == null ||
                    $("input[name=PreScreenAnswer2]:checked").val() == null ||
                    $("input[name=PreScreenAnswer3]:checked").val() == null ||
                    $("input[name=PreScreenAnswer4]:checked").val() == null ||
                    $("input[name=PreScreenAnswer5]:checked").val() == null ||
                    $("input[name=PreScreenAnswer6]:checked").val() == null ||
                    $("input[name=PreScreenAnswer7]:checked").val() == null ||
                    $("input[name=PreScreenAnswer8]:checked").val() == null ||
                    $("input[name=PreScreenAnswer9]:checked").val() == null) {
                    ltcApp.warningMessage(null, "Please select the options for Pre-Screen");
                    return;
                }
                form.PreScreen = new Object();
                form.IsPreScreenDate = $('#hdnPreScreenDate').val();
                form.PreScreen.Answer1 = $("input[name=PreScreenAnswer1]:checked").val();
                form.PreScreen.Answer2 = $("input[name=PreScreenAnswer2]:checked").val();
                form.PreScreen.Answer3 = $("input[name=PreScreenAnswer3]:checked").val();
                form.PreScreen.Answer4 = $("input[name=PreScreenAnswer4]:checked").val();
                form.PreScreen.Answer5 = $("input[name=PreScreenAnswer5]:checked").val();
                form.PreScreen.Answer6 = $("input[name=PreScreenAnswer6]:checked").val();
                form.PreScreen.Answer7 = $("input[name=PreScreenAnswer7]:checked").val();
                form.PreScreen.Answer8 = $("input[name=PreScreenAnswer8]:checked").val();
                form.PreScreen.Answer9 = $("input[name=PreScreenAnswer9]:checked").val();

            } else {
                form.IsPreScreenDate = null;
            }

            if (form.InPerson) {
                if ($("input[name=InPersonScreenAnswer1]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer2]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer3]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer4]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer5]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer6]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer7]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer8]:checked").val() == null ||
                    $("input[name=InPersonScreenAnswer9]:checked").val() == null) {
                    ltcApp.warningMessage(null, "Please select the options for InPerson-Screen");
                    return;
                }
                form.InPersonScreen = new Object();
                form.InPersonDate = $("#hdnInPersonDate").val();
                form.InPersonScreen.Answer1 = $("input[name=InPersonScreenAnswer1]:checked").val();
                form.InPersonScreen.Answer2 = $("input[name=InPersonScreenAnswer2]:checked").val();
                form.InPersonScreen.Answer3 = $("input[name=InPersonScreenAnswer3]:checked").val();
                form.InPersonScreen.Answer4 = $("input[name=InPersonScreenAnswer4]:checked").val();
                form.InPersonScreen.Answer5 = $("input[name=InPersonScreenAnswer5]:checked").val();
                form.InPersonScreen.Answer6 = $("input[name=InPersonScreenAnswer6]:checked").val();
                form.InPersonScreen.Answer7 = $("input[name=InPersonScreenAnswer7]:checked").val();
                form.InPersonScreen.Answer8 = $("input[name=InPersonScreenAnswer8]:checked").val();
                form.InPersonScreen.Answer9 = $("input[name=InPersonScreenAnswer9]:checked").val();

            } else {
                form.InPersonDate = null;
            }

            form.AdditionalInformation = $("#txtAdditionalInformation").val();

            //convert object to json string
            var string = JSON.stringify(form);
            console.log(string);
            //convert string to Json Object

            //   if (template != null && article != null) {
            var subscriberID = $("#IdValue").val();
            var queueId = $("#QueueID").val();

            var data = {
                SubscriberID: subscriberID,
                QueueID: queueId,
                StorageInJson: string,
                IsPreScreen: form.IsPreScreen,
                IsInPersonScreen: form.InPerson,
                FormID: formId,
                InPersonScreenDate: form.InPersonDate,
                PreScreenDate: form.IsPreScreenDate
            };
            $("#btnSave").attr("disabled", true);


            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Home/Upsert',
                success: function (data) {
                    $("#QueueID").val(data.QueueId)
                    ltcApp.successMessage("Success", 'Form has been saved');
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error saving the form');
                    $("#btnSave").attr("disabled", false);

                },
                complete: function () {
                    $("#btnSave").attr("disabled", false);

                }
            })



        },
        saveFormPdf: function () {
            var currentHtml = $("#staffScreener").val()
            //var string = JSON.stringify(form);

            var data = {
                SubscriberID: subscriberID,
                QueueID: queueId,
                StorageInJson: string,
                IsPreScreen: form.IsPreScreen,
                IsInPersonScreen: form.InPerson,
                FormID: form.FormID,
                InPersonScreenDate: form.InPersonDate,
                PreScreenDate: form.IsPreScreenDate
            };
            

            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Home/Upsert',
                success: function (data) {
                    $("#QueueID").val(data.QueueId)
                    ltcApp.successMessage("Success", 'Form has been saved');
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error saving the form');
                    $("#btnSave").attr("disabled", false);

                },
                complete: function () {
                    $("#btnSave").attr("disabled", false);

                }
            })



        },

        saveForm: function () {

            var form = new Object();
            var screenName = $("input[name=UName]:checked").val();
            if (screenName == "userName") {
                form.StaffScreener = 'loggedIn User';
                form.StaffScreenerType = "UserName";
            } else {
                if ($("#staffScreener").val() == "") {
                    ltcApp.warningMessage(null, "Please enter staff screener");
                    return;
                } else {
                    form.StaffScreener = $("#staffScreener").val();
                    form.StaffScreenerType = "Other";

                }

            }


            if ($("#patientName").val() == "") {
                ltcApp.warningMessage(null, "Please enter patient name");
                return;
            } else {
                form.PatientName = $("#patientName").val();
            }
            var age = $("#patientAge").val();
            if (age < 0) {
                ltcApp.warningMessage(null, "Please enter valid patient age");
                return;
            }
            if ($("#patientAge").val() == "") {
                ltcApp.warningMessage(null, "Please enter patient age");
                return;
            } else {
                form.PatientAge = $("#patientAge").val();
            }

            var screenNameWhoAns = $("input[name=UNameWhoAns]:checked").val();
            if (screenNameWhoAns == "userNameWhoAns") {
                form.WhoAnswer = 'UserNameWhoAnswer';
                form.WhoAnsType = "UserNameWhoAnswer";
            } else {
                if ($("#whoAns").val() == "") {
                    ltcApp.warningMessage(null, "Please let us know,who filled the form");
                    return;
                } else {
                    form.WhoAnswer = $("#whoAns").val();
                    form.WhoAnsType = "otherWhoAns";

                }

            }



            if ($("#txtTemperature").val() == "") {
                ltcApp.warningMessage(null, "Please provide temperature");
                return;
            } else {
                form.Temperature = $("#txtTemperature").val();
            }
            if (form.IsPreScreen == false && form.InPerson == false) {
                ltcApp.warningMessage(null, "Please select the option");
            }
            form.IsPreScreen = $('input[name="prescreen"]').is(":checked");
            form.InPerson = $('input[name="inperson"]').is(":checked");
            form.FormID = $("#FormID").val(); 
            if (form.FormID == 1 || form.FormID== 2) {
                if (form.IsPreScreen) {
                    if ($("input[name=PreScreenAnswer1]:checked").val() == null ||
                        $("input[name=PreScreenAnswer2]:checked").val() == null ||
                        $("input[name=PreScreenAnswer3]:checked").val() == null ||
                        $("input[name=PreScreenAnswer4]:checked").val() == null ||
                        $("input[name=PreScreenAnswer5]:checked").val() == null ||
                        $("input[name=PreScreenAnswer6]:checked").val() == null ||
                        $("input[name=PreScreenAnswer7]:checked").val() == null ||
                        $("input[name=PreScreenAnswer8]:checked").val() == null ||
                        $("input[name=PreScreenAnswer9]:checked").val() == null) {
                        ltcApp.warningMessage(null, "Please select the options for Pre-Screen");
                        return;
                    }
                    form.PreScreen = new Object();
                    form.IsPreScreenDate = $('#hdnPreScreenDate').val();
                    form.PreScreen.Answer1 = $("input[name=PreScreenAnswer1]:checked").val();
                    form.PreScreen.Answer2 = $("input[name=PreScreenAnswer2]:checked").val();
                    form.PreScreen.Answer3 = $("input[name=PreScreenAnswer3]:checked").val();
                    form.PreScreen.Answer4 = $("input[name=PreScreenAnswer4]:checked").val();
                    form.PreScreen.Answer5 = $("input[name=PreScreenAnswer5]:checked").val();
                    form.PreScreen.Answer6 = $("input[name=PreScreenAnswer6]:checked").val();
                    form.PreScreen.Answer7 = $("input[name=PreScreenAnswer7]:checked").val();
                    form.PreScreen.Answer8 = $("input[name=PreScreenAnswer8]:checked").val();
                    form.PreScreen.Answer9 = $("input[name=PreScreenAnswer9]:checked").val();

                } else {
                    form.IsPreScreenDate = null;
                }

                if (form.InPerson) {
                    if ($("input[name=InPersonScreenAnswer1]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer2]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer3]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer4]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer5]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer6]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer7]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer8]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer9]:checked").val() == null) {
                        ltcApp.warningMessage(null, "Please select the options for InPerson-Screen");
                        return;
                    }
                    form.InPersonScreen = new Object();
                    form.InPersonDate = $("#hdnInPersonDate").val();
                    form.InPersonScreen.Answer1 = $("input[name=InPersonScreenAnswer1]:checked").val();
                    form.InPersonScreen.Answer2 = $("input[name=InPersonScreenAnswer2]:checked").val();
                    form.InPersonScreen.Answer3 = $("input[name=InPersonScreenAnswer3]:checked").val();
                    form.InPersonScreen.Answer4 = $("input[name=InPersonScreenAnswer4]:checked").val();
                    form.InPersonScreen.Answer5 = $("input[name=InPersonScreenAnswer5]:checked").val();
                    form.InPersonScreen.Answer6 = $("input[name=InPersonScreenAnswer6]:checked").val();
                    form.InPersonScreen.Answer7 = $("input[name=InPersonScreenAnswer7]:checked").val();
                    form.InPersonScreen.Answer8 = $("input[name=InPersonScreenAnswer8]:checked").val();
                    form.InPersonScreen.Answer9 = $("input[name=InPersonScreenAnswer9]:checked").val();

                } else {
                    form.InPersonDate = null;
                }
            } else {
                if (form.IsPreScreen) {
                    if ($("input[name=PreScreenAnswer1]:checked").val() == null ||
                        $("input[name=PreScreenAnswer2]:checked").val() == null ||
                        $("input[name=PreScreenAnswer3]:checked").val() == null ||
                        $("input[name=PreScreenAnswer4]:checked").val() == null ||
                        $("input[name=PreScreenAnswer5]:checked").val() == null ||
                        $("input[name=PreScreenAnswer6]:checked").val() == null ||
                        $("input[name=PreScreenAnswer7]:checked").val() == null ||
                        $("input[name=PreScreenAnswer8]:checked").val() == null ||
                        $("input[name=PreScreenAnswer9]:checked").val() == null) {
                        ltcApp.warningMessage(null, "Please select the options for Pre-Screen");
                        return;
                    }

                    form.PreScreen = new Object();
                    form.IsPreScreenDate = $('#hdnPreScreenDate').val();
                    form.PreScreen.Answer1 = $("input[name=PreScreenAnswer1]:checked").val();
                    form.PreScreen.Answer2 = $("input[name=PreScreenAnswer2]:checked").val();
                    form.PreScreen.Answer3 = $("input[name=PreScreenAnswer3]:checked").val();
                    form.PreScreen.Answer4 = $("input[name=PreScreenAnswer4]:checked").val();
                    form.PreScreen.Answer5 = $("input[name=PreScreenAnswer5]:checked").val();
                    form.PreScreen.Answer6 = $("input[name=PreScreenAnswer6]:checked").val();
                    form.PreScreen.Answer7 = $("input[name=PreScreenAnswer7]:checked").val();
                    form.PreScreen.Answer8 = $("input[name=PreScreenAnswer8]:checked").val();
                    form.PreScreen.Answer9 = $("input[name=PreScreenAnswer9]:checked").val();
                  


                } else if (form.InPerson) {
                    if ($("input[name=InPersonScreenAnswer1]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer2]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer3]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer4]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer5]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer6]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer7]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer8]:checked").val() == null ||
                        $("input[name=InPersonScreenAnswer9]:checked").val() == null) {
                        ltcApp.warningMessage(null, "Please select the options for InPerson-Screen");
                        return;
                    }
                    form.InPersonScreen = new Object();
                    form.InPersonDate = $("#hdnInPersonDate").val();
                    form.InPersonScreen.Answer1 = $("input[name=InPersonScreenAnswer1]:checked").val();
                    form.InPersonScreen.Answer2 = $("input[name=InPersonScreenAnswer2]:checked").val();
                    form.InPersonScreen.Answer3 = $("input[name=InPersonScreenAnswer3]:checked").val();
                    form.InPersonScreen.Answer4 = $("input[name=InPersonScreenAnswer4]:checked").val();
                    form.InPersonScreen.Answer5 = $("input[name=InPersonScreenAnswer5]:checked").val();
                    form.InPersonScreen.Answer6 = $("input[name=InPersonScreenAnswer6]:checked").val();
                    form.InPersonScreen.Answer7 = $("input[name=InPersonScreenAnswer7]:checked").val();
                    form.InPersonScreen.Answer8 = $("input[name=InPersonScreenAnswer8]:checked").val();
                    form.InPersonScreen.Answer9 = $("input[name=InPersonScreenAnswer9]:checked").val();
                   
                }
            }
            form.AdditionalInformation = $("#txtAdditionalInformation").val();

            //convert object to json string
            var string = JSON.stringify(form);
            console.log(string);
            //convert string to Json Object

            //   if (template != null && article != null) {
            var subscriberID = $("#IdValue").val();
            var queueId = $("#QueueID").val();

            var data = {
                SubscriberID: subscriberID,
                QueueID: queueId,
                StorageInJson: string,
                IsPreScreen: form.IsPreScreen,
                IsInPersonScreen: form.InPerson,
                FormID: form.FormID,
                InPersonScreenDate: form.InPersonDate,
                PreScreenDate: form.IsPreScreenDate
            };
            $("#btnSave").attr("disabled", true);


            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Home/Upsert',
                success: function (data) {
                    $("#QueueID").val(data.QueueId)
                    ltcApp.successMessage("Success", 'Form has been saved');
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error saving the form');
                    $("#btnSave").attr("disabled", false);

                },
                complete: function () {
                    $("#btnSave").attr("disabled", false);
                    //$("#inPerson").attr("disabled", true);

                }
            })



        },
        saveSubscription: function () {


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
            //if ($("#ddlSalutation").val() == "-1") {
            //    ltcApp.warningMessage(null, "Please provide salutation");
            //    return;
            //} else {
            //    Salutation = $("#ddlSalutation").val();
            //}
            if ($("#email").val() == "") {
                ltcApp.warningMessage(null, "Please provide email");
                return;
            } else {
                email = $("#email").val();
            }


            //   if (template != null && article != null) {
            var IdValue = $("#IdValue").val();
            $("#btnSaveSub").attr("disabled", true);

            var data = {
                ID: IdValue,
                FirstName: firstName,
                LastName: lname,
                MiddleInitial: mname,
                EmailAddress: email,
                Salutation: Salutation

            };


            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Subscribers/Upsert',
                success: function (data) {
                    ltcApp.successMessage("Success", 'Subscriber has been added into the system.');
                    HomeView.LoadAllSubscribers();
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading preview');
                    $("#btnSaveSub").attr("disabled", false);

                },
                complete: function () {
                    $("#btnSaveSub").attr("disabled", false);

                    $('#subscriberModel').modal('hide');
                    $("#fname").val('');
                    $("#lname").val('');
                    $("#mname").val('');
                    $("#ddlSalutation").val('');
                    $("#email").val('');
                    $("#IdValue").val('0');
                }
            })



        },
        LoadAllSubscribers: function () {
            $.ajax({
                type: "GET",
                url: '/Subscribers/GetAll',
                success: function (data) {
                    if (data != null) {
                        var subscribers = data;
                        $("#subscriberId").html('<option value="-1"> --Select Subscriber--</option>');
                        $.each(subscribers, function (index, item) {
                            $("#subscriberId").append('<option value="' + item.ID + '">' + item.LastName + " " + item.FirstName + '</option>');

                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading subscribers');
                },
                complete: function () {
                }
            });
        },
        LoadAllTypes: function () {
            $.ajax({
                type: "GET",
                url: '/Home/GetAllTypes',
                success: function (data) {
                    if (data != null) {
                        var types = data;
                        $("#formId").html('<option value="-1"> --Select Type--</option>');
                        $.each(types, function (index, item) {
                            $("#formId").append('<option value="' + item.ID + '">' + item.Covid_Form_Description + '</option>');

                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading types');
                },
                complete: function () {
                }
            });
        },
        OpenForm: function () {
            var subId = $("#subscriberId").val();
            var formId = $("#formId").val();
            if (subId  == -1) {
                ltcApp.warningMessage(null, "Please select subscriber");
                return;
            }
            if (formId == -1) {
                ltcApp.warningMessage(null, "Please select form");
                return;
            }
            window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId;
            //if (formId == 3) {
            //    window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId;
            //} else if (formId == 4) {
            //    window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId;
            //}
            //else if (formId == 4) {
            //    window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId;
            //} else {
            //    window.location.href = "../Home/CovidFormOntario?subscriberId=" + subId;
            //}

        },
        OpenFormList: function (subId, formId) {
            if (subId == -1) {
                ltcApp.warningMessage(null, "Please select subscriber");
                return;
            }
            if (formId == -1) {
                ltcApp.warningMessage(null, "Please select form");
                return;
            }
            window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId;

            //if (formId == 3) {
            //    window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId;
            //} else if (formId == 4) {
            //    window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId;
            //}
            //if (formId == 1) {
            //    window.location.href = "../Home/CovidForm?subscriberId=" + subId;
            //} else {
            //    window.location.href = "../Home/CovidFormOntario?subscriberId=" + subId;
            //}

        },
        OpenFormViewList: function (subId, formId) {
            if (subId == -1) {
                ltcApp.warningMessage(null, "Please select subscriber");
                return;
            }
            if (formId == -1) {
                ltcApp.warningMessage(null, "Please select form");
                return;
            }
            window.location.href = "../Home/CovidFormView?subscriberId=" + subId + "&formId=" + formId;

            //if (formId == 3) {
            //    window.location.href = "../Home/CovidFormView?subscriberId=" + subId + "&formId=" + formId;
            //} else if (formId == 4) {
            //    window.location.href = "../Home/CovidFormOntarioView?subscriberId=" + subId + "&formId=" + formId;
            //}
            //if (formId == 1) {
            //    window.location.href = "../Home/CovidFormView?subscriberId=" + subId;
            //} else {
            //    window.location.href = "../Home/CovidFormOntarioView?subscriberId=" + subId;
            //}

        },
        DeleteForm: function (queueId) {
            var data = {
                Id: queueId

            };
            var result = confirm("Are you sure, you want to delete?");
            if (result) {

                Layout.showLoader();
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    dataType: 'json',
                    url: '/Home/DeleteForm',
                    success: function (data) {
                        ViewForm.refresh();

                        ltcApp.successMessage("Deleted!", "Form has been deleted.", "success");
                    },
                    error: function (xhr, textStatus, errorThrown) {
                        ltcApp.errorMessage("Error", 'Error saving the form');
                    },
                    complete: function () {
                    }
                });


            }




        },

    };

}();

$("input[name=prescreen]").change(function () {
    $(".PreScreen").attr('disabled', false);
    $("#inPerson").attr('disabled', false);
    var d = new Date();
    $("#PreScreenDate").html((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear());
    $("#hdnPreScreenDate").val((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear());


})
$("input[name=inperson]").change(function () {
    $(".InPerson").attr('disabled', false);
    var d = new Date();
    $("#InPersonDate").html((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear());
    $("#hdnInPersonDate").val((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear());

})


HomeView.init();

$(document).ready(function () {


});


