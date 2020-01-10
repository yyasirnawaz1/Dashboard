var Common = {
    getParameterByName: function (name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, '\\$&');
        var regex = new RegExp('[?&]' + name + '(=([^&#]*)|&|#|$)'),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, ' '));
    },

    showLoader: function () {
        //$("body").block({
        //    message: '<i class="icon-spinner4 spinner"></i>',
        //    //timeout: 2000, //unblock after 2 seconds
        //    overlayCSS: {
        //        //backgroundColor: '#0E8F92',
        //        opacity: 0.9,
        //        cursor: 'wait'
        //    },
        //    css: {
        //        border: 0,
        //        padding: 0,
        //        color: '#fff',
        //        backgroundColor: 'transparent'
        //    }
        //});
    },

    hideLoader: function () {
        //$.unblockUI();
    },
}