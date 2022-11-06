//всплывающее меню
function menu_visibl() {
    const content = document.getElementById("header-content")
    const menu = document.getElementById('menu')

    content.style.visibility = "hidden"
    menu_btn.style.visibility = "hidden"
    menu.style.left = 0
}
function menu_hide() {
    const menu = document.getElementById('menu')
    const content = document.getElementById("header-content")
    
    content.style.visibility = "visible"
    menu_btn.style.visibility = "visible"
    menu.style.left = "-100%"
}


const menu_btn = document.getElementById("menu_btn")
const close_menu_btn = document.getElementById("close_menu_btn")

menu_btn.addEventListener("click", menu_visibl)
close_menu_btn.addEventListener("click", menu_hide)


//переключение между кнопками
const button = document.querySelectorAll('.segment-btn')
button.forEach((elem) => {

    elem.onclick = () => {
    document.querySelector('.form__box-btn.segment-btn.active')?.classList.remove('active')
    elem.classList.add('active')
  }
})

const button2 = document.querySelectorAll('.walls-btn')
button2.forEach((elem) => {

    elem.onclick = () => {
    document.querySelector('.form__box-btn.walls-btn.active')?.classList.remove('active')
    elem.classList.add('active')
  }
})

const button3 = document.querySelectorAll('.finishing-btn')
button3.forEach((elem) => {

    elem.onclick = () => {
    document.querySelector('.form__box-btn.finishing-btn.active')?.classList.remove('active')
    elem.classList.add('active')
  }
})

const roomSlideValue = document.getElementById("room-span");
const roomInputSlider = document.getElementById("room-input");
roomInputSlider.oninput = (()=>{
    let value = roomInputSlider.value;
    roomSlideValue.textContent = value;
    roomSlideValue.classList.add("show");
});

const floorSlideValue = document.getElementById("floor-span");
const floorInputSlider = document.getElementById("floor-input");

floorInputSlider.oninput = (()=>{
    let value = floorInputSlider.value;
    floorSlideValue.textContent = value;
    floorSlideValue.classList.add("show");
});

const flatSlideValue = document.getElementById("flat-span");
const flatInputSlider = document.getElementById("flat-input");

flatInputSlider.oninput = (()=>{
    let value = flatInputSlider.value;
    flatSlideValue.textContent = value;
    flatSlideValue.classList.add("show");
});

const kitchenSlideValue = document.getElementById("kitchen-span");
const kitchenInputSlider = document.getElementById("kitchen-input");

kitchenInputSlider.oninput = (()=>{
    let value = kitchenInputSlider.value;
    kitchenSlideValue.textContent = value;
    kitchenSlideValue.classList.add("show");
});


//делаем кнопку "загрузить файл excel"
let inputs = document.querySelectorAll('.input__file');
      Array.prototype.forEach.call(inputs, function (input) {
        let label = input.nextElementSibling,
          labelVal = label.querySelector('.input__file-button-text').innerText;
    
        input.addEventListener('change', function (e) {
          let countFiles = '';
          if (this.files && this.files.length >= 1)
            countFiles = this.files.length;
    
          if (countFiles)
            label.querySelector('.input__file-button-text').innerText = 'Выбрано файлов: ' + countFiles;
          else
            label.querySelector('.input__file-button-text').innerText = labelVal;
        });
      });

      let procRows = table.querySelectorAll("tbody tr");
      for (let i = 0; i < procRows.length; i++) {
        procRows[i].innerHTML += '<td><button  title="Remove"></td>';
      }
      
      table.querySelector("tbody").addEventListener("click", function(e) {
          if (e.target.nodeName == "BUTTON") {
              let cell = e.target.parentNode;     
              cell.parentNode.classList.add("hidden");
              e.target.remove();
          }
      })