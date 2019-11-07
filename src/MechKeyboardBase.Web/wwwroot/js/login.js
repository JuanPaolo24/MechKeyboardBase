
let loginModule = (function() {
    let userInfo = window.localStorage;
    let loginSession = window.sessionStorage;
    let form = document.getElementById("form__login");
    let login = document.getElementById("loginbtn");

    let postLogin = function() {
        const formData = new FormData(form);
        let jsonObject = {};

        for (const [key, value]  of formData.entries()) {
            jsonObject[key] = value;
        }

        var headers = {
            "Content-Type": "application/json",                                                                                                
            "Access-Control-Origin": "*"
        }

        var status;

        fetch('/users/authenticate', {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(jsonObject)
        }).then(function (response) {
            status = response.status;
            return response.json();
        }).then(function(data) {
            switch(status) {
                case 200:
                    userInfo.setItem('username', data.username);
                    userInfo.setItem('token', data.token);
                    userInfo.setItem('id', data.id);
                    loginSession.setItem('emailstatus', 'confirmed');
                    loginSession.setItem('state', 'loggedIn');
                    window.location = "profile.html";
                  break;
                case 202:
                    userInfo.setItem('username', data.username);
                    userInfo.setItem('token', data.token);
                    userInfo.setItem('id', data.id);
                    loginSession.setItem('emailstatus', 'notconfirmed');
                    loginSession.setItem('state', 'loggedIn');
                    window.location = "profile.html";
                  break;
                default:
                  alert("Login failed");
              }
        });
    };

    login.addEventListener('click', postLogin);


})();


let linkModule = (function() {
    let registerLinkButton = document.getElementById("signuplink");
    let forgetLinkButton = document.getElementById("forgetpasswordlink");

    registerLinkButton.addEventListener('click', function(){
        window.location = "register.html";
    });

    forgetLinkButton.addEventListener('click', function() {
        window.location = "resetpassword.html";
    })
})();