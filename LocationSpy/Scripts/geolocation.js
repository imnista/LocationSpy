// The new version of doUserLocation(), since 2013.05.
// http://nap7.com
// Rely on Jquery and JS lib http://api.map.baidu.com/api?v=2.0&ak=
function exeCurrentLocation(callback) {

    getCurrentPositionByBaiduMap(callback);

    // Get location by Baidu Geolocation service
    function getCurrentPositionByBaiduMap(callback) {
        var geolocation = new BMap.Geolocation();
        geolocation.enableSDKLocation();
        geolocation.getCurrentPosition(function (r) {
            if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                console.log("# Get location by Baidu Map Geolocation");
                callback(r.point.lat, r.point.lng);
            } else {
                console.warn("# Failed to get location by Baidu Map Geolocation");
                getCurrentPositionByGeolocation(callback);
            }
        }, { enableHighAccuracy: true });
    }

    // If failed again, try HTML5 Geolocation
    function getCurrentPositionByGeolocation() {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function(position) {
                    console.log("# Get location by HTML5 Geolocation");
                    callback(position.coords.latitude, position.coords.longitude);
                },
                function(error) {
                    console.warn("# Failed to get location by HTML5 Geolocation, message: " + error.message);
                    getCurrentPositionByIP(callback);
                },
                {
                    enableHighAcuracy: false,
                    timeout: 5000,
                    maximumAge: 60000
                });
        } else {
            console.warn("# Failed to get location by HTML5 Geolocation, not supported.");
            getCurrentPositionByIP(callback);
        }
    }

    // If failed again, try IPInfo.io
    function getCurrentPositionByIP(callback) {
        // http://api.jquery.com/jQuery.get/
        $.get("http://ipinfo.io",
            function(response) {
                if (response.loc) {
                    console.log("# Get location by IP");
                    var location = response.loc;
                    var locationArr = location.split(",");
                    callback(locationArr[0], locationArr[1]);
                    return;
                } else {
                    console.error("# Failed to get location by IP.");
                }
            },
            "jsonp").fail(function() {
            console.error("# Failed to get location by IP.");
        });
    }
};