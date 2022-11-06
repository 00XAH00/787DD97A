//всплывающее меню
function menu_visibl() {
    // const content = document.getElementById("header-content")
    const menu = document.getElementById('menu')

    // content.style.visibility = "hidden"
    menu_btn.style.visibility = "hidden"
    menu.style.left = 0
}
function menu_hide() {
    const menu = document.getElementById('menu')
    // const content = document.getElementById("header-content")
    
    // content.style.visibility = "visible"
    menu_btn.style.visibility = "visible"
    menu.style.left = "-100%"
}


const menu_btn = document.getElementById("menu_btn")
const close_menu_btn = document.getElementById("close_menu_btn")

menu_btn.addEventListener("click", menu_visibl)
close_menu_btn.addEventListener("click", menu_hide)

// Убрать скролинк
const headerbtn = document.querySelector('.header-btn');
if (headerbtn) {
    const headerbtn = document.querySelector('.header-btn');
    headerbtn.addEventListener("click", function (e) {
        document.body.classList.toggle('_lock')
    });
}

const headerbtn__close = document.querySelector('.close-btn');
if (headerbtn__close) {
    const headerbtn__close = document.querySelector('.close-btn');
    headerbtn__close.addEventListener("click", function (e) {
        document.body.classList.toggle('_lock')
    });
}

const headerbtn__nav = document.querySelector('.menu__list');
if (headerbtn__nav) {
    const headerbtn__nav = document.querySelector('.menu__list');
    headerbtn__nav.addEventListener("click", function (e) {
        document.body.classList.toggle('_lock')
    });
}
// Убрать скролинк



const form = {
  name: document.getElementById('name'),
  surname: document.getElementById('surname'),
  email: document.getElementById('email'),
  password: document.getElementById('password'),
  button: document.querySelector('.button'),
}

function handleInput(e, name) {
    const { value } = e.target
    if (value){
      form[name].classList.add('filed')
    } else {
      form[name].classList.remove('filed')
    }
  }


form.name.oninput = (e) => handleInput(e, 'name')
form.surname.oninput = (e) => handleInput(e, 'surname')
form.email.oninput = (e) => handleInput(e, 'email')
form.password.oninput = (e) => handleInput(e, 'password')