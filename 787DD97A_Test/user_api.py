import pytest, requests, json, random, string, pprint, base64, time
from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.desired_capabilities import DesiredCapabilities
from selenium.webdriver.common.by import By



class TestUserApi:
    user = None
    token = None
    user_uuid = None

    @staticmethod
    def randomword(length):
        letters = string.ascii_lowercase
        return ''.join(random.choice(letters) for i in range(length))

    def test_user_register(self):
        self.__class__.user = self.randomword(10)
        url = "https://backend.787dd97a.xahprod.ru/api/User/Register"

        payload = json.dumps({
            "name": "test_auto",
            "surname": "test_auto",
            "email": self.__class__.user,
            "password": "test_auto"
        })
        headers = {
            'Content-Type': 'application/json',
        }

        response = requests.request("POST", url, headers=headers, data=payload)
        response = json.loads(response.text)

        assert (response["uuid"] is not None)
        assert (response["email"] is not None)
        assert (response["password"] is not None)

    def test_user_login_unique_device_id(self):
        url = "http://backend.787dd97a.xahprod.ru/api/User/Login"

        payload = json.dumps({
            "username": self.__class__.user,
            "password": "test_auto",
            "deviceId": "test"
        })
        headers = {
            'Content-Type': 'application/json',
        }

        response = requests.request("POST", url, headers=headers, data=payload)
        response = response.text
        self.__class__.token = response

        response = response.split(".")[1]
        response = base64.b64decode(response)
        response = response.decode("UTF-8")
        response = json.loads(response)


        assert (response.get("aud") == "787dd97aClient")
        assert (response.get("iss") == "787dd97aServer")
        assert (response.get("http://schemas.microsoft.com/ws/2008/06/identity/claims/role") == "0")
        assert (response.get("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress") == self.__class__.user)

    def test_user_get_info(self):
        url = "https://backend.787dd97a.xahprod.ru/api/User"

        payload={}
        headers = {
            'Authorization': f'Bearer {self.__class__.token}'
        }

        response = requests.request("GET", url, headers=headers, data=payload)
        response = json.loads(response.text)
        self.__class__.user_uuid = response.get("uuid")

        assert (response.get("uuid"))
        assert (response.get("password") == "B0-EA-FC-FD-A8-17-A2-1C-F7-60-78-9E-22-63-0B-BD-D9-E6-25-F9-3B-FA-3C-12-C9-0B-CB-A5-45-E4-62-E9-B7-3D-95-F3-DC-37-06-D0-F5-CB-7E-DB-4D-E6-AF-EC-89-AA-15-65-90-3F-DA-19-6A-21-D0-23-C4-F4-21-16")
        assert (response.get("firstName") == "test_auto")
        assert (response.get("secondName") == "test_auto")

    def test_user_update_token(self):
        url = "https://backend.787dd97a.xahprod.ru/api/User/GetDevices"

        payload={}
        headers = {
            'Authorization': f'Bearer {self.__class__.token}'
        }

        response = requests.request("GET", url, headers=headers, data=payload)
        response = json.loads(response.text)

        assert (response[0]["deviceId"] == "test")
        assert (response[0]["useruuid"] == self.__class__.user_uuid)

class TestSelenium:
    user = None
    opts = Options()
    # opts.add_argument("--headless")
    opts.add_argument(f"user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36")
    preferences = {
        "webrtc.ip_handling_policy" : "disable_non_proxied_udp",
        "webrtc.multiple_routes_enabled": False,
        "webrtc.nonproxied_udp_enabled" : False
    }
    opts.add_experimental_option("prefs", preferences)
    opts.add_argument("--disable-blink-features=AutomationControlled")
    webdriver.DesiredCapabilities.CHROME['acceptSslCerts']=True
    caps = DesiredCapabilities().CHROME
    driver = webdriver.Chrome('./chromedriver', options=opts, desired_capabilities=caps)

    @staticmethod
    def randomword(length):
        letters = string.ascii_lowercase
        return ''.join(random.choice(letters) for i in range(length))

    def test_user_register(self):
        self.__class__.user = self.randomword(10)
        self.__class__.driver.get("https://787dd97a.xahprod.ru/html/registration.html")

        name_user_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-name"]')
        name_user_input.click()
        name_user_input.send_keys("test_auto")
        user_second_name_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-second-name"]')
        user_second_name_input.click()
        user_second_name_input.send_keys("test_auto")
        user_email_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-email"]')
        user_email_input.click()
        user_email_input.send_keys(f'{self.__class__.user}@auto_test.com')

        user_password_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-password"]')
        user_password_input.click()
        user_password_input.send_keys("test_auto")
        user_password_repeat_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-password-repeat"]')
        user_password_repeat_input.click()
        user_password_repeat_input.send_keys("test_auto")

        button_register_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="register-btn"]')
        button_register_input.click()

        time.sleep(0.5)
        assert (self.__class__.driver.current_url == 'https://787dd97a.xahprod.ru/html/login.html')

    def test_user_login(self):
        self.__class__.driver.get("https://787dd97a.xahprod.ru/html/login.html")

        email_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="email"]')
        email_input.click()
        email_input.send_keys(f'{self.__class__.user}@auto_test.com')
        password_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="password"]')
        password_input.click()
        password_input.send_keys("test_auto")

        button_login_input = self.__class__.driver.find_element(By.XPATH, '//*[@id="sign-in-btn"]')
        button_login_input.click()

        time.sleep(0.5)
        assert (self.__class__.driver.current_url == 'https://787dd97a.xahprod.ru/html/personal-account.html')

    def test_get_user_data(self):
        self.__class__.driver.get('https://787dd97a.xahprod.ru/html/personal-account.html')
        time.sleep(2)

        user_name = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-info-name"]')
        user_surname = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-info-surname"]')
        user_email = self.__class__.driver.find_element(By.XPATH, '//*[@id="user-info-email"]')

        assert (str(user_name.text) == 'Имя: test_auto')
        assert (str(user_surname.text) == 'Фамилия: test_auto')
        assert (str(user_email.text) == f'Почта: {self.__class__.user}@auto_test.com')

    def test_user_logout(self):
        self.__class__.driver.get('https://787dd97a.xahprod.ru/html/personal-account.html')

        user_logout_btn = self.__class__.driver.find_element(By.XPATH, '//*[@id="logout__button"]')
        user_logout_btn.click()

        time.sleep(0.5)
        redirection_url = self.__class__.driver.current_url

        self.__class__.driver.get('https://787dd97a.xahprod.ru/html/login.html')
        time.sleep(0.5)

        assert (self.__class__.driver.execute_script("return localStorage.getItem('token')") is None)
        assert (redirection_url == "https://787dd97a.xahprod.ru/index.html")
        assert (self.__class__.driver.current_url == "https://787dd97a.xahprod.ru/html/login.html")

    def test_user_accont_without_auth(self):
        self.__class__.driver.get('https://787dd97a.xahprod.ru/html/personal-account.html')
        time.sleep(0.5)
        pprint.pprint(self.__class__.driver.execute_script("return localStorage.getItem('token')"))

        assert (self.__class__.driver.execute_script("return localStorage.getItem('token')") is None)
        assert (self.__class__.driver.current_url == "https://787dd97a.xahprod.ru/html/login.html")

    def go_to_test_page(self, url: str) -> None:
        self.__class__.driver.get(url)
        time.sleep(0.5)
        menu = self.__class__.driver.find_element(By.XPATH, '//*[@id="menu_btn"]')
        menu.click()
        time.sleep(0.5)

    def test_redirections(self):
        self.go_to_test_page('https://787dd97a.xahprod.ru/html/personal-account.html')
        main_page = self.__class__.driver.find_element(By.XPATH, '//*[@id="menu"]/div/div[2]/ul/li[1]/a')
        main_page.click()
        time.sleep(0.5)
        assert (self.__class__.driver.current_url == "https://787dd97a.xahprod.ru/index.html")


        self.go_to_test_page('https://787dd97a.xahprod.ru/html/personal-account.html')
        main_page = self.__class__.driver.find_element(By.XPATH, '//*[@id="menu"]/div/div[2]/ul/li[2]/a')
        main_page.click()
        time.sleep(0.5)
        assert (self.__class__.driver.current_url == "https://787dd97a.xahprod.ru/html/login.html")

        self.go_to_test_page('https://787dd97a.xahprod.ru/html/personal-account.html')
        main_page = self.__class__.driver.find_element(By.XPATH, '//*[@id="menu"]/div/div[2]/ul/li[3]/a')
        main_page.click()
        time.sleep(0.5)
        assert (self.__class__.driver.current_url == "https://787dd97a.xahprod.ru/#")

