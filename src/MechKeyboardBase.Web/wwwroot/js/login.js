
let loginModule = (function() {
    let userInfo = window.localStorage;
    let loginSession = window.sessionStorage;
    let form = document.querySelector('form');
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
            if (status == 200) {
                userInfo.setItem('username', data.username);
                userInfo.setItem('token', data.token);
                userInfo.setItem('id', data.id);
                loginSession.setItem('state', 'loggedIn');
                window.location = "../page/userprofile.html";
            } else {
                alert("Login failed");
            }
        });
    };

    login.addEventListener('click', postLogin);


})();


let linkModule = (function() {
    let registerLinkButton = document.getElementById("signuplink");

    registerLinkButton.addEventListener('click', function(){
        window.location = "../page/register.html";
    });
})();