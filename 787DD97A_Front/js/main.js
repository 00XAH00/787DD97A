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

//ползунок для калькулятора
const roomRangeSlider = document.getElementById('room-rangeSlider');

if (roomRangeSlider) {
    noUiSlider.create(roomRangeSlider, {
        start: [1, 15],
        connect: true,
        step: 1,
        range: {
            'min': [1],
            'max': [15]
        }
    });

    const roomInput0 = document.getElementById('room-input0');
    const roomInput1 = document.getElementById('room-input1');
    const roomInputs = [roomInput0, roomInput1];

    roomRangeSlider.noUiSlider.on('update', function(values, handle){
        // console.log(handle)
        roomInputs[handle].value = Math.round(values[handle]);
    });

    const setRangeSlider = (i, value) => {
        let arr = [null, null];
        arr[i] = value;

        roomRangeSlider.noUiSlider.set(arr);
    };

    roomInputs.forEach((el, index) => {
        el.addEventListener('change', (e) => {
            setRangeSlider(index, e.currentTarget.value);
        });
    });
}

const floorRangeSlider = document.getElementById('floor-rangeSlider');

if (floorRangeSlider) {
    noUiSlider.create(floorRangeSlider, {
        start: [2, 100],
        connect: true,
        step: 1,
        range: {
            'min': [2],
            'max': [100]
        }
    });

    const floorInput0 = document.getElementById('floor-input0');
    const floorInput1 = document.getElementById('floor-input1');
    const floorInputs = [floorInput0, floorInput1];

    floorRangeSlider.noUiSlider.on('update', function(values, handle){
        // console.log(handle)
        floorInputs[handle].value = Math.round(values[handle]);
    });

    const setRangeSlider = (i, value) => {
        let arr = [null, null];
        arr[i] = value;

        floorRangeSlider.noUiSlider.set(arr);
    };

    floorInputs.forEach((el, index) => {
        el.addEventListener('change', (e) => {
            setRangeSlider(index, e.currentTarget.value);
        });
    });
}

const flatRangeSlider = document.getElementById('flat-rangeSlider');

if (flatRangeSlider) {
    noUiSlider.create(flatRangeSlider, {
        start: [10, 130],
        connect: true,
        step: 1,
        range: {
            'min': [10],
            'max': [130]
        }
    });

    const flatInput0 = document.getElementById('flat-input0');
    const flatInput1 = document.getElementById('flat-input1');
    const flatInputs = [flatInput0, flatInput1];

    flatRangeSlider.noUiSlider.on('update', function(values, handle){
        // console.log(handle)
        flatInputs[handle].value = Math.round(values[handle]);
    });

    const setRangeSlider = (i, value) => {
        let arr = [null, null];
        arr[i] = value;

        flatRangeSlider.noUiSlider.set(arr);
    };

    flatInputs.forEach((el, index) => {
        el.addEventListener('change', (e) => {
            setRangeSlider(index, e.currentTarget.value);
        });
    });
}

const kitchenRangeSlider = document.getElementById('kitchen-rangeSlider');

if (kitchenRangeSlider) {
    noUiSlider.create(kitchenRangeSlider, {
        start: [2, 15],
        connect: true,
        step: 1,
        range: {
            'min': [2],
            'max': [15]
        }
    });

    const kitchenInput0 = document.getElementById('kitchen-input0');
    const kitchenInput1 = document.getElementById('kitchen-input1');
    const kitchenInputs = [kitchenInput0, kitchenInput1];

    kitchenRangeSlider.noUiSlider.on('update', function(values, handle){
        // console.log(handle)
        kitchenInputs[handle].value = Math.round(values[handle]);
    });

    const setRangeSlider = (i, value) => {
        let arr = [null, null];
        arr[i] = value;

        floorRangeSlider.noUiSlider.set(arr);
    };

    kitchenInputs.forEach((el, index) => {
        el.addEventListener('change', (e) => {
            setRangeSlider(index, e.currentTarget.value);
        });
    });
}

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