import datetime
import time, threading, pprint
from bs4 import BeautifulSoup
from math import ceil

# from user_agent import generate_user_agent, generate_navigator

from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.desired_capabilities import DesiredCapabilities
from selenium.webdriver.common.by import By

class avito():
    def __init__(self):
        self.URL = "https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg"

        self.opts = Options()
        self.opts.add_argument("--headless")
        self.opts.add_argument(f"user-agent=Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_4) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/83.0.4103.97 Safari/537.36")
        
        preferences = {
            "webrtc.ip_handling_policy" : "disable_non_proxied_udp",
            "webrtc.multiple_routes_enabled": False,
            "webrtc.nonproxied_udp_enabled" : False
        }
        self.opts.add_experimental_option("prefs", preferences)
        self.opts.add_argument("--disable-blink-features=AutomationControlled")

        self.caps = DesiredCapabilities().CHROME
        self.caps["pageLoadStrategy"] = "eager"
        self.driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)

    def get_links(self) -> list:
        apartment_links = []
        def thread_links_get(start:int, end:int):
            driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)
            for page in range(start, end):
                driver.get(f"{self.URL}?p={page}") # Получаем нужную страницу
                source = driver.page_source
                soup = BeautifulSoup(source, 'lxml')

                apartments_div = soup.find_all('div', class_='iva-item-content-rejJg') # Находим блок с квартирой
                for item in apartments_div:
                    link = item.find_all('a', class_='link-link-MbQDP')[0].get('href') # Поулчаем ссылку на объявление
                    apartment_links.append(f"https://www.avito.ru{link}")
                time.sleep(0.3)
            driver.quit()

        self.driver.get(self.URL)
        last_page = int(self.driver.find_elements(By.CLASS_NAME, 'pagination-item-JJq_j')[-2].text)+1 # находим последнюю страницу
        self.driver.quit()

        thread_first = threading.Thread(target=thread_links_get, args=(1, last_page))
        thread_first.start()
        thread_first.join()

        return apartment_links

    def get_apartments(self, links:list):
        driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)
        apartments_info = {}
        count_skiped_apartment = 0
        for link in links:
            # Получение страницы с квартирой
            driver.get(link)
            source = driver.page_source
            soup = BeautifulSoup(source, 'lxml')

            # обнуление данных о квартире
            floor = None
            rooms = None
            segment = None
            house_floors = None
            material = None
            apartment_floor = None
            apatments_area = None
            kitchen_area = None
            balcony = False
            condition = None
            segment = None
            material = None
            address = None
            undeground_minutes = None
            undeground_name = None

            # Получение информации о квартире
            apartment_about = soup.find('div', class_="style-item-view-block-SEFaY")
            apartment_about = apartment_about.find_all('li', class_="params-paramsList__item-appQw")
            for item in apartment_about:
                item_split = item.text.split(":")

                match item_split[0]:
                    case "Количество комнат":
                        rooms = item_split[1].lstrip()
                    case "Общая площадь":
                        apatments_area = item_split[1].split("\xa0")[0].lstrip()
                    case "Площадь кухни":
                        kitchen_area = item_split[1].split("\xa0")[0].lstrip()
                    case "Этаж":
                        floor = item_split[1].split(" из ")
                        apartment_floor = floor[0].lstrip()
                        house_floors = floor[1]
                    case "Балкон или лоджия":
                        if (item.text.lower() == "балкон") or (item.text.lower() == "лоджия"): balcony = True
                        else: balcony = False
                    case "Отделка" | "Ремонт":
                        # condition = item_split[1].lstrip()
                        if (item_split[1] == " чистовая") or (item_split[1] == " дизайнерский"): condition = "современная отделка"
                        elif (item_split[1] == " косметический") or (item_split[1] == " евро"): condition = "муниципальный ремонт"
                        elif (item_split[1] == " предчистовая") or (item_split[1] == " без отделки") or (item_split[1] == " требует ремонта"): condition = "без отделки"
                    # case "Ремонт":
                        # if (item_split[1] == " требует ремонта"): condition = "без отделки"
                        # elif (item_split[1] == " евро") or (item_split[1] == " дизайнерский"): condition = "современная отделка"
                        # elif (item_split[1] == " ="): condition = "муниципальный ремонт"
            if ((rooms is None) or (apatments_area is None) or (kitchen_area is None) or (apartment_floor is None) or (house_floors is None) or (condition is None)):
                count_skiped_apartment += 1
                continue

            # Получение информации о доме
            house_about = soup.find('div', class_="style-item-params-McqZq")
            house_about = house_about.find_all("li", class_="style-item-params-list-item-aXXql")
            for item in house_about:
                item_split = item.text.split(":")

                match item_split[0]:
                    case "Название новостройки":
                        segment = 1
                    case "Год постройки":
                        year = int(item_split[1])
                        if (year > int(datetime.date.today().year)-5): segment = 1
                        elif (year >= 2000): segment = 2
                        elif  (year < 2000): segment = 3
                    case "Тип дома":
                        if (item_split[1] == "\xa0монолитно-кирпичный") or (item_split[1] == "\xa0монолитный"): material = 1
                        elif (item_split[1] == "\xa0кирпичный"): material = 2
                        elif (item_split[1] == "\xa0панельный"): material = 3
            if ((segment is None) or (material is None)):
                count_skiped_apartment += 1
                continue

            # Получение блока с информацей о метро
            position = soup.find('div', class_="style-item-address-KooqC")
            # Получение адреса
            address = position.find("span", class_="style-item-address__string-wt61A").text
            # Получение ближайшего метро к дому
            undeground = position.find('span', class_="style-item-address-georeferences-item-TZsrp")
            undeground_name = undeground.find('span', class_='').text
            # Получение времени до метро
            undeground_minutes = undeground.find('span', class_="style-item-address-georeferences-item-interval-ujKs2").text
            undeground_minutes_temp = undeground_minutes.split(" ")[0].split("–")
            if (len(undeground_minutes_temp) == 2):
                undeground_minutes = undeground_minutes_temp
                undeground_minutes = int(ceil((int(undeground_minutes[0]) + int(undeground_minutes[1])) / 2))
            else:
                undeground_minutes = undeground_minutes.split(" ")[1]
            if ((address is None) or (undeground_name is None) or (undeground_minutes is None)):
                count_skiped_apartment += 1
                continue

            # segment
            # 1 - Новостройка
            # 2 - современное жилье
            # 3 - старый жилой фонд
            # material
            # 1 - монолитный
            # 2 - кирпичный
            # 3 - панельный
            apartments_info.update({
                link.split("/")[-1]: {
                    "adress": address,
                    "undeground": undeground_name,
                    "undeground_minutes": undeground_minutes,
                    "rooms": rooms,
                    "segment": segment,
                    "house_floors": house_floors,
                    "material": material,
                    "apartment_floor": apartment_floor,
                    "apatments_area": apatments_area,
                    "kitchen_area": kitchen_area,
                    "balcony": balcony,
                    "condition": condition
                }
            })

            time.sleep(1.5)
        driver.quit()

        pprint.pprint(apartments_info)
        print(f"Обработано {len(links)} квартир, пропущенно {count_skiped_apartment}")
