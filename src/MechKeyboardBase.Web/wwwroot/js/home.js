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
    fetch('/api/keyboard')
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


