function getLocation() {
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(showPosition);
    }
}

function showPosition(position) {
    let fieldLatitude = document.querySelector("#latitude"),
        fieldLongitude = document.querySelector("#longitude");

    fieldLatitude.value = position.coords.latitude;
    fieldLongitude.value = position.coords.longitude;
}

window.addEventListener("load", function () {
    getLocation();
});