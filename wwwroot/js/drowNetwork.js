

// create an array with edges
var myEdges = new vis.DataSet([
   edges
]);
function drow() {
    var myNodes = new vis.DataSet([
       nodes
    ]);
    // create a network
    var container = document.getElementById("mynetwork");
    var data = {
        nodes: myNodes,
        edges: myEdges,
    };
    var options = {};
    var network = new vis.Network(container, data, options);

    // Handler event when slider changes
    document.getElementById('slider').oninput = function () {
        // Get the value of the slider
        var sliderValue = this.value;

        // Display the selected value next to slider
        document.getElementById('sliderValueDisplay').innerHTML = sliderValue;

        // Loop through the edges
        edges.forEach(function (edge) {
            // Check if the edge should be displayed based on its weight
            if (edge.weight <= sliderValue) {
                // Edge should be displayed
                // Unhide edge if it's hidden and update DataSet
                if (edge.hidden) {
                    edge.hidden = false;
                    edges.update(edge);
                }
            } else {
                // Edge should not be displayed
                // Hide edge if not already hidden and update DataSet
                if (!edge.hidden) {
                    edge.hidden = true;
                    edges.update(edge);
                }
            }
        });
    }
}