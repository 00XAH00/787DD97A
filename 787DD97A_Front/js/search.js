let apartments_array

function Getanalog() {
    const adressValue = document.getElementById('location').value
    const houseFloorValue = document.getElementById('floor-input').value
    const apatmentFloorValue = document.getElementById('floor').value
    const apartmentAreaValue = document.getElementById('flat-input').value
    const kitchentAreaValue = document.getElementById('kitchen-input').value
    const segment_buttonOneValue = document.getElementById('segment-btn-first')
    const segment_buttonSecondValue = document.getElementById('segment-btn-second')
    const segment_buttonThirdValue = document.getElementById('segment-btn-third')
    const balconyValue = document.getElementById('balcony-checkbox').checked
    const distanceFromMetroStationValue = document.getElementById('distance').value
    const repairButtonFirstValue = document.getElementById('repair-btn-first')
    const repairButtonSecondValue = document.getElementById('repair-btn-second')
    const repairButtonThirdValue = document.getElementById('repair-btn-third')
    const materialButtonFirstValue = document.getElementById('material-btn-first')
    const materialButtonSecondValue = document.getElementById('material-btn-second')
    const materialButtonThirdValue = document.getElementById('material-btn-third')
    const roomsValue = document.getElementById('room-input').value
    const undergroundValue = document.getElementById('underground').value

    let segment = -1
    segment_buttonOneValue.classList.forEach(btn_class => {
        console.log(btn_class)
        if (btn_class === "active") {
            segment = 2
        }
    })
    if (segment === -1) {
        segment_buttonSecondValue.classList.forEach(btn_class => {
            console.log(btn_class)
            if (btn_class === "active") {
                segment = 1
            }
        })
    }
    if (segment === -1) {
        segment_buttonThirdValue.classList.forEach(btn_class => {
            console.log(btn_class)
            if (btn_class === "active") {
                segment = 3
            }
        })
    }

    let repair = -1
    repairButtonFirstValue.classList.forEach(btn_class => {
        console.log(btn_class)
        if (btn_class === "active") {
            repair = "без отделки"
        }
    })
    if (repair === -1) {
        repairButtonSecondValue.classList.forEach(btn_class => {
            console.log(btn_class)
            if (btn_class === "active") {
                repair = "муниципальный ремонт"
            }
        })
    }
    if (repair === -1) {
        repairButtonThirdValue.classList.forEach(btn_class => {
            console.log(btn_class)
            if (btn_class === "active") {
                repair = "современный ремонт"
            }
        })
    }

    let material = -1
    materialButtonFirstValue.classList.forEach(btn_class => {
        console.log(btn_class)
        if (btn_class === "active") {
            material = 2
        }
    })
    if (material === -1) {
        materialButtonSecondValue.classList.forEach(btn_class => {
            console.log(btn_class)
            if (btn_class === "active") {
                material = 1
            }
        })
    }
    if (material === -1) {
        materialButtonThirdValue.classList.forEach(btn_class => {
            console.log(btn_class)
            if (btn_class === "active") {
                material = 3
            }
        })
    }

    var formdata = new FormData();
    // formdata.append("Adress", adressValue);
    // formdata.append("NumberOfStoreys", houseFloorValue);
    // formdata.append("FloorLocation", apatmentFloorValue);
    // formdata.append("ApartmentArea", apartmentAreaValue);
    // formdata.append("KitchentArea", kitchentAreaValue);
    // formdata.append("Segment", segment);
    // formdata.append("balcony", balconyValue);
    // formdata.append("DistanceFromMetroStation", distanceFromMetroStationValue);
    // formdata.append("repair", repair);
    // formdata.append("Undeground", undergroundValue);
    // formdata.append("Rooms", roomsValue);
    // formdata.append("Material", material);

    formdata.append("Adress", "ул. Октябрьская, вл. 98");
    formdata.append("NumberOfStoreys", "13");
    formdata.append("FloorLocation", "2");
    formdata.append("ApartmentArea", "52");
    formdata.append("KitchentArea", "10");
    formdata.append("Segment", "1");
    formdata.append("balcony", "true");
    formdata.append("DistanceFromMetroStation", "5");
    formdata.append("repair", "без отделки");
    formdata.append("Undeground", "Марьина Роща");
    formdata.append("Rooms", "2");
    formdata.append("Material", "1");

    var requestOptions = {
        method: 'POST',
        body: formdata,
        redirect: 'follow'
    };

    fetch("https://backend.787dd97a.xahprod.ru/api/Apartment/analogs", requestOptions)
    .then(response => response.text())
    .then(result => {
        result = JSON.parse(result)
        apartments_array = result
        console.log(result)

        const table = document.getElementById("apartments-table")
        table.style.display = "block"

        const table_body = document.getElementById("body-table")

        
        result.forEach(apartment => {
            let balcony
            if (apartment["balcony"]) balcony = "Да"
            else balcony = "Нет"

            let segment
            if (apartment["segment"] === 1) segment = "Новостройка"
            else if (apartment["segment"] === 2) segment = "Современное жилье"
            else segment = "Старый жилой фонд"

            let material
            if(apartment["material"] === 1) material = "Монолитный"
            else if (apartment["material"] === 2) material = "Кирпичный"
            else material = "Панельный"

            table_body.innerHTML +=
                `<tr>
                <td>${apartment["adress"]}</td>
                <td>${apartment["undeground"]}</td>
                <td>${apartment["undeground_minutes"]}</td>
                <td>${apartment["rooms"]}</td>
                <td>${segment}</td>
                <td>${apartment["house_floors"]}</td>
                <td>${material}</td>
                <td>${apartment["apartment_floor"]}</td>
                <td>${apartment["apatments_area"]}</td>
                <td>${apartment["kitchen_area"]}</td>
                <td>${balcony}</td>
                <td>${apartment["condition"]}</td>
                <td><button title="Remove"></button></td>
                </tr>`
            console.log(apartment)
        })
    })
    .catch(error => console.log('error', error));
}

document.getElementById('analog-btn').addEventListener('click', Getanalog)