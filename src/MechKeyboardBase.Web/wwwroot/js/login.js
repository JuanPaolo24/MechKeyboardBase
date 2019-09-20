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

var next = document.getElementById("next");
var previous = document.getElementById("previous");

var row2left = document.getElementById("row2__leftdescription");
var row2right = document.getElementById("row2__rightdescription");
var row3left = document.getElementById("row3__leftdescription");
var row3right = document.getElementById("row3__rightdescription");

var row2leftimg = document.getElementById("row2__leftimage");
var row2rightimg = document.getElementById("row2__rightimage");
var row3leftimg = document.getElementById("row3__leftimage");
var row3rightimg = document.getElementById("row3__rightimage");


var rowArray = [row2left, row2right, row3left, row3right];
var imageArray = [row2leftimg, row2rightimg, row3leftimg, row3rightimg];


document.addEventListener("DOMContentLoaded", function () {
    
    var pageNumber = 1;
    fetchCurrentPage(pageNumber);
});

///https://codepen.io/chichichi/pen/boXOKv Refer to this for pure pagination

function clearView() {
    for(var i= 0; i < 4; i++) {
        rowArray[i].innerHTML = "";
        imageArray[i].innerHTML = "";
    }
}


next.onclick = function() {
    clearView();
    fetchCurrentPage(2);
}

previous.onclick = function() {
    clearView();
    fetchCurrentPage(1);
}



function setPaginationSettings() {
    fetch('/api/keyboard')
    .then(function (response) {
        return response.json();
    })
    .then(function (keybs) {

        var pagesContainer = document.getElementById("pages");

        var numberOfPage = Math.ceil(keybs.length / 4);
        console.log(numberOfPage);
        
        for(var i=0; i<numberOfPage; i++) {
            var a = document.createElement('a');
            a.innerHTML = i + 1;
            pagesContainer.appendChild(a);
        }

    });
}


function fetchCurrentPage(pageNumber) {
    var pageSettings = '?number=' + pageNumber + '&size=4';
    fetch('/api/keyboard/page' + pageSettings)
    .then(function (response) {
        return response.json();
    })
    .then (function (keyboards) {
        
        for(var i= 0; i < keyboards.length; i++) {

            var img = document.createElement('img');
            img.src = keyboards[i].imageUrl;
            img.height = 300;
            img.width = 500;
            img.className = "rounded-corners";
            imageArray[i].appendChild(img);
            
            var keyboardComponents = [
	            keyboards[i].keyboardName,
	            keyboards[i].case,
	            keyboards[i].pcb,
	            keyboards[i].plate,
	            keyboards[i].keycaps,
	            keyboards[i].switch,
	            keyboards[i].username
            ];
            
            keyboardComponents.forEach(function (keyboard) {
                var p = document.createElement('p');
                p.innerHTML = keyboard;
                rowArray[i].appendChild(p);
            });
            
        }
    });
}



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
