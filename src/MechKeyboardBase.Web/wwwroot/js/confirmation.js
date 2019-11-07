let getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

let confirmEmail = (function() {
    let id = getUrlParameter('id');
    let userToken = getUrlParameter('userToken');
    let notification = document.getElementById('notification');

    var headers = {
        "Content-Type": "application/json"
    }

    fetch('/users/confirm?id=' + id + '&userToken=' + userToken, {
        method: 'POST',
        headers: headers
    }).then(function(response) {
        if(response.status == 200) {
            let message = document.createElement('p');
            message.innerHTML = "Account Confirmed!"
            notification.appendChild(message);
        } 

    });

})();