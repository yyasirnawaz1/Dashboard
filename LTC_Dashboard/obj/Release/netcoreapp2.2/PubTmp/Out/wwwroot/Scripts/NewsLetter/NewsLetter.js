
var Newsletter = function () {
    return {
        init: function () {
            //ltcApp.showLoader();


            this.loadSystemTemplates();
            this.loadShellTemplates();
            //this.loadUserDefinedTemplates();
            this.loadTemplateTypes();
            this.loadIndustries();
            this.loadSubIndustries();
            //$('html, body').animate({
            //    scrollTop: $('.mt-content-body').offset().top - 50 //#DIV_ID is an example. Use the id of your destination on the page
            //}, 'slow');

            //ltcApp.hideLoader();
        },

        initActionPage: function () {


            $("#sendNewsletterDTP").kendoDateTimePicker({
                value: new Date(),
                dateInput: true
            });

            this.loadTemplateActionView();

            //$('#templateEditorWindow').on('shown', function () {
            //    Newsletter.IframeSizing();
            //})
        },


        useTempalte: function () {

            if (SelectedSystemDefinedTemplateId == null || SelectedSystemDefinedTemplateId <= 0) {
                //alert("No template Selected");
                return;
            }

            swal({
                title: "Copy Template!",
                text: "Enter the name of new template (newly created template will show under user defined tab):",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: false,
                inputPlaceholder: "Template Name"
            }, function (name) {
                if (name === false) return false;
                if (name === "") {
                    swal.showInputError("You need to write something!");
                    return false
                }
                var data = {
                    TemplateId: SelectedSystemDefinedTemplateId, Title: name
                };
                $.ajax({
                    url: '/Newsletters/CopySystemTemplate',
                    method: 'POST',
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    dataType: 'json',
                    success: function (d) {
                        //alert("Template created!");

                        Newsletter.init();
                    }
                });


                //console.log(name);
                //pass it to ajax
                //create a new copy of template
                // get selected template id 
            });



            //swal({
            //    title: "Are you sure?",
            //    text: "Do you want to create a copy of selelcted template?",
            //    type: "warning",
            //    showCancelButton: true,
            //    confirmButtonClass: "btn-danger",
            //    confirmButtonText: "Yes",
            //    closeOnConfirm: true
            //},
            //function () {

            //    bootbox.prompt("Enter the name of new template", function (name) {




            //    });

            //    //prompt to enter name for the copy
            //});

        },

        userDefinedOptionChanged: function (obj) {

            var val = $(obj).val();
            if (val == enumNewsletterUserDefinedOptions.send) {
                this.sendTemplate();
            }
            else if (val == enumNewsletterUserDefinedOptions.create) {
                this.createTemplate();
            }
            else if (val == enumNewsletterUserDefinedOptions.modify) {
                this.modifyTemplate();
            }
            else if (val == enumNewsletterUserDefinedOptions.remove) {
                this.removeTemplate();
            }
            $("#ddlPredefinedNewsLetterAction").val('-1');
        },

        sendTemplate: function () {
            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId <= 0) {
                //alert("No template Selected");
                return;
            }

            // move to default selection of dropdown
            $('#sendNewsletterModel').modal('show'); //show model

            //clear previous values and enable/dislabe fields
            $("#lblserverTime").html('');
            $("#rbNew").click();

            $("#sendNewsletterDTP").data("kendoDateTimePicker").value(new Date());
            $("#sendNewsletterDTP").data("kendoDateTimePicker").enable(false);

            $("#rbSubscribers").click();
            $("#txtSendNewsletterEmail").val('');
            $("#txtSendNewsletterEmail").hide();

            this.loadServerTime();//load server time
        },

        createTemplate: function () {

            //var dialog = $("#templateEditorWindow").data("kendoWindow");
            //dialog.open();
            //$('#templateEditorWindow').modal('show');
            $(".templateEditordiv").show();
            $(".divNewsLetterList").hide();

            $("#btnTemplateReset").hide();
            this.loadTemplateDetailInEditor(false);
        },

        modifyTemplate: function (id) {

            //var dialog = $("#templateEditorWindow").data("kendoWindow");
            //dialog.open();

            //$('#templateEditorWindow').modal('show');
            $(".templateEditordiv").show();
            $(".divNewsLetterList").hide();

            $("#btnTemplateReset").show();
            $('.newsletter-industry-ddl').hide();

            this.loadTemplateDetailInEditor(true, id);

        },

        loadTemplateDetailInEditor: function (isModified, id, isForduplication) {

            if (isModified && isForduplication && id > 0) {
                $("#btnModify").show();
                $("#btnSave").hide();

                SelectedUserDefinedTemplateId = id;
                var item = NewsLetter_SystemTemplates.find(x => x.ID === id);
                var shellTemplate = NewsLetter_ShellTemplates.find(x => x.ID === item.ShellTemplateId);

                $("#templateEditor").data("kendoEditor").value(item.Markup);
                //this.setIframeHtml('editorPreview', shellTemplate.Markup.replace('[maincontent]', item.Markup));

                var currTemplateType = NewsLetter_TemplatesTypes.find(x => x.TemplateId == SelectedUserDefinedTemplateId);
                if (currTemplateType != null && currTemplateType != undefined) {
                    $("#ddlTemplatesTypes").val(currTemplateType.TemplateTypeId);
                }
                else {
                    $("#ddlTemplatesTypes").val('-1');
                }
                if (item.ShellTemplateId != null && item.ShellTemplateId != undefined)
                    $("#ddlShellTemplates").val(item.ShellTemplateId);
                else
                    $("#ddlShellTemplates").val('-1');


                if (item.IndustryId != null && item.IndustryId != undefined)
                    $("#ddlIndustries").val(item.IndustryId);
                else
                    $("#ddlIndustries").val('-1');


                var currIndustry = $("#ddlIndustries option:selected").val();
                $("#ddlSubIndustries").html('<option value="-1"> --select Sub-Industry--</option>');

                if (currIndustry == -1) {
                    return;
                }

                $.each(NewsLetter_SubIndustries, function (index, value) {
                    var item = NewsLetter_SubIndustries[index];
                    if (item.IndustryId == currIndustry)
                        $("#ddlSubIndustries").append('<option value="' + item.ID + '">' + item.Title + '</option>');
                });

                if (item.SubIndustryId != null && item.SubIndustryId != undefined)
                    $("#ddlSubIndustries").val(item.SubIndustryId);

                $("#txtTemplateTitle").val(item.Title);
            }
            else if (isModified && id > 0) {
                $("#btnModify").show();
                $("#btnSave").hide();

                SelectedUserDefinedTemplateId = id;
                var item;
                if (isForduplication)
                    item = NewsLetter_UserDefinedTemplates.find(x => x.ID === id);
                else
                    item = NewsLetter_SystemTemplates.find(x => x.ID === id);
                var shellTemplate = NewsLetter_ShellTemplates.find(x => x.ID === item.ShellTemplateId);

                $("#templateEditor").data("kendoEditor").value(item.Markup);
                //this.setIframeHtml('editorPreview', shellTemplate.Markup.replace('[maincontent]', item.Markup));

                var currTemplateType = NewsLetter_TemplatesTypes.find(x => x.TemplateId == SelectedUserDefinedTemplateId);
                if (currTemplateType != null && currTemplateType != undefined) {
                    $("#ddlTemplatesTypes").val(currTemplateType.TemplateTypeId);
                }
                else {
                    $("#ddlTemplatesTypes").val('-1');
                }
                if (item.ShellTemplateId != null && item.ShellTemplateId != undefined)
                    $("#ddlShellTemplates").val(item.ShellTemplateId);
                else
                    $("#ddlShellTemplates").val('-1');


                if (item.IndustryId != null && item.IndustryId != undefined)
                    $("#ddlIndustries").val(item.IndustryId);
                else
                    $("#ddlIndustries").val('-1');


                this.dllIndustryChange();

                if (item.SubIndustryId != null && item.SubIndustryId != undefined)
                    $("#ddlSubIndustries").val(item.SubIndustryId);

                $("#txtTemplateTitle").val(item.Title);
            }
            else {
                $("#btnModify").hide();
                $("#btnSave").show();

                SelectedUserDefinedTemplateId = id;
                $("#templateEditor").data("kendoEditor").value('');
                //this.setIframeHtml('editorPreview', '');


                $("#ddlTemplatesTypes").val('-1');
                $("#ddlShellTemplates").val('-1');
                $("#ddlIndustries").val('-1');
                $("#ddlSubIndustries").html('<option value="-1"> --select Sub-Industry--</option>'); // clear sub industry field
                $("#txtTemplateTitle").val('');
            }
        },

        removeTemplate: function () {

            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId <= 0) {
                //alert("No template Selected");
                return;
            }

            swal({
                title: "Are you sure?",
                text: "Do you want to permanently delete selelcted template?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                closeOnConfirm: true
            },
                function () {
                    $.ajax({
                        type: "GET",
                        url: '/Newsletters/RemoveSelectedUserDefinedTemplate?tempId=' + SelectedUserDefinedTemplateId,
                        success: function (data) {
                            //if (!data)
                            //ltcApp.errorMessage("Error", 'error removing template');
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            //ltcApp.errorMessage("Error", 'error removing template');
                        },
                        complete: function () {
                        }
                    });

                    //prompt to enter name for the copy
                });


        },

        loadTemplateActionView: function () {
            $(function () {
                var body = $(document.body);
                var editorWrapper = $("#editorWrapper");
                function onDragStart(e) {
                    if (!e.sender.dataItem(e.sourceNode).value) {
                        e.preventDefault();
                    } else {
                        kendo.ui.progress(editorWrapper, true);
                    }
                }

                function onDrag(e) {
                    if ($(e.dropTarget).closest(editorWrapper)[0]) {
                        e.setStatusClass("k-add");
                    } else {
                        e.setStatusClass("k-denied");
                    }
                }
                function onDrop(e) {
                    if ($(e.dropTarget).closest(editorWrapper)[0]) {
                        e.preventDefault();

                        $("#templateEditor").data("kendoEditor").exec("inserthtml", { value: e.sender.dataItem(e.sourceNode).value });
                    }
                    kendo.ui.progress(editorWrapper, false);
                }

                $("#NewsletterTreeview").kendoTreeView({
                    dataSource: {
                        data: [
                            {
                                text: "Newsletter", value: null, expanded: true,
                                items: Newsletter.LoadEmailTree()
                            }
                        ]
                    },
                    dataTextField: "text",
                    dataValueField: "value",
                    dragAndDrop: true,
                    dragstart: onDragStart,
                    drag: onDrag,
                    drop: onDrop
                });
                $("#EmailTreeview").kendoTreeView({
                    dataSource: {
                        data: [
                            {
                                text: "Email Confirmation", value: null, expanded: true,
                                items: Newsletter.LoadNewsletterTree()
                            }
                        ]
                    },
                    dataTextField: "text",
                    dataValueField: "value",
                    dragAndDrop: true,
                    dragstart: onDragStart,
                    drag: onDrag,
                    drop: onDrop
                });
                $("#OfficeImageTreeview").kendoTreeView({
                    //dataSource: {
                    //    data: [
                    //        {
                    //            text: "Images", value: null, expanded: true,
                    //            items: Newsletter.GetOfficeImages()
                    //        }
                    //    ]
                    //},
                    //dataTextField: "text",
                    //dataValueField: "value",
                    //dragAndDrop: true,
                    //dragstart: onDragStart,
                    //drag: onDrag,
                    //drop: onDrop
                });
                $("#ImageUploadButton").kendoButton({
                    //click: function (e) {
                    //    $.ajax({
                    //        async: false,
                    //        cache: false,
                    //        url: window.location.protocol + "//" + window.location.host + "/ImageManagement/Index",
                    //        contentType: 'application/html; charset=utf-8',
                    //        type: 'GET',
                    //        dataType: 'html'
                    //    }).success(function (data) {
                    //        $("#modal-window .modal-title").text("Image Management");
                    //        $("#modal-window .modal-body").html(data);
                    //        $("#modal-window .modal-body #UserImageTreeview").kendoTreeView({
                    //            dataSource: {
                    //                data: [
                    //                    {
                    //                        text: "Images", value: null, expanded: true,
                    //                        items: Newsletter.GetUserImages()
                    //                    }
                    //                ]
                    //            },
                    //            dataTextField: "text",
                    //            dataValueField: "value",
                    //            dragAndDrop: false
                    //        });
                    //        $("#modal-window").on("click", "#btn-close", function () {
                    //            $("#modal-window").modal("hide");
                    //        });
                    //        $("#modal-window").modal("show");
                    //    }).error(function (data) {
                    //        alert(data.responseText);
                    //    });
                    //}
                });
                //$("#templateEditor").kendoEditor({
                //    //tools: [
                //    //  //"insertImage"
                //    //]
                //});
                $("#templateEditor").kendoEditor({
                    tools: [
                        "bold",
                        "italic",
                        "underline",
                        "strikethrough",
                        "justifyLeft",
                        "justifyCenter",
                        "justifyRight",
                        "justifyFull",
                        "insertUnorderedList",
                        "insertOrderedList",
                        "indent",
                        "outdent",
                        "createLink",
                        "unlink",
                        "insertImage",
                        "insertFile",
                        "subscript",
                        "superscript",
                        "tableWizard",
                        "createTable",
                        "addRowAbove",
                        "addRowBelow",
                        "addColumnLeft",
                        "addColumnRight",
                        "deleteRow",
                        "deleteColumn",
                        "viewHtml",
                        "formatting",
                        "cleanFormatting",
                        "fontName",
                        "fontSize",
                        "foreColor",
                        "backColor",
                        "print"
                    ],
                    //keyup: function () {
                    //    console.log(this.value());
                    //}
                });
            });
        },

        loadTemplateTypes: function () {
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetTemplateTypes',
                success: function (data) {
                    if (data != null) {
                        NewsLetter_TemplatesTypes = data;
                        $("#ddlTemplatesTypes").html('<option value="-1"> --select template type--</option>');
                        $.each(NewsLetter_TemplatesTypes, function (index, value) {
                            var item = NewsLetter_TemplatesTypes[index];
                            $("#ddlTemplatesTypes").append('<option value="' + item.TemplateTypeId + '">' + item.TemplateTypeName + '</option>');
                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    //ltcApp.errorMessage("Error", 'Error loading template types');
                },
                complete: function () {
                }
            });
        },

        ddlTemplatesTypes_OnChange: function () {
            //newletter type change if not -1 before, alert that changing type will remove any exisiting types

        },

        loadShellTemplates: function () {
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetShellTemplates',
                success: function (data) {
                    if (data != null) {
                        NewsLetter_ShellTemplates = data;
                        $("#ddlShellTemplates").html('<option value="-1"> -- select base template --</option>');
                        $.each(NewsLetter_ShellTemplates, function (index, value) {
                            var item = NewsLetter_ShellTemplates[index];
                            $("#ddlShellTemplates").append('<option value="' + item.ID + '">' + item.Title + '</option>');
                        });

                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    //ltcApp.errorMessage("Error", 'Error loading shell templates');
                },
                complete: function () {
                }
            });
        },

        ddlShellTemplate_OnChange: function () {
            var shellId = $("#ddlShellTemplates option:selected").val();
            var item = NewsLetter_ShellTemplates.find(x => x.ID === Number(shellId));

            var currentContent = $("#templateEditor").data("kendoEditor").value();

            //this.setIframeHtml('editorPreview', item.Markup.replace('[maincontent]', currentContent));
        },

        loadSystemTemplates: function () {
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetPreDefinedTemplates',
                success: function (data) {
                    if (data != null) {
                        NewsLetter_SystemTemplates = data;
                        $("#SystemTemplateList").html('');
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    alert('Error loading system templates');
                },
                complete: function () {
                }
            });
        },

        loadSelectedSystemTemplate: function (tempId, obj) {
            SelectedUserDefinedTemplateId = null;
            SelectedSystemDefinedTemplateId = tempId;
            //var item = NewsLetter_SystemTemplates.find(x => x.ID === tempId);
            //if (item != null) {
            //    this.setIframeHtml('editorPreview', item.ShellMarkup.replace("[maincontent]", item.Markup));
            //} else {
            //    this.setIframeHtml('editorPreview', '');
            //}
        },

        //loadUserDefinedTemplates: function () {
        //    $.ajax({
        //        type: "GET",
        //        url: '/Newsletters/GetUserDefinedTemplates',
        //        success: function (data) {
        //            if (data != null) {
        //                $("#userDefineTemplateList").html('');
        //                NewsLetter_UserDefinedTemplates = data;
        //                $.each(NewsLetter_UserDefinedTemplates, function (index, value) {
        //                    var item = NewsLetter_UserDefinedTemplates[index];
        //                    $("#userDefineTemplateList").append(' <div class="tile image" onclick="Newsletter.loadSelectedUserdefinedTemplate(' + item.ID + ',this)"><div class="tile-body"><img src="../assets/pages/media/gallery/image2.jpg" alt=""></div><div class="tile-object"><div class="name"> ' + item.Title + ' </div></div></div>');
        //                });
        //            }
        //        },
        //        error: function (xhr, textStatus, errorThrown) {
        //            //ltcApp.errorMessage("Error", 'Error loading user defined templates');
        //        },
        //        complete: function () {
        //            if (SelectedUserDefinedTemplateId != null)
        //                Newsletter.loadSelectedUserdefinedTemplate(SelectedUserDefinedTemplateId, this)
        //        }
        //    });
        //},

        loadSelectedUserdefinedTemplate: function (tempId, obj) {
            SelectedUserDefinedTemplateId = tempId;
            SelectedSystemDefinedTemplateId = null;
            //var item = NewsLetter_UserDefinedTemplates.find(x => x.ID === tempId);
            //if (item != null) {
            //    this.setIframeHtml('editorPreview', item.ShellMarkup.replace("[maincontent]", item.Markup));
            //} else {
            //    this.setIframeHtml('editorPreview', '');
            //}

        },

        setIframeHtml: function (iframeId, html, isPreFromModel) {


            $("#btnNewsletterPreviewModal").unbind("click");
            if (isPreFromModel)
                $("#btnNewsletterPreviewModal").click(function () {
                    $('#newsletterPreviewModal').modal('hide');
                    $('#PreDefinedNewsLetter').modal('show');
                });
            else
                $("#btnNewsletterPreviewModal").click(function () {
                    $('#newsletterPreviewModal').modal('hide');
                });


            $("#newsletterPreviewModal").modal('show');
            var iframe = document.getElementById(iframeId);
            iframe = iframe.contentWindow || (iframe.contentDocument.document || iframe.contentDocument);

            iframe.document.open();
            iframe.document.write(html);
            iframe.document.close();
        },

        clearTabSelection: function (tab) {
            if (tab == 'system') {
                SelectedUserDefinedTemplateId = null;
                $("#SystemTemplateList .image:first").click();
            }
            else {
                $("#userDefineTemplateList .image:first").click();
            }
        },
        //send newsletter secion start

        loadServerTime: function () {
            $.ajax({
                type: "GET",
                url: '/Newsletters/LoadServerTime',
                success: function (data) {
                    if (data != null) {
                        $("#lblserverTime").html(data);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    //ltcApp.errorMessage("Error", 'Error loading server time');
                },
                complete: function () {
                }
            });
        },

        scheduleFor: function (val) {
            if (val == 'now') {
                $("#sendNewsletterDTP").data("kendoDateTimePicker").enable(false);
            } else { // val = future
                $("#sendNewsletterDTP").data("kendoDateTimePicker").enable(true);
            }
        },

        sendAs: function (val) {
            if (val == 'subscriber') {
                $("#txtSendNewsletterEmail").hide();
            } else { // val = email
                $("#txtSendNewsletterEmail").show();
            }
        },

        sendNewsletter: function () {
            var sendToSubcribers = true;

            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId < 0) {
                //alert("No template selected!");
                return;
            }

            if ($("input[name='rbSchedule']:checked").val() == 'future') {
                var checkTime = Date.parse($("#sendNewsletterDTP").val())
                if (isNaN(checkTime)) {
                    //alert("Incorrect date time!");
                    return;
                }
            }

            if ($("input[name='rbSendAs']:checked").val() == 'singleemail') {
                sendToSubcribers = false;
                //if ($("#txtSendNewsletterEmail").val().trim() == '' || !ltcApp.validateEmail($("#txtSendNewsletterEmail").val().trim())) {
                //    alert("Invalid email address!");
                //    return;
                //}
            }


            var data = {
                ScheduledDateTime: $("#sendNewsletterDTP").val(),
                SendToSubscribers: sendToSubcribers,
                Email: $("#txtSendNewsletterEmail").val().trim(),
                TemplateId: SelectedUserDefinedTemplateId
            };

            Layout.showLoader();
            $.ajax({
                url: '/Newsletters/SendNewsletter',
                method: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                success: function (d) {
                    Layout.hideLoader();
                    $('#sendNewsletterModel').modal('hide');
                    alert("Newsletter Scheduled successfully!");
                },
                error: function (xhr, data, errorThrown) {
                    //show error
                    Layout.hideLoader();
                }
            });
        },

        //Load editor treelist
        LoadNewsletterTree: function () {
            var data = [
                { text: 'Office Name', value: '[OfficeName]' },
                { text: 'Subscriber Name', value: '[Subscriber]' },
                { text: 'Patient Salutation', value: '[PatientSalutation]' },
                { text: 'Patient FirstName"', value: '[PatientFirstName]' },
                { text: 'Patient LastName', value: '[PatientLastName]' },
                { text: 'Date', value: '[Date]' },
                { text: 'Newsletter Title', value: '[subjecttext]' },
                { text: 'Address Block', value: '[AddressBlock]' },
                { text: 'Office Email', value: '[OfficeEmail]' },
                { text: 'Office Street 1', value: '[OfficeStreet]' },
                { text: 'Office Street 2', value: '[OfficeStreet2]' },
                { text: 'Office Street 3', value: '[OfficeStreet3]' },
                { text: 'Province', value: '[OfficeProvince]' },
                { text: 'City', value: '[City]' },
                { text: 'Country', value: '[Country]' },
                { text: 'Postal Code', value: '[PostalCode]' },
                { text: 'Office Phone Number', value: '[OfficePhoneatientName]' },
                { text: 'Fax', value: '[Fax]' },
            ];
            return data;
        },

        LoadEmailTree: function () {
            var data = [
                { text: 'Patient Name', value: '[PatientName]' },
                { text: 'Patient Salutation', value: '[PatientSalutation]' },
                { text: 'Patient FirstName', value: '[PatientFirstName]' },
                { text: 'Patient LastName', value: '[PatientLastName]' },
                { text: 'Patient Email', value: '[PatientEmail]' },
                { text: 'Family Member List', value: '[FamilyList]' },
                { text: 'Appoint Date', value: '[AppointDate]' },
                { text: 'Appoint Time', value: '[AppointTime]' },
                { text: 'Provider Name', value: '[ProviderName]' },
                { text: 'Job Description', value: '[JobDescription]' },
                { text: 'Office Name', value: '[OfficeName]' },
                { text: 'Office Email', value: '[OfficeEmail]' },
                { text: 'Address Block', value: '[AddressBlock]' },
                { text: 'Office Street1', value: '[OfficeStreet]' },
                { text: 'Office Street2', value: '[OfficeStreet2]' },
                { text: 'Office Street3', value: '[OfficeStreet3]' },
                { text: 'Province', value: '[OfficeProvince]' },
                { text: 'City', value: '[City]' },
                { text: 'Country', value: '[Country]' },
                { text: 'PostalCode', value: '[PostalCode]' },
                { text: 'Office Phone Number', value: '[OfficePhone]' },
                { text: 'Fax', value: '[Fax]' },
                { text: 'Confirmation Button', value: '[clickhere]' },
            ];
            return data;
        },

        presaveNewsletter: function () {
            if ($("#ddlShellTemplates option:selected").val() == '-1' || $("#ddlShellTemplates option:selected").val() == undefined) {
                alert("Please select a base template.");
                return;
            }
            if ($("#ddlIndustries option:selected").val() == '-1' || $("#ddlIndustries option:selected").val() == undefined) {
                alert("Please select an industry.");
                return;
            }
            if ($("#ddlSubIndustries option:selected").val() == '-1' || $("#ddlSubIndustries option:selected").val() == undefined) {
                alert("Please select an sub-industry.");
                return;
            }

            // not mandatorys
            if ($("#ddlTemplatesTypes option:selected").val() != '-1' && $("#ddlTemplatesTypes option:selected").val() != undefined) {
                currTemplateTypeId = $("#ddlTemplatesTypes option:selected").val();
            }

            $("#newsletterSaveModel").modal('show');
        },

        saveNewsletterEditor: function (isSave) {
            var currTemplateTypeId = 0;
            if ($("#txtTemplateTitle").val().trim() == '') {
                alert("Title cannot be empty.");
                return;
            }

            //var currUserTemplate = NewsLetter_UserDefinedTemplates.find(x => x.ID === SelectedUserDefinedTemplateId);
            //var currShellTemplate = NewsLetter_ShellTemplates.find(x=>x.ID === currUserTemplate.ShellTemplateId);
            var currTemplateId = 0;
            if (!isSave) {
                currTemplateId = SelectedUserDefinedTemplateId;
            }


            var data = {
                Title: $("#txtTemplateTitle").val(),
                ID: currTemplateId,
                TemplateTypeId: currTemplateTypeId,
                ShellTemplateId: $("#ddlShellTemplates option:selected").val(),
                Markup: $("#templateEditor").data("kendoEditor").value(),
                IndustryId: $("#ddlIndustries option:selected").val(),
                SubIndustryId: $("#ddlSubIndustries option:selected").val(),
            };
            Layout.showLoader();
            $.ajax({
                url: '/Newsletter/SaveNewsletterEditor',
                method: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                success: function (d) {
                    Layout.hideLoader();
                    alert("Template Saved!");
                    //$('#templateEditorWindow').modal('hide');
                    $(".templateEditordiv").hide();
                    $(".divNewsLetterList").show();

                    PreNewsLetterControls.loadFromHomePage();
                    Newsletter.loadSystemTemplates();
                    $("#newsletterSaveModel").modal('hide');
                    $('#divPreviewPreNewsLetter').html('<iframe id="previewPreNewsLetter" class="" style="min-height:400px; width:100%;"></iframe>');
                },
                error: function (xhr, data, errorThrown) {
                    //show error
                    Layout.hideLoader();
                }
            });

            //$('.newsletter-industry-ddl').show();
            //$('#publicNewsLetterCreateNew').show();
        },

        resetNewsletterEditor: function () {

            this.loadTemplateDetailInEditor(true, SelectedUserDefinedTemplateId);
        },

        previewNewsletterEditor: function () {
            var shellId = $("#ddlShellTemplates option:selected").val();

            if (shellId === "-1")
                alert('Select Shell Template First.');
            else {
                var item = NewsLetter_ShellTemplates.find(x => x.ID === Number(shellId));

                var currentContent = $("#templateEditor").data("kendoEditor").value();

                this.setIframeHtml('editorPreview', item.Markup.replace('[maincontent]', currentContent));
            }

        },

        cancelNewsletterEditor: function () {
            //$('#PreDefinedNewsLetter').modal('show');
            //$('#templateEditorWindow').modal('hide');
            $("#btnModify").hide();
            $("#btnSave").hide();
            //$('.newsletter-industry-ddl').show();
            //$('#publicNewsLetterCreateNew').show();
            $(".templateEditordiv").hide();
            $(".divNewsLetterList").show();
        },

        loadIndustries: function () {
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetIndustries',
                success: function (data) {
                    if (data != null) {
                        NewsLetter_Industries = data;
                        $("#ddlIndustries").html('<option value="-1"> --select Industry--</option>');
                        $.each(NewsLetter_Industries, function (index, value) {
                            var item = NewsLetter_Industries[index];
                            $("#ddlIndustries").append('<option value="' + item.ID + '">' + item.Title + '</option>');
                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    //ltcApp.errorMessage("Error", 'Error loading industries');
                },
                complete: function () {
                }
            });
        },

        ddlIndustries_OnChange: function () {
            this.dllIndustryChange();

        },

        dllIndustryChange: function () {

            var currIndustry = $("#ddlIndustries option:selected").val();
            $("#ddlSubIndustries").html('<option value="-1"> --select Sub-Industry--</option>');

            if (currIndustry == -1) {
                return;
            }

            $.each(NewsLetter_SubIndustries, function (index, value) {
                var item = NewsLetter_SubIndustries[index];
                if (item.IndustryId == currIndustry)
                    $("#ddlSubIndustries").append('<option value="' + item.ID + '">' + item.Title + '</option>');
            });
            $("#ddlSubIndustries").val('-1');
        },

        loadSubIndustries: function () {
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetSubIndustries',
                success: function (data) {
                    if (data != null) {
                        NewsLetter_SubIndustries = data;
                        $("#ddlSubIndustries").html('<option value="-1"> --select Sub-Industry--</option>');
                        //$("#ddlSubIndustries").html('<option value="-1"> --select SubIndustry--</option>');
                        //$.each(NewsLetter_SubIndustries, function (index, value) {
                        //    var item = NewsLetter_SubIndustries[index];
                        //    $("#ddlSubIndustries").append('<option value="' + item.ID + '">' + item.Title+ '</option>');
                        //});
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    //ltcApp.errorMessage("Error", 'Error loading sub industries');
                },
                complete: function () {
                }
            });
        },

        IframeSizing: function () {
            //$("#editorPreview").width($(".EditorCol").width());
            //$("#editorPreview").height($(".EditorCol").height());
        },

        GetUserImages: function () {
            var data;
            $.ajax({
                async: false,
                cache: false,
                url: window.location.protocol + "//" + window.location.host + "/ImageManagement/GetUserImages",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json',
                success: function (d) {
                    data = JSON.parse(d);
                    return data;
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });

           
        },

        GetOfficeImages: function () {
            var data = [];
            $.ajax({
                async: false,
                cache: false,
                url: window.location.protocol + "//" + window.location.host + "/ImageManagement/GetOfficeImages",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json',
                success: function (d) {
                    d = JSON.parse(d);
                    $(d).each(function (i, val) {
                        data.push({ text: val.name, value: "<img src='" + val.path + "'>" });
                    });
                    return data;
                },
                error: function (data) {
                    alert(data.responseText);
                }
            });

        }
    };
}();

$(document).ready(function () {
    Newsletter.init();
    Newsletter.initActionPage();

});


var enumNewsletterUserDefinedOptions = {
    "send": "send", "assign": "assign", "create": "create", "modify": "modify", "remove": "remove"
};

$(window).resize(function () {
    Newsletter.IframeSizing();
});

//global variables

var NewsLetter_TemplatesTypes;
var NewsLetter_SystemTemplates;
var NewsLetter_ShellTemplates;
var NewsLetter_UserDefinedTemplates;
var NewsLetter_Industries;
var NewsLetter_SubIndustries;
var SelectedUserDefinedTemplateId;
var SelectedSystemDefinedTemplateId;
var isKendoWindowLoaded = false;