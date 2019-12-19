var Report = {
   
    loadOfficeDropdown: function () {
        var officeDATAURL = window.location.origin + "/Home/GetOffices";
        $.getJSON(officeDATAURL, function (data) {
            var items = '<option>Select an Office</option>';
            $.each(data, function (i, offices) {
                items += "<option on value = " + offices.Id+">" + offices.ClinicName + "</option>";
            });
            $('#officedropdown').html(items);
            $('#officedropdown').selectpicker('refresh');
        });
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
        var DATAURL = window.location.origin + "/Report/LoadReviews";
        Layout.showLoader();
        $.ajax({
            type: "POST",
            url: DATAURL,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{ office_sequence: " + office_sequence+", startDate: '" + startDate+"', endDate : '" + endDate+"' }",
            success: function (response) {
             
                if (response !== null) {
                    var str = '';
                  
                  
                    var column = [];
                    $("#reviewCount").empty();
                    if (response.obj.source != null) {
                        
                        var ItemColX = [];
                        var totalArray = [];
                        var ItemBarChartCol = [];
                        $.each(response.obj.AverageRatingBySource, function (key, item) {
                            var ItemCol = [];
                            var ItemColBar = [];
                            ItemCol.push(item.Review_Link_Name);
                            ItemColBar.push(item.Review_Link_Name);
                            if (key == 0) {
                                ItemColX.push('x');
                                $.each(item.currentRating, function (index, cRating) {
                                    ItemColX.push(cRating.Date);
                                     

                                });
                                totalArray.push(ItemColX);
                                ItemBarChartCol.push(ItemColX);
                            }
                            $.each(item.currentRating, function (index, cRating) {
                                ItemCol.push(cRating.Rating);
                                ItemColBar.push(cRating.Reviews);
                            });
                            totalArray.push(ItemCol);
                            ItemBarChartCol.push(ItemColBar);
                        });
                        LoadBarChart(ItemBarChartCol);
                        LoadAverges(totalArray);
                        $('#rowLineChart').show();
                        $('#rowBarChart').show();
                    } else {
                        $('#rowLineChart').hide();
                        $('#rowBarChart').hide();

                    }  

                    if (response.obj.source != null) {
                        $.each(response.obj.source, function (key, item) {
                            column.push([item.Review_Link_Name, item.RatingCount]);
                            str += "<div id='rowDailySMS' class='col cardStart order-" + key + "'>" +
                                "<div id = 'card-rowDailySMS' class='card bg-blue-400' style = 'zoom: 1;'> " +
                                "<div class='card-body dashboardCard pt-1 pb-1'>" +
                                "<div class='d-flex cardHeading'>" +
                                "<h3 class='font-weight-semibold mb-0 font-variant-smallCaps'>New " + item.Review_Link_Name + " Reviews </h3>" +
                                "<div class='list-icons ml-auto' >";
                            if (item.RatingPecentage <= 0) {
                                str += "<span class='text-danger smallFont'><i class='icon-stats-decline2 mr-2'></i> " + item.RatingPecentage+"%</span>";
                            } else {
                                str += "<span class='text-success-600 smallFont'><i class='icon-stats-growth2 mr-2'></i>" + item.RatingPecentage +"%</span>";
                            }
                            str += "<a class='list-icons-item' data-action='remove' onclick='Report.removeCard(this)' data-name='rowDailySMS'></a>" +
                                "</div></div><div class='d-flex justify-content-center cardContent'><span id='dailySmsCount' class='font-weight-semibold mb-0 font-size-xs card-count'>" + item.CurrentMonthCount + "</span>" +
                                "</div> <div class='d-flex justify-content-end cardContent'></div></div></div></div > ";

                        });
                       
                        $('#rowNewBySource').show();
                        LoadChartBySource(column, response.obj.reviewResults.length);
                    } else {
                        $('#rowNewBySource').hide();

                    }  

                    $("#reviewCount").append(str);
                    $("#tblBody").empty();
                    str = '';
                    if (response.obj.reviewResults != null) {
                        $('#reviewCard').show();
                       
                        $.each(response.obj.reviewResults, function (key, item) {
                         
                            str += "<tr><td>" + item.Date + "</td >" +
                                "<td>" + item.Name + "</td>" +
                                "<td>" + item.Review + "</td>" +
                                "<td>" + item.Rating + "</td>" +
                                "<td><span class='badge badge-success'>" + item.Publisher + "</span></td></tr>";

                        });
                       
                        $("#tblBody").append(str);
                    } else {
                        $('#reviewCard').hide();
                    }
                    if (response.obj.totalReviewRatings != null) {
                        $('#rowTotalReview').show();
                        LoadChartOne(response.obj.totalReviewRatings);
                    } else {
                        $('#rowTotalReview').hide();
                    }
                   
                    if (response.obj.AverageRating != 0) {
                       
                        $('#rowAverageRating').show();
                         
                        var dv = '';
                        if (response.obj.AverageRatingPercentage <= 0) {
                            dv = "<span class='text-danger'><i class='icon-stats-decline2 mr-2'></i> " + response.obj.AverageRatingPercentage+"</span>";
                        } else {
                            dv = "<span class='text-success-600'><i class='icon-stats-growth2 mr-2'></i>" + response.obj.AverageRatingPercentage +"</span>";
                        }
                        LoadAverageChart(response.obj.AverageRating, dv);
                    } else {
                        $('#rowAverageRating').show();
                        LoadAverageChart(0, dv);
                    }
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
    setTimeout(function () {
        chart4.load({

        });
    }, 1500);

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
function LoadAverageChart(Average, dv)
{
    var donut_chart_element3 = document.getElementById('chart-3');
    var chart3 = c3.generate({
       
        bindto: donut_chart_element3,
        data: {
            columns: [
                ['Average', 0]
            ],
            type: 'gauge',
            onclick: function (d, i) { },
            onmouseover: function (d, i) { },
            onmouseout: function (d, i) { }
        },
        gauge: {
            fullCircle: true,
            label: {
                format: function (value, ratio) {
                    return value;
                },
                show: false // to turn off the min/max labels.
            },
            min: 0, // 0 is default, //can handle negative min e.g. vacuum / voltage / current flow / rate of change
            max: 5, // 100 is default
            units: ' %',
            width: 39 // for adjusting arc thickness
        },
        color: {
            pattern: ['#FF0000', '#F97600', '#F6C600', '#60B044'], // the three color levels for the percentage values.
            
        },
        size: {
            height: 320
        }
    });
    setTimeout(function () {
        chart3.load({
            columns: [['Average', Average]]
        });
        $("#dvAverageRatings").empty();
        $('#dvAverageRatings').append(dv);
    }, 1000);



}
function LoadChartBySource(col, title)
{
    var donut_chart_element2 = document.getElementById('chart-2');
    var chart2 = c3.generate({
        bindto: donut_chart_element2,
        data: {
            columns: col,
            color: {
                pattern: ['#3C5A99', '#d32323', '#8447a3', '#F4B400']
            },
            type: 'donut',
            onmouseover: function (d, i) { },
            onmouseout: function (d, i) { },
            onclick: function (d, i) { },
            order: null // set null to disable sort of data. desc is the default.
        },

        legend: {
            position: 'right'
        },
        axis: {
            x: {
                label: 'Sepal.Width'
            },
            y: {
                label: 'Petal.Width'
            }
        },
        donut: {
            label: {
                //            format: function (d, ratio) { return ""; }
            },
            title: title + " Reviews",
            width: 50,

        }
    });
   
}
function LoadChartOne(totalReviewRatings) {
     
    var donut_chart_element1 = document.getElementById('chart-1');
    var chart = c3.generate({
        bindto: donut_chart_element1,
        data: {
            columns: [
                ["1 Star", totalReviewRatings.OneStar],
                ["2 Star", totalReviewRatings.TwoStar],
                ["3 Star", totalReviewRatings.ThreeStar],
                ["4 Star", totalReviewRatings.FourStar],
                ["5 Star", totalReviewRatings.FiveStar],
            ],

            type: 'donut',
            onmouseover: function (d, i) { /*console.log("onmouseover", d, i, this);*/ },
            onmouseout: function (d, i) { },
            onclick: function (d, i) { },
            order: null // set null to disable sort of data. desc is the default.
        },

        legend: {
            position: 'right'
        },
        axis: {
            x: {
                label: 'Sepal.Width'
            },
            y: {
                label: 'Petal.Width'
            }
        },

        donut: {
            title: totalReviewRatings.SumOfRatings + " Reviews",
            width: 50,


        }
    });
    

}
$(document).ready(function () {
    Report.loadOfficeDropdown();
    Report.CalendarSetup("dashboardCalendar");

    $('#dashboardCalendar').on('apply.daterangepicker', function (ev, picker) {
       
        if ($('#officedropdown').val() != "Select an Office") {
           
            var str = "as of " + picker.endDate.format('MM/DD/YYYY');
            $("#smallHeading1").empty().append(str);
            $("#smallHeading2").empty().append(str);
            $("#smallHeading3").empty().append(str); 
            Report.loadReviews($('#officedropdown').val(), picker.startDate.format('MM/DD/YYYY'), picker.endDate.format('MM/DD/YYYY'));
        }
    });
});