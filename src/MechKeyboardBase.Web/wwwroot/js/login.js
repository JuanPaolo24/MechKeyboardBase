
function getFormData() {
    var form = document.querySelector('form');
    var data = new FormData(form);
    
    for (var pair of data.entries()) {
        console.log(pair[0]+ ', ' + pair[1]); 
    }
}

var localStorage= window.localStorage;

function postLogin() {

    var form = document.querySelector('form');
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
            localStorage.setItem('username', data.username);
            localStorage.setItem('token', data.token);
            window.location = "../page/userprofile.html";
        } else {
            alert("Login failed");
        }
    });
}

document.getElementById("loginbtn").onclick = function() {
    postLogin();
}


