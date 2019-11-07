
let sendEmailConfirm = (function() {
    let email = document.getElementById('resetpassword_email');
    let sendLink = document.getElementById('sendemail');

    let sendConfirm = function () {
        var headers = {
            "Content-Type": "application/json"
        }

    fetch('/users/password', {
        method: 'POST',
        headers: headers,
        body: JSON.stringify(email.value)
    }).then(function(response) {
        if(response.status == 200) {
            alert("Email successfully sent!");
        } else {
            alert("Email did not send");
        }
    });
    };


    sendLink.addEventListener('click', sendConfirm);

})();






