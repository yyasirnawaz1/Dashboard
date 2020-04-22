var allProviders;
var globalFormVariables = {
    AppointCounter: null,
    FormID: null
}

var globalSurveyVariables = {
    AppointCounter: null,
    FormID: null,
    PatientNumber: null,
    OfficeSequence: null,
}

var globalVariable = {
    Office_Sequence: null,
}

var HomeLayout = {

    loadProvidersDropdown: function () {

        var providerDATAURL = window.location.origin + "/Home/GetProviders";
        $.getJSON(providerDATAURL, function (data) {

            allProviders = data;

            var items = '<option selected>Everyone</option>'
                + '<option disabled>Doctors only</option>'
                + '<option disabled>hygienists only</option>';
            $.each(data, function (i, offices) {
                items += '<option value="' + offices.Provider + '" disabled>' + offices.Name + '</option>';

            });
            $('#ddlProviders').html(items);
            $('#ddlProviders').selectpicker('refresh');
        });

    },

    setOfficeName: function () {
        $.get("/Home/GetOfficeName", function (data) {
            if (data != null) {
                globalVariable.Office_Sequence = data.Office_Sequence;
                $('#divAppHeaderOfficeName').html(data.Name);
            }
        });
    },

    removeSelectedTitles: function (content) {
        var json = JSON.parse(content);
        for (i = 0; i < json.length; i++)
            if (json[i].type == 'LineSeprator' || json[i].type == 'NewLine')
                json[i].label = '';
        return JSON.stringify(json)
    },

    renderFormDesignerInIframe: function (content, id) {
        var iframe = document.getElementById(id),
            iframeWin = iframe.contentWindow || iframe,
            iframeDoc = iframe.contentDocument || iframeWin.document;
         
        iframeDoc.open();
        iframeDoc.write('<html><head>');
        iframeDoc.write('</head><body>');
        iframeDoc.write('<link rel="stylesheet" href="/Resources/theme/css/styles.css"><script type="text/javascript" src="/Resources/theme/js/vendor/jquery/jquery.min.js"></script><script type="text/javascript" src="/Resources/theme/js/vendor/jquery/jquery-ui.min.js"></script><script type="text/javascript" src="/Resources/theme/js/vendor/bootstrap/bootstrap.min.js"></script><script src="/Resources/FormDesigner/assets/js/form-render.min.js"></script><link href="/Resources/FormDesigner/assets/css/RateYo/RateYo.css" rel="stylesheet" /><script src="/Resources/FormDesigner/assets/js/Rateyo/jquery.rateyo.min.js"></script><link href="/Resources/Signature/jquery.signature.css" rel="stylesheet" /><script src="/Resources/Signature/jquery.signature.js"></script>');
        iframeDoc.write('<script src="/Scripts/SharedJS/RenderContent.js"></script>');
        iframeDoc.write('<div id="render-container"  style="padding: 30px;"></div>');
        iframeDoc.write('<script type="text/javascript">$(document).ready(function () {var content = ' + content + ';  RenderContent.RenderContentInIframe(content);});</script>');

        iframeDoc.write('</body></html>');
        iframeDoc.close();

        $('#thePreviewPanel').modal('show');
    },

    renderFormDesignerFormSubmit: function (content, id) {
        var iframe = document.getElementById(id),
            iframeWin = iframe.contentWindow || iframe,
            iframeDoc = iframe.contentDocument || iframeWin.document;

        iframeDoc.open();
        iframeDoc.write('<html><head>');
        iframeDoc.write('</head><body>');
        iframeDoc.write('<link rel="stylesheet" href="/Resources/theme/css/styles.css"><script type="text/javascript" src="/Resources/theme/js/vendor/jquery/jquery.min.js"></script><script type="text/javascript" src="/Resources/theme/js/vendor/jquery/jquery-ui.min.js"></script><script type="text/javascript" src="/Resources/theme/js/vendor/bootstrap/bootstrap.min.js"></script><script src="/Resources/FormDesigner/assets/js/form-render.min.js"></script><link href="/Resources/FormDesigner/assets/css/RateYo/RateYo.css" rel="stylesheet" /><script src="/Resources/FormDesigner/assets/js/Rateyo/jquery.rateyo.min.js"></script><link href="/Resources/Signature/jquery.signature.css" rel="stylesheet" /><script src="/Resources/Signature/jquery.signature.js"></script>');
        iframeDoc.write('<script src="/Scripts/SharedJS/RenderContent.js"></script>');
        iframeDoc.write('<div id="render-container"  style="padding: 30px;"></div>');
        iframeDoc.write('<script type="text/javascript">$(document).ready(function () {var content = ' + content + ';  RenderContent.RenderContentInIframe(content);});</script>');

        iframeDoc.write('</body></html>');
        iframeDoc.close();

        $('#modalFormSubmit').modal('show');
    },

    closeTab: function (id) {
        var tabContentId = $('#tab-' + id+'> a').attr("href");
        $('#tab-' + id ).hide(); //remove li of tab
        $(tabContentId).hide(); //remove respective tab content

        if ($('#Tabs a:visible:first').length > 0) {
            setTimeout(function () { $('#Tabs a:visible:first').click(); }, 1000);
        }
        else {
            $("#nodataDiv").show();
        }
    }
}

$(function () {
    HomeLayout.setOfficeName();
    HomeLayout.loadProvidersDropdown();
});
