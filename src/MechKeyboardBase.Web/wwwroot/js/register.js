var password = document.getElementById("register_password");
var confirmPassword = document.getElementById("register_confirm");
var register = document.getElementById("registerbtn");


function validatePassword() {
    if(password.value != confirmPassword.value) {
        return false;
      } else {
        return true;
      }
}


function postRegister() {

    var form = document.querySelector('form');
    const formData = new FormData(form);
    let jsonObject = {};

    formData.delete('confirmpassword');

    for (const [key, value]  of formData.entries()) {
        jsonObject[key] = value;
    }

    var headers = {
        "Content-Type": "application/json"                                                                                              
    }

    fetch('/users/register', {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(jsonObject)
    }).then(function (response) {
        if (response.status == 200) {
            alert("register successful");
        } else {
            alert("register fail");
        }
        return response.json();
    });
}



register.onclick = function() {
    if (validatePassword() == true) {
        postRegister();
    } else {
        alert("Passwords Don't Match");
    }
}


