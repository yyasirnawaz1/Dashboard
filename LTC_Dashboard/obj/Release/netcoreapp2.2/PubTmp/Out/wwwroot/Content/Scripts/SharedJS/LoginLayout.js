var Layout = {

    currentModel: '',
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
        iframeDoc.write('<link rel="stylesheet" href="/Content/theme/css/styles.css"><script type="text/javascript" src="/Content/theme/js/vendor/jquery/jquery.min.js"></script><script type="text/javascript" src="/Content/theme/js/vendor/jquery/jquery-ui.min.js"></script><script type="text/javascript" src="/Content/theme/js/vendor/bootstrap/bootstrap.min.js"></script><script src="/Content/FormDesigner/assets/js/form-render.min.js"></script><link href="/Content/FormDesigner/assets/css/RateYo/RateYo.css" rel="stylesheet" /><script src="/Content/FormDesigner/assets/js/Rateyo/jquery.rateyo.min.js"></script>');
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
};

$(document).ready(function () {

    $('#thePreviewPanel').on('hidden.bs.modal', function (e) {
        if (Layout.currentModel != '') {
            $("#" + Layout.currentModel).modal('show');
        }
    });

    $('#thePreviewPanel').on('shown.bs.modal', function (e) {
     
        if ($("#PublicSurveyloginModal").data('bs.modal') && $("#PublicSurveyloginModal").data('bs.modal').isShown) {
            Layout.currentModel = 'PublicSurveyloginModal';
            $("#thePreviewPanel > div").removeClass("modal-lg");
            $("#thePreviewPanel > div").addClass("modal-full");
        }
        else
            if ($("#PublicFormloginModal").data('bs.modal') && $("#PublicFormloginModal").data('bs.modal').isShown) {
                Layout.currentModel = 'PublicFormloginModal';
                $("#thePreviewPanel > div").removeClass("modal-lg");
                $("#thePreviewPanel > div").addClass("modal-full");
            }
            else
                if ($("#PreDefinedNewsLetter").data('bs.modal') && $("#PreDefinedNewsLetter").data('bs.modal').isShown) {
                    Layout.currentModel = 'PreDefinedNewsLetter';
                    $("#thePreviewPanel > div").removeClass("modal-full");
                    $("#thePreviewPanel > div").addClass("modal-lg");
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

});