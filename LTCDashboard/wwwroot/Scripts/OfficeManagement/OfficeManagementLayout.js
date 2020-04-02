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
    loadUserData: function () {
        var DATAURL = window.location.origin + "/Account/GetUserdata";
        $.ajax({
            type: "GET",
            url: DATAURL,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                if (response !== null) {
                    $('#txtSalutation').val(response.Salutation);
                    $('#txtInitials').val(response.Initials);
                    $('#txtFirstName').val(response.FirstName);
                    $('#txtLastName').val(response.LastName);
                    $('#txtPhone').val(response.Phone);
                    $('#txtFax').val(response.Fax);
                    $('#txtAddressLine1').val(response.AddressLine1);
                    $('#txtAddressLine2').val(response.AddressLine2);
                    $('#txtAddressLine3').val(response.AddressLine3);
                    $('#txtCity').val(response.City);
                    $('#txtProvince').val(response.Province);
                    $('#txtCountry').val(response.Country);
                    $('#txtPostalCode').val(response.PostalCode);
                } else {
                    //alert("Something went wrong");
                }
            },
            failure: function (response) {
                //alert(response.responseText);
            },
            error: function (response) {
                //alert(response.responseText);
            }
        });
    },
}

$(document).ready(function () {
    OfficeManagement.loadOfficeDropdown();
    OfficeManagement.loadProvidersDropdown();
    OfficeManagement.loadUserData();
});