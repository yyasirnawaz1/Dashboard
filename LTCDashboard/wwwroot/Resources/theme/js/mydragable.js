const dragStart = (event) => {
    event.dataTransfer.setData("text/html", event.target.id);
};

const allowDrop = (event) => {
    event.preventDefault();
    //event.currentTarget.style.background = '#7f8082';
};

const drop = (event) => {
    event.preventDefault();
    const data = event.dataTransfer.getData("text/html");
    const element = document.querySelector(`#${data}`).cloneNode(true);
    // element.id = "newId";
    //event.currentTarget.style.background = 'white';
    try {
        //event.target.appendChild(element);
        g = document.createElement('div');
        const paneldivname = getrandomname();
        const chartdivname = getrandomname();

        //  Split Element Id and extract chart type & columns
        //  The Naming convention of div to be dragged should be like following
        //  chart_line_6 
        //  this shows first is chart which is not being used now
        //  second part is line, this will direct to plot a line chart
        //  third part is 6 , it shows number of columns to occupy
        var parts = element.id.split('_');
        var wgttype = parts[0];
        var chtype = parts[1];
        var cc = parts[2];

        console.log(wgttype);

        const upper = getchar(chtype);
        if (wgttype === 'overview') {
            $.ajax({
                url: '/Dashboard/OverViewWidget',
                type: "post",
                dataType: "html",
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(), //add parameter
                success: function (data) {
                    //success
                    g.innerHTML = data;
                    event.target.appendChild(g);
                },
                error: function () {
                    alert("error");
                }
            });
        }
        else if (wgttype === 'chart') {
            g.innerHTML = getchartbox(cc, paneldivname, upper, chartdivname);
            event.target.appendChild(g);
            var chart = new CanvasJS.Chart(chartdivname,
                {
                    //title: {
                    //    text: chtype + " Chart"  //**Change the title here
                    //},
                    data: [
                        {
                            type: chtype,
                            dataPoints: [
                                { x: 10, y: 71 },
                                { x: 20, y: 55 },
                                { x: 30, y: 50 },
                                { x: 40, y: 65 },
                                { x: 50, y: 95 },
                                { x: 60, y: 68 },
                                { x: 70, y: 28 },
                                { x: 80, y: 34 },
                                { x: 90, y: 14 }
                            ]
                        }
                    ]
                });

            chart.render();
        }



    } catch (error) {
        console.warn("you can't move the item to the same place");
        console.warn(error);
    }
};

function getchar(chtype) {
    const upper = chtype.replace(/^\w/, c => c.toUpperCase());
    return upper;
}

function getrandomname() {
    var length = 20;
    var tempname = '';
    var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXTZabcdefghiklmnopqrstuvwxyz'.split('');
    if (!length) {
        length = Math.floor(Math.random() * chars.length);
    }

    for (var i = 0; i < length; i++) {
        tempname += chars[Math.floor(Math.random() * chars.length)];
    }
    return tempname;
}


function getchartbox(cc, paneldivname, upper, chartdivname) {
    var sdiv = "<div class='col-md-" + cc + "'>"
        + "<div class='panel panel-default' id=" + paneldivname + ">"
        + "    <div class='panel-heading'>"
        + "        <h3 class='panel-title'>" + upper + " Chart</h3>"
        + "        <div class='panel-elements panel-elements-cp pull-right'>"
        + "            <button class='btn btn-default btn-sm btn-icon' onClick=app.panel.toggle('#" + paneldivname + "');><span class='fa fa-chevron-down'></span></button>"
        + "            <button class='btn btn-default btn-sm btn-icon' onClick=panelExpand('#" + paneldivname + "');><span class='fa fa-expand'></span></button>"
        + "        </div>"
        + "    </div>"
        + "	<div class='app-widget-tile app-widget-tile-primary app-widget-gradient margin-bottom-0'>"
        + "     <div class='line'>"
        + "            <div class='title'>Sales Per Year</div>"
        + "            <div class='title pull-right text-success'>+32.9%</div>"
        + "     </div>"
        + "     <div class='intval'>24,834</div>"
        + "     <div class='line'>"
        + "            <div class='subtitle'>Total items sold</div>"
        + "            <div class='subtitle pull-right text-success'><span class='icon-arrow-up'></span> good</div>"
        + "     </div>"
        + " </div>"
        + "    <div class='panel-body'>"
        + "        <div class='block ' style='height:300px;min-height:300px;width:100%'>"
        + "            <div id=" + chartdivname + " style='height:275px;min-height:275px;width:100%'>"
        + "            </div>"
        + "        </div>"
        + "    </div>"
        + "</div>"
        + "</div>";
    return sdiv;
}

function panelExpand(id) {
    app.panel.expand(id);
    window.dispatchEvent(new Event('resize'));
}