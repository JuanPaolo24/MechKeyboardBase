
let Main = (function (){

    const next = document.getElementById("next");
    const previous = document.getElementById("previous");

    let keyboardContainer = document.getElementById("keyboard__profile");

    let currentPage = 1;

    let page_number = document.getElementById('pages').getElementsByClassName('clickPageNumber'); 

    let currentPageSize = window.screen.availWidth > 1024 ? 3 : 2;

    let currentForm;
    let editBtn = document.getElementById("editkeyboardbtn");
    let deleteBtn = document.getElementById("deletekeyboardbtn");
    let form = document.querySelector('form');

    let fetchCurrentPage = function(pageNumber) {
        var pageSettings = '?number=' + pageNumber + '&size=' + currentPageSize;
        fetch('/api/keyboard/profile/' + localStorage.getItem('username') + "/page" + pageSettings)
        .then(function (response) {
            return response.json();
        })
        .then (function (keyboards) {

        for(var i= 0; i < keyboards.length; i++) {

        var keyboardItem = document.createElement('div');
        keyboardItem.className = "keyboard__item";
        keyboardItem.id = "keyboard" + i;

        var img = document.createElement('img');
        img.className = "background__img";
        img.src = keyboards[i].imageUrl;

        var editimg = document.createElement('img');
        editimg.src = "resources/images/pencil-edit-button.svg";
        editimg.className = "edit__img";
        editimg.id = "editbutton" + i;

        var description = document.createElement('div');
        description.className = "keyboard__info";
        description.id = "keyboard__description" + i;
        
        editimg.addEventListener('click', function() {
            let targetButton = event.target.id.substring(event.target.id.length-1);
            let currentDescription = document.getElementById("keyboard__description" + targetButton);
            currentForm = currentDescription;
            fillForm(currentDescription);
            keyboardFormModule().showEditKeyboardForm();
        });

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
            description.appendChild(p);
        });

        keyboardItem.appendChild(img);
        keyboardItem.appendChild(editimg);
        keyboardItem.appendChild(description);
        keyboardContainer.appendChild(keyboardItem);
            }
    
            selectedPage();
            deleteBtn.addEventListener('click', function() {
                deleteKeyboard(currentForm);
            });

            editBtn.addEventListener('click', function() {
                editKeyboard(currentForm);
            });
            
        });
    
    }

    let fillForm = function(description) {

        let keyboardName = document.getElementById("keyboardName");
        let keyboardCase = document.getElementById("keyboardCase");
        let pcb = document.getElementById("pcb");
        let plate = document.getElementById("plate");
        let keycaps = document.getElementById("keycaps");
        let keyboardSwitch = document.getElementById("switch");

        let keyboardDetails = [keyboardName, keyboardCase, pcb, plate, keycaps, keyboardSwitch, image];

        for (let i = 0; i < keyboardDetails.length - 1; i++) {
            keyboardDetails[i].value = description.getElementsByTagName('p')[i].innerHTML;
            keyboardDetails[6].value = "";
        }
    };

    let editKeyboard = function(description) {
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

        let keyboardTitle = description.getElementsByTagName('p')[0].innerHTML;

        
        fetch('/api/keyboard/?name=' + keyboardTitle, {
            method: 'PUT',
            headers: headers,
            body: JSON.stringify(jsonObject)
        }).then(function (response) {
            if (response.status == 200) {
                alert("Keyboard Edited!");
                
            }  else {
                alert("Keyboard unabled to be edited!");
            }
            window.location.reload();
        });
    };

    let deleteKeyboard = function(description) {
        let headers = {
            "Authorization": "Bearer " + localStorage.getItem('token'),
            "Content-Type": "application/json",                                                                                                
            "Access-Control-Origin": "*"
        }

        let keyboardTitle = description.getElementsByTagName('p')[0].innerHTML;
    
        fetch('/api/keyboard/?keyboardname=' + keyboardTitle, {
            method: 'DELETE',
            headers: headers
        }).then(function (response) {
            if (response.status == 202) {
                alert("Keyboard deleted!");
                
            }  else {
                alert("Keyboard not deleted!");
            }
            window.location.reload();
        });
    };
    
    
    let selectedPage = function() {
      for (let i = 0; i < page_number.length; i++) {
          if (i == currentPage - 1) {
                page_number[i].style.backgroundColor = "black";
                page_number[i].style.color = "white";
          } 
          else {
                page_number[i].style.backgroundColor = "white";
                page_number[i].style.color = "black";
          }
      }  
    };
    
    let clickPage = function() {
        document.addEventListener('click', function(e) {
            if(e.target.nodeName == "SPAN" && e.target.classList.contains("clickPageNumber")) {
                currentPage = e.target.textContent;
                keyboardContainer.innerHTML = "";
                fetchCurrentPage(currentPage);
    
            }
        });
    }
    
    
    let paginationSettings = function() {
        fetch('/api/keyboard/userprofile?username=' + localStorage.getItem('username'))
        .then(function (response) {
            return response.json();
        })
        .then(function (keybs) {
    
        var pagesContainer = document.getElementById("pages");
    
        var numberOfPage = Math.ceil(keybs.length / currentPageSize);
    
        for(var i=1; i<numberOfPage + 1; i++) {
            pagesContainer.innerHTML += "<span class='clickPageNumber'>" + i + "</span>";
        }
    
        page_number[0].style.backgroundColor = "black";
        page_number[0].style.color = "white";
    
        next.onclick = function() {
            if(currentPage < numberOfPage) {
                currentPage++;
                keyboardContainer.innerHTML = "";
                fetchCurrentPage(currentPage);
            }
        }
    
    
        previous.onclick = function() {
            if(currentPage > 1) {
                currentPage--;
                keyboardContainer.innerHTML = "";
                fetchCurrentPage(currentPage);
            }
        }   
    
        });
    }

    fetchCurrentPage(1);
    paginationSettings();
    clickPage();

});

let keyboardFormModule = (function () {
    let addButton = document.getElementById("addnew");
    let closeButton = document.getElementById("closeaddkeyboardform");
    let keyboardForm = document.getElementById("popupform__addandedit");
    let modalwrapper = document.getElementById("modal__container");

    let savekeyboardBtn = document.getElementById("savekeyboardbtn");
    let editkeyboardBtn = document.getElementById("editkeyboardbtn");
    let deletekeyboardBtn = document.getElementById("deletekeyboardbtn");

    let formTitle = document.getElementById("popupform__title");
    let formDescription = document.getElementById("popupform__description");

    let showAddKeyboardForm = function () {
        savekeyboardBtn.style.display = "inline";
        editkeyboardBtn.style.display = "none";
        deletekeyboardBtn.style.display = "none";
        formTitle.innerHTML = "Add keyboard";
        formDescription.innerHTML = "Add a new keyboard to your collection";
        keyboardForm.style.display = "block";
        modalwrapper.style.display = "block";
    };

    let showEditKeyboardForm = function () {
        editkeyboardBtn.style.display = "inline";
        savekeyboardBtn.style.display = "none";
        deletekeyboardBtn.style.display = "inline";
        formTitle.innerHTML = "Edit keyboard";
        formDescription.innerHTML = "Edit an existing keyboard from your collection"
        keyboardForm.style.display = "block";
        modalwrapper.style.display = "block";
    }

    let hideKeyboardForm = function () {
        keyboardForm.style.display = "none";
        modalwrapper.style.display = "none";
    }

    let showModal = function (event) {
        if (event.target == modalwrapper) {
            modalwrapper.style.display = "none";
            keyboardForm.style.display = "none";
        }
    }

    addButton.addEventListener('click', showAddKeyboardForm);
    closeButton.addEventListener('click', hideKeyboardForm);
    window.addEventListener('click', showModal);

    return {
        showEditKeyboardForm : showEditKeyboardForm
    };

});

let userSettingsModule = (function () {
    let title = document.getElementById("page__title");

    let p = document.createElement('p');
    p.innerHTML = "Welcome to your Collection " + localStorage.getItem('username');
    title.appendChild(p);

});

let MenuModule = (function () {
    let logout = document.getElementById("logout");

    let clearSession = function() {
        localStorage.clear();
        sessionStorage.clear();
        window.location = "index.html"
    };

    logout.addEventListener('click', clearSession);

});
 
let addKeyboardModule = (function () {
    let save = document.getElementById("savekeyboardbtn");
    let form = document.querySelector('form');

    let saveKeyboard = function() {

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

        fetch('/api/keyboard', {
            method: 'POST',
            headers: headers,
            body: JSON.stringify(jsonObject)
        }).then(function (response) {
            console.log(response.status);
            if (response.status == 200) {
                form.reset();
                alert("Keyboard added!");
                window.location.reload();
            } else {
                alert("Keyboard not added!");
            }
            return response.json();
        });
    };

    save.addEventListener('click', saveKeyboard);
});

let checkAuthentication = (function (){
    let currentSession = sessionStorage.getItem('state');

    if(currentSession == null) {
        window.location = "login.html";
    } else {
        Main();
        addKeyboardModule();
        keyboardFormModule();
        userSettingsModule();
        MenuModule();
        
    }
})();
