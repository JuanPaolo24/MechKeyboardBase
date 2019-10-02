let GetUserContentModule = (function (){
    const next = document.getElementById("next");
    const previous = document.getElementById("previous");

    let row2left = document.getElementById("row2__leftdescription");
    let row2right = document.getElementById("row2__rightdescription");
    let row3left = document.getElementById("row3__leftdescription");
    let row3right = document.getElementById("row3__rightdescription");

    let row2leftimg = document.getElementById("row2__leftimage");
    let row2rightimg = document.getElementById("row2__rightimage");
    let row3leftimg = document.getElementById("row3__leftimage");
    let row3rightimg = document.getElementById("row3__rightimage");


    let rowArray = [row2left, row2right, row3left, row3right];
    let imageArray = [row2leftimg, row2rightimg, row3leftimg, row3rightimg];


    let currentPage = 1;


    let page_number = document.getElementById('pages').getElementsByClassName('clickPageNumber'); 

    let fetchCurrentPage = function(pageNumber) {
        var pageSettings = '?username=' + localStorage.getItem('username') + '&number=' + pageNumber + '&size=4';
        fetch('/api/keyboard/userprofile/page' + pageSettings)
        .then(function (response) {
            return response.json();
        })
        .then (function (keyboards) {

        for(var i= 0; i < keyboards.length; i++) {

        var img = document.createElement('img');
        img.src = keyboards[i].imageUrl + "?";
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
    
            selectedPage();
        });
    
    }
    
    
    let selectedPage = function() {
      for (let i = 0; i < page_number.length; i++) {
          if (i == currentPage - 1) {
              page_number[i].style.backgroundColor = "dodgerblue";
          } 
          else {
              page_number[i].style.backgroundColor = "lightgrey";
          }
      }  
    }
    
    let clickPage = function() {
        document.addEventListener('click', function(e) {
            if(e.target.nodeName == "SPAN" && e.target.classList.contains("clickPageNumber")) {
                currentPage = e.target.textContent;
                clearView();
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
    
        var numberOfPage = Math.ceil(keybs.length / 4);
    
        for(var i=1; i<numberOfPage + 1; i++) {
            pagesContainer.innerHTML += "<span class='clickPageNumber'>" + i + "</span>";
        }
    
        page_number[0].style.backgroundColor = "dodgerblue";
    
        next.onclick = function() {
            if(currentPage < numberOfPage) {
                currentPage++;
                clearView();
                fetchCurrentPage(currentPage);
            }
        }
    
    
        previous.onclick = function() {
            if(currentPage > 1) {
                currentPage--;
                clearView();
                fetchCurrentPage(currentPage);
            }
        }   
    
        });
    }
    
    let clearView = function() {
        for(var i= 0; i < 4; i++) {
            rowArray[i].innerHTML = "";
            imageArray[i].innerHTML = "";
        }
    }

    fetchCurrentPage(1);
    paginationSettings();
    clickPage();

});

let keyboardFormModule = (function () {
    let addButton = document.getElementById("addbtn");
    let closeButton = document.getElementById("closeaddkeyboardform");
    let keyboardForm = document.getElementById("addkeyboard-form");
    let modalwrapper = document.getElementById("modal__container");
    let savekeyboardBtn = document.getElementById("savekeyboardbtn");
    let editkeyboardBtn = document.getElementById("editkeyboardbtn");

    let formTitle = document.getElementById("form__title");

    let showAddKeyboardForm = function () {
        savekeyboardBtn.style.visibility = "visible";
        editkeyboardBtn.style.visibility = "hidden";
        formTitle.innerHTML = "Add a new keyboard";
        keyboardForm.style.display = "block";
        modalwrapper.style.display = "block";
    };

    let showEditKeyboardForm = function () {
        editkeyboardBtn.style.visibility = "visible";
        savekeyboardBtn.style.visibility = "hidden";
        formTitle.innerHTML = "Edit an existing keyboard";
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
    let title = document.getElementById("row1__title");

    let p = document.createElement('p');
    p.innerHTML = "Welcome to your Collection " + localStorage.getItem('username');
    title.appendChild(p);
});

let logoutModule = (function () {
    let logout = document.getElementById("logout");

    let clearSession = function() {
        localStorage.clear();
        sessionStorage.clear();
        window.location = "../index.html"
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

let editKeyboardModule = (function () {
    let row2leftdescription = document.getElementById("row2__leftdescription");
    let row2rightdescription = document.getElementById("row2__rightdescription");
    let row3leftdescription = document.getElementById("row3__leftdescription");
    let row3rightdescription = document.getElementById("row3__rightdescription");

    let row2leftedit = document.getElementById("row2__leftedit");
    let row2rightedit = document.getElementById("row2__rightedit");
    let row3leftedit = document.getElementById("row3__leftedit");
    let row3rightedit = document.getElementById("row3__rightedit");

    let save = document.getElementById("editkeyboardbtn");
    let form = document.querySelector('form');

    let keyboardName = document.getElementById("keyboardName");
    let keyboardCase = document.getElementById("keyboardCase");
    let pcb = document.getElementById("pcb");
    let plate = document.getElementById("plate");
    let keycaps = document.getElementById("keycaps");
    let keyboardSwitch = document.getElementById("switch");

    let currentForm;

    let keyboardDetails = [keyboardName, keyboardCase, pcb, plate, keycaps, keyboardSwitch, image];

    let fillForm = function(description) {
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

        console.log(jsonObject);

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
                window.location.reload();
            }  else {
                alert("Keyboard unabled to be edited!");
                window.location.reload();
            }
        });
    };


    row2leftedit.addEventListener('click', function(){
        fillForm(row2leftdescription);
        currentForm = row2leftdescription;
        keyboardFormModule().showEditKeyboardForm();
    });
    row2rightedit.addEventListener('click', function() {
        fillForm(row2rightdescription);
        currentForm = row2rightdescription;
        keyboardFormModule().showEditKeyboardForm();
    });
    row3leftedit.addEventListener('click', function() {
        fillForm(row3leftdescription);
        currentForm = row3leftdescription;
        keyboardFormModule().showEditKeyboardForm();
    });
    row3rightedit.addEventListener('click', function() {
        fillForm(row3rightdescription);
        currentForm = row3rightdescription;
        keyboardFormModule().showEditKeyboardForm();
    });

    save.addEventListener('click', function() {
        editKeyboard(currentForm);
    });

});

let deleteKeyboardModule = (function () {
    let row2leftdelete = document.getElementById("row2__leftdelete");
    let row2rightdelete = document.getElementById("row2__rightdelete");
    let row3leftdelete = document.getElementById("row3__leftdelete");
    let row3rightdelete = document.getElementById("row3__rightdelete");

    let row2leftdescription = document.getElementById("row2__leftdescription");
    let row2rightdescription = document.getElementById("row2__rightdescription");
    let row3leftdescription = document.getElementById("row3__leftdescription");
    let row3rightdescription = document.getElementById("row3__rightdescription");

    
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
                window.location.reload();
            }  else {
                alert("Keyboard not deleted!");
                window.location.reload();
            }
        });
    }

    row2leftdelete.addEventListener('click', function() {
        deleteKeyboard(row2leftdescription);
    });

    row2rightdelete.addEventListener('click', function() {
        deleteKeyboard(row2rightdescription);
    });

    row3leftdelete.addEventListener('click', function() {
        deleteKeyboard(row3leftdescription);
    });

    row3rightdelete.addEventListener('click', function() {
        deleteKeyboard(row3rightdescription);
    });

});

let checkAuthentication = (function (){
    let currentSession = sessionStorage.getItem('state');

    if(currentSession == null) {
        console.log("hit");
        window.location = "../page/login.html";
    } else {
        GetUserContentModule();
        keyboardFormModule();
        userSettingsModule();
        logoutModule();
        addKeyboardModule();
    }
})();

document.addEventListener('DOMContentLoaded', function() {
    deleteKeyboardModule();
    editKeyboardModule();
});
