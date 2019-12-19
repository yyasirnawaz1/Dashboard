var Dashboard = {
    breakdownTable: null,
    breakdownTableConfiguration: null,
    charts: [],
    loadedCharts: [],
    loadedPatients: [],
    breakdownList: null,
    breakdownTableBasic: null,
    breakdownListServiceAnalysis: null,
    breakdownListCancellationAndNoShows: null,
    officeList: null,
    chartType: 'PracticeMetrics',
    selectionHTML: ' <div class="col-12 draggable order-{order} order-last" data-id="{id}" data-name="{name}" data-title="{title}" data-position="{position}" data-order="{order}"><div class="nav-link"><i class="{icon}"></i>{title}</div></div>',

    //////Small Cards
    NewPatient: '<div id="cardStart-{name}" class="col col-lg-3 col-md-4 col-sm-6 cardStart order-{order}"><div id="card-{name}" class="card bg-blue-400"><div class="card-body dashboardCard pt-1 pb-1"><div class="d-flex cardHeading"><h3 class="font-weight-semibold mb-0 font-variant-smallCaps">{heading}</h3><div class="list-icons ml-auto"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div><div class="d-flex justify-content-center cardContent"><span id="card-{name}-value" class="font-weight-semibold mb-0 font-size-xs">0</span></div><div class="d-flex justify-content-end cardContent" data-toggle="modal" data-target="#patientListModal"><input type="button" class="btn bg-purple-400" value="Patient List" onclick="Dashboard.getPatientList();"/></div></div></div></div>',
    TotalNetProduction: '<div id="cardStart-{name}" class="col col-lg-3 col-md-4 col-sm-6 cardStart order-{order}"><div id="card-{name}" class="card bg-teal-400"><div class="card-body dashboardCard pt-1 pb-1"><div class="d-flex cardHeading"><h3 class="font-weight-semibold mb-0 font-variant-smallCaps">{heading}</h3><div class="list-icons ml-auto"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div><div class="d-flex justify-content-center cardContent"><span id="card-{name}-value" class="font-weight-semibold mb-0 font-size-xs">0</span></div><div class="d-flex justify-content-end cardContent"><input onclick="Dashboard.loadChartBreakdownData(\'{name}\')" type="button" class="btn bg-purple-400" value="Breakdown" /></div></div></div></div>',
    TotalPaymentReceipt: '<div id="cardStart-{name}" class="col col-lg-3 col-md-4 col-sm-6 cardStart order-{order}"><div id="card-{name}" class="card bg-pink-400"><div class="card-body dashboardCard pt-1 pb-1"><div class="d-flex cardHeading"><h3 class="font-weight-semibold mb-0 font-variant-smallCaps">{heading}</h3><div class="list-icons ml-auto"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div><div class="d-flex justify-content-center cardContent"><span id="card-{name}-value" class="font-weight-semibold mb-0 font-size-xs">0</span></div><div class="d-flex justify-content-end cardContent"><input onclick="Dashboard.loadChartBreakdownData(\'{name}\')" type="button" class="btn bg-purple-400" value="Breakdown" /></div></div></div></div>',
    TotalNetPaymentReceipt: '<div id="cardStart-{name}" class="col col-lg-3 col-md-4 col-sm-6 cardStart order-{order}"><div id="card-{name}" class="card bg-blue-400"><div class="card-body dashboardCard pt-1 pb-1"><div class="d-flex cardHeading"><h3 class="font-weight-semibold mb-0 font-variant-smallCaps">{heading}</h3><div class="list-icons ml-auto"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div><div class="d-flex justify-content-center cardContent"><span id="card-{name}-value" class="font-weight-semibold mb-0 font-size-xs">0</span></div><div class="d-flex justify-content-end cardContent"><input onclick="Dashboard.loadChartBreakdownData(\'{name}\')" type="button" class="btn bg-purple-400" value="Breakdown" /></div></div></div></div>',
    TotalNetHygenistProduction: '<div id="cardStart-{name}" class="col col-lg-3 col-md-4 col-sm-6 cardStart order-{order}"><div id="card-{name}" class="card bg-success"><div class="card-body dashboardCard pt-1 pb-1"><div class="d-flex cardHeading"><h3 class="font-weight-semibold mb-0 font-variant-smallCaps">{heading}</h3><div class="list-icons ml-auto"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div><div class="d-flex justify-content-center cardContent"><span id="card-{name}-value" class="font-weight-semibold mb-0 font-size-xs">0</span></div><div class="d-flex justify-content-end cardContent"><input onclick="Dashboard.loadChartBreakdownData(\'{name}\')" type="button" class="btn bg-purple-400" value="Breakdown" /></div></div></div></div>',
    TotalNetDoctorProduction: '<div id="cardStart-{name}" class="col col-lg-3 col-md-4 col-sm-6 cardStart order-{order}"><div id="card-{name}" class="card bg-pink-400"><div class="card-body dashboardCard pt-1 pb-1"><div class="d-flex cardHeading"><h3 class="font-weight-semibold mb-0 font-variant-smallCaps">{heading}</h3><div class="list-icons ml-auto"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div><div class="d-flex justify-content-center cardContent"><span id="card-{name}-value" class="font-weight-semibold mb-0 font-size-xs">0</span></div><div class="d-flex justify-content-end cardContent"><input onclick="Dashboard.loadChartBreakdownData(\'{name}\')" type="button" class="btn bg-purple-400" value="Breakdown" /></div></div></div></div>',
    AverageProductionPerPatient: '<div id="cardStart-{name}" class="col col-lg-3 col-md-4 col-sm-6 cardStart order-{order}"><div id="card-{name}" class="card bg-success"><div class="card-body dashboardCard pt-1 pb-1"><div class="d-flex cardHeading"><h3 class="font-weight-semibold mb-0 font-variant-smallCaps">{heading}</h3><div class="list-icons ml-auto"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div><div class="d-flex justify-content-center cardContent"><span id="card-{name}-value" class="font-weight-semibold mb-0 font-size-xs">0</span></div><div class="d-flex justify-content-end cardContent"></div></div></div></div>',

    ////charts
    RecareReappointmentPassive: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="progressMeter"><div class="c100 p30 big green"><span>30%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div></div></div></div></div>',
    RecareReappointmentActive: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="progressMeter"><div class="c100 p41 big green"><span>41%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div></div></div></div></div>',
    TreatmentAcceptance: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="chart-container"><div class="progressMeter sm"><div class="c100 p45"><span>45%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div></div><div class="chart smallBarGraph" id="{name}-chart"></div></div></div></div></div>',
    TreatmentScheduled: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="chart-container"><div class="progressMeter sm"><div class="c100 p63"><span>63%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div></div><div class="chart smallBarGraph" id="{name}-chart"></div></div></div></div></div>',
    CancellationAndNoShows: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"/><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"/></div></div></div><div class="card-body"><div class="chart-container"><div class="chart mediumBarGraph" id="{name}-chart" /></div></div><div class="d-flex justify-content-end cardContent"><input onclick="Dashboard.loadChartBreakdownData(\'{name}\')" type="button" class="btn bg-purple-400" value="Breakdown" /></div></div></div>',
    UnscheduledTreatment: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="chart-container"><div class="progressMeter progressMetersmall sm"><div class="c100 p20"><span>20%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div></div><div class="chart smallBarGraph" id="{name}-chart"></div><input type="button" class="btn bg-blue-400 pull-right chartPatientButton" data-toggle="modal" data-target="#patientListModal" value="Patient List" /></div></div></div></div>',
    UnscheduledRecareActivePatient: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="chart-container"><div class="progressMeter progressMetersmall sm"><div class="c100 p80"><span>80%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div></div><div class="chart smallBarGraph" id="{name}-chart"></div><input type="button" class="btn bg-blue-400 pull-right chartPatientButton" data-toggle="modal" data-target="#patientListModal" value="Patient List" /></div></div></div></div>',
    NetCollection: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="progressMeter"><div class="c100 p41 big green"><span>41%</span><div class="slice"><div class="bar"></div><div class="fill"></div></div></div></div></div></div></div>',
    ProductionBreakdown: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="chart-container"><div class="chart" id="{name}-chart" data-charttype="pie"></div></div></div></div></div>',
    ServiceAnalysis: '<div id="cardStart-{name}" class="col-md-4 cardStart order-{order}"><div id="card-{name}" class="card dashboardGraph"><div class="card-header header-elements-inline"><h5 class="card-title font-variant-smallCaps">{heading}</h5><div class="header-elements"><div class="list-icons"><a class="icon-stats-growth chartButton" onclick="openChart(\'{name}\', \'{heading}\')"></a><a class="list-icons-item" data-action="remove" onclick="Dashboard.removeCard(this)" data-name="{name}"></a></div></div></div><div class="card-body"><div class="chart-container"><div class="chart" id="{name}-chart" data-charttype="pie"></div></div></div><div class="d-flex justify-content-end cardContent"><input onclick="Dashboard.loadChartBreakdownData(\'{name}\')" type="button" class="btn bg-purple-400" value="Breakdown" /></div></div></div>',


    CalendarSetup: function(id) {
        $('#' + id).daterangepicker({
            opens: 'left',
            applyClass: 'bg-slate-600',
            cancelClass: 'btn-light',
            alwaysShowCalendars: true,
            startDate: moment().subtract(29, 'days'),
            endDate: moment(),
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            }
        });
    },

    PageSetup: function() {
        Dashboard.CalendarSetup("dashboardCalendar");
        Dashboard.CalendarSetup("dashboardModelCalendar");

        Dashboard.loadOfficeDropdown();
        Dashboard.loadProvidersDropdown();
        Dashboard.loadCharts();
        Dashboard.loadUserData();

        $('#dashboardCalendar').on('apply.daterangepicker', function(ev, picker) {
            Dashboard.reloadCharts();
            Dashboard.loadedPatients = null;
        });

        $('#breakdownButtonSwitch').bootstrapSwitch();

        $('#breakdownButtonSwitch').on('switchChange.bootstrapSwitch',
            function(event, state) {
                if (!state) {
                    $("#breakdownAdvancedDetail").show();
                    $("#breakdownbasicDetail").hide();
                    Dashboard.loadAdvanceTable();
                } else {
                    $("#breakdownbasicDetail").show();
                    $("#breakdownAdvancedDetail").hide();
                    Dashboard.loadBasicTable();
                }
            });

    },

    loadOfficeDropdown: function() {
        var officeDATAURL = window.location.origin + "/Home/GetOffices";
        //var DATAURL = window.location.origin + "/Home/GetBusinessNames";
        $.getJSON(officeDATAURL, function(data) {
            var items = '';//<option value="-1">Select an Office</option>
            $.each(data, function(i, offices) {
                items += "<option value='" + offices.Id + "'>" + offices.ClinicName + "</option>";
            });
            Dashboard.officeList = data
            $('#officedropdown').html(items);
            $('#officedropdown').selectpicker({ actionsBox: true });
            $('#officedropdown').selectpicker('refresh');
            $('#officedropdown').selectpicker('selectAll');
        });

        $("#officedropdown").on("changed.bs.select",
            function(e, clickedIndex, newValue, oldValue) {
                var selectedOffices = $("#officedropdown").val();

                if (selectedOffices.length == 0) {
                    $.each(Dashboard.loadedCharts, function(i, item) {
                        $("#cardStart-" + item.chartName).remove();
                    });
                }
                Dashboard.loadProvidersDropdown();
                Dashboard.loadedPatients = null;
            });
    },

    isproviderEventBound: false,
    loadProvidersDropdown: function() {

        var selectedOffices = $("#officedropdown").val();

        if (selectedOffices.length > 0) {

            Layout.showLoader();
            $('#providerdropdown').selectpicker({ actionsBox: true });
            var providerDATAURL = window.location.origin + "/Home/GetProviders?office_sequence=" + selectedOffices.join();
            $.getJSON(providerDATAURL, function(data) {
                var items = ''; //<option value="-1">Select a Provider</option>
                $.each(data, function(i, providers) {
                    items += "<option value='" + providers.Office_Sequence + "_" + providers.Provider + "'>" + providers.Name + "</option>";
                });
                $('#providerdropdown').html(items);
                $('#providerdropdown').selectpicker('refresh');

                Layout.hideLoader();
            });
            if (!Dashboard.isproviderEventBound) {
                $("#providerdropdown").on("changed.bs.select", function(e, clickedIndex, newValue, oldValue) {

                    var selectedProvider = $("#providerdropdown").val();

                    if (selectedProvider.length > 0) {
                        //debugger;
                        $(".draggable").draggable("enable");
                    }
                    else {
                        //debugger;
                        $(".draggable").draggable("disable");
                    }

                    Dashboard.reloadCharts();
                });
                Dashboard.isproviderEventBound = true;
            }
            Dashboard.loadedPatients = null;
        }
        else {
            var items = ''; //<option value="-1">Select a Provider</option>
            $('#providerdropdown').html(items);
            $('#providerdropdown').selectpicker({ actionsBox: true });
            $('#providerdropdown').selectpicker('refresh');
        }
    },

    loadUserData: function() {
        var DATAURL = window.location.origin + "/Account/GetUserdata";
        $.ajax({
            type: "GET",
            url: DATAURL,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(response) {
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
            failure: function(response) {
                //alert(response.responseText);
            },
            error: function(response) {
                //alert(response.responseText);
            }
        });
    },

    loadCharts: function() {
        Layout.showLoader();
        $.ajax({
            type: "POST",
            url: "/Dashboard/GetCharts",
            data: { pageName: 'PracticeMetrics' },
            success: function(data) {
                if (data.Success) {

                    Dashboard.charts = data.Data;
                    Dashboard.userCharts = data.UserCharts;

                    $.each(data.Data, function(i, data) {

                        if (Dashboard.userCharts.filter(x => x.Chart_Id == data.ID).length > 0) {
                            Dashboard.loadedCharts.push({ chartId: data.ID, chartName: data.Name, chartTitle: data.Title, position: data.Position, order: data.Order });
                        } else {
                            var html = Dashboard.selectionHTML
                                ///blue/g
                                .replace(/{position}/g, data.Position)
                                .replace(/{order}/g, data.Order)
                                .replace(/{title}/g, data.Title)
                                .replace(/{id}/g, data.ID)
                                .replace(/{name}/g, data.Name)
                                .replace(/{icon}/g, data.Icon);
                            $("#ChartSelectionOptions ." + data.Position).append(html);
                        }
                    });

                    Dashboard.initializeDragDrop();
                    $('#providerdropdown').selectpicker('selectAll');
                }
            },
            error: function(errorData) {
                Dashboard.onError(errorData);
            }
        });
    },

    loadChartsData: function(chartName, chartType) {
        var selectedProvider = $("#providerdropdown").val();

        var selectedOffices = $("#officedropdown").val();


        if (selectedOffices.length > 0 && selectedProvider.length > 0) {

            $.ajax({
                type: "POST",
                url: "/Dashboard/LoadChartData",
                data: {
                    chartName: chartName,
                    offices: selectedOffices,
                    providers: selectedProvider,
                    startDate: moment($('#dashboardCalendar').data('daterangepicker').startDate).format("YYYY-MM-DD"),
                    endDate: moment($('#dashboardCalendar').data('daterangepicker').endDate).format("YYYY-MM-DD")
                },
                success: function(data) {
                    if (data.Success) {
                        try {

                            if (data.hasMultipleRecords) {

                                if (chartType.includes('PieOnly')) {
                                    Dashboard.PieChart(chartName + '-chart', data.Data);
                                } else if (chartType.includes('BarOnly')) {
                                    Dashboard.BarChart(chartName + '-chart', data.Data);
                                }
                            }
                            else {
                                if (data.IsCurrency) {
                                    var formatter = new Intl.NumberFormat('en-US', {
                                        style: 'currency',
                                        currency: 'USD',
                                    });
                                    $("#card-" + chartName + "-value").html(formatter.format(data.Data).replace('.00', ''));
                                }
                                else if (data.Data > 1000) {
                                    var formatter = new Intl.NumberFormat('en-US');
                                    $("#card-" + chartName + "-value").html(formatter.format(data.Data));
                                }
                                else {
                                    $("#card-" + chartName + "-value").html(data.Data);
                                }
                            }
                        } catch (e) {

                        }
                    }
                    Layout.hideCardLoader('card-' + chartName);
                },
                error: function(errorData) {
                    Dashboard.onError(errorData);
                    Layout.hideCardLoader('card-' + chartName);
                }
            });
        }

    },

    loadChartBreakdownData: function(chartName) {
        var selectedProvider = $("#providerdropdown").val();
        var selectedOffices = $("#officedropdown").val();

        if (selectedOffices.length > 0 && selectedProvider.length > 0) {
            $("#breakdownDetail").html('');
            Layout.showLoader();
            $.ajax({
                type: "POST",
                url: "/Dashboard/LoadChartDataBreakdown",
                data: {
                    chartName: chartName,
                    offices: selectedOffices,
                    providers: selectedProvider,
                    startDate: moment($('#dashboardCalendar').data('daterangepicker').startDate).format("YYYY-MM-DD"),
                    endDate: moment($('#dashboardCalendar').data('daterangepicker').endDate).format("YYYY-MM-DD")
                },
                success: function(data) {
                    if (data.Success) {

                        //OfficeName
                        $.each(data.Data, function(index, item) {
                            var officeDetail = Dashboard.officeList.filter(x => x.Id == item.Office_Sequence);
                            try {
                                if (officeDetail.length > 0) {
                                    data.Data[index].OfficeName = officeDetail[0].ClinicName;
                                }

                                data.Data[index].Name = data.Data[index].Title + " " + data.Data[index].LastName + " " + data.Data[index].FirstName;
                            } catch (e) {

                            }
                        });

                        if (Dashboard.breakdownTable != null) {
                            Dashboard.breakdownTable.destroy();
                        }

                        if (Dashboard.breakdownTableServiceAnalysis != null) {
                            Dashboard.breakdownTableServiceAnalysis.data("kendoGrid").dataSource.data([]);
                            $("#breakdown-tableServiceAnalysis").html('');
                        }

                        if (chartName == "ServiceAnalysis") {
                            Dashboard.breakdownList = null;
                            Dashboard.breakdownListServiceAnalysis = data.Data;
                            Dashboard.breakdownTableBasic = null;
                            Dashboard.breakdownListCancellationAndNoShows = null;
                        }
                        else
                            if (chartName == "CancellationAndNoShows") {
                                Dashboard.breakdownList = null;
                                Dashboard.breakdownListServiceAnalysis = null;
                                Dashboard.breakdownTableBasic = null;
                                Dashboard.breakdownListCancellationAndNoShows = data.Data;
                            }
                            else {
                                Dashboard.breakdownList = data.Data;
                                Dashboard.breakdownTableBasic = data.Data;
                                Dashboard.breakdownListServiceAnalysis = null;
                                Dashboard.breakdownListCancellationAndNoShows = null;
                            }

                        $("#chartBreakdown").modal('show');
                    }
                    Layout.hideLoader();
                },
                error: function(errorData) {
                    Dashboard.onError(errorData);
                    Layout.hideLoader();
                }
            });
        }

    },

    loadBasicTable: function() {
        if (Dashboard.breakdownTableBasic != null) {
            try {
                Dashboard.breakdownTableBasic.destroy();
                $("#breakdownBasic-table").html('');
            } catch (e) {

            }
        }
        Dashboard.breakdownTableBasic = $("#breakdownBasic-table").kendoGrid({
            dataSource: {
                data: Dashboard.breakdownList,
                group: [
                    { field: "OfficeName", title: officeNameSetting, dir: "asc" },
                    { field: "InvoiceDateString", title: "Invoice Date", dir: "asc" },
                    { field: "InvoiceTypeDetail", title: "Invoice Type", dir: "asc" },
                    { field: "Name", title: "Patient", dir: "asc" },
                ],

                sort: {
                    field: "InvoiceDateString",
                    dir: "desc"
                }
            },
            selectable: "multiple row",
            pageable: {
                buttonCount: 5
            },
            scrollable: true,
            groupable: true,
            columns: [
                {
                    field: "PatAmount",
                    title: patAmountSetting,
                    format: "{0:c}"
                }, {
                    field: "InsAmount",
                    title: insAmountSetting,
                    format: "{0:c}"
                }
                , {
                    field: "OfficeName",
                    groupHeaderTemplate: officeNameSetting,
                    title: officeNameSetting
                }, {
                    field: "InvoiceTypeDetail",
                    groupHeaderTemplate: "Invoice Type",
                }, {
                    field: "InvoiceDateString",
                    groupHeaderTemplate: "Invoice Date",
                }
            ]
        });
    },

    loadAdvanceTable: function() {
        if (Dashboard.breakdownTable != null) {
            try {
                Dashboard.breakdownTable.destroy();
                $("#breakdown-table").html('');

                Dashboard.breakdownTableConfiguration.data("kendoPivotConfigurator").destroy()
                $("#configurator").html('');
            } catch (e) {

            }
        }

        Dashboard.breakdownTable = $("#breakdown-table").kendoPivotGrid({
            filterable: true,
            columnWidth: 120,
            height: 570,
            dataSource: {
                data: Dashboard.breakdownList,
                schema: {
                    model: {
                        fields: {
                            Name: { type: "string" },
                            InvoiceDateString: { type: "string", label: "Invoice Date" },
                            InvoiceTypeDetail: { type: "string", label: "Invoice Type" },
                            PatAmount: { type: "number", label: patAmountSetting },
                            InsAmount: { type: "number", label: insAmountSetting },
                            Total: { type: "number" },
                            OfficeName: { type: "string" },
                        }
                    },
                    cube: {
                        dimensions: {
                            OfficeName: { caption: officeNameSetting },
                            InvoiceDateString: { caption: "All Dates" },
                            InvoiceTypeDetail: { caption: "All Types" },
                            Name: { caption: "All Patients" },
                        },
                        measures: {
                            "Sum PatAmount": { field: "PatAmount", format: "{0:c}", aggregate: "sum" },
                            "Sum InsAmount": { field: "InsAmount", format: "{0:c}", aggregate: "sum" },
                            "Total": { field: "TotalAmount", format: "{0:c}", aggregate: "sum" },
                        }
                    }
                },
                columns: [
                    //{ name: "Name" },
                    //{ name: "InvoiceDate"},
                    //{ name: "InvoiceTypeDetail"},
                    //{ name: "PatAmount" },
                    //{ name: "InsAmount" },
                ],
                rows: [
                    { name: "OfficeName", title: officeNameSetting },
                    { name: "InvoiceTypeDetail", label: "Type", title: "Type" },
                    { name: "Name" },
                    { name: "InvoiceDateString", title: "Invoice Date" },
                ],
                measures: ["Sum PatAmount", "Sum InsAmount", "Total"]
            }
        }).data("kendoPivotGrid");


        Dashboard.breakdownTableConfiguration = $("#configurator").kendoPivotConfigurator({
            dataSource: Dashboard.breakdownTable.dataSource,
            filterable: true,
            height: 570
        });
    },

    loadServiceTable: function() {
        if (Dashboard.breakdownTableServiceAnalysis != null) {
            try {
                Dashboard.breakdownTableServiceAnalysis.data("kendoGrid").dataSource.data([]);
                $("#breakdown-tableServiceAnalysis").html('');
            } catch (e) {

            }
        }

        if (Dashboard.breakdownListServiceAnalysis != null) {
            Dashboard.breakdownTableServiceAnalysis = $("#breakdown-tableServiceAnalysis").kendoGrid({
                dataSource: {
                    data: Dashboard.breakdownListServiceAnalysis,
                    group: [
                        { field: "OfficeName", dir: "asc" },
                        { field: "ProviderName", dir: "asc" },
                        { field: "CodeCategory", dir: "asc" },
                    ],
                    sort: {
                        field: "InvoiceDateString",
                        dir: "desc"
                    }
                },
                selectable: "multiple row",
                pageable: {
                    buttonCount: 5
                },
                scrollable: true,
                groupable: true,
                columns: [
                    {
                        field: "InvoiceDateString",
                        title: "Invoice Date"
                    }, {
                        field: "ServiceCode",
                        title: "Code",
                    }, {
                        field: "PatAmount",
                        title: patAmountSetting,
                        format: "{0:c}"
                    }, {
                        field: "InsAmount",
                        title: insAmountSetting,
                        format: "{0:c}"
                    },
                ]
            });
        }
    },

    loadCancellationAndNoShows: function() {
        if (Dashboard.breakdownTableCancellationAndNoShows != null) {
            try {
                Dashboard.breakdownTableCancellationAndNoShows.data("kendoGrid").dataSource.data([]);
                $("#breakdown-tableServiceAnalysis").html('');
            } catch (e) {

            }
        }

        if (Dashboard.breakdownListCancellationAndNoShows != null) {
            Dashboard.breakdownTableCancellationAndNoShows = $("#breakdown-tableServiceAnalysis").kendoGrid({
                dataSource: {
                    data: Dashboard.breakdownListCancellationAndNoShows,
                    group: [
                        { field: "OfficeName", dir: "asc" },
                        { field: "ProviderName", dir: "asc" },
                        { field: "CodeCategory", dir: "asc" },
                    ],
                    sort: {
                        field: "InvoiceDateString",
                        dir: "desc"
                    }
                },
                selectable: "multiple row",
                pageable: {
                    buttonCount: 5
                },
                scrollable: true,
                groupable: true,
                columns: [
                    {
                        field: "InvoiceDateString",
                        title: "Invoice Date"
                    }, {
                        field: "ServiceCode",
                        title: "Code",
                    }, {
                        field: "PatAmount",
                        title: patAmountSetting,
                        format: "{0:c}"
                    }, {
                        field: "InsAmount",
                        title: insAmountSetting,
                        format: "{0:c}"
                    },
                ]
            });
        }
    },

    chartSelection: function(id, name, chartTitle, position, order, addToDatabase) {
        var data = Dashboard.charts.filter(x => x.ID == id);

        if (data.length > 0) {

            var html = Dashboard[name];//Dashboard['NewPatient']
            html = html.replace(/{heading}/g, chartTitle);
            html = html.replace(/{order}/g, order);
            html = html.replace(/{name}/g, name);
            $(".droppable ." + position).append(html);
            Layout.showCardLoader('card-' + name);


            data = data[0];
            var chartType = data.Chart_Type;

            Dashboard.loadChartsData(name, chartType);



            if (chartType.includes('PieOnly')) {
                //Dashboard.PieChart(name + '-chart', null);
            } else
                if (chartType.includes('LineNCircular')) {
                    // Dashboard.BarChart(name + '-chart');
                } else if (chartType.includes('BarOnly')) {
                    //Dashboard.BarChart(name + '-chart');
                } else
                    if (chartType.includes('CircularOnly')) {

                    } else
                        if (chartType.includes('CardOnly')) {

                        } else {
                            Layout.hideCardLoader('card-' + name);
                        }
        } else {


        }

        if (addToDatabase) {
            $.ajax({
                type: "POST",
                url: "/Dashboard/AddCard",
                data: { chartId: id, addToDatabase: addToDatabase },
                success: function(html) {

                },
                error: function(errorData) {

                    Dashboard.onError(errorData);
                }
            });
        }

    },

    PieChart: function(id, data) {
        var model = [];
        for (var i = 0; i < data.length; i++) {
            model.push([data[i].xLabel, data[i].yValue]);
        }
        if (data == null) {
            var donut_chart_element = document.getElementById(id);
            if (donut_chart_element) {

                // Generate chart
                var donut_chart = c3.generate({
                    bindto: donut_chart_element,
                    //size: { width: 300 },
                    color: {
                        pattern: ['#3F51B5', '#FF9800', '#4CAF50', '#00BCD4', '#F44336']
                    },
                    data: {
                        columns: [
                            ['data1', 10],
                            ['data2', 40],
                            ['data3', 50],
                        ],
                        type: 'donut'
                    },
                    donut: {
                        //title: "Production Breakdown"
                    }
                });

                // Change data
                //setTimeout(function () {
                //    donut_chart.load({
                //        columns: [
                //            ["setosa", 0.2, 0.2, 0.2, 0.2, 0.2, 0.4, 0.3, 0.2, 0.2, 0.1, 0.2, 0.2, 0.1, 0.1, 0.2, 0.4, 0.4, 0.3, 0.3, 0.3, 0.2, 0.4, 0.2, 0.5, 0.2, 0.2, 0.4, 0.2, 0.2, 0.2, 0.2, 0.4, 0.1, 0.2, 0.2, 0.2, 0.2, 0.1, 0.2, 0.2, 0.3, 0.3, 0.2, 0.6, 0.4, 0.3, 0.2, 0.2, 0.2, 0.2],
                //            ["versicolor", 1.4, 1.5, 1.5, 1.3, 1.5, 1.3, 1.6, 1.0, 1.3, 1.4, 1.0, 1.5, 1.0, 1.4, 1.3, 1.4, 1.5, 1.0, 1.5, 1.1, 1.8, 1.3, 1.5, 1.2, 1.3, 1.4, 1.4, 1.7, 1.5, 1.0, 1.1, 1.0, 1.2, 1.6, 1.5, 1.6, 1.5, 1.3, 1.3, 1.3, 1.2, 1.4, 1.2, 1.0, 1.3, 1.2, 1.3, 1.3, 1.1, 1.3],
                //            ["virginica", 2.5, 1.9, 2.1, 1.8, 2.2, 2.1, 1.7, 1.8, 1.8, 2.5, 2.0, 1.9, 2.1, 2.0, 2.4, 2.3, 1.8, 2.2, 2.3, 1.5, 2.3, 2.0, 2.0, 1.8, 2.1, 1.8, 1.8, 1.8, 2.1, 1.6, 1.9, 2.0, 2.2, 1.5, 1.4, 2.3, 2.4, 1.8, 1.8, 2.1, 2.4, 2.3, 1.9, 2.3, 2.5, 2.3, 1.9, 2.0, 2.3, 1.8],
                //        ]
                //    });
                //}, 4000);
                //setTimeout(function () {
                //    donut_chart.unload({
                //        ids: 'data1'
                //    });
                //    donut_chart.unload({
                //        ids: 'data2'
                //    });
                //}, 8000);


            }
        } else {
            var donut_chart_element = document.getElementById(id);
            if (donut_chart_element) {

                // Generate chart
                var donut_chart = c3.generate({
                    bindto: donut_chart_element,
                    //size: { width: 300 },
                    color: {
                        pattern: ['#3F51B5', '#FF9800', '#4CAF50', '#00BCD4', '#F44336']
                    },
                    data: {
                        columns: model,
                        //json: data,
                        keys: {
                            x: 'xLabel',
                            value: ['yValue'],
                        },
                        type: 'donut'
                    },
                    donut: {
                        //title: "Production Breakdown"
                    }
                });

            }
        }

    },

    BarChart: function(id, data) {
        // Main variables
        $("#" + id).html('');
        var element = document.getElementById(id);
        var model = [];
        for (var i = 0; i < data.length; i++) {
            model.push({ "x": data[i].xLabel, "y-axis": data[i].yValue });
        }

        if (element) {

            // Construct chart
            var svg = dimple.newSvg(element, "100%");

            svg.attr("width", "100%")
                .attr("height", "100%");

            // Define chart
            var myChart = new dimple.chart(svg, model);

            // Set bounds
            myChart.setBounds("10%", 0, "90%", "100%")

            // Set margins
            myChart.setMargins(55, 5, 0, 50);

            // Create axes
            // ------------------------------

            // Horizontal
            var x = myChart.addCategoryAxis("x", "x");
            x.addOrderRule("Date");

            // Vertical
            var y = myChart.addMeasureAxis("y", "y-axis");
            // Add bars
            myChart.addSeries(null, dimple.plot.bar);

            // Font size
            x.fontSize = "12";
            y.fontSize = "12";

            // Font family
            x.fontFamily = "Roboto";
            y.fontFamily = "Roboto";

            // Draw
            myChart.draw();

            // Remove axis titles
            x.titleShape.remove();

            // Add a method to draw the chart on resize of the window
            $(window).on('resize', resize);
            $('.sidebar-control').on('click', resize);

            // Resize function
            function resize() {

                // Redraw chart
                myChart.draw(0, true);

                // Remove axis titles
                x.titleShape.remove();
            }
        }


        // Resize chart on sidebar width change
        $('.sidebar-control').on('click', function() {
            bar_chart.resize();
        });
    },

    removeCard: function(e) {

        var target = $(e),
            slidingSpeed = 150;

        var dataId = target.data('name');

        // If not disabled
        if (!target.hasClass('disabled')) {
            target.closest('.card').slideUp({
                duration: slidingSpeed,
                start: function() {
                    target.addClass('d-block');
                },
                complete: function() {
                    target.closest('.cardStart').remove();
                    var data = Dashboard.charts.filter(x => x.Name == dataId);
                    if (data.length > 0) {
                        data = data[0];

                        var currentChart = Dashboard.loadedCharts.filter(x => x.chartName == dataId);
                        if (currentChart.length > 0) {
                            Dashboard.loadedCharts.splice(Dashboard.loadedCharts.indexOf(currentChart[0]), 1);
                        }

                        Dashboard.RemoveCardFromdatabase(data.ID);

                        var html = Dashboard.selectionHTML
                            .replace(/{position}/g, data.Position)
                            .replace(/{order}/g, data.Order)
                            .replace(/{title}/g, data.Title)
                            .replace(/{id}/g, data.ID)
                            .replace(/{name}/g, dataId)
                            .replace(/{icon}/g, data.Icon);

                        $("#ChartSelectionOptions ." + data.Position).append(html);
                        Dashboard.initializeDragDrop();
                    }
                }
            });
        }
    },

    RemoveCardFromdatabase: function(chartId) {

        $.ajax({
            type: "POST",
            url: "/Dashboard/RemoveChart",
            data: { chartId: chartId },
            success: function(data) {
                if (data.Success) {

                }
            },
            error: function(errorData) {
                Dashboard.onError(errorData);
            }
        });
    },

    initializeDragDrop: function() {
        $(".draggable").draggable({
            revert: true
        });
        $(".droppable").droppable({
            drop: function(event, ui) {
                ui.draggable.detach();//.appendTo($(this))

                var chartId = ui.draggable.data('id');
                var chartName = ui.draggable.data('name');
                var chartTitle = ui.draggable.data('title');
                var position = ui.draggable.data('position');
                var order = ui.draggable.data('order');

                Dashboard.loadedCharts.push({ chartId: chartId, chartName: chartName, chartTitle: chartTitle, position: position, order: order });
                Dashboard.chartSelection(chartId, chartName, chartTitle, position, order, true);
            }
        });
        //debugger;
        //$(".draggable").draggable("disable");
    },

    reloadCharts: function() {
        var selectedProvider = $("#providerdropdown").val();

        var selectedOffices = $("#officedropdown").val();


        if (selectedProvider.length > 0 && selectedOffices.length > 0) {
            $.each(Dashboard.loadedCharts, function(i, item) {
                $("#cardStart-" + item.chartName).remove();
                Dashboard.chartSelection(item.chartId, item.chartName, item.chartTitle, item.position, item.order, false);
            });
        }
        else {
            $.each(Dashboard.loadedCharts, function(i, item) {
                $("#cardStart-" + item.chartName).remove();
            });
        }

    },

    onError: function(errorData) {
        console.log(errorData);
    },

    getPatientList: function() {

        if (Dashboard.loadedPatients != null) {

        }
        else {
            var selectedProvider = $("#providerdropdown").val();

            var selectedOffices = $("#officedropdown").val();

            Layout.showLoader();
            $.ajax({
                type: "POST",
                url: "/Dashboard/LoadPatientList",
                data: {
                    offices: selectedOffices,
                    providers: selectedProvider,
                    startDate: moment($('#dashboardCalendar').data('daterangepicker').startDate).format("YYYY-MM-DD"),
                    endDate: moment($('#dashboardCalendar').data('daterangepicker').endDate).format("YYYY-MM-DD")
                },
                success: function(data) {
                    if (data.Success) {
                        Dashboard.loadedPatients = data.Data;
                        var html = "";
                        $.each(data.Data, function(i, patient) {
                            html += '<a href="#" class="dropdown-item" onclick="Dashboard.loadPatientDetail(\'' + patient.Office_Sequence + '\',\'' + patient.PatientNumber + '\',\'' + patient.LastName + " " + patient.FirstName + '\',this)">' + patient.LastName + " " + patient.FirstName + '</a>';
                        });
                        $("#patientListDiv").html(html);
                        $("#patientCount").html(data.Data.length);

                        if (data.Data.length > 0) {
                            $("#patientListDiv a:first").click();
                        }
                    }
                    Layout.hideLoader();
                },
                error: function(errorData) {
                    Dashboard.onError(errorData);
                    Layout.hideLoader();
                }
            });
        }
    },

    loadPatientDetail: function(officeSequence, id, patientName, obj) {
        $("#patientListHeading").html(patientName);
        $("#patientListDiv a").removeClass('selectedPatient');
        $(obj).addClass('selectedPatient');
        Dashboard.loadPatientProfile(id, officeSequence);
        Dashboard.loadPatientAppointment(id, officeSequence);
        Dashboard.loadPatientTreatment(id, officeSequence);
    },

    loadPatientProfile: function(id, officeSequence) {
        Layout.showLoader();
        $.ajax({
            type: "POST",
            url: "/Dashboard/LoadPatientProfile",
            data: {
                id: id,
                officeSequence: officeSequence,
                //startDate: moment($('#dashboardCalendar').data('daterangepicker').startDate).format("YYYY-MM-DD"),
                //endDate: moment($('#dashboardCalendar').data('daterangepicker').endDate).format("YYYY-MM-DD")
            },
            success: function(data) {
                if (data.Success) {
                    var item = data.Data;
                    if (item != null) {
                        if (item.Age != null) {
                            if (!isNaN(item.Age)) {
                                try {
                                    $("#lblPatientAge").html(Number(item.Age).toFixed(0));
                                } catch (e) {

                                }
                            }
                        }

                        if (item.Sex.toLowerCase() == "f") {
                            $("#lblSex").html("Female");
                        } else {
                            $("#lblSex").html("Male");
                        }


                        $("#lblMaritialStatus").html(item.MaritalStatus);
                        $("#lblInsuranceProvider").html(item.InsuranceProvider);
                        $("#lblPatientAddress").html(item.Address);
                        $("#lblPatientHomePhone").html(item.HomeNumber);
                        $("#lblPatientWorkPhone").html(item.WorkPhone);
                        $("#lblPatientOtherPhone").html(item.OtherPhone);
                        $("#lblTotalOutstandingCashBalance").html('$XXXX'); //$
                        $("#lblTotalOutstandingInsuranceBalance").html('$XXXX'); //$
                        $("#lblOutstandingBalance").html('$XXXXX'); //$
                    }
                    else {
                        $("#lblPatientAge").html('');
                        $("#lblSex").html('');
                        $("#lblMaritialStatus").html('');
                        $("#lblInsuranceProvider").html('');
                        $("#lblPatientAddress").html('');
                        $("#lblPatientHomePhone").html('');
                        $("#lblPatientWorkPhone").html('');
                        $("#lblPatientOtherPhone").html('');
                        $("#lblTotalOutstandingCashBalance").html(''); //$
                        $("#lblTotalOutstandingInsuranceBalance").html(''); //$
                        $("#lblOutstandingBalance").html(''); //$
                    }

                    var html = "";
                    //$.each(data.Data, function (i, patient) {
                    //    html += '<a href="#" class="dropdown-item" onclick="Dashboard.loadPatientDetail("' + patient.PatientNumber + '")">' + patient.LastName + " " + patient.FirstName + '</a>';
                    //});

                }
                Layout.hideLoader();
            },
            error: function(errorData) {
                Dashboard.onError(errorData);
                Layout.hideLoader();
            }
        });
    },

    loadPatientAppointment: function(id, officeSequence) {
        $.ajax({
            type: "POST",
            url: "/Dashboard/LoadPatientAppointment",
            data: {
                id: id,
                officeSequence: officeSequence,
                startDate: moment($('#dashboardCalendar').data('daterangepicker').startDate).format("YYYY-MM-DD"),
                endDate: moment($('#dashboardCalendar').data('daterangepicker').endDate).format("YYYY-MM-DD")
            },
            success: function(data) {
                if (data.Success) {
                    var html = "";
                    var totalAmount = 0;
                    $.each(data.Data, function(i, appointment) {
                        totalAmount += appointment.Fee;
                        html += "<tr><td>" + appointment.InvoiceDate + "</td><td>" + appointment.Provider + "</td><td>" + appointment.Procedure + "</td><td>" + appointment.Status + "</td><td>" + appointment.Fee + "</td></tr>";
                    });
                    html += "<tr><td></td><td></td><td></td><td></td><td>$" + totalAmount + "</td></tr>";
                    $("#patientAppointmentDiv").html(html);
                }
            },
            error: function(errorData) {
                Dashboard.onError(errorData);
            }
        });
    },

    loadPatientTreatment: function(id, officeSequence) {
        $.ajax({
            type: "POST",
            url: "/Dashboard/LoadPatientTreatment",
            data: {
                id: id,
                officeSequence: officeSequence,
                startDate: moment($('#dashboardCalendar').data('daterangepicker').startDate).format("YYYY-MM-DD"),
                endDate: moment($('#dashboardCalendar').data('daterangepicker').endDate).format("YYYY-MM-DD")
            },
            success: function(data) {
                if (data.Success) {
                    var html = "";
                    //$.each(data.Data, function (i, patient) {
                    //    html += '<a href="#" class="dropdown-item" onclick="Dashboard.loadPatientDetail("' + patient.PatientNumber + '")">' + patient.LastName + " " + patient.FirstName + '</a>';
                    //});

                }
            },
            error: function(errorData) {
                Dashboard.onError(errorData);
            }
        });
    }
}

$(document).ready(function() {
    Dashboard.PageSetup();


    $("#chartBreakdown").on('show.bs.modal', function() {

        if (Dashboard.breakdownList != null) {
            $("#breakdownDetailDiv").show();
        }
        else {
            $("#breakdownDetailDiv").hide();
        }

        if (Dashboard.breakdownListServiceAnalysis != null) {
            $("#serviceAnalysisDetailDiv").show();
        } else {
            $("#serviceAnalysisDetailDiv").hide();
        }

        if (Dashboard.breakdownListCancellationAndNoShows != null) {
            $("#serviceAnalysisDetailDiv").show();
        } else {
            $("#serviceAnalysisDetailDiv").hide();
        }
        


        setTimeout(function() {

            if ($('[id="breakdownButtonSwitch"]').is(':checked')) {
                Dashboard.loadBasicTable();
            } else {
                Dashboard.loadAdvanceTable();
            }

            Dashboard.loadServiceTable();
        }, 1000);

    });
});