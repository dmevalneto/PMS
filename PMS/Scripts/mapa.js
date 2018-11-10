

var myLatLng = { lat: 0, lng: 0 };


function geocodeAddress(geocoder, resultsMap) {
    geocoder = new google.maps.Geocoder();
    var address = document.getElementById('txtEndereco').value;
    console.log(address);
    geocoder.geocode({ 'address': address }, function (results, status) {
        console.log(status)
        if (status === 'OK') {
            myLatLng = { lat: results[0].geometry.location.lat(), lng: results[0].geometry.location.lng() };
            setFormAddress(results);
            initMap();
        } else {
            alert('Não foi possível carregar o endereço');
        }
    });
}

function getLocation() {
    if (navigator.geolocation) {
        console.log("navigator")
        navigator.geolocation.getCurrentPosition(showPosition);
        $("#divMapa").show();
    } else {
        console.log("error")
        x.innerHTML = "Função não encontrada no navegador.";
    }
}

function showPosition(position) {
    console.log(position);
    myLatLng = { lat: position.coords.latitude, lng: position.coords.longitude };
    var latlng = new google.maps.LatLng(myLatLng.lat, myLatLng.lng);
    getAddress(latlng);
    initMap();
}

////EnviaAjax("/Ocorrencia/GetLocalizacao", latitude, longitude);
//function EnviaAjax(url, lat, long) {
//    $.ajax(url + "?Latitude=" + lat + "&Longitude=" + long, {
//        success: function (data) {
//        },
//        error: function () {
//        }
//    });
//}

function getAddress(LatLng) {
    geocoder = new google.maps.Geocoder();
    geocoder.geocode({ 'location': LatLng }, function (results, status) {
        if (status === 'OK') {
            setFormAddress(results);
        } else {
            alert('Não foi possível carregar o endereço');
        }
    });
}

function setFormAddress(results) {
    var components = {};
    var address_components = results[0].address_components;
    var address = results[0].formatted_address.split('-');
    var logradouro = address[0].split(',')[0];
    var numero = address[0].split(',')[1];


    jQuery.each(address_components, function (k, v1) { jQuery.each(v1.types, function (k2, v2) { components[v2] = v1.long_name }); });

    $("#txtEndereco").val(address[0])
    $('#latitude').val(results[0].geometry.location.lat());
    $('#longitude').val(results[0].geometry.location.lng());
    $('#logradouro').val(logradouro);
    $('#numero').val(numero);
    $('#bairro').val(components.sublocality);
    $('#cep').val(components.postal_code);
    $('#cidade').val(components.administrative_area_level_2);
    $('#estado').val(components.administrative_area_level_1);
}

var gmarkers = [];


function initMap() {
    var map = new google.maps.Map(document.getElementById('map'), {
        zoom: 20,
        center: myLatLng
    });

    if (myLatLng.lat != 0) {
        var latlng = new google.maps.LatLng(myLatLng.lat, myLatLng.lng);
        addMarker(latlng, map);
    }

    google.maps.event.addListener(map, 'click', function (event) {
        getAddress(event.latLng);
        addMarker(event.latLng, map);
    });
}

function addMarker(location, map) {
    removeMarkers();
    var marker = new google.maps.Marker({
        map: map,
        draggable: true,
        position: location,
        title: String(location)
    });

    gmarkers.push(marker);
}

function removeMarkers() {
    for (i = 0; i < gmarkers.length; i++) {
        gmarkers[i].setMap(null);
    }
}

