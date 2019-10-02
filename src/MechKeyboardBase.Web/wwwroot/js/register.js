
let registerModule = (function () {
    let password = document.getElementById("register_password");
    let confirmPassword = document.getElementById("register_confirm");
    let register = document.getElementById("registerbtn");
    let form = document.querySelector('form');

    let postRegister = function () {
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
                window.location = "../page/login.html";
            } else {
                alert("register fail");
            }
            return response.json();
        });
    };

    let validatePassword = function () {
        if(password.value != confirmPassword.value) {
            alert("Passwords Don't Match");
          } else {
            postRegister();
          }
    };

    register.addEventListener('click', validatePassword);

})();

let linkModule = (function() {
    let loginLinkButton = document.getElementById("loginlink");

    loginLinkButton.addEventListener('click', function(){
        window.location = "../page/login.html";
    });
})();