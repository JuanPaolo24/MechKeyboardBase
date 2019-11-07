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



let postNewPassword = (function(){
    let token = getUrlParameter('token');
    let password = document.getElementById('change_password');
    let confirmPassword = document.getElementById('change_confirm');
    let saveNewPassword = document.getElementById('savepassword');

    let postPassword = function () {


    var headers = {
        "Content-Type": "application/json"                                                                                              
    }

    fetch('/users/resetpassword?token=' + token, {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(password.value)
    }).then(function(response){
        if(response.status == 200) {
            alert("Password successfully changed!");
            window.location.reload();
        } else {
            alert("Password not changed");
        }
    });
    }

    
    let validatePassword = function () {
        if(password.value != confirmPassword.value) {
            alert("Passwords Don't Match");
          } else {
            postPassword();
          }
    };
    
    saveNewPassword.addEventListener('click', validatePassword);

})();
