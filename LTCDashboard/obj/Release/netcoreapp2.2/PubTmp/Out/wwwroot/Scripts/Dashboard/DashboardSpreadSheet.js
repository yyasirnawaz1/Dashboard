var DashboardSpreadSheet = {
    spreadsheetTable: null,

    loadChartSpreadSheetData: function (chartName) {
        Dashboard.setting.isBreakdownTable = false;
        Dashboard.setting.isSpreadsheetTale = true;
        Dashboard.setting.loadedChartName = chartName;

        Dashboard.loadModelData(chartName);
    },

    setupSpreadSheetGrid: function () {
      
        if (Dashboard.chartsDetailData != null) {
            DashboardSpreadSheet.spreadsheetTable = $("#spreadsheet-Table").kendoSpreadsheet({
                //columns: 6,
                //rows: [Dashboard.chartsDetailData.length],
                //toolbar: true,
                //sheetsbar: true,
                excel: {
                    proxyURL: "/Dashboard/Index_Proxy"
                },
                pdf: {
                    proxyURL: "/Dashboard/Index_Proxy"
                },
                sheets: [{
                    name: "Service Analysis",
                    rows: [...DashboardSpreadSheet.organizeResults(Dashboard.chartsDetailData, Dashboard.setting.loadedChartName)],
                    columns: [{
                        width: 100
                    }, {
                        width: 215
                    }, {
                        width: 115
                    }, {
                        width: 115
                    }, {
                        width: 115
                    }, {
                        width: 115
                    }, {
                        width: 115
                    }, {
                        width: 155
                    }]
                }]
            });
        }
    },

    organizeResults: function (data, chartName) {
        debugger;
        if (chartName == "ServiceAnalysis") {
            return DashboardSpreadSheet.organizedDataForServiceAnalysisChart(data);
        } else if (chartName == "CancellationAndNoShows") {
            return DashboardSpreadSheet.organizedDataForCancellationAndNoShowsChart(data);
        } else if (chartName == "TotalNetProduction" ||
            chartName == "TotalNetHygenistProduction" ||
            chartName == "TotalNetDoctorProduction" ||
            chartName == "AverageProductionPerPatient" ||
            chartName == "TotalPaymentReceipt" ||
            chartName == "TotalNetPaymentReceipt") {
            return DashboardSpreadSheet.organizedDataForSmallCharts(data);
        }
        return null;
    },

    organizedDataForServiceAnalysisChart: function (data) {
        var resultset = [];
        resultset.push({
            height: 25,
            cells: [
                {
                    value: "Office Name",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Provider Name",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Code Category",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Service Code",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Invoice Date",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                }, {
                    value: "Invoice Number",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "PatAmount",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "InsAmount",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                }
            ]
        });

        for (var i = 0; i < data.length; i++) {
            var dataSet = data[i];
            var record = {
                cells: [
                    {
                        value: dataSet.OfficeName,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.ProviderName,
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.CodeCategory,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.ServiceCode,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.InvoiceDateString,
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.InvoiceNumber,
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.PatAmount,
                        format: "$#,##0.00",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.InsAmount,
                        format: "$#,##0.00",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }
                ]
            };
            resultset.push(record);
        }
        return resultset;
    },

    organizedDataForCancellationAndNoShowsChart: function (data) {
        var resultset = [];
        resultset.push({
            height: 25,
            cells: [
                {
                    value: "Office Name",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Provider Name",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Patient Name",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Contact Date",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Job",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                }, {
                    value: "Time",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                }
            ]
        });

        for (var i = 0; i < data.length; i++) {
            var dataSet = data[i];
            var record = {
                cells: [
                    {
                        value: dataSet.OfficeName,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.ProviderName,
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.PatientName,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.ContactDateString,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.Job,
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.TimeString,
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }
                ]
            };
            resultset.push(record);
        }
        return resultset;
    },

    organizedDataForSmallCharts: function (data) {
        var resultset = [];
        resultset.push({
            height: 25,
            cells: [
                {
                    value: officeNameSetting,
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Invoice Date",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Invoice Type",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: "Patient",
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                },
                {
                    value: patAmountSetting,
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                }, {
                    value: insAmountSetting,
                    background: "rgb(167,214,255)",
                    textAlign: "center",
                    color: "rgb(0,62,117)"
                }
            ]
        });

        for (var i = 0; i < data.length; i++) {
            var dataSet = data[i];
            var record = {
                cells: [
                    {
                        value: dataSet.OfficeName,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.InvoiceDateString,
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.InvoiceTypeDetail,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.Name,
                        textAlign: "center",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.PatAmount,
                        format: "$#,##0.00",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }, {
                        value: dataSet.InsAmount,
                        format: "$#,##0.00",
                        background: "rgb(255,255,255)",
                        color: "rgb(0,62,117)"
                    }
                ]
            };
            resultset.push(record);
        }
        return resultset;
    }
}