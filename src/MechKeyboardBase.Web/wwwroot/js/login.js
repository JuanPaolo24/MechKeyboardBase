var loginform = document.getElementById("loginform");
var signupform = document.getElementById("signupform");
var login = document.getElementById("login");
var logout = document.getElementById("logout");
var signuplink = document.getElementById("signuplink");
var loginlink = document.getElementById("loginlink");
var closeLogin = document.getElementById("closeloginform");
var closeSignup = document.getElementById("closesignupform");
var modalwrapper = document.getElementById("modal__container");
var loginbutton = document.getElementById("loginbtn");




document.addEventListener("DOMContentLoaded", function () {
    fetch('/api/keyboard')
    .then(function (response) {
        return response.json();
    })
    .then (function (keyboards) {
        var row2description = document.getElementById("row2__leftdescription");

        var keyboardComponents = [
            keyboards[0].keyboardName,
            keyboards[0].case,
            keyboards[0].pcb,
            keyboards[0].plate,
            keyboards[0].keycaps,
            keyboards[0].switch,
            keyboards[0].username
        ];

        keyboardComponents.forEach(function(components) {
            var p = document.createElement('p');
            p.innerHTML = components;
            row2description.appendChild(p);
        });


    });

});



function showLoginForm() {
    loginform.style.display = "block";
    modalwrapper.style.display = "block";
}

function hideLoginForm() {
    loginform.style.display = "none";
    modalwrapper.style.display = "none";
}

function showSignupForm() {
    signupform.style.display = "block";
    modalwrapper.style.display = "block";
}

function hideSignupForm() {
    signupform.style.display = "none";
    modalwrapper.style.display = "none";
}

window.onclick = function(event) {
    if (event.target == modalwrapper) {
        this.modalwrapper.style.display = "none";
        if (signupform.style.display == "block") {
            signupform.style.display = "none";
        }

        if (loginform.style.display == "block") {
            loginform.style.display = "none";
        }
    }
}

closeLogin.onclick = function() {
    hideLoginForm();
}

closeSignup.onclick = function() {
    hideSignupForm();
}


login.onclick = function() {
    showLoginForm();
};


signuplink.onclick = function() {
    loginform.style.display = "none";
    showSignupForm();
}

loginlink.onclick = function() {
    signupform.style.display = "none";
    showLoginForm();
}

loginbutton.onclick = function() {
    location.href = "page/userauthenticated.html";
}


logout.onclick = function() {
    location.href = "../index.html";
}
