var OfficeManagement = {

    loadOfficeDropdown: function () {
        var officeDATAURL = window.location.origin + "/Home/GetOffices";
        //var DATAURL = window.location.origin + "/Home/GetBusinessNames";
        $.getJSON(officeDATAURL, function (data) {
            var items = '<option>Select an Office</option>';
            $.each(data, function (i, offices) {
                items += "<option>" + offices.ClinicName + "</option>";
            });
            $('#officedropdown').html(items);
            $('#officedropdown').selectpicker('refresh');
            //console.log(items);
        });
    },
    loadProvidersDropdown: function () {
        if (!$("#officedropdown").length) {
            return;
        }

        var providerDATAURL = window.location.origin + "/Home/GetProviders";
        $.getJSON(providerDATAURL, function (data) {
            var items = '<option>Select a Provider</option>';
            $.each(data, function (i, offices) {
                items += "<option>" + offices.Name + "</option>";

            });
            $('#providerdropdown').html(items);
            $('#providerdropdown').selectpicker('refresh');
            //console.log(items);
        });

    },
}

$(document).ready(function () {
    OfficeManagement.loadOfficeDropdown();
    OfficeManagement.loadProvidersDropdown();
});