
let checkAuthentication = (function (){
    let currentSession = sessionStorage.getItem('state');

    if(currentSession == null) {
        console.log("hit");
        window.location = "../page/login.html";
    } 
})();


let changeUserSettings = (function () {
    let form = document.querySelector('form');
    let saveAccount = document.getElementById('saveaccount');
    let userInfo = window.localStorage;

    let changeUser = function() {

        const formData = new FormData(form);
        let jsonObject = {};

        for (const [key, value]  of formData.entries()) {
            jsonObject[key] = value;
        }

        let headers = {
            "Authorization": "Bearer " + localStorage.getItem('token'),
            "Content-Type": "application/json",                                                                                                
            "Access-Control-Origin": "*"
        }

        fetch('/users/' + localStorage.getItem('id'), {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(jsonObject)
        }).then(function (response) {
            console.log(response);
            if(response.status == 200) {
                fetch('/api/keyboard', {
                    method: 'PATCH',
                    headers: headers,
                    body: JSON.stringify(jsonObject.username)
                }).then(function (response) {
                    if(response.status == 200) {
                        alert("Keyboards updated");
                    } else {
                        alert("Keyboards not updated");
                    }
                });
                userInfo.setItem('username', jsonObject.username);
                alert("User Information updated!");
                window.location.reload();
            } else {
                alert("User Information not updated!");
            }
        });
    };


    saveAccount.addEventListener('click', function() {
        changeUser();
    });

})();


let logoutModule = (function () {
    let logout = document.getElementById("logout");

    let clearSession = function() {
        localStorage.clear();
        sessionStorage.clear();
        window.location = "../index.html"
    };

    logout.addEventListener('click', clearSession);

})();

let checkEmailStatus = (function () {
    let emailstatus = sessionStorage.getItem('emailstatus');
    let statusDiv = document.getElementById('row2__status');

    if(emailstatus == 'notconfirmed') {
        let p = document.createElement('p');
        p.innerHTML = "Your email has not been activated yet. Please check your inbox for a link to get this done.";
        statusDiv.appendChild(p);
    }
    
})();

