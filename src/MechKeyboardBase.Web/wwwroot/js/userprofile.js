var addButton = document.getElementById("addbtn");
var keyboardForm = document.getElementById("addkeyboard-form");
var modalwrapper = document.getElementById("modal__container");
var closeButton = document.getElementById("closeaddkeyboardform");
var logout = document.getElementById("logout");
var saveBtn = document.getElementById("savekeyboardbtn");

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


document.addEventListener("DOMContentLoaded", function() {
    fetchCurrentPage(1);
    paginationSettings();
    clickPage();
});


///https://codepen.io/chichichi/pen/boXOKv Refer to this for pure pagination

let fetchCurrentPage = function(pageNumber) {
    var pageSettings = '?username=' + localStorage.getItem('username') + '&number=' + pageNumber + '&size=4';
    fetch('/api/keyboard/userprofile/page' + pageSettings)
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
    sessionStorage.clear();
    window.location = "../index.html"
}


function test() {
    var form = document.querySelector('form');
    const formData = new FormData(form);
    let jsonObject = {};

    for (const [key, value]  of formData.entries()) {
        jsonObject[key] = value;
    }
    console.log(jsonObject);
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
            form.reset();
            alert("Keyboard added!");
            window.location.reload();
        } else {
            alert("Keyboard not added!");
        }
        return response.json();
    });
}


saveBtn.onclick = function() {
    saveKeyboard();
}