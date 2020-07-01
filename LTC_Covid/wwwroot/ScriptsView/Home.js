

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

            var currentHtml = $(".content").html()
            var queueId = $("#QueueID").val();

            var string = JSON.stringify(currentHtml);
            var data = {
                pdf: currentHtml,
                QueueID: queueId,
                FromTable: 1
            };


            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Home/SavePdf',
                success: function (data) {
                    ltcApp.successMessage("Success", 'Form has been saved');
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error saving the form');

                },
                complete: function () {

                }
            })



        },
        sendForm: function (subscriberID, queueId) {







            var data = {
                Id: subscriberID,
                QueueId: queueId,
            };


            Layout.showLoader();
            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Home/SendEmail',
                success: function (data) {

                    ltcApp.successMessage("Success", 'Form has been sent');
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error sending the form');
                    Layout.hideLoader();

                },
                complete: function () {
                    Layout.hideLoader();

                }
            })



        },
        saveForm: function () {

            var form = new Object();
            var screenName = $("input[name=UName]:checked").val();
            if (screenName == "userName") {
                form.StaffScreener = $("#LoggedInUser").val();
                form.StaffScreenerType = "UserName";
            } else {
                //if ($("#staffScreener").val() == "") {
                //    ltcApp.warningMessage(null, "Please enter staff screener");
                //    return;
                //} else {
                form.StaffScreener = $("#staffScreener").val();
                form.StaffScreenerType = "Other";

                //}

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
            form.PatientAge = $("#patientAge").val();

            //if ($("#patientAge").val() == "") {
            //    ltcApp.warningMessage(null, "Please enter patient age");
            //    return;
            //} else {
            //    form.PatientAge = $("#patientAge").val();
            //}

            var screenNameWhoAns = $("input[name=UNameWhoAns]:checked").val();
            if (screenNameWhoAns == "userNameWhoAns") {

                form.WhoAnswer = $("#SubscriberFullName").val();;
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
            debugger;
            var contactMethod = $("input[name=contactMethod]:checked").val();
            if (contactMethod == "phone") {
                if ($("#txtPhone").val() == "") {
                    ltcApp.warningMessage(null, "Please enter phone number");
                    return;
                } else {
                    form.contactDetail = $("#txtPhone").val();
                    form.contactMethodType = "phone";
                }
            } else if (contactMethod == "email") {
                if ($("#txtEmail").val() == "") {
                    ltcApp.warningMessage(null, "Please enter email");
                    return;
                } else {
                   
                    if (ltcApp.validateEmail($("#txtEmail").val())) {
                        form.contactDetail = $("#txtEmail").val();
                        form.contactMethodType = "email";
                    } else {
                        ltcApp.warningMessage(null, "Invalid email address.");
                        return;
                    }

                   
                }
            } else if (contactMethod == "other") {
                if ($("#txtOther").val() == "") {
                    ltcApp.warningMessage(null, "Please enter other contact method");
                    return;
                } else {
                    form.Phone = $("#txtOther").val();
                    form.contactMethodType = "other";
                    form.contactDetail = $("#txtOther").val();
                }
            } if (contactMethod == null) {
                ltcApp.warningMessage(null, "Please select contact method");
                return;
            }



            if (form.IsPreScreen == false && form.InPerson == false) {
                ltcApp.warningMessage(null, "Please select the option");
            }
            form.IsPreScreen = $('input[name="prescreen"]').is(":checked");
            form.InPerson = $('input[name="inperson"]').is(":checked");
            form.FormID = $("#FormID").val();
            debugger;
            if (form.FormID == 1 || form.FormID == 2) {
                if ((form.IsPreScreen == false && form.InPerson == false)
                    || (form.IsPreScreen == false && form.InPerson == null)
                    || (form.IsPreScreen == null && form.InPerson == false)
                    || (form.IsPreScreen == null && form.InPerson == null)
                ) {
                    ltcApp.warningMessage(null, "Please fill the form.");
                    return;
                }
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
                        ltcApp.warningMessage(null, "Please select the options for In-Office Screen");
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
                if (form.FormID == 3) {
                    if (form.IsPreScreen == false || form.IsPreScreen == null) {
                        ltcApp.warningMessage(null, "Please fill the form.");
                        return;
                    }
                } else if (form.FormID == 4) {
                    if (form.InPerson == false || form.InPerson == null) {
                        ltcApp.warningMessage(null, "Please fill the form.");
                        return;
                    }
                }

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
                        ltcApp.warningMessage(null, "Please select the options for In-Office Screen");
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
            var IscovidPossible = false;

            if ($("input[name=InPersonScreenAnswer1]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer2]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer3]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer4]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer5]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer6]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer7]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer8]:checked").val() == "Yes" ||
                $("input[name=InPersonScreenAnswer9]:checked").val() == "Yes") {
                IscovidPossible = true;
            }

            if ($("input[name=PreScreenAnswer1]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer2]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer3]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer4]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer5]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer6]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer7]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer8]:checked").val() == "Yes" ||
                $("input[name=PreScreenAnswer9]:checked").val() == "Yes") {
                IscovidPossible = true;
            }

            if (form.InPerson) {
                if ($("#txtTemperature").val() == "") {
                    ltcApp.warningMessage(null, "Please provide temperature");
                    return;
                } else {
                    form.Temperature = $("#txtTemperature").val();
                }
            }
            //convert object to json string
            var string = JSON.stringify(form);
            Layout.showLoader();
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
                PreScreenDate: form.IsPreScreenDate,
                IsCOVIDPossible: IscovidPossible,
                Counter: $("#hdnCounter").val(),
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
                    if (form.IsPreScreen == true && form.IsInPersonScreen == false) {
                        $("#inPerson").attr('disabled', false);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error saving the form');
                    $("#btnSave").attr("disabled", false);
                    Layout.hideLoader();

                },
                complete: function () {
                    $("#btnSave").attr("disabled", false);
                    //$("#inPerson").attr("disabled", true);
                    Layout.hideLoader();
                    window.location.href = "../Home/ViewForms";
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
                    if (data.success) {
                        ltcApp.successMessage("Success", 'Subscriber has been added into the system.');
                        HomeView.LoadAllSubscribers();
                    } else {
                        ltcApp.errorMessage("Error", data.Message);

                    }
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
            if (subId == -1) {
                ltcApp.warningMessage(null, "Please select subscriber");
                return;
            }
            if (formId == -1) {
                ltcApp.warningMessage(null, "Please select form");
                return;
            }
            window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId + "&IsNew=true&queueId=0";

        },
        OpenFormList: function (subId, formId, queueId) {

            if (subId == -1) {
                ltcApp.warningMessage(null, "Please select subscriber");
                return;
            }
            if (formId == -1) {
                ltcApp.warningMessage(null, "Please select form");
                return;
            }
            window.location.href = "../Home/CovidForm?subscriberId=" + subId + "&formId=" + formId + "&queueId=" + queueId;


        },
        Cancel: function () {
            window.location.href = "../Home/ViewForms";

        },
        OpenFormViewList: function (queueId) {

            window.location.href = "../Home/CovidFormView?queueId=" + queueId;

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
    //$("#inPerson").attr('disabled', false);
    var d = new Date();
    $("#PreScreenDate").html((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds());
    $("#hdnPreScreenDate").val((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds());


})
$("input[name=inperson]").change(function () {
    $(".InPerson").attr('disabled', false);
    var d = new Date();
    $("#InPersonDate").html((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds());
    $("#hdnInPersonDate").val((d.getMonth() + 1) + "/" + d.getDate() + "/" + d.getFullYear() + " " + d.getHours() + ":" + d.getMinutes() + ":" + d.getSeconds());

})


HomeView.init();

$(document).ready(function () {


});


