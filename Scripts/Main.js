//javascript.js
//set map options
var myLatLng = { lat: 10.25005493563659, lng: 106.99757784043774 };
var mapOptions = {
    center: myLatLng,
    zoom: 8,
    mapTypeId: google.maps.MapTypeId.ROADMAP

};

//create map
var map = new google.maps.Map(document.getElementById('googleMap'), mapOptions);

//create a DirectionsService object to use the route method and get a result for our request
var directionsService = new google.maps.DirectionsService();

//create a DirectionsRenderer object which we will use to display the route
var directionsDisplay = new google.maps.DirectionsRenderer();

//bind the DirectionsRenderer to the map
directionsDisplay.setMap(map);


//define calcRoute function
function calcRoute() {
    //create request
    var request = {
        origin: document.getElementById("from").value,
        destination: document.getElementById("to").value,
        travelMode: google.maps.TravelMode.DRIVING, //WALKING, BYCYCLING, TRANSIT
        unitSystem: google.maps.UnitSystem.IMPERIAL
    }

    //pass the request to the route method
    directionsService.route(request, function (result, status) {
        if (status == google.maps.DirectionsStatus.OK) {
            /*let batdau = document.getElementById("from").value*/
            //Get distance and time
            const output = document.querySelector('#output');
            output.innerHTML = "<div class='alert-info'>Điểm xuất phát: " + document.getElementById("from").value
                + ".<br />Điểm đến: " + document.getElementById("to").value
                + ".<br /> Khoảng cách <i class='fas fa-road'></i> : " + Math.round((result.routes[0].legs[0].distance.value) / 1000) + "km"
                + ".<br />Ước tính <i class='fas fa-hourglass-start'></i> : " + result.routes[0].legs[0].duration.text
                + ".<br />Giá tiền  </i> : " + Math.round((result.routes[0].legs[0].distance.value) / 1000) * 20000+ "vnđ"
                + ".</div>";
            //display route
            directionsDisplay.setDirections(result);
        } else {
            //delete route from map
            directionsDisplay.setDirections({ routes: [] });
            //center map in London
            map.setCenter(myLatLng);

            //show error message
            output.innerHTML = "<div class='alert-danger'><i class='fas fa-exclamation-triangle'></i> Could not retrieve driving distance.</div>";
        }
    });

}

//create autocomplete objects for all inputs
var option = {
    types: ['(cities)']
}
var input1 = document.getElementById("from");
var autocomplete1 = new google.maps.places.Autocomplete(input1, option);

var input2 = document.getElementById("to");
var autocomplete2 = new google.maps.places.Autocomplete(input2, option);
