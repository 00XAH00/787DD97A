function user_info(){
    const UserName = document.getElementById("user-info-name")
    const UserSecondName = document.getElementById("user-info-surname")
    const UserEmail = document.getElementById("user-info-email")
    


    var myHeaders = new Headers();
    myHeaders.append("Authorization", `Bearer ${localStorage.getItem("token")}`);

    var requestOptions = {
        method: 'GET',
        headers: myHeaders,
        redirect: 'follow'
    };

    fetch("https://backend.787dd97a.xahprod.ru/api/User", requestOptions)
    .then(response => response.text())
    .then(result => {
        result = JSON.parse(result)

        UserName.textContent = `Имя: ${result.firstName}`
        UserSecondName.textContent = `Фамилия: ${result.secondName}`
        UserEmail.textContent = `Почта: ${result.email}`
        console.log(result)
    })
    .catch(error => console.log('error', error));
}

user_info()

const logoutBtn = document.getElementById("logout__button").addEventListener("click", LogOut)

function LogOut() {
    localStorage.removeItem("token")
    location.href = "/index.html"
}