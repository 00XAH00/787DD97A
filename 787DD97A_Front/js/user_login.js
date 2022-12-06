function userlogin() {

    const userEmail = document.getElementById('email').value
    const userPassword = document.getElementById('password').value


    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var raw = JSON.stringify({
        "username": userEmail,
        "password": userPassword,
        "deviceId": "test"
    });

    var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: raw,
    redirect: 'follow'
    };

    fetch("https://backend.787dd97a.xahprod.ru/api/User/Login", requestOptions)
    .then(response => response.text())
    .then(result => {
        localStorage.setItem("token", result)
        location.href = "/html/personal-account.html"
    })
    .catch(error => {
        document.getElementById("error").textContent = "Не правильный пароль"
        console.log('error', error)
    });
}

if (localStorage.getItem("token")) location.href = "/html/personal-account.html"
document.getElementById("sign-in-btn").addEventListener("click", userlogin)