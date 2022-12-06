//всплывающее меню
function menu_visibl() {
    const menu = document.getElementById('menu')

    menu_btn.style.visibility = "hidden"
    menu.style.left = 0
}
function menu_hide() {
    const menu = document.getElementById('menu')


    menu_btn.style.visibility = "visible"
    menu.style.left = "-100%"
}


const menu_btn = document.getElementById("menu_btn")
const close_menu_btn = document.getElementById("close_menu_btn")

menu_btn.addEventListener("click", menu_visibl)
close_menu_btn.addEventListener("click", menu_hide)


//валидация
// const EmailInput = document.getElementById('email');
// EmailInput.addEventListener('input', onInput);


// const email_regexp = /^(([^<>()[\].,;:\s@"]+(\.[^<>()[\].,;:\s@"]+)*)|(".+"))@(([^<>()[\].,;:\s@"]+\.)+[^<>()[\].,;:\s@"]{2,})$/iu;

// function onInput() {
//   if (isEmailValid(EmailInput.value)) {
//     EmailInput.style.borderColor = 'green';
//   } else {
//     EmailInput.style.borderColor = 'red';
//   }
// }

// EmailInput.addEventListener('input', onInput);


// function isEmailValid(value) {
//     return email_regexp.test(value);
// }