
var Newsletter = function () {
    return {

        init: function () {

            this.loadArticleTypes();
            this.loadTemplateTypes();
            this.loadUserDefinedTemplates(true);
            //this.initUserDefinedTable();
            //Layout.showLoader();

            //this.loadArticles();
            //this.loadSystemTemplates();

        },


        initActionPage: function (loadOnlyImages) {
            kendo.destroy($("#OfficeImageTreeview"));
            var now = new Date();
            var utc = new Date(now.getTime() + now.getTimezoneOffset() * 60000);

            $("#sendNewsletterDTP").kendoDateTimePicker({
                value: utc,
                dateInput: true
            });

            this.loadTemplateActionView(loadOnlyImages);

            $('#templateEditorWindow').on('shown',
                function () {
                    Newsletter.IframeSizing();
                });
        },
        ItemSelected: function (id) {
            var post_arr = [];
            $('#marketingTemplateList input[type=checkbox]').each(function () {
                if (jQuery(this).is(":checked")) {
                    var id = this.id;
                    post_arr.push(id);
                }
            });

            if (post_arr.length > 0) {
                $('#btnDeleteSelectOptions').removeAttr('disabled');
            } else {
                $("#btnDeleteSelectOptions").attr("disabled", true);
            }
        },

        initKendoWindow: function () {

        },
        saveArticleTemplate: function () {


            var tempId = $("#ddlTemplatesTypes2").val();
            if (tempId == "-1") {
                ltcApp.warningMessage(null, "No article Selected!");
            }
            if ($("#txtArticleTitle").val() == "") {
                ltcApp.warningMessage(null, "Please provide article name!");
            }

            var name = $("#txtArticleTitle").val();
            //var articleWithSameName = NewsLetter_UserDefinedTemplates.find(x => x.TemplateTitle == name);
            //if (articleWithSameName != null) {
            //    ltcApp.warningMessage(null, "Newsletter with same name already exists.");
            //    return;
            //}

            var content = '';
            var template = NewsLetter_SystemTemplates.find(x => x.TemplateID == tempId);
            var article = articles.find(x => x.ArticleID == SelectedArticleId);
            $("#dvHidden").html('');
            if (template != null && article != null) {

                var data = {
                    ArticleID: SelectedArticleId
                };
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    dataType: 'json',
                    url: '/Newsletter/GetArticleTemplateDetail',
                    success: function (data) {

                        if (data != null) {
                            var item1 = data;

                            if (item1 != null) {
                                if (item1.Content.length > 0) {
                                    $('#dvHidden').html(template.TemplateSourceMarkup);
                                    $("#content").html('');
                                    $("#content2").html('');
                                    $("#content").html(item1.Content);
                                    content = $("#dvHidden").html();
                                } else {
                                    $('#dvHidden').html('');
                                    $("#content").html('');
                                    $("#content2").html('');
                                    $("#content").html('');
                                    content = $("#dvHidden").html();
                                }


                            } else {
                                $('#dvHidden').html('');
                                $("#content").html('');
                                $("#content2").html('');
                                $("#content").html('');
                                content = $("#dvHidden").html();

                            }


                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {

                        ltcApp.errorMessage("Error", 'Error loading preview');

                    },
                    complete: function () {
                        Layout.hideLoader();

                        $("#btnSaveArticle").attr("disabled", true);


                        var element = $("#html-content-holder"); // global variable
                        document.getElementById("dvManage").style.position = "";
                        document.getElementById("dvManage").style.top = "0px";
                        document.getElementById("dvManage").style.left = "0px;";
                        $("#html-content-holder").html("<!DOCTYPE html><html lang='en'><head><meta charset='utf-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'></head><body>" + content + "</body></html>");
                        var element = $("#html-content-holder"); // global variable
                        html2canvas(element, {
                            useCORS: true,
                            imageTimeout: 15000,
                            onrendered: function (canvas) {
                                $("#previewImage").append(canvas);

                                getCanvas = canvas;
                                var imageData = getCanvas.toDataURL("image/png");

                                var data = {
                                    TemplateId: $("#ddlTemplatesTypes2").val(),
                                    ArticleId: SelectedArticleId,
                                    Title: $("#txtArticleTitle").val(),
                                    Content: content,
                                    ContentImageString: imageData
                                };


                                $.ajax({
                                    url: '/Newsletter/CopyArticle',
                                    method: 'POST',
                                    data: JSON.stringify(data),
                                    contentType: 'application/json',
                                    dataType: 'json',
                                    success: function (d) {

                                        $('#useArticle').modal('hide');
                                        ltcApp.successMessage(null, "Template created!");
                                        // Newsletter.init();
                                        $('#btnSaveArticle').removeAttr('disabled');

                                        document.getElementById("dvManage").style.position = "absolute";
                                        document.getElementById("dvManage").style.top = "-9999px";
                                        document.getElementById("dvManage").style.left = "-9999px;";
                                    }
                                });



                            }
                        });

                    }
                })

                //content = template.TemplateSourceMarkup;

            }








        },
        useArticleTempalte: function () {

            if (SelectedArticleId == null || SelectedArticleId <= 0) {
                ltcApp.warningMessage(null, "No article Selected");
                return;
            }

            $('#useArticle').modal('show');
            var article = articles.find(x => x.ArticleID == SelectedArticleId);
            $("#txtArticleTitle").val(article.Title)
        },
        moveArticleTempalte: function () {

            if (SelectedArticleId == null || SelectedArticleId <= 0) {
                ltcApp.warningMessage(null, "No article Selected");
                return;
            }

            $('#moveArticle').modal('show');
        },
        useTempalte: function () {


            if (SelectedSystemDefinedTemplateId == null || SelectedSystemDefinedTemplateId <= 0) {
                ltcApp.warningMessage(null, "No template Selected");
                return;
            }

            swal({
                title: "Copy Template!",
                text: "Template will save under Marketing Templates",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: true,
                inputPlaceholder: "Enter the name of new template"
            }, function (name) {
                if (name === false) return false;
                if (name === "") {
                    swal.showInputError("You need to write something!");
                    return false;
                }

                //var articleWithSameName = NewsLetter_UserDefinedTemplates.find(x => x.TemplateTitle == name);
                //if (articleWithSameName != null) {
                //    ltcApp.warningMessage(null, "Newsletter with same name already exists.");
                //}
                $('.confirm').attr("disabled", true);
                var data = {
                    TemplateId: SelectedSystemDefinedTemplateId, Title: name
                };
                $.ajax({
                    url: '/Newsletter/CopySystemTemplate',
                    method: 'POST',
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    dataType: 'json',
                    success: function (d) {

                        ltcApp.successMessage(null, "Template created!");
                        Newsletter.init();
                        $('.confirm').removeAttr('disabled');

                    },
                    error: function (xhr, textStatus, errorThrown) {
                        ltcApp.errorMessage("Error", 'Please try again later!');
                    },
                });



            });

        },

        userDefinedOptionChanged: function (val) {

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
            else if (val == enumNewsletterUserDefinedOptions.makeDefault) {
                this.MakeDefaultTemplate(true);
            }
            else if (val == enumNewsletterUserDefinedOptions.removeDefault) {
                this.MakeDefaultTemplate(false);
            }

            setTimeout(function () {
                $(document).off("focusin");
                $(document).off("focus");
            },
                3000);

        },

        sendTemplate: function () {

            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId <= 0) {
                ltcApp.warningMessage(null, "No template Selected");
                return;
            }
            this.loadServerTime();//load server time

            // move to default selection of dropdown
            $('#sendNewsletterModel').modal('show'); //show model

            //clear previous values and enable/dislabe fields
            $("#lblserverTime").html('');
            $("#rbNew").click();



            $("#rbSubscribers").click();
            $("#txtSendNewsletterEmail").val('');
            $("#txtSendNewsletterEmail").hide();

        },

        createTemplate: function () {
            this.initKendoWindow();
            //var dialog = $("#templateEditorWindow").data("kendoWindow");
            //dialog.open();
            $('#templateEditorWindow').modal('show');
            $("#btnTemplateReset").hide();
            $("#btnHardReset").hide();

            this.loadTemplateDetailInEditor(false);
        },

        modifyTemplate: function () {
            this.initKendoWindow();
            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId <= 0) {
                ltcApp.warningMessage(null, "No template Selected");
                return;
            }


            $('#templateEditorWindow').modal('show');
            $("#btnHardReset").show();
            $("#btnTemplateReset").show();
            this.loadTemplateDetailInEditor(true);

        },
        HardReset: function () {



            var item = NewsLetter_UserDefinedTemplates.find(x => x.LetterID === SelectedUserDefinedTemplateId);

            //   $("#templateEditor").data("kendoEditor").value(item.TemplateSourceMarkup);


            var currTemplateType = NewsLetter_TemplatesTypes.find(x => x.TypeID == item.TypeID);
            if (currTemplateType != null && currTemplateType != undefined) {
                $("#ddlTemplatesTypes").val(currTemplateType.TypeID);
            }
            else {
                $("#ddlTemplatesTypes").val('-1');
            }

            if (item != null && item != undefined) {
                $("#ddlCategoryArticle3").val(currTemplateType.CategoryID);
            }
            else {
                $("#ddlCategoryArticle3").val('-1');
            }

            $("#txtTemplateTitle").val(item.TemplateTitle);



            var data = {
                LetterID: SelectedUserDefinedTemplateId
            };
            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Newsletter/GetUserDefinedTemplateDetail',
                success: function (data) {

                    if (data != null) {
                        var item1 = data;

                        if (item1 != null) {
                            if (item1.TemplateSourceMarkup.length > 0) {
                                Newsletter.setIframeHtml('editorPreview', item1.TemplateSourceMarkup);
                                $("#templateEditor").data("kendoEditor").value(item1.TemplateSourceMarkup);
                            } else {
                                Newsletter.setIframeHtml('editorPreview', '');
                                $("#templateEditor").data("kendoEditor").value('');

                            }


                        } else {
                            Newsletter.setIframeHtml('editorPreview', '');
                            $("#templateEditor").data("kendoEditor").value('');

                        }


                    }
                },
                error: function (xhr, textStatus, errorThrown) {

                    ltcApp.errorMessage("Error", 'Error loading preview');

                },
                complete: function () {
                    Layout.hideLoader();
                }
            })



            $(document).off("focusin");
            $(document).off("focus");
        },
        loadTemplateDetailInEditor: function (isModified) {

            Newsletter.setIframeHtml('editorPreview', '');
            $("#templateEditor").data("kendoEditor").value('');

            if (isModified) {

                $("#btnModify").show();
                $("#btnSave").hide();

                var item = NewsLetter_UserDefinedTemplates.find(x => x.LetterID === SelectedUserDefinedTemplateId);



                if (item.TypeID == 8) {
                    $('#dvCategory').show();
                } else {
                    $('#dvCategory').hide();
                }


                var currTemplateType = NewsLetter_TemplatesTypes.find(x => x.TypeID == item.TypeID);
                if (currTemplateType != null && currTemplateType != undefined) {
                    $("#ddlTemplatesTypes").val(currTemplateType.TypeID);
                    $("#ddlCategoryArticle3").val(item.CategoryID);

                }
                else {
                    $("#ddlTemplatesTypes").val('-1');
                    $("#ddlCategoryArticle3").val('-1');
                }

                $("#txtTemplateTitle").val(item.TemplateTitle);

                var data = {
                    LetterID: SelectedUserDefinedTemplateId
                };
                $.ajax({
                    type: "POST",
                    data: JSON.stringify(data),
                    contentType: 'application/json',
                    dataType: 'json',
                    url: '/Newsletter/GetUserDefinedTemplateDetail',
                    success: function (data) {
                        Layout.hideLoader();
                        if (data != null) {
                            var item = data;

                            if (item != null) {
                                $("#previewContentEmpty").addClass('hide');
                                $("#previewContent").removeClass('hide');
                                Newsletter.setIframeHtml('editorPreview', item.MainBodymarkup);
                                $("#templateEditor").data("kendoEditor").value(item.MainBodymarkup);

                            } else {
                                $("#previewContentEmpty").removeClass('hide');
                                $("#previewContent").addClass('hide');
                                Newsletter.setIframeHtml('editorPreview', '');
                                $("#templateEditor").data("kendoEditor").value('');
                            }


                        }
                    },
                    error: function (xhr, textStatus, errorThrown) {

                        ltcApp.errorMessage("Error", 'Error loading preview');

                    },
                    complete: function () {
                        Layout.hideLoader();
                    }
                })



            }
            else {
                $('#dvCategory').hide();

                $("#btnModify").hide();
                $("#btnSave").show();

                $("#templateEditor").data("kendoEditor").value('');
                this.setIframeHtml('editorPreview', '');
                $("#ddlCategoryArticle3").val('-1');

                $("#ddlTemplatesTypes").val('-1');

                $("#txtTemplateTitle").val('');

            }
            $(document).off("focusin");
            $(document).off("focus");
        },

        DeleteImage: function (fileName) {

            swal({
                title: "Are you sure?",
                text: "Do you want to permanently delete selected image?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                closeOnConfirm: true
            },

                function () {


                    var input = {
                        file: fileName
                    };
                    $.ajax({
                        type: "POST",
                        data: JSON.stringify(input),
                        url: '/ImageManagement/DeleteImage',
                        contentType: 'application/json',
                        success: function (data) {
                            Newsletter.initActionPage(true);

                            if (!data)
                                ltcApp.errorMessage("Error", 'Error removing image');
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            ltcApp.errorMessage("Error", 'error removing image');
                        },
                        complete: function () {

                            setTimeout(function () {
                                Newsletter.initActionPage(true);
                            },
                                3000);

                            $("#modal-window").modal("hide");
                            ltcApp.successMessage(null, "Image removed!");
                            $("#ImageUploadButton").click();
                            Layout.hideLoader();


                        }
                    });

                    //prompt to enter name for the copy
                });


        },
        refreshList: function () {



            var editorWrapper = $("#editorWrapper");
            var data = [];
            $.ajax({
                type: "GET",
                url: "/ImageManagement/GetUserImagesLink",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (d) {

                    d = JSON.parse(d);
                    $(d).each(function (i, val) {
                        data.push({ text: val.name, value: "<img src='" + val.path + "'>" });
                    });

                    $("#OfficeImageTreeview").kendoTreeView({
                        dragAndDrop: true,
                        //dragstart: onDragStart,
                        //drag: onDrag,
                        //drop: onDrop,
                        dataSource: data
                    }).data("kendoTreeView");
                }
            });
        },
        removeTemplate: function () {

            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId <= 0) {
                ltcApp.warningMessage(null, "No template Selected");
                return;
            }


            swal({
                title: "Are you sure?",
                text: "Do you want to permanently delete selected template?",
                type: "warning",
                showCancelButton: true,
                confirmButtonClass: "btn-danger",
                confirmButtonText: "Yes",
                closeOnConfirm: true
            },

                function () {
                    var input = {
                        tempId: SelectedUserDefinedTemplateId
                    };
                    $.ajax({
                        type: "POST",
                        data: JSON.stringify(input),
                        url: '/Newsletter/RemoveSelectedUserDefinedTemplate',
                        contentType: 'application/json',
                        success: function (data) {
                            if (!data)
                                ltcApp.errorMessage("Error", 'error removing template');
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            ltcApp.errorMessage("Error", 'error removing template');
                        },
                        complete: function () {

                            $("#ddlCategoryArticle2").prop('selectedIndex', 0);
                            ltcApp.successMessage(null, "Template removed!");
                            $('#ddlTemplatesTypes1').prop('selectedIndex', 0);
                            if (currentView == 'user') {
                                Newsletter.loadUserDefinedTemplates(true);
                            } else {
                                Newsletter.loadUserDefinedTemplates(false);
                            }
                            // Newsletter.loadTemplateTypes(); //reload selected template types and repopulate the dropdown
                        }
                    });


                });


        },
        MakeDefaultTemplate: function (isDefaultCheck) {
            var item = NewsLetter_UserDefinedTemplates.find(x => x.LetterID === SelectedUserDefinedTemplateId);
            if (item.IsDefault) {
                ltcApp.errorMessage("Warning!", "Please select other template as Default");
                return;
            }
            //Layout.showLoader();

            var data = {
                IsDefault: isDefaultCheck,
                LetterID: SelectedUserDefinedTemplateId,
            };

            $.ajax({
                url: '/Newsletter/MakeDefault',
                method: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                success: function (d) {
                    if (d) {
                        ltcApp.successMessage(null, "Updated");
                        if (currentView == 'user') {
                            Newsletter.loadUserDefinedTemplates(true);
                        } else {
                            Newsletter.loadUserDefinedTemplates(false);
                        }

                        $('#ddlTemplatesTypes1').prop('selectedIndex', 0);

                    } else {
                        ltcApp.errorMessage("Warning!", "Please other template as Default");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                },
                complete: function () {
                    Layout.hideLoader();
                }
            });
        },
        loadTemplateActionView: function (loadOnlyImages) {


            $(function () {

                var body = $(document.body);
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

                var editorWrapper = $("#editorWrapper");
                if (loadOnlyImages) {
                    $("#OfficeImageTreeview").kendoTreeView({
                        dataSource: {
                            data: [
                                {
                                    text: "Images",
                                    value: null,
                                    expanded: false,
                                    items: Newsletter.GetOfficeImages()
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
                } else {


                    $("#EmailTreeview").kendoTreeView({
                        dataSource: {
                            data: [
                                {
                                    text: "Email Confirmation",
                                    value: null,
                                    expanded: true,
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

                    var dt = $("#OfficeImageTreeview").kendoTreeView({

                        dataSource: {
                            data: [
                                {
                                    text: "Images", value: null, expanded: false,
                                    items: Newsletter.GetOfficeImages()
                                }
                            ]
                        },
                        dataTextField: "text",
                        dataValueField: "value",
                        dragAndDrop: true,
                        dragstart: onDragStart,
                        drag: onDrag,
                        drop: onDrop
                    }).data("kendoTreeView");




                    $("#ImageUploadButton").kendoButton({
                        click: function (e) {
                            $.ajax({
                                async: false,
                                cache: false,
                                url: window.location.protocol + "//" + window.location.host + "/ImageManagement/Index",
                                contentType: 'application/html; charset=utf-8',
                                type: 'GET',
                                dataType: 'html',
                                success: function (data) {
                                    $("#modal-window .modal-title").text("Image Management");
                                    $("#modal-window .modal-body").html(data);
                                    $("#modal-window .modal-body #UserImageTreeview").kendoTreeView({
                                        template: kendo.template($("#treeview-template").html()),
                                        dataSource: {
                                            data: [
                                                {
                                                    text: "Images", value: null, expanded: false,
                                                    items: Newsletter.GetOfficeImages(),


                                                }
                                            ],
                                            select: function (e) {
                                                alert('a');
                                            }
                                        },
                                        dataTextField: "text",
                                        dataValueField: "value",
                                        dragAndDrop: false
                                    });
                                    $("#modal-window").on("click", "#btn-close", function () {
                                        $("#modal-window").modal("hide");
                                    });

                                    $("#modal-window").modal("show");
                                },
                                error: function (data) {
                                    alert(data.responseText);
                                }
                            });
                        }
                    });
                    function onChange(e) {
                        somethingChanged = true;
                    }

                    function onPaste(e) {
                        somethingChanged = true;
                    }
                    $("#templateEditor").kendoEditor({
                        change: onChange,
                        paste: onPaste,
                        tools: [
                            "bold",
                            "italic",
                            "underline",
                            {
                                name: "fontName",
                                items: [
                                    { text: "Andale Mono", value: "Andale Mono" },
                                    { text: "Arial", value: "Arial" },
                                    { text: "Arial Black", value: "Arial Black" },
                                    { text: "Book Antiqua", value: "Book Antiqua" },
                                    { text: "Comic Sans MS", value: "Comic Sans MS" },
                                    { text: "Courier New", value: "Courier New" },
                                    { text: "Georgia", value: "Georgia" },
                                    { text: "Helvetica", value: "Helvetica" },
                                    { text: "Impact", value: "Impact" },
                                    { text: "Symbol", value: "Symbol" },
                                    { text: "Tahoma", value: "Tahoma" },
                                    { text: "Terminal", value: "Terminal" },
                                    { text: "Times New Roman", value: "Times New Roman" },
                                    { text: "Trebuchet MS", value: "Trebuchet MS" },
                                    { text: "Verdana", value: "Verdana" },
                                    { text: "Webdings", value: "Webdings" },
                                    { text: "Wingdings", value: "Wingdings" }
                                ]
                            },
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
                        ]



                    });
                }
            });
        },
        moveArticle: function (cateId) {

            if (SelectedArticleId == null || SelectedArticleId <= 0) {
                ltcApp.warningMessage(null, "No article Selected");
                return;
            }
            var str = '';
            var input = {
                CategoryId: cateId,
                ArticleId: SelectedArticleId,
            };
            $.ajax({
                url: '/Newsletter/moveArticle',
                method: 'POST',
                data: JSON.stringify(input),
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {

                    if (data != null) {
                        $('#moveArticle').modal('hide');

                        ltcApp.successMessage("Article Moved", 'Article has been moved.');
                        $("#ddlCategoryArticle2").prop('selectedIndex', 0);

                        var noTemp = '<div class="row" style="padding:10px;margin-left:25px;margin-right:25px">No Article Found! </div>';
                        $.ajax({
                            type: "GET",
                            url: '/Newsletter/GetArticles',
                            success: function (data) {

                                if (data != null) {
                                    $("#tblBodyArticle").html('');
                                    articles = data;
                                    if (articles.length < 1) {
                                        $("#tblBodyArticle").html(noTemp);
                                    } else {
                                        var str = '';

                                        $.each(articles, function (index, item) {

                                            var st = item.ModificationDate;
                                            var modificationDate = new Date(st);
                                            str += "<tr><td>" +
                                                item.Title +
                                                "</td ><td>" +
                                                modificationDate.toISOString().split('T')[0] +
                                                "</td><td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedArticle(" +
                                                item.ArticleID +
                                                ",this)\"> " +
                                                "<div class=\"dropdown\">" +
                                                "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                                "<i class=\"icon-menu9\"></i></a>" +
                                                "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.moveArticleTempalte();\"><i class=\"icon-pencil\"></i> Move</a><a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.useArticleTempalte();\"><i class=\"icon-pencil\"></i> Use</a><a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadArticlePreview();\"><i class=\"icon-file-eye\"></i> Preview</a></div></div></td></tr>";

                                        });

                                        $("#tblBodyArticle").append(str);

                                    }
                                }
                            },
                            error: function (xhr, textStatus, errorThrown) {

                                ltcApp.errorMessage("Error", 'Error loading Articles');
                            },
                            complete: function () {
                                Layout.hideLoader();
                            }
                        });
                    }
                },

                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading template types');
                },
                complete: function () {

                }
            });
        },
        loadArticleTypes: function () {
            var str = '';
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetArticleTypes',
                success: function (data) {
                    if (data != null) {
                        articles_categories = data;
                        $("#ddlCategoryArticle").html('<option value="-1"> --Select Article Category--</option>');

                        $("#ddlCategoryArticle2").html('<option value="-1"> --Select Category--</option>');
                        $("#ddlCategoryArticle3").html('<option value="-1"> --Select Category--</option>');
                        $.each(articles_categories, function (index, item) {
                            str += "<tr><td><a href=\"#\" onclick=Newsletter.moveArticle('" + item.CategoryID + "');>" + item.CategoryName + "</a></td></tr>";
                            $("#ddlCategoryArticle").append('<option value="' + item.CategoryID + '">' + item.CategoryName + '</option>');

                            $("#ddlCategoryArticle2").append('<option value="' + item.CategoryID + '">' + item.CategoryName + '</option>');
                            $("#ddlCategoryArticle3").append('<option value="' + item.CategoryID + '">' + item.CategoryName + '</option>');
                        });
                    }
                    $('#tblBodyMoveArticle').html(str);
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading article categories');
                },
                complete: function () {
                }
            });
        },
        loadTemplateTypes: function () {
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetTemplateTypes',
                success: function (data) {
                    if (data != null) {
                        NewsLetter_TemplatesTypes = data;
                        $("#ddlTemplatesTypes").html('<option value="-1"> --Select Template Type--</option>');
                        $("#ddlTemplatesTypes1").html('<option value="-1"> --Select All--</option>');
                        $.each(NewsLetter_TemplatesTypes, function (index, value) {
                            var item = NewsLetter_TemplatesTypes[index];
                            $("#ddlTemplatesTypes").append('<option value="' + item.TypeID + '">' + item.TypeName + '</option>');
                            if (item.TypeID != 8) {
                                $("#ddlTemplatesTypes1").append('<option value="' + item.TypeID + '">' + item.TypeName + '</option>');
                            }


                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading template types');
                },
                complete: function () {
                }
            });
        },
        ddlTemplatesTypes1_OnChange: function () {
            this.loadAfterSelection();
        },
        loadAfterSelectionSecond: function () {

            var categoryArticle1 = $("#ddlCategoryArticle2").val();

            if (NewsLetter_UserDefinedTemplates != null) {
                $("#tblBodyMarketing").empty();
                if (NewsLetter_UserDefinedTemplates.length < 1) {
                    $("#tblBodyMarketing").html('<tr> <td colspan="3"> No record found! </td></tr>');
                }
                else {
                    var strMarketing = '';

                    var newArray = [];

                    if (categoryArticle1 != -1) {
                        $.each(NewsLetter_UserDefinedTemplates,
                            function (index, value) {

                                if (value.TypeID == 8 && (value.CategoryID == categoryArticle1)) {
                                    newArray.push(value);
                                }
                            });
                    } else {
                        newArray = NewsLetter_UserDefinedTemplates;
                    }
                    if (newArray.length < 1) {
                        $("#tblBodyMarketing").html('<tr> <td colspan="3"> No record found! </td></tr>');
                    }

                    $.each(newArray, function (index, item) {
                        var st = item.ModificationDate;
                        var modificationDate = new Date(st);

                        if (item.TypeID == 8) {

                            strMarketing += "<tr id=\"tr_" + item.LetterID + "\"><td><input type=\"checkbox\" id=\"" + item.LetterID + "\" value=\"" + item.LetterID + "\" onClick=\"Newsletter.ItemSelected(" + item.LetterID + ")\" class=\"form-input-styled\"></td><td>" +
                                item.TemplateTitle +
                                "</td >";
                            strMarketing += "<td>" +
                                modificationDate.toISOString().split('T')[0] +
                                "</td><td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedUserdefinedTemplate(" +
                                item.LetterID +
                                ",this)\"> " +
                                "<div class=\"dropdown\">" +
                                "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                "<i class=\"icon-menu9\"></i></a>" +
                                "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'send\');\"><i class=\"icon-envelop2\"></i> Send Newsletter</a>" +
                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'modify\');\"><i class=\"icon-pencil\"></i> Modify</a>" +
                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'remove\');\"><i class=\"icon-trash\"></i> Delete</a>" +
                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a>";

                            strMarketing += "</div></div></div></td></tr>";
                        }
                    });


                    $("#tblBodyMarketing").append(strMarketing);

                }
            }

        },
        loadAfterSelection: function () {
            var selectedType = $("#ddlTemplatesTypes1").val();

            if (NewsLetter_UserDefinedTemplates != null) {
                $("#tblBody").empty();
                if (NewsLetter_UserDefinedTemplates.length < 1) {
                    $("#tblBody").html('<tr> <td colspan="5"> No record found! </td></tr>');
                }
                else {
                    var strParadigm = '';

                    var newArray = [];


                    if (selectedType != -1) {
                        $.each(NewsLetter_UserDefinedTemplates,
                            function (index, value) {

                                if (value.TypeID != 8 && (value.TypeID == selectedType)) {
                                    newArray.push(value);
                                }
                            });
                    } else if (selectedType == -1) {
                        newArray = NewsLetter_UserDefinedTemplates;
                    }
                    if (newArray.length < 1) {
                        $("#tblBody").html('<tr> <td colspan="5"> No record found! </td></tr>');
                    }
                    $.each(newArray, function (index, item) {
                        var st = item.ModificationDate;
                        var modificationDate = new Date(st);

                        if (item.TypeID != 8) {

                            strParadigm += "<tr><td>" +
                                item.TemplateTitle +
                                "</td >" +
                                "<td>" +
                                item.TypeName +
                                "</td>" +
                                "<td>";
                            if (item.IsDefault) {
                                strParadigm += "<span class=\"badge badge-success\">Yes</span>";
                            } else {
                                strParadigm += "<span class=\"badge badge-secondary\">No</span>";
                            }
                            strParadigm += "</td><td>" +
                                modificationDate.toISOString().split('T')[0] +
                                "</td>" +
                                "<td  class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedUserdefinedTemplate(" +
                                item.LetterID +
                                ",this)\"> " +
                                "<div class=\"dropdown\">" +
                                "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                "<i class=\"icon-menu9\"></i></a>" +
                                "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'send\');\"><i class=\"icon-envelop2\"></i> Send Newsletter</a>" +
                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'modify\');\"><i class=\"icon-pencil\"></i> Modify</a>" +
                                "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a><div class=\"dropdown-divider\"></div>";
                            if (!item.IsParadigmNewsletter) {
                                strParadigm +=
                                    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'remove\');\"><i class=\"icon-trash\"></i> Delete</a>";
                            }
                            if (item.IsDefault) {
                                //strParadigm +=
                                //    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'removeDefault\');\"><i class=\"icon-minus3\"></i> Remove Default</a>";
                            } else {
                                strParadigm +=
                                    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'makeDefault\');\"><i class=\"icon-checkmark4\"></i> Make Default</a>;";
                            }
                            strParadigm += "</div></div></div></td></tr>";
                        }
                    });


                    $("#tblBody").append(strParadigm);

                }
            }

        },
        ddlTemplatesTypes_OnChange: function () {
            var selectedType = $("#ddlTemplatesTypes").val();
            if (selectedType == 8) {
                $('#dvCategory').show();
            } else {
                $('#dvCategory').hide();
            }
        },
        ddlArticleCategory_OnChange: function () {
            var selectedType = $("#ddlCategoryArticle").val();

            if (articles_categories != null) {
                $("#tblArticle").empty();
                if (articles_categories.length < 1) {
                    $("#tblArticle").html('<tr> <td colspan="3"> No record found! </td></tr>');
                }
                else {
                    var strParadigm = '';

                    var newArray = [];

                    if (selectedType != -1) {
                        $.each(articles,
                            function (index, value) {

                                if (value.CategoryID == selectedType) {
                                    newArray.push(value);
                                }
                            });
                    } else {
                        newArray = articles;
                    }
                    if (newArray.length < 1) {
                        $("#tblArticle").html('<tr> <td colspan="3"> No record found! </td></tr>');
                    }
                    var str = '';
                    $.each(newArray, function (index, value) {

                        item = newArray[index];
                        //console.log(item.ModificationDate);
                        var st = item.ModificationDate;
                        var modificationDate = new Date(st);
                        str += "<tr><td>" +
                            item.Title +
                            "</td ><td>" +
                            modificationDate.toISOString().split('T')[0] +
                            "</td><td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedArticle(" +
                            item.ArticleID +
                            ",this)\"> " +
                            "<div class=\"dropdown\">" +
                            "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                            "<i class=\"icon-menu9\"></i></a>" +
                            "<div class=\"dropdown-menu dropdown-menu-right\">" +
                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.moveArticleTempalte();\"><i class=\"icon-pencil\"></i> Move</a><a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.useArticleTempalte();\"><i class=\"icon-pencil\"></i> Use</a><a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadArticlePreview();\"><i class=\"icon-file-eye\"></i> Preview</a></div></div></td></tr>";

                    });


                    $("#tblArticle").append(str);

                }
            }
        },
        ddlArticleCategory1_OnChange: function () {
            this.loadAfterSelection();
        },

        ddlArticleCategory2_OnChange: function () {
            this.loadAfterSelectionSecond();
        },



        loadSystemTemplateDDL: function () {

            $.ajax({
                type: "GET",
                url: '/Newsletter/GetSystemTemplates',
                success: function (data) {
                    if (data != null) {

                        NewsLetter_SystemTemplates = data;
                        $("#ddlTemplatesTypes2").html('<option value="-1">Select a design</option>');
                        $.each(NewsLetter_SystemTemplates, function (index, value) {
                            var item = NewsLetter_SystemTemplates[index];
                            $("#ddlTemplatesTypes2").append('<option value="' + item.TemplateID + '">' + item.TemplateTitle + '</option>');
                        });



                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading system templates');

                },
                complete: function () {

                }
            });
        },

        loadSystemTemplates: function () {

            $.ajax({
                type: "GET",
                url: '/Newsletter/GetSystemTemplates',
                success: function (data) {
                    if (data != null) {
                        $("#tblBodySystem").html('');
                        NewsLetter_SystemTemplates = data;


                        if (NewsLetter_SystemTemplates.length < 1) {
                            $("#tblBodySystem").html('<tr> <td colspan="3"> No record found! </td></tr>');
                        } else {

                            var strSystem = '';
                            $.each(NewsLetter_SystemTemplates, function (index, item) {
                                var st = item.ModificationDate;
                                var modificationDate = new Date(st);
                                strSystem += "<tr><td>" +
                                    item.TemplateTitle +
                                    "</td >" +
                                    "<td style=\"display: none\">" +
                                    item.TypeName +
                                    "</td>";
                                strSystem += "<td>" +
                                    modificationDate.toISOString().split('T')[0] +
                                    "</td><td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedSystemTemplate(" +
                                    item.TemplateID +
                                    ",this)\"> " +
                                    "<div class=\"dropdown\">" +
                                    "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                    "<i class=\"icon-menu9\"></i></a>" +
                                    "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.useTempalte();\"><i class=\"icon-pencil\"></i> Use</a>" +
                                    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a>";

                            });

                            $("#tblBodySystem").append(strSystem);

                        }

                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading system templates');

                },
                complete: function () {

                }
            });
        },

        loadSelectedSystemTemplate: function (tempId, obj) {

            var imgs = $('.imageUserCard').length;
            for (var i = 0; i < imgs; i++) {
                $('.imageUserCard')[i].style.backgroundColor = 'transparent';
                //= '../Resources/Limitless/global_assets/images/placeholders/placeholder.jpg';
            }


            SelectedUserDefinedTemplateId = null;
            SelectedSystemDefinedTemplateId = tempId;
            var item = NewsLetter_SystemTemplates.find(x => x.TemplateID === tempId);
            if (item != null) {
                $("#previewContentEmpty").addClass('hide');
                $("#previewContent").removeClass('hide');
                this.setIframeHtml('previewContent', item.TemplateSourceMarkup);
            } else {
                $("#previewContentEmpty").removeClass('hide');
                $("#previewContent").addClass('hide');
                this.setIframeHtml('previewContent', '');
            }
        },
        loadSelectedArticle: function (tempId, obj) {
            //var imgs = $('.imageUserCard2').length;
            //for (var i = 0; i < imgs; i++) {
            //    $('.imageUserCard2')[i].style.backgroundColor = 'transparent';
            //}
            //obj.style.backgroundColor = 'darkseagreen';

            SelectedArticleId = tempId;



            //var item = articles.find(x => x.ArticleID === tempId);
            //if (item != null) {
            //    $("#previewArticleContent").removeClass('hide');
            //    this.setIframeHtml('previewArticleContent', item.Content);
            //} else {

            //}
        },
        loadPreview: function () {
            Newsletter.setIframeHtml('previewContent', '');
            var data = {
                LetterID: SelectedUserDefinedTemplateId
            };
            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Newsletter/GetUserDefinedTemplateDetail',
                success: function (data) {
                    Layout.hideLoader();
                    if (data != null) {
                        var item = data;

                        if (item != null) {
                            $("#previewContentEmpty").addClass('hide');
                            $("#previewContent").removeClass('hide');
                            if (item.TemplateSourceMarkup.length > 0) {
                                Newsletter.setIframeHtml('previewContent', item.MainBodymarkup);
                            } else {
                                Newsletter.setIframeHtml('previewContent', '');

                            }


                        } else {
                            $("#previewContentEmpty").removeClass('hide');
                            $("#previewContent").addClass('hide');
                            Newsletter.setIframeHtml('previewContent', '');
                        }


                    }
                },
                error: function (xhr, textStatus, errorThrown) {

                    ltcApp.errorMessage("Error", 'Error loading preview');

                },
                complete: function () {
                    $('#previewNewsletterModel').modal('show'); //show model
                }
            })


        },
        loadArticlePreview: function () {
            Newsletter.setIframeHtml('previewArticleContent', '');
            var data = {
                ArticleID: SelectedArticleId
            };
            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                url: '/Newsletter/GetArticleTemplateDetail',
                success: function (data) {
                    Layout.hideLoader();
                    if (data != null) {
                        var item = data;

                        if (item != null) {
                            $("#previewArticleContent").removeClass('hide');
                            Newsletter.setIframeHtml('previewArticleContent', item.Content);

                        } else {
                            $("#previewArticleContent").addClass('hide');
                            Newsletter.setIframeHtml('previewArticleContent', '');
                        }


                    }
                },
                error: function (xhr, textStatus, errorThrown) {

                    ltcApp.errorMessage("Error", 'Error loading preview');

                },
                complete: function () {
                    $('#previewArticleModel').modal('show'); //show model
                    Layout.hideLoader();
                }
            })

        },
        loadArticles: function () {
            Layout.showLoader();
            this.loadSystemTemplateDDL();
            var noTemp = '<div class="row" style="padding:10px;margin-left:25px;margin-right:25px">No Article Found! </div>';
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetArticles',
                success: function (data) {

                    if (data != null) {
                        $("#tblBodyArticle").html('');
                        articles = data;
                        if (articles.length < 1) {
                            $("#tblBodyArticle").html(noTemp);
                        } else {
                            var str = '';

                            $.each(articles, function (index, item) {

                                var st = item.ModificationDate;
                                var modificationDate = new Date(st);
                                str += "<tr><td>" +
                                    item.Title +
                                    "</td ><td>" +
                                    modificationDate.toISOString().split('T')[0] +
                                    "</td><td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedArticle(" +
                                    item.ArticleID +
                                    ",this)\"> " +
                                    "<div class=\"dropdown\">" +
                                    "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                    "<i class=\"icon-menu9\"></i></a>" +
                                    "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.moveArticleTempalte();\"><i class=\"icon-pencil\"></i> Move</a><a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.useArticleTempalte();\"><i class=\"icon-pencil\"></i> Use</a><a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadArticlePreview();\"><i class=\"icon-file-eye\"></i> Preview</a></div></div></td></tr>";

                            });

                            $("#tblBodyArticle").append(str);

                        }
                    }

                },
                error: function (xhr, textStatus, errorThrown) {

                    ltcApp.errorMessage("Error", 'Error loading Articles');
                },
                complete: function () {
                    Layout.hideLoader();
                }
            });
        },
        deleteSelectedNewsletters: function () {
            var post_arr = [];
            //Layout.showLoader();
            // Get checked checkboxes
            $('#marketingTemplateList input[type=checkbox]').each(function () {
                if (jQuery(this).is(":checked")) {
                    var id = this.id;


                    post_arr.push(id);

                }
            });

            if (post_arr.length > 0) {
                swal({
                    title: "Are you sure?",
                    text: "Do you want to permanently delete selected template(s)?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonClass: "btn-danger",
                    confirmButtonText: "Yes",
                    closeOnConfirm: true
                },

                    function () {
                        var input = {
                            SelectedIds: post_arr
                        };
                        $.ajax({
                            type: "POST",
                            data: JSON.stringify(input),
                            url: '/Newsletter/DeleteSelected',
                            contentType: 'application/json',
                            success: function (data) {
                                if (!data)
                                    ltcApp.errorMessage("Error", 'error removing template');
                            },
                            error: function (xhr, textStatus, errorThrown) {
                                ltcApp.errorMessage("Error", 'error removing template');
                            },
                            complete: function () {
                                $("#btnDeleteSelectOptions").attr("disabled", true);
                                ltcApp.successMessage(null, "Templates removed!");
                                if (currentView == 'user') {
                                    Newsletter.loadUserDefinedTemplates(true);
                                } else {
                                    Newsletter.loadUserDefinedTemplates(false);
                                }

                                $('#ddlTemplatesTypes1').prop('selectedIndex', 0);
                            }
                        });
                        $.each(post_arr, function (i, l) {
                            $("#tr_" + l).remove();

                        });
                        $("#btnDeleteSelectOptions").attr("disabled", true);


                    });

            }
        },

        loadUserDefinedTemplates: function (isParadigm) {
            Layout.showLoader();

            $('#ddlTemplatesTypes1').prop('selectedIndex', 0);
            var noTemp = '<tr> <td colspan="5"> No record found! </td></tr>';
            var data = {
                IsParadigm: isParadigm
            };

            $.ajax({
                type: "POST",
                data: JSON.stringify(data),
                url: '/Newsletter/GetUserDefinedTemplates',
                contentType: 'application/json',
                dataType: 'json',
                success: function (data) {
                    if (data != null) {
                        $("#tblBody").empty();
                        $("#tblBodyMarketing").empty();
                        NewsLetter_UserDefinedTemplates = data;

                        if (NewsLetter_UserDefinedTemplates.length < 1) {
                            $("#tblBody").html('<tr> <td colspan="5"> No record found! </td></tr>');
                            $("#tblBodyMarketing").html(noTemp);
                        }
                        else {
                            var strParadigm = '';
                            var strMarketing = '';



                            $.each(NewsLetter_UserDefinedTemplates, function (index, item) {


                                var st = item.ModificationDate;
                                var modificationDate = new Date(st);
                                if (item.TypeID != 8) {

                                    strParadigm += "<tr><td>" +
                                        item.TemplateTitle +
                                        "</td >" +
                                        "<td>" +
                                        item.TypeName +
                                        "</td>" +
                                        "<td>";
                                    if (item.IsDefault) {
                                        strParadigm += "<span class=\"badge badge-success\">Yes</span>";
                                    } else {
                                        strParadigm += "<span class=\"badge badge-secondary\">No</span>";
                                    }
                                    strParadigm += "</td><td>" +
                                        modificationDate.toISOString().split('T')[0] +
                                        "</td>" +
                                        "<td  class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedUserdefinedTemplate(" +
                                        item.LetterID +
                                        ",this)\"> " +
                                        "<div class=\"dropdown\">" +
                                        "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                        "<i class=\"icon-menu9\"></i></a>" +
                                        "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'send\');\"><i class=\"icon-envelop2\"></i> Send Newsletter</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'modify\');\"><i class=\"icon-pencil\"></i> Modify</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a><div class=\"dropdown-divider\"></div>";
                                    if (!item.IsParadigmNewsletter) {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'remove\');\"><i class=\"icon-trash\"></i> Delete</a>";
                                    }
                                    if (item.IsDefault) {
                                        //strParadigm +=
                                        //    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'removeDefault\');\"><i class=\"icon-minus3\"></i> Remove Default</a>";
                                    } else {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'makeDefault\');\"><i class=\"icon-checkmark4\"></i> Make Default</a>;";
                                    }
                                    strParadigm += "</div></div></div></td></tr>";
                                } else {

                                    strMarketing += "<tr id=\"tr_" + item.LetterID + "\"><td><input type=\"checkbox\" id=\"" + item.LetterID + "\" value=\"" + item.LetterID + "\" onClick=\"Newsletter.ItemSelected(" + item.LetterID + ")\" class=\"form-input-styled\"></td><td>" +
                                        item.TemplateTitle +
                                        "</td >";
                                    strMarketing += "<td>" +
                                        modificationDate.toISOString().split('T')[0] +
                                        "</td><td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedUserdefinedTemplate(" +
                                        item.LetterID +
                                        ",this)\"> " +
                                        "<div class=\"dropdown\">" +
                                        "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                        "<i class=\"icon-menu9\"></i></a>" +
                                        "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'send\');\"><i class=\"icon-envelop2\"></i> Send Newsletter</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'modify\');\"><i class=\"icon-pencil\"></i> Modify</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'remove\');\"><i class=\"icon-trash\"></i> Delete</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a>";

                                    strMarketing += "</div></div></div></td></tr>";
                                }
                            });

                            if (isParadigm) {
                                $("#tblBody").append(strParadigm);
                            } else {
                                $("#tblBodyMarketing").append(strMarketing);
                            }
                            if (IsLoadedAlready == false) {

                                if (isParadigm) {
                                    try {
                                        IsLoadedAlready = true;
                                        setTimeout(function () {

                                            $('#tblParadigm').DataTable({
                                                "order": [[3, "desc"]],
                                                "paging": false,
                                            });



                                        },
                                            3000);

                                    } catch (e) {

                                    }
                                } else {
                                    try {
                                        $('#tblMarketing').DataTable({
                                            "order": [[1, "desc"]],
                                            "paging": false,
                                        });
                                    } catch (e) {

                                    }


                                    setTimeout(function () {

                                        $('#tblMarketing').on('click', 'thead th', function (event) {
                                            var clickedHeader = $(this).closest('th').index();

                                            if (clickedHeader > -1) {
                                                $('#ddlCategoryArticle2').prop('selectedIndex', 0);
                                            }
                                        });

                                        if (isParadigm) {

                                            $('#tblParadigm').on('click', 'thead th', function (event) {

                                                var clickedHeader = $(this).closest('th').index();

                                                if (clickedHeader > -1) {
                                                    $('#ddlTemplatesTypes1').prop('selectedIndex', 0);

                                                }
                                            });
                                        }
                                    },
                                        3000);
                                }

                            }

                        }
                    }

                },
                error: function (xhr, textStatus, errorThrown) {

                    ltcApp.errorMessage("Error", 'Error loading user defined templates');
                },
                complete: function () {
                    Layout.hideLoader();
                    if (SelectedUserDefinedTemplateId != null)
                        Newsletter.loadSelectedUserdefinedTemplate(SelectedUserDefinedTemplateId, null);

                }
            });
        },
        Search: function () {
            $('#ddlTemplatesTypes1').prop('selectedIndex', 0);
            var searchText = $('#txtSearch').val().toLowerCase();
            var noTemp = '<tr> <td colspan="5"> No record found! </td></tr>';
            if (searchText == "") {
                if (currentView == 'user') {
                    Newsletter.loadUserDefinedTemplates(true);
                } else if (currentView == 'marketing') {
                    Newsletter.loadUserDefinedTemplates(false);
                } else if (currentView == 'article') {
                    this.loadArticles();
                } else if (currentView == 'system') {
                    this.loadSystemTemplates();
                }



            } else {
                if (NewsLetter_UserDefinedTemplates != null) {

                    var resFound1 = false;
                    var resFound2 = false;
                    $("#tblBody").html('');
                    $("#tblBodyMarketing").html('');
                    if (NewsLetter_UserDefinedTemplates.length < 1) {
                        $("#tblBody").html(noTemp);
                        $("#tblBodyMarketing").html('<tr> <td colspan="5"> No record found! </td></tr>');
                    } else {
                        var strParadigm = '';
                        var strMarketing = '';
                        $.each(NewsLetter_UserDefinedTemplates, function (index, item) {
                            var st = item.ModificationDate;
                            var modificationDate = new Date(st);

                            var ind = item.TemplateTitle.toLowerCase().indexOf(searchText);
                            if (ind != -1) {
                                if (item.TypeID != 8) {
                                    resFound1 = true;
                                    strParadigm += "<tr><td>" +
                                        item.TemplateTitle +
                                        "</td >" +
                                        "<td>" +
                                        item.TypeName +
                                        "</td>" +
                                        "<td>";

                                    if (item.IsDefault) {
                                        strParadigm += "<span class=\"badge badge-success\">Yes</span>";
                                    } else {
                                        strParadigm += "<span class=\"badge badge-secondary\">No</span>";
                                    }
                                    strParadigm += "</td><td>" +
                                        modificationDate.toISOString().split('T')[0] +
                                        "</td>" +
                                        "<td  class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedUserdefinedTemplate(" +
                                        item.LetterID +
                                        ",this)\"> " +
                                        "<div class=\"dropdown\">" +
                                        "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                        "<i class=\"icon-menu9\"></i></a>" +
                                        "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'send\');\"><i class=\"icon-envelop2\"></i> Send Newsletter</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'modify\');\"><i class=\"icon-pencil\"></i> Modify</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a><div class=\"dropdown-divider\"></div>";
                                    if (!item.IsParadigmNewsletter) {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'remove\');\"><i class=\"icon-trash\"></i> Delete</a>";
                                    } if (item.IsDefault) {
                                        //strParadigm +=
                                        //    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'removeDefault\');\"><i class=\"icon-minus3\"></i> Remove Default</a>";
                                    } else {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'makeDefault\');\"><i class=\"icon-checkmark4\"></i> Make Default</a>;";
                                    }
                                    strParadigm += "</div></div></div></td></tr>";
                                } else {
                                    resFound2 = true;
                                    strMarketing += "<tr id=\"tr_" + item.LetterID + "\"><td><input type=\"checkbox\" id=\"" + item.LetterID + "\" value=\"" + item.LetterID + "\" onClick=\"Newsletter.ItemSelected(" + item.LetterID + ")\" class=\"form-input-styled\"></td><td>" +
                                        item.TemplateTitle +
                                        "</td >";
                                    strMarketing += "<td>" +
                                        modificationDate.toISOString().split('T')[0] +
                                        "</td><td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedUserdefinedTemplate(" +
                                        item.LetterID +
                                        ",this)\"> " +
                                        "<div class=\"dropdown\">" +
                                        "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                        "<i class=\"icon-menu9\"></i></a>" +
                                        "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'send\');\"><i class=\"icon-envelop2\"></i> Send Newsletter</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'modify\');\"><i class=\"icon-pencil\"></i> Modify</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'remove\');\"><i class=\"icon-trash\"></i> Delete</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a>";

                                    strMarketing += "</div></div></div></td></tr>";
                                }
                            }
                        });


                        $("#tblBody").append(strParadigm);
                        $("#tblBodyMarketing").append(strMarketing);

                        if (!resFound2) {
                            $("#tblBodyMarketing").html('');
                            $("#tblBodyMarketing").html(noTemp);
                        }
                        if (!resFound1) {
                            $("#tblBody").html('');
                            $("#tblBody").html('<tr> <td colspan="5"> No record found! </td></tr>');
                        }
                    }
                }
                if (currentView == 'system') {
                    if (NewsLetter_SystemTemplates != null) {
                        var resultFound = false;
                        $("#tblBodySystem").html('');
                        if (NewsLetter_SystemTemplates.length < 1) {
                            $("#tblBodySystem").html('<tr> <td colspan="3"> No record found! </td></tr>');
                        } else {
                            var strSystem = '';

                            $.each(NewsLetter_SystemTemplates, function (index, item) {
                                var st = item.ModificationDate;
                                var modificationDate = new Date(st);
                                if (item.TemplateTitle.toLowerCase().includes(searchText)) {
                                    resultFound = true;
                                    strSystem += "<tr><td>" +
                                        item.TemplateTitle +
                                        "</td >" +
                                        "<td>" +
                                        modificationDate.toISOString().split('T')[0] +
                                        "</td>" +
                                        "<td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedSystemTemplate(" +
                                        item.TemplateID +
                                        ",this)\"> " +
                                        "<div class=\"dropdown\">" +
                                        "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                        "<i class=\"icon-menu9\"></i></a>" +
                                        "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.useTempalte();\"><i class=\"icon-pencil\"></i> Use</a>" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadPreview();\"><i class=\"icon-file-eye\"></i> Preview</a>";
                                }
                            });

                            if (!resultFound) {
                                $("#tblBodySystem").html('<tr> <td colspan="3"> No record found! </td></tr>');
                            }
                            $("#tblBodySystem").append(strSystem);
                            //$("#SystemTemplateList .image:first").click();
                        }

                    }
                }
                if (currentView == 'article') {
                    if (articles != null) {
                        var resultFound = false;
                        $("#tblBodyArticle").html('');
                        if (articles.length < 1) {
                            $("#tblBodyArticle").html('<tr> <td colspan="3"> No record found! </td></tr>');
                        } else {
                            var str = '';
                            $.each(articles, function (index, item) {
                                var st = item.ModificationDate;
                                var modificationDate = new Date(st);
                                if (item.Title.toLowerCase().includes(searchText) || item.Content.toLowerCase().includes(searchText)) {
                                    resultFound = true;
                                    str += "<tr><td>" +
                                        item.Title +
                                        "</td ><td>" +
                                        modificationDate.toISOString().split('T')[0] +
                                        "</td>" +
                                        "<td class=\"text-center\"><div class=\"list-icons\"  onclick=\"Newsletter.loadSelectedArticle(" +
                                        item.ArticleID +
                                        ",this)\"> " +
                                        "<div class=\"dropdown\">" +
                                        "<a href=\"#\" class=\"list-icons-item\" data-toggle=\"dropdown\">" +
                                        "<i class=\"icon-menu9\"></i></a>" +
                                        "<div class=\"dropdown-menu dropdown-menu-right\">" +
                                        "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadArticlePreview();\"><i class=\"icon-file-eye\"></i> Preview</a></div></div></td></tr>";
                                }
                            });

                            if (!resultFound) {
                                $("#tblBodyArticle").html('<tr> <td colspan="3"> No record found! </td></tr>');
                            }
                            $("#tblBodyArticle").append(str);
                        }

                    }

                }

            }

        },
        loadSelectedUserdefinedTemplate: function (tempId, obj) {



            SelectedUserDefinedTemplateId = tempId;
            SelectedSystemDefinedTemplateId = null;
         

        },

        setIframeHtml: function (iframeId, html) {
            var iframe = document.getElementById(iframeId);
            iframe = iframe.contentWindow || (iframe.contentDocument.document || iframe.contentDocument);

            iframe.document.open();
            iframe.document.write(html);
            iframe.document.close();
        },

        clearTabSelection: function (tab) {
            if (tab == 'system') {
                currentView = 'system';
                this.loadSystemTemplates();

                //if (NewsLetter_SystemTemplates == null) {
                //    //Layout.showLoader();

                //}
                $("#btnSelectedTemplate").show();
                $("#btnSelectOptions").hide();
                SelectedUserDefinedTemplateId = null;
                $("#SystemTemplateList .image:first").click();
            } else
                if (tab == 'user') {
                    currentView = 'user';

                    $("#btnSelectedTemplate").hide();
                    $("#btnSelectOptions").show();
                    $("#userDefineTemplateList .image:first").click();
                    $("#deleteTemplate").hide();
                    this.loadUserDefinedTemplates(true);
                    //Layout.showLoader();

                }
                else if (tab == 'marketing') {
                    currentView = 'marketing';
                    $("#deleteTemplate").show();
                    $("#btnSelectedTemplate").hide();
                    $("#btnSelectOptions").show();
                    $("#userDefineTemplateList .image:first").click();
                    this.loadUserDefinedTemplates(false);
                    //Layout.showLoader();

                }
                else if (tab == 'article') {
                    currentView = 'article';

                    if (articles == null) {
                        this.loadArticles();
                        //Layout.showLoader();

                    }
                    $("#deleteTemplate").hide();
                    $("#btnSelectedTemplate").hide();
                    $("#btnSelectOptions").hide();
                }
        },
        //send newsletter secion start

        loadServerTime: function () {

            $.ajax({
                type: "GET",
                url: '/Newsletter/LoadServerTime',
                success: function (data) {

                    if (data != null) {

                        $("#lblserverTime").html(data);
                        var res = new Date(data);
                        console.log(res);
                        $("#sendNewsletterDTP").data("kendoDateTimePicker").value(res);

                    }
                },
                error: function (xhr, textStatus, errorThrown) {

                    ltcApp.errorMessage("Error", 'Error loading server time');
                    var now = new Date();

                    $("#sendNewsletterDTP").data("kendoDateTimePicker").value(now);

                },
                complete: function () {
                    $("#sendNewsletterDTP").data("kendoDateTimePicker").enable(false);
                    Layout.hideLoader();
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
        uploadPicture: function () {
            $("#btnUploadImage").attr("disabled", true);
            var formData = new FormData();
            var totalFiles = document.getElementById("file").files.length;
            if (totalFiles > 0) {

                var file = document.getElementById("file").files[0];
                formData.append("file", file);

                $.ajax({
                    type: "POST",
                    url: '/ImageManagement/FileUpload',
                    data: formData,
                    dataType: 'json',
                    contentType: false,
                    processData: false,
                    success: function (data) {
                        if (data.Success) {
                            $('#btnUploadImage').removeAttr('disabled');
                            $("#modal-window").modal("hide");
                            //Newsletter.refreshList();
                            Newsletter.initActionPage(true);







                            setTimeout(function () {
                                Newsletter.initActionPage(true);
                            },
                                3000);
                            ltcApp.successMessage(null, "Uploaded successfully!");

                        }
                        else {
                            ltcApp.warningMessage(null, "Please try later.");
                        }
                    },
                    error: function (error) {
                        $("#modal-window").modal("hide");
                    }
                });

            }
        },
        sendNewsletter: function () {


            var sendToSubcribers = true;

            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId < 0) {
                ltcApp.warningMessage(null, "No template selected!");
                return;
            }
            var sendDate = new $("#sendNewsletterDTP").val();
            if ($("input[name='rbSchedule']:checked").val() == 'future') {
                sendDate = $("#sendNewsletterDTP").val();
            }


            if ($("input[name='rbSendAs']:checked").val() == 'singleemail') {
                sendToSubcribers = false;
                if ($("#txtSendNewsletterEmail").val().trim() == '' || !ltcApp.validateEmail($("#txtSendNewsletterEmail").val().trim())) {
                    ltcApp.warningMessage(null, "Invalid email address!");
                    return;
                }
            }

            $("#btnSend").attr("disabled", true);
            //    //Layout.showLoader();

            var data = {
                ScheduledDateTime: sendDate,
                SendToSubscribers: sendToSubcribers,
                Email: $("#txtSendNewsletterEmail").val().trim(),
                TemplateId: SelectedUserDefinedTemplateId,
                Offset: moment().format("Z")
            };

            $.ajax({
                url: '/Newsletter/SendNewsletterAsync',
                method: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                success: function (d) {

                    if (d) {
                        $('#sendNewsletterModel').modal('hide');

                        if ($("input[name='rbSchedule']:checked").val() == 'future') {
                            ltcApp.successMessage(null, "Newsletter scheduled successfully!");
                        } else {
                            ltcApp.successMessage(null, "Newsletter sent successfully!");
                        }

                        $('#btnSend').removeAttr('disabled');
                        Layout.hideLoader();

                    } else {
                        $('#sendNewsletterModel').modal('hide');
                        ltcApp.errorMessage(null, "Specified email does not exists.");
                        $('#btnSend').removeAttr('disabled');
                        Layout.hideLoader();

                        return;
                    }

                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#sendNewsletterModel').modal('hide');
                    ltcApp.warningMessage(null, "There is an error, please try later.");
                    $('#btnSend').removeAttr('disabled');
                    Layout.hideLoader();

                    return;


                }
            });
        },

        //Load editor treelist
        LoadNewsletterTree: function () {
            var data = [
                { text: 'Office Name', value: '&OfficeName&' },
                { text: 'Patient Name', value: '&PatientName&' },
                { text: 'Patient Email', value: '&PatientEmail&' },
                { text: 'Appoint Date', value: '&AppointDate&' },
                { text: 'Appoint Time', value: '&AppointTime&' },
                { text: 'Provider Name', value: '&ProviderName&' },
                { text: 'Date', value: '&Date&' },
                { text: 'Newsletter Title', value: '&subjecttext&' },
                { text: 'Office Email', value: '&OfficeEmail&' },
                { text: 'Office Street 1', value: '&OfficeStreet&' },
                { text: 'Office Street 2', value: '&OfficeStreet2&' },
                { text: 'Office Street 3', value: '&OfficeStreet3&' },
                { text: 'Province', value: '&OfficeProvince&' },
                { text: 'City', value: '&City&' },
                { text: 'Country', value: '&Country&' },
                { text: 'Postal Code', value: '&PostalCode&' },
                { text: 'Office Phone Number', value: '&OfficePhone&' },
                { text: 'Fax', value: '&Fax&' },
                { text: 'Office Web Address', value: '&OfficeWebAddress&' },
                { text: 'Confirmation Field', value: '&ClickHere&' },
                { text: 'Confirmation Button', value: '<a href="&ClickHere&"></a>' },
                { text: 'Pre-Confirmation Button', value: '<a href="&ClickHere&"></a>' }
                // <img src="&ImageSource&">
                // <img src="&ImageSource&">
            ];
            return data;
        },

        LoadEmailTree: function () {
            var data = [
                { text: 'Patient Name', value: '&PatientName&' },
                { text: 'Patient Salutation', value: '&PatientSalutation&' },
                { text: 'Patient FirstName', value: '&PatientFirstName&' },
                { text: 'Patient LastName', value: '&PatientLastName&' },
                { text: 'Patient Email', value: '&PatientEmail&' },
                { text: 'Family Member List', value: '&FamilyList&' },
                { text: 'Appoint Date', value: '&AppointDate&' },
                { text: 'Appoint Time', value: '&AppointTime&' },
                { text: 'Provider Name', value: '&ProviderName&' },
                { text: 'Job Description', value: '&JobDescription&' },
                { text: 'Office Name', value: '&OfficeName&' },
                { text: 'Office Email', value: '&OfficeEmail&' },
                { text: 'Address Block', value: '&AddressBlock&' },
                { text: 'Office Street1', value: '&OfficeStreet&' },
                { text: 'Office Street2', value: '&OfficeStreet2&' },
                { text: 'Office Street3', value: '&OfficeStreet3&' },
                { text: 'Province', value: '&OfficeProvince&' },
                { text: 'City', value: '&City&' },
                { text: 'Country', value: '&Country&' },
                { text: 'PostalCode', value: '&PostalCode&' },
                { text: 'Office Phone Number', value: '&OfficePhone&' },
                { text: 'Fax', value: '&Fax&' },
                //{ text: 'Confirmation Button', value: '&clickhere&' },
            ];
            return data;
        },


        saveNewsletterEditor: function (isSave) {
            //Newsletter.setIframeHtml('editorPreview', '');
            //$("#templateEditor").data("kendoEditor").value('');

            if ($("#txtTemplateTitle").val().trim() == '') {
                ltcApp.warningMessage(null, "Title cannot be empty.");
                return;
            }
            //Layout.showLoader();
            var isParadigm = false;
            var currTemplateTypeId = 0;
            var currCategoryTypeId = 1;



            if ($("#ddlTemplatesTypes option:selected").val() != '-1' &&
                $("#ddlTemplatesTypes option:selected").val() != undefined) {
                currTemplateTypeId = $("#ddlTemplatesTypes option:selected").val();
            } else {
                ltcApp.warningMessage(null, "Please select Template Type.");
                return;
            }

            if (currTemplateTypeId == 8) {
                $('#dvCategory').show();

                if ($("#ddlCategoryArticle3 option:selected").val() != '-1' &&
                    $("#ddlCategoryArticle3 option:selected").val() != undefined) {
                    currCategoryTypeId = $("#ddlCategoryArticle3 option:selected").val();
                } else {
                    ltcApp.warningMessage(null, "Please select category.");
                    return;
                }
            } else {
                $('#dvCategory').hide();
                currCategoryTypeId = 1;
            }

            var currTemplateId = 0;
            var htmlMain = '';

            $("#html-content-holder").html("<!DOCTYPE html><html lang='en'><head><meta charset='utf-8'><meta http-equiv='X-UA-Compatible' content='IE=edge'></head><body>" + $("#templateEditor").data("kendoEditor").value() + "</body></html>");
            var element = $("#html-content-holder"); // global variable
            document.getElementById("dvManage").style.position = "";
            document.getElementById("dvManage").style.top = "0px";
            document.getElementById("dvManage").style.left = "0px;";

            html2canvas(element, {
                useCORS: true,
                imageTimeout: 15000,
                onrendered: function (canvas) {
                    $("#previewImage").append(canvas);

                    getCanvas = canvas;
                    var imgageData = getCanvas.toDataURL("image/png");
                    htmlMain = imgageData;//.replace(/^data[:&image\/(png|jpg|jpeg)[;&/i, "data:application/octet-stream;");



                    if (!isSave) {

                        //var articleWithSameName = NewsLetter_UserDefinedTemplates.find(x => x.TemplateTitle == $("#txtTemplateTitle").val() && x.LetterID != SelectedUserDefinedTemplateId);
                        //if (articleWithSameName != null) {
                        //    ltcApp.warningMessage(null, "Newsletter with same name already exists.");
                        //    return;
                        //}
                        var item = NewsLetter_UserDefinedTemplates.find(x => x.LetterID === SelectedUserDefinedTemplateId);
                        isParadigm = item.IsParadigmNewsletter;
                        currTemplateId = SelectedUserDefinedTemplateId;
                        var data = {
                            TemplateTitle: $("#txtTemplateTitle").val(),
                            LetterID: currTemplateId,
                            TypeID: currTemplateTypeId,
                            TemplateSourceMarkup: item.TemplateSourceMarkup,
                            MainBodymarkup: $("#templateEditor").data("kendoEditor").value(),
                            IsParadigmNewsletter: isParadigm,
                            IsDefault: item.IsDefault,
                            ContentImageString: htmlMain,
                            CategoryID: currCategoryTypeId
                        };

                    } else {

                        //var articleWithSameName = NewsLetter_UserDefinedTemplates.find(x => x.TemplateTitle == $("#txtTemplateTitle").val());
                        //if (articleWithSameName != null) {
                        //    ltcApp.warningMessage(null, "Newsletter with same name already exists.");
                        //    return;
                        //}

                        var data = {
                            TemplateTitle: $("#txtTemplateTitle").val(),
                            LetterID: currTemplateId,
                            TypeID: currTemplateTypeId,
                            TemplateSourceMarkup: '',
                            MainBodymarkup: $("#templateEditor").data("kendoEditor").value(),
                            IsParadigmNewsletter: isParadigm,
                            IsDefault: false,
                            ContentImageString: htmlMain,
                            CategoryID: currCategoryTypeId

                        };
                    }

                    $("#btnSave").attr("disabled", true);

                    $.ajax({
                        url: '/Newsletter/SaveNewsletterEditor',
                        method: 'POST',
                        data: JSON.stringify(data),
                        contentType: 'application/json',
                        dataType: 'json',
                        success: function (d) {

                            if (d) {
                                if (currentView == 'user') {
                                    Newsletter.loadUserDefinedTemplates(true);
                                } else if (currentView == 'marketing') {
                                    Newsletter.loadUserDefinedTemplates(false);
                                }

                                $("#ddlCategoryArticle2").prop('selectedIndex', 0);

                                $('#templateEditorWindow').modal('hide');

                                $('#ddlTemplatesTypes1').prop('selectedIndex', 0);
                                // Newsletter.loadTemplateTypes(); //reload selected template types and repopulate the dropdown
                                ltcApp.successMessage(null, "Template Saved!");



                            } else {
                                ltcApp.errorMessage("Error", "Unable to save. Please try again later");
                                $('#templateEditorWindow').modal('hide');

                            }
                        },
                        error: function (xhr, textStatus, errorThrown) {
                            ltcApp.successMessage(null, "This action cannot be done");
                            $('#templateEditorWindow').modal('hide');
                        },
                        complete: function () {
                            somethingChanged = false;
                            $('#btnSave').removeAttr("disabled");
                            Layout.hideLoader();
                            document.getElementById("dvManage").style.position = "absolute";
                            document.getElementById("dvManage").style.top = "-9999px";
                            document.getElementById("dvManage").style.left = "-9999px;";
                            $("#txtTemplateTitle").val();
                            currTemplateId = '';
                            $("#templateEditor").data("kendoEditor").value('');

                        }
                    });
                    //$("#btn-Convert-Html2Image").attr("download", "your_pic_name.png").attr("href", newData);
                }
            });





        },

        resetNewsletterEditor: function () {
            this.loadTemplateDetailInEditor(true);
        },

        previewNewsletterEditor: function () {

            var currentContent = $("#templateEditor").data("kendoEditor").value();
            //if (SelectedUserDefinedTemplateId != null) {
            //    this.setIframeHtml('editorPreview', currentContent);
            //    $('#previewNewsletterModel1').modal('show');
            //}
            this.setIframeHtml('editorPreview', currentContent);
            $('#previewNewsletterModel1').modal('show');

        },

        cancelNewsletterEditor: function () {

            if (somethingChanged) {
                ltcApp.promptConfirmation("Warning", "Are you sure want to close without saving?");
            } else {
                $('#templateEditorWindow').modal('hide');
                Newsletter.setIframeHtml('editorPreview', '');
                $("#templateEditor").data("kendoEditor").value('');
                $("#txtTemplateTitle").val();
                currTemplateId = '';
            }

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
                            $("#ddlIndustries").append('<option value="' + item.Id + '">' + item.Title + '</option>');
                        });
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading industries');
                },
                complete: function () {
                    Layout.hideLoader();
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
                if (item.IndustryTypeId == currIndustry)
                    $("#ddlSubIndustries").append('<option value="' + item.Id + '">' + item.SubTypeTitle + '</option>');
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
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading sub industries');
                },
                complete: function () {
                }
            });
        },

        IframeSizing: function () {

        },

        GetUserImages: function () {
            $.ajax({
                type: "GET",
                url: '/ImageManagement/GetUserImages',
                dataType: 'json',
                async: false,
                cache: false,
                success: function (d) {
                    data = JSON.parse(d);
                    return data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });

            //return data;
        },

        GetOfficeImageTest: function () {
            var data = [];
            $.ajax({
                async: false,
                cache: false,
                url: "/ImageManagement/GetUserImagesLink",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json',
                success: function (d) {
                    d = JSON.parse(d);
                    $(d).each(function (i, val) {
                        data.push({ text: val.name, value: "<img src='" + window.location.origin + "/" + val.path + "'>" });
                    });
                    return data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });

            //return data;
        },
        GetOfficeImages: function () {
            var data = [];
            $.ajax({
                async: false,
                cache: false,
                url: "/ImageManagement/GetUserImagesLink",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json',
                success: function (d) {
                    d = JSON.parse(d);
                    $(d).each(function (i, val) {
                        data.push({ text: val.name, value: "<img src='" + window.location.origin + "/" + val.path + "'>" });
                    });
                    return data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });

            //return data;
        },
        GetOfficeImagesOld: function () {
            var data = [];
            $.ajax({
                async: false,
                cache: false,
                url: "/ImageManagement/GetUserImages",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json',
                success: function (d) {
                    d = JSON.parse(d);
                    $(d).each(function (i, val) {
                        data.push({ text: val.name, value: "<img src='" + window.location.origin + "/" + val.path + "'>" });
                    });
                    return data;
                },
                error: function (data) {
                    console.log(data.responseText);
                }
            });

            //return data;
        }
    };
}();
$(document).ready(function () {

    Newsletter.init();
    Newsletter.initActionPage(false);
    $(document).off("focusin");
    $(document).off("focus");

});



var enumNewsletterUserDefinedOptions = {
    "send": "send", "assign": "assign", "create": "create", "modify": "modify", "remove": "remove", "makeDefault": "makeDefault", "removeDefault": "removeDefault"
};

$(window).resize(function () {
    Newsletter.IframeSizing();
});

//global variables
var somethingChanged = false;



var NewsLetter_TemplatesTypes;
var NewsLetter_SystemTemplates;
var NewsLetter_ShellTemplates;
var NewsLetter_UserDefinedTemplates;
var NewsLetter_Industries;
var articles;

var articles_categories;
var NewsLetter_SubIndustries;
var SelectedUserDefinedTemplateId;
var SelectedSystemDefinedTemplateId;
var SelectedArticleId;

var IsLoadedAlready = false;
var isKendoWindowLoaded = false;
var getCanvas; // global variable
var currentView = 'user';


