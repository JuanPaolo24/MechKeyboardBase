
let MenuModule = (function() {
    let rightMenu = document.getElementById('right');

    let session = sessionStorage.getItem('state');
    let loginPage = document.createElement('a');
    let signupPage = document.createElement('a');

    let account = document.createElement('a');
    let collection = document.createElement('a');
    let logout = document.createElement('a');

    let loadMenu = function() {
        if (session == "loggedIn") {
    
            account.innerHTML = "<li>Account</li>";
            account.href = "account.html";
            collection.innerHTML = "<li>Collection</li>";
            collection.href = "profile.html";
            logout.innerHTML = "<li>Logout</li>";
            logout.id = "logout";
    
            logout.onclick = function() {
                localStorage.clear();
                sessionStorage.clear();
                window.location.reload();
            }

            rightMenu.appendChild(account);
            rightMenu.appendChild(collection);
            rightMenu.appendChild(logout);

        } else {
            account.innerHTML = "";
            collection.innerHTML = "";
            logout.innerHTML = "";

            loginPage.innerHTML = "<li>Login</li>";
            loginPage.href = "login.html";

            signupPage.innerHTML = "<li>Signup</li>";
            signupPage.href = "register.html";
    
            rightMenu.appendChild(loginPage);
            rightMenu.appendChild(signupPage);
            
        }
    }


    loadMenu();

})();


let GetContentModule = (function() {
    const next = document.getElementById("next");
    const previous = document.getElementById("previous");

    let keyboardContainer = document.getElementById("keyboard__index");

    let currentPage = 1;

    let page_number = document.getElementById('pages').getElementsByClassName('clickPageNumber'); 

    let currentPageSize = window.screen.availWidth > 1024 ? 6 : 2;

    let fetchCurrentPage = function(pageNumber) {
        var pageSettings = '?number=' + pageNumber + '&size=' + currentPageSize;
        fetch('/api/keyboard/page' + pageSettings)
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
        

        var description = document.createElement('div');
        description.className = "keyboard__info";
        description.id = "keyboard__description" + i;

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
        keyboardItem.appendChild(description);
        keyboardContainer.appendChild(keyboardItem);
        
        }
    
            selectedPage();
    
        });
    
    }


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
      }
      
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
          fetch('/api/keyboard')
          .then(function (response) {
              return response.json();
          })
          .then(function (keybs) {
      
          var pagesContainer = document.getElementById("pages");
      
          var numberOfPage = Math.ceil(keybs.length / currentPageSize);
      
          pageNumber = numberOfPage;
      
          for(var i=1; i<numberOfPage + 1; i++) {
              pagesContainer.innerHTML += "<span class='clickPageNumber'>" + i + "</span>";
          }
          
          page_number[0].style.backgroundColor = "black";
          page_number[0].style.color = "white";
      
          next.onclick = function() {
              if(currentPage < pageNumber) {
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
})();






