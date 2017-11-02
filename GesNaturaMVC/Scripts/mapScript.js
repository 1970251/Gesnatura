(function (window, google) {

    //map options
    var options = {
        center: {
            lat: 41.00077,
            lng: -8.095855
            
        },
        zoom: 10
    },
    element = document.getElementById('map'),
    //map
    map = new google.maps.Map(element, options);

}(window, google));
