
var Newsletter = function () {
    return {
        
        init: function () {
               
         
            Layout.showLoader(); 
            this.loadUserDefinedTemplates();
            this.loadSystemTemplates();
            this.loadTemplateTypes();
            this.loadArticles();

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
                function() {
                    Newsletter.IframeSizing();
                });
        },

        initKendoWindow: function () {
         
        },

        useTempalte: function () {

            if (SelectedSystemDefinedTemplateId == null || SelectedSystemDefinedTemplateId <= 0) {
                ltcApp.warningMessage(null, "No template Selected");
                return;
            }

            swal({
                title: "Copy Template!",
                text: "Enter the name of new template (newly created template will show under user defined tab):",
                type: "input",
                showCancelButton: true,
                closeOnConfirm: true,
                inputPlaceholder: "Template Name"
            }, function (name) {
                if (name === false) return false;
                if (name === "") {
                    swal.showInputError("You need to write something!");
                    return false;
                }

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
                    }
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

            setTimeout(function() {
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

            // move to default selection of dropdown
            $('#sendNewsletterModel').modal('show'); //show model

            //clear previous values and enable/dislabe fields
            $("#lblserverTime").html('');
            $("#rbNew").click();

            var now = new Date();
            var utc = new Date(now.getTime() + now.getTimezoneOffset() * 60000);

            $("#sendNewsletterDTP").data("kendoDateTimePicker").value(utc);
            $("#sendNewsletterDTP").data("kendoDateTimePicker").enable(false);

            $("#rbSubscribers").click();
            $("#txtSendNewsletterEmail").val('');
            $("#txtSendNewsletterEmail").hide();

            this.loadServerTime();//load server time
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
              
                $("#templateEditor").data("kendoEditor").value(item.TemplateSourceMarkup);

                
                var currTemplateType = NewsLetter_TemplatesTypes.find(x => x.TypeID == item.TypeID);
                if (currTemplateType != null && currTemplateType != undefined) {
                    $("#ddlTemplatesTypes").val(currTemplateType.TypeID);
                }
                else {
                    $("#ddlTemplatesTypes").val('-1');
                }
                
                $("#txtTemplateTitle").val(item.TemplateTitle);
              
               


                this.setIframeHtml('editorPreview', item.TemplateSourceMarkup);

            
            $(document).off("focusin");
            $(document).off("focus");
        },
        loadTemplateDetailInEditor: function (isModified) {
           
            if (isModified) {
              
                $("#btnModify").show();
                $("#btnSave").hide();
               
                var item = NewsLetter_UserDefinedTemplates.find(x => x.LetterID === SelectedUserDefinedTemplateId);
                
                
                $("#templateEditor").data("kendoEditor").value(item.MainBodymarkup);

                
                var currTemplateType = NewsLetter_TemplatesTypes.find(x => x.TypeID == item.TypeID);
                if (currTemplateType != null && currTemplateType != undefined) {
                    $("#ddlTemplatesTypes").val(currTemplateType.TypeID);
                }
                else {
                    $("#ddlTemplatesTypes").val('-1');
                }
                
                $("#txtTemplateTitle").val(item.TemplateTitle);
              
                


                this.setIframeHtml('editorPreview', item.MainBodymarkup);

            }
            else {
                $("#btnModify").hide();
                $("#btnSave").show();

                $("#templateEditor").data("kendoEditor").value('');
                this.setIframeHtml('editorPreview', '');


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

                            setTimeout(function() {
                                    Newsletter.initActionPage(true);
                                },
                                3000);

                            $("#modal-window").modal("hide");
                            ltcApp.successMessage(null, "Image removed!");
                            $("#ImageUploadButton").click();

                           

                        }
                    });

                    //prompt to enter name for the copy
                });


        },
        refreshList: function() {
           
          
                
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

                        ltcApp.successMessage(null, "Template removed!");

                        Newsletter.loadUserDefinedTemplates();
                        Newsletter.loadTemplateTypes(); //reload selected template types and repopulate the dropdown
                    }
                });

       
            });


        },
        MakeDefaultTemplate: function (isDefaultCheck) {
            var item = NewsLetter_UserDefinedTemplates.find(x => x.LetterID === SelectedUserDefinedTemplateId);
            if (item.IsDefault) {
                ltcApp.errorMessage("Warning!", "Please other template as Default");
                return;
            }
            Layout.showLoader();

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
                        Newsletter.loadUserDefinedTemplates();

                    } else {
                        ltcApp.errorMessage("Warning!", "Please other template as Default");
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                },
                complete: function () {
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
                
              var dt =   $("#OfficeImageTreeview").kendoTreeView({
                   
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
                            dataType: 'html'
                        }).success(function (data) {
                            $("#modal-window .modal-title").text("Image Management");
                            $("#modal-window .modal-body").html(data);
                            $("#modal-window .modal-body #UserImageTreeview").kendoTreeView({
                                template : kendo.template($("#treeview-template").html()),
                                dataSource: {
                                    data: [
                                        {
                                            text: "Images", value: null, expanded: false, 
                                            items: Newsletter.GetOfficeImages(),
                                           
   
                                        }
                                    ],
                                    select: function(e) {
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
                            }).error(function (data) {
                                alert(data.responseText);
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
            
            var selectedType = $("#ddlTemplatesTypes1").val();
            
                 if (NewsLetter_UserDefinedTemplates != null) {
                        $("#tblBody").empty();
                        if (NewsLetter_UserDefinedTemplates.length < 1) {
                            $("#userDefineTemplateList").html('<tr> <td colspan="3"> No record found! </td></tr>');
                        } 
                        else {
                            var strParadigm = '';
                            
                            var newArray = [];
                             
                            if (selectedType != -1) {
                                $.each(NewsLetter_UserDefinedTemplates,
                                    function(index, value) {
                                       
                                        if (value.TypeID != 8 && value.TypeID == selectedType) {
                                            newArray.push(value);
                                        }
                                    });
                            } else {
                                newArray = NewsLetter_UserDefinedTemplates;
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
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'removeDefault\');\"><i class=\"icon-minus3\"></i> Remove Default</a>";
                                    } else {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'makeDefault\');\"><i class=\"icon-checkmark4\"></i> Make Default</a>;";
                                    }
                                        strParadigm +="</div></div></div></td></tr>";
                                }  
                            });
                            
                            
                             $("#tblBody").append(strParadigm);
                             
                        }
                    }
             
        },

        ddlTemplatesTypes_OnChange: function () {

        },
    
     
 
      

        loadSystemTemplates: function () {
         
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetSystemTemplates',
                success: function (data) {
                    if (data != null) {
                        $("#tblBodySystem").html('');
                        NewsLetter_SystemTemplates = data;
                        
                        if (NewsLetter_SystemTemplates.length < 1  ) {
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
                //= '../Content/Limitless/global_assets/images/placeholders/placeholder.jpg';
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
            var imgs = $('.imageUserCard2').length;
            for (var i = 0; i < imgs; i++) {
                $('.imageUserCard2')[i].style.backgroundColor = 'transparent';
            }
            //obj.style.backgroundColor = 'darkseagreen';

            SelectedArticleId = tempId;
            var item = articles.find(x => x.ArticleID === tempId);
            if (item != null) {
                $("#previewArticleContent").removeClass('hide');
                this.setIframeHtml('previewArticleContent', item.Content);
            } else {
                $("#previewArticleContent").addClass('hide');
                this.setIframeHtml('previewArticleContent', '');
            }
        },
        loadPreview: function () {
          
            $('#previewNewsletterModel').modal('show'); //show model
        },
        loadArticlePreview: function () {
          
            $('#previewArticleModel').modal('show'); //show model
        },
        loadArticles: function () {
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
                                    "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.loadArticlePreview();\"><i class=\"icon-file-eye\"></i> Preview</a></div></div></td></tr>";
                                   
                            });                                 

                            $("#tblBodyArticle").append(str);

                        }
                    }
                     
                },
                error: function (xhr, textStatus, errorThrown) {
                    
                    ltcApp.errorMessage("Error", 'Error loading Articles');
                },
                complete: function () {
 

                }
            });
        },

        loadUserDefinedTemplates: function (selectedTypeId) {
            var noTemp = '<tr> <td colspan="3"> No record found! </td></tr>';
            $.ajax({
                type: "GET",
                url: '/Newsletter/GetUserDefinedTemplates',
                success: function (data) {
                    if (data != null) {
                        $("#tblBody").empty();
                        $("#tblBodyMarketing").empty();
                        NewsLetter_UserDefinedTemplates = data;
                       
                        if (NewsLetter_UserDefinedTemplates.length < 1) {
                            $("#tblBody").html('<tr> <td colspan="3"> No record found! </td></tr>');
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
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'removeDefault\');\"><i class=\"icon-minus3\"></i> Remove Default</a>";
                                    } else {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'makeDefault\');\"><i class=\"icon-checkmark4\"></i> Make Default</a>;";
                                    }
                                        strParadigm +="</div></div></div></td></tr>";
                                } else {

                                    strMarketing += "<tr><td>" +
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
                                   
                                    strMarketing +="</div></div></div></td></tr>";
                                }
                            });
                             
                            
                             $("#tblBody").append(strParadigm);
                             $("#tblBodyMarketing").append(strMarketing);
                            
                        }
                    }
                     
                },
                error: function (xhr, textStatus, errorThrown) {
                    
                    ltcApp.errorMessage("Error", 'Error loading user defined templates');
                },
                complete: function () {
                    if (SelectedUserDefinedTemplateId != null)
                        Newsletter.loadSelectedUserdefinedTemplate(SelectedUserDefinedTemplateId, null);
                   


                }
            });
        },
        Search: function () {
             
            var searchText = $('#txtSearch').val().toLowerCase();
            var noTemp = '<tr> <td colspan="3"> No record found! </td></tr>';
            if (searchText == "") {
                this.loadUserDefinedTemplates();
                this.loadSystemTemplates();
                this.loadArticles();
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
                                    }if (item.IsDefault) {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'removeDefault\');\"><i class=\"icon-minus3\"></i> Remove Default</a>";
                                    } else {
                                        strParadigm +=
                                            "<a href=\"#\" class=\"dropdown-item\"  onclick=\"Newsletter.userDefinedOptionChanged(\'makeDefault\');\"><i class=\"icon-checkmark4\"></i> Make Default</a>;";
                                    }
                                        strParadigm +="</div></div></div></td></tr>";
                                } else {
                                    resFound2 = true;
                                    strMarketing += "<tr><td>" +
                                        item.TemplateTitle +
                                        "</td >" ;
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
                                   
                                    strMarketing +="</div></div></div></td></tr>";
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
                            if (item.Title.toLowerCase().includes(searchText) || item.Content.toLowerCase().includes(searchText) ) {
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

        },
        loadSelectedUserdefinedTemplate: function (tempId, obj) {
            
            var imgs = $('.imageCard').length;
            for (var i = 0; i < imgs; i++) {
                $('.imageCard')[i].style.backgroundColor = 'transparent';
                //= '../Content/Limitless/global_assets/images/placeholders/placeholder.jpg';
            }
           
            SelectedUserDefinedTemplateId = tempId;
            SelectedSystemDefinedTemplateId = null;
            var item = NewsLetter_UserDefinedTemplates.find(x => x.LetterID === tempId);
          
            if (item != null) {
                $("#previewContentEmpty").addClass('hide');
                $("#previewContent").removeClass('hide');
                this.setIframeHtml('previewContent', item.MainBodymarkup);
          
               
            } else {
                $("#previewContentEmpty").removeClass('hide');
                $("#previewContent").addClass('hide');
                this.setIframeHtml('previewContent', '');
            }

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
                $("#btnSelectedTemplate").show();
                $("#btnSelectOptions").hide();
                SelectedUserDefinedTemplateId = null;
                $("#SystemTemplateList .image:first").click();
            }else 
            if (tab == 'user') {
                $("#btnSelectedTemplate").hide();
                $("#btnSelectOptions").show();
                $("#userDefineTemplateList .image:first").click();
                $("#deleteTemplate").hide();
            } 
            else if (tab == 'marketing') {
                $("#deleteTemplate").show();
                $("#btnSelectedTemplate").hide();
                $("#btnSelectOptions").show();
                $("#userDefineTemplateList .image:first").click();
            }
            else if (tab == 'article') {
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
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.errorMessage("Error", 'Error loading server time');
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
        uploadPicture: function () {
           
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
                            //Newsletter.refreshList();
                            Newsletter.initActionPage(true);
                            setTimeout(function() {
                                    Newsletter.initActionPage(true);
                                },
                                3000);
                            ltcApp.successMessage(null, "Uploaded successfully!");
                            $("#modal-window").modal("hide");
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
            Layout.showLoader();
           
            var sendToSubcribers = true;

            if (SelectedUserDefinedTemplateId == null || SelectedUserDefinedTemplateId < 0) {
                ltcApp.warningMessage(null, "No template selected!");
                return;
            }
            var sendDate = new Date();
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
           


            var data = {
                ScheduledDateTime: sendDate,
                SendToSubscribers: sendToSubcribers,
                Email: $("#txtSendNewsletterEmail").val().trim(),
                TemplateId: SelectedUserDefinedTemplateId
            };

            $.ajax({
                url: '/Newsletter/SendNewsletterAsync',
                method: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                success: function (d) {
                    $('#sendNewsletterModel').modal('hide');

                    if ($("input[name='rbSchedule']:checked").val() == 'future') {
                        ltcApp.successMessage(null, "Newsletter scheduled successfully!");
                    } else {
                        ltcApp.successMessage(null, "Newsletter sent successfully!");
                    }


                },
                error: function (xhr, textStatus, errorThrown) {
                    $('#sendNewsletterModel').modal('hide');
                    ltcApp.warningMessage(null, "There is an error, please try later.");
                    return;


                }
            });
        },

        //Load editor treelist
        LoadNewsletterTree: function () {
            var data = [
                { text: 'Office Name', value: '[OfficeName]' },
                { text: 'Patient Name', value: '[PatientName]' },
                { text: 'Patient Email', value: '[PatientEmail]' },
                { text: 'Appoint Date', value: '[AppointDate]' },
                { text: 'Appoint Time', value: '[AppointTime]' },
                { text: 'Provider Name', value: '[ProviderName]' },
                { text: 'Date', value: '[Date]' },
                { text: 'Newsletter Title', value: '[subjecttext]' },
                { text: 'Office Email', value: '[OfficeEmail]' },
                { text: 'Office Street 1', value: '[OfficeStreet]' },
                { text: 'Office Street 2', value: '[OfficeStreet2]' },
                { text: 'Office Street 3', value: '[OfficeStreet3]' },
                { text: 'Province', value: '[OfficeProvince]' },
                { text: 'City', value: '[City]' },
                { text: 'Country', value: '[Country]' },
                { text: 'Postal Code', value: '[PostalCode]' },
                { text: 'Office Phone Number', value: '[OfficePhone]' },
                { text: 'Fax', value: '[Fax]' },
                { text: 'Confirmation Field', value: '[ClickHere]' },
                { text: 'Confirmation Button', value: '<a href="[ClickHere]"><img src="[ImageSource]"></a>' }
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
	                //{ text: 'Confirmation Button', value: '[clickhere]' },
            ];
            return data;
        }, 
      

        saveNewsletterEditor: function (isSave) {
          
            if ($("#txtTemplateTitle").val().trim() == '') {
                ltcApp.warningMessage(null, "Title cannot be empty.");
                return;
            }
            Layout.showLoader();
            var isParadigm = false;
            var currTemplateTypeId = 0;

            // not mandatorys
            if ($("#ddlTemplatesTypes option:selected").val() != '-1' &&
                $("#ddlTemplatesTypes option:selected").val() != undefined) {
                currTemplateTypeId = $("#ddlTemplatesTypes option:selected").val();
            } else {
                ltcApp.warningMessage(null, "Please select Template Type.");
            }

            var currTemplateId = 0;
           
            if (!isSave) {
                
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
                    IsDefault: item.IsDefault
            };

            } else {
                var data = {
                    TemplateTitle: $("#txtTemplateTitle").val(),
                    LetterID: currTemplateId,
                    TypeID: currTemplateTypeId,
                    TemplateSourceMarkup: '',
                    MainBodymarkup: $("#templateEditor").data("kendoEditor").value(),
                    IsParadigmNewsletter: isParadigm,
                    IsDefault: false
                };
            }

         
            $.ajax({
                url: '/Newsletter/SaveNewsletterEditor',
                method: 'POST',
                data: JSON.stringify(data),
                contentType: 'application/json',
                dataType: 'json',
                success: function (d) {
              
                    if (d) {
                        Newsletter.loadUserDefinedTemplates();
                        Newsletter.loadTemplateTypes(); //reload selected template types and repopulate the dropdown
                        ltcApp.successMessage(null, "Template Saved!");
                        $('#templateEditorWindow').modal('hide');
                    } else {
                        ltcApp.errorMessage("Error", "Move Action cannot be done");

                        $('#templateEditorWindow').modal('hide');
                            
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    ltcApp.successMessage(null, "This action cannot be done");
                    $('#templateEditorWindow').modal('hide');
                },
                complete: function () {
                    somethingChanged = false;
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
            var data;
            $.ajax({
                async: false,
                cache: false,
                url: "/ImageManagement/GetUserImages",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json'
            }).success(function (d) {
                data = JSON.parse(d);
            }).error(function (data) {
                console.log(data.responseText);
            });

            return data;
        },

        GetOfficeImageTest: function () {
            var data = [];
            $.ajax({
                async: false,
                cache: false,
                url: "/ImageManagement/GetUserImagesLink",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json'
            }).success(function (d) {
                d = JSON.parse(d);
                $(d).each(function (i, val) {
                    data.push({ text: val.name, value: "<img src='" +window.location.origin+"/"+ val.path + "'>" });
                });
            }).error(function (data) {
                console.log(data.responseText);
            });
           
           


           

            return data;
        },
        GetOfficeImages: function () {
            var data = [];
            $.ajax({
                async: false,
                cache: false,
                url: "/ImageManagement/GetUserImagesLink",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json'
            }).success(function (d) {
                d = JSON.parse(d);
                $(d).each(function (i, val) {
                    data.push({ text: val.name, value: "<img src='" +window.location.origin+"/"+ val.path + "'>" });
                });
            }).error(function (data) {
                console.log(data.responseText);
            });
           
            return data;
        },
        GetOfficeImagesOld: function () {
            var data = [];
            $.ajax({
                async: false,
                cache: false,
                url: "/ImageManagement/GetUserImages",
                contentType: 'application/json; charset=utf-8',
                type: 'GET',
                dataType: 'json'
            }).success(function (d) {
                d = JSON.parse(d);
                $(d).each(function (i, val) {
                    data.push({ text: val.name, value: "<img src='" +window.location.origin+"/"+ val.path + "'>" });
                });
            }).error(function (data) {
                console.log(data.responseText);
            });

            return data;
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
    "send": "send", "assign": "assign", "create": "create", "modify": "modify", "remove": "remove", "makeDefault" : "makeDefault","removeDefault" : "removeDefault" 
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
var NewsLetter_SubIndustries;
var SelectedUserDefinedTemplateId;
var SelectedSystemDefinedTemplateId;
var SelectedArticleId;
var isKendoWindowLoaded = false;
 