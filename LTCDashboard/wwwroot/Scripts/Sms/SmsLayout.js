var OTable = null; 
var SMS = {
    
    loadOfficeDropdown: function () {
        var officeDATAURL = window.location.origin + "/Home/GetOffices";
        //var DATAURL = window.location.origin + "/Home/GetBusinessNames";
        $.getJSON(officeDATAURL, function (data) {
            var items = '';//<option value="-1">Select an Office</option>
            $.each(data, function (i, offices) {
                items += "<option value='" + offices.Id + "'>" + offices.ClinicName + "</option>";
            });
            $('#officedropdown').html(items);
            $('#officedropdown').selectpicker({ actionsBox: true });
            $('#officedropdown').selectpicker('refresh');
            
            $('#officedropdown').selectpicker('selectAll');
        });

        $("#officedropdown").on("changed.bs.select",
            function (e, clickedIndex, newValue, oldValue) {
                var selectedOffices = $("#officedropdown").val();
                var index = selectedOffices.indexOf('-1');

                if (index > -1)
                    selectedOffices.splice(index, 1);

                if (selectedOffices.length == 0) {
                    $.each(Dashboard.loadedCharts, function (i, item) {
                        //$("#cardStart-" + item.chartName).remove();
                    });
                }
               
            });
    },   
    SelectedRadio: function (id) {
        if (id == 1) {
            $('#chart-5').show();
            $('#chart-4').hide();
        } else if (id == 2) {
            $('#chart-4').show();
            $('#chart-5').hide();
        }
    },
    removeCard: function (e) {

        var target = $(e),
            slidingSpeed = 150;

        var dataId = target.data('name');

        // If not disabled
        if (!target.hasClass('disabled')) {
            target.closest('.card').slideUp({
                duration: slidingSpeed,
                start: function () {
                    target.addClass('d-block');
                },
                complete: function () {
                    target.closest('.cardStart').remove();
                 
                }
            });
        }
    },
    LoadAppointments: function (office_sequence, startDate, endDate) {
      
        if ($.fn.DataTable.isDataTable('#AppointmentTable')) {
            $('#AppointmentTable').DataTable().destroy();
        }

        $('#AppointmentTable tbody').empty();

      
        OTable = $('#AppointmentTable').dataTable({
            "bServerSide": true,
            "sAjaxSource": "/SMS/LoadAppointments?office_sequence=" + office_sequence + "&startDate=" + startDate + "&endDate=" + endDate,
            "sServerMethod": "POST",
            "fnDrawCallback": function (oSettings) {
                Layout.hideLoader();
            },
            "aoColumns": [
                { "mData": "Name" },
                { "mData": "ActionTypeDetail" },
                { "mData": "AppointmentDateTime" },
                { "mData": "Response" },
                { "mData": "SMSSendDateString" },
                { "mData": "EmailSendDateString" },
            ],
            "ordering": false,
            "searching": false,
            "serverSide": true,
            "processing": true,
            "language": {
                "processing": "loading ... "
            }

        });  
        return OTable;
    },
    CalendarSetup: function (id) {
        $('#' + id).daterangepicker({
            opens: 'left',
            applyClass: 'bg-slate-600',
            cancelClass: 'btn-light',
            alwaysShowCalendars: true,
            startDate: moment().subtract(29, 'days'),
            endDate: moment(),
            ranges: {
                'Today': [moment(), moment()],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            }
        });
       
    },
    loadReviews: function (office_sequence, startDate, endDate) {
        
        var DATAURL = window.location.origin + "/Sms/LoadDashboard";
        Layout.showLoader();
        $.ajax({
            type: "POST",
            url: DATAURL,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{ office_sequence: '" + office_sequence+"', startDate: '" + startDate+"', endDate : '" + endDate+"' }",
            success: function (response) {
                var str = '';
                if (response !== null) {
                    if (response.obj.SMSCounts != null) {
                         
                        var ItemColX = [];
                        var totalArray = [];
                        var ItemCol = [];
                        var ItemColEmail = [];
                        var ItemRecall = [];
                        var ItemColPreconfirm = [];

                        ItemColEmail.push('Email');
                        ItemCol.push('SMS');
                        ItemRecall.push('Recall');
                        ItemColPreconfirm.push('Confirmation/Preconfirmation');
                        ItemColX.push('x');
                        $.each(response.obj.SMSCounts, function (key, item) {
                            ItemColX.push(item.Date);
                        });
                        $.each(response.obj.SMSCounts, function (key, item) {
                            
                            ItemCol.push(item.Count);
                        });
                        $.each(response.obj.EmailCounts, function (key, item) {
                            ItemColEmail.push(item.Count);
                        });

                        $.each(response.obj.ConfirmationCounts, function (key, item) {
                            ItemColPreconfirm.push(item.Count);
                        });

                        $.each(response.obj.RecallCounts, function (key, item) {
                            ItemRecall.push(item.Count);
                        });

                            totalArray.push(ItemColX);
                            totalArray.push(ItemCol);
                        totalArray.push(ItemColEmail);
                        totalArray.push(ItemColPreconfirm);
                        totalArray.push(ItemRecall);
                        $('#chart-5').show();
                        $('#chart-4').hide();
                        LoadBarChart(totalArray);
                        LoadAverges(totalArray);
                        //$('#rowLineChart').show();
                    } else {
                        $('#chart-5').hide();
                        $('#chart-4').hide();

                    }  
                   
                     
                   

                    $("#dailySmsCount").empty();
                    $("#tblBody").empty();
                    $('#dailySmsCount').append(response.obj.DailySMSCount);                  
                    $("#dailyEmailCount").empty();
                    $('#dailyEmailCount').append(response.obj.DailyEmailCount);                  
                    $("#dailyConfirmationCount").empty();
                    $('#dailyConfirmationCount').append(response.obj.DailyPreConfirmationCount);                  
                    $("#dailyRecallCount").empty();
                    $('#dailyRecallCount').append(response.obj.DailyRecallCount);                  

                    OTable = SMS.LoadAppointments(office_sequence, startDate, endDate);
                    
                    Layout.hideLoader();
                } else {
                    Layout.hideLoader();
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
function LoadBarChart(col) {
    var donut_chart_element5 = document.getElementById('chart-5');

    var chart5 = c3.generate({
        bindto: donut_chart_element5,
        data: {
            x: 'x',
            columns: col,
            type: 'bar'
        },
        axis: {
            x: {
                type: 'timeseries',
                tick: {
                    format: '%Y-%m-%d'
                }
            }
        },

        bar: {
            width: {
                ratio: 0.5 // this makes bar width 50% of length between ticks
            }

        }
    });

     
}
function Reset() {
    $("#tblBody").empty();

    $('#rowLineChart').hide();
    $('#rowBarChart').hide();

    $("#dailySmsCount").empty();
    $('#dailySmsCount').append(0);
    $("#dailyEmailCount").empty();
    $('#dailyEmailCount').append(0);
    $("#dailyConfirmationCount").empty();
    $('#dailyConfirmationCount').append(0);
    $("#dailyRecallCount").empty();
    $('#dailyRecallCount').append(0);
}
function LoadAverges(col)
{
    var donut_chart_element4 = document.getElementById('chart-4');
    var chart4 = c3.generate({
        bindto: donut_chart_element4,
        data: {
            x: 'x',
            columns: col,
        },
        axis: {
            x: {
                type: 'timeseries',
                tick: {
                    format: '%Y-%m-%d'
                }
            }
        }
    });
   

}

 