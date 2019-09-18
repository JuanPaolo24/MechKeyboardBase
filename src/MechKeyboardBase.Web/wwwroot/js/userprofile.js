var addButton = document.getElementById("addbtn");
var keyboardForm = document.getElementById("addkeyboard-form");
var modalwrapper = document.getElementById("modal__container");
var closeButton = document.getElementById("closeaddkeyboardform");

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