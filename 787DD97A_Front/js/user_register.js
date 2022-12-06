function userRegister() {

    const userName = document.getElementById('user-name').value
    const userSecondName = document.getElementById('user-second-name').value
    const userEmail = document.getElementById('user-email').value
    const userPassword = document.getElementById('user-password').value
    const userPasswordRepeat = document.getElementById('user-password-repeat').value


    if (userPassword === userPasswordRepeat) {
        var myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
    
        var raw = JSON.stringify({
            "name": userName,
            "surname": userSecondName,
            "email": userEmail,
            "password": userPassword
        });
    
        var requestOptions = {
            method: 'POST',
            headers: myHeaders,
            body: raw,
            redirect: 'follow'
        };
    
        fetch("https://backend.787dd97a.xahprod.ru/api/User/Register", requestOptions)
        .then(response => response.text())
        .then(result => {
            result = JSON.parse(result)
            if (result["uuid"]) {
                location.href = "/html/login.html"
            }
            else document.getElementById("error").textContent = "Пользователь уже существует"
            console.log(result)
        })
        .catch(error => console.log('error', error));
    }
    else {
        document.getElementById("error").textContent = "Пароли не совпадают"
    }
}

document.getElementById("register-btn").addEventListener("click", userRegister)