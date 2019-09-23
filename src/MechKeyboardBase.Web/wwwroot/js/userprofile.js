var addButton = document.getElementById("addbtn");
var keyboardForm = document.getElementById("addkeyboard-form");
var modalwrapper = document.getElementById("modal__container");
var closeButton = document.getElementById("closeaddkeyboardform");
var logout = document.getElementById("logout");
var saveBtn = document.getElementById("savekeyboardbtn");

addButton.onclick = function() {
    keyboardForm.style.display = "block";
    modalwrapper.style.display = "block";
}

closeButton.onclick = function() {
    keyboardForm.style.display = "none";
    modalwrapper.style.display = "none";
}


window.onclick = function(event) {
    if (event.target == modalwrapper) {
        this.modalwrapper.style.display = "none";
        this.keyboardForm.style.display = "none";
    }
}


document.addEventListener("DOMContentLoaded", function() {
    userSettings();
});


function userSettings() {
    var title = document.getElementById("row1__title");

    var p = document.createElement('p');
    p.innerHTML = "Welcome to your Collection " + localStorage.getItem('username');
    title.appendChild(p);

}
 
logout.onclick = function() {
    localStorage.clear();
    window.location = "../index.html"
}


function saveKeyboard() {

    var form = document.querySelector('form');
    const formData = new FormData(form);
    let jsonObject = {};

    for (const [key, value]  of formData.entries()) {
        jsonObject[key] = value;
    }

    var headers = {
        "Authorization": "Bearer " + localStorage.getItem('token'),
        "Content-Type": "application/json",                                                                                                
        "Access-Control-Origin": "*"
    }


    fetch('/api/keyboard', {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(jsonObject)
    }).then(function (response) {
        console.log(response.status);
        if (response.status == 200) {
            alert("Keyboard added!");
        } else {
            alert("Keyboard not added!");
        }
        return response.json();
    });
}


saveBtn.onclick = function() {
    saveKeyboard();
}