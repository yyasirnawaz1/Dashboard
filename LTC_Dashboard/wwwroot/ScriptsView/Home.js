

var HomeView = function () {

    return {
        
        init: function () {
 
        },

        getScheduledNewsLetterStatistics: function ()
        {
            Layout.showLoader(); 
            var category = $('#Status').val();
            var period = $('#Period').val();
            var actionURL = '/Newsletter/GetScheduledNewsLetterStatistics' + '?category=' + category + '&period=' + period;
            $.get(actionURL, function (data) {
                
                $('#scheduledNewsletterContainer').html(data);

            });

        },
         
    };

}();

HomeView.init();

$(document).ready(function () {
    HomeView.getScheduledNewsLetterStatistics();
    $('#Status').on("change", function () { HomeView.getScheduledNewsLetterStatistics(); });
    $('#Period').on("change", function () { HomeView.getScheduledNewsLetterStatistics(); });
    $('#reloadnewsletterstat').on("click", function () { HomeView.getScheduledNewsLetterStatistics(); });

});


