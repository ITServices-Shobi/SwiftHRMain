$(function () {
    'use strict';

    //result = JSON.parse(result);
    //var labels = result['labels'].split(",");
    //var data = JSON.parse("[" + result['data'] + "]");

    // -----------------------
    // - MONTHLY EMPLOYEES CHART -
    // -----------------------

    // Get context with jQuery - using jQuery's .get() method.
    var salesChartCanvas = $('#swifthrcharts').get(0).getContext('2d');
    // This will get the first returned node in the jQuery collection.
    var salesChart = new Chart(salesChartCanvas);

    var months = ["Null","January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];

    
    var dataPerMonth = JSON.parse("[" + document.getElementById("chartData").value + "]");
    var monthNumbers = JSON.parse("[" + document.getElementById("chartMonths").value + "]");

    var monthsTo = months[monthNumbers[0]] + "," + months[monthNumbers[1]] + "," + months[monthNumbers[2]] + "," + months[monthNumbers[3]] + "," + months[monthNumbers[4]] + "," + months[monthNumbers[5]];
    var monthsToDisplay = monthsTo.split(',');
    //document.getElementById("TotalEmployees").textContent = monthNumbers[1];
       

    //var dataPerMonth = [45, 48, 40, 19, 86, 88]
    //var monthsToDisplay = ['A','B','C','D','E','F']

  
        var salesChartData = {
            labels: monthsToDisplay,
            datasets: [
                {
                    label: 'Digital Goods',
                    fillColor: 'rgba(60,141,188,0.9)',
                    strokeColor: 'rgba(60,141,188,0.8)',
                    pointColor: '#3b8bba',
                    pointStrokeColor: 'rgba(60,141,188,1)',
                    pointHighlightFill: '#fff',
                    pointHighlightStroke: 'rgba(60,141,188,1)',
                    data: dataPerMonth
                    
                }
            ]
        };

        var salesChartOptions = {
            // Boolean - If we should show the scale at all
            showScale: true,
            // Boolean - Whether grid lines are shown across the chart
            scaleShowGridLines: true,
            // String - Colour of the grid lines
            scaleGridLineColor: 'rgba(0,0,0,.05)',
            // Number - Width of the grid lines
            scaleGridLineWidth: 2,
            // Boolean - Whether to show horizontal lines (except X axis)
            scaleShowHorizontalLines: true,
            // Boolean - Whether to show vertical lines (except Y axis)
            scaleShowVerticalLines: true,
            // Boolean - Whether the line is curved between points
            bezierCurve: true,
            // Number - Tension of the bezier curve between points
            bezierCurveTension: 0.3,
            // Boolean - Whether to show a dot for each point
            pointDot: true,
            // Number - Radius of each point dot in pixels
            pointDotRadius: 4,
            // Number - Pixel width of point dot stroke
            pointDotStrokeWidth: 1,
            // Number - amount extra to add to the radius to cater for hit detection outside the drawn point
            pointHitDetectionRadius: 20,
            // Boolean - Whether to show a stroke for datasets
            datasetStroke: true,
            // Number - Pixel width of dataset stroke
            datasetStrokeWidth: 2,
            // Boolean - Whether to fill the dataset with a color
            datasetFill: true,
            // String - A legend template
            legendTemplate: '<ul class=\'<%=name.toLowerCase()%>-legend\'><% for (var i=0; i<datasets.length; i++){%><li><span style=\'background-color:<%=datasets[i].lineColor%>\'></span><%=datasets[i].label%></li><%}%></ul > ',
// Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
maintainAspectRatio: true,
    // Boolean - whether to make the chart responsive to window resizing
    responsive: true
        };

// Create the line chart
salesChart.Line(salesChartData, salesChartOptions);
});