import datetime
import time, threading, pprint
from bs4 import BeautifulSoup
from math import ceil
# from json import dumps
from modules.db import db
from re import sub

from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.desired_capabilities import DesiredCapabilities
from selenium.webdriver.common.by import By


class avito():
    def __init__(self):
        self.database = db()
        self.URLS = ["https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg", "https://www.avito.ru/moskva/kvartiry/prodam/vtorichka-ASgBAQICAUSSA8YQAUDmBxSMUg"]

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
        webdriver.DesiredCapabilities.CHROME['acceptSslCerts']=True
        self.caps = DesiredCapabilities().CHROME

        self.driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)

    def get_links(self, devision:int) -> list:
        apartment_links = []
        def thread_links_get(start:int, end:int, url:str):
            driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)
            for page in range(start, end):
                # Получаем нужную страницу
                driver.get(f"{url}?p={page}")
                source = driver.page_source
                soup = BeautifulSoup(source, 'lxml')
                # Получение ссылки на квартиры
                apartments_div = soup.find_all('div', class_='iva-item-content-rejJg') # Находим блок с квартирой
                for item in apartments_div:
                    link = item.find_all('a', class_='link-link-MbQDP')[0].get('href') # Поулчаем ссылку на объявление
                    apartment_links.append(f"https://www.avito.ru{link}")
                time.sleep(0.3)
            driver.quit()

        # Получение ссылок на новые и бу квартиры
        for apurl in self.URLS:
            self.driver.get(apurl)
            last_page = int(self.driver.find_elements(By.CLASS_NAME, 'pagination-item-JJq_j')[-2].text) # находим последнюю страницу

            thread_first = threading.Thread(target=thread_links_get, args=(1, (int(last_page/devision))+1, apurl))
            thread_first.start()
            thread_first.join()
        self.driver.quit()

        # thread_second = threading.Thread(target=thread_links_get, args=((int((last_page/devision)//2)), (int(last_page/devision)+1)))
        # thread_second.start()
        # thread_second.join()

        return apartment_links

    def get_apartments(self, links:list) -> dict:
        driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)
        count_skiped_apartment = 0
        count_apartment_done = 0
        for link in links:
            time.sleep(3)
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
            price = None
            balcony = False


            # Получение информации о квартире
            try:
                apartment_about = soup.find_all('div', class_="style-item-view-block-SEFaY")
                for div in apartment_about:
                    temp = div.find_all('li', class_="params-paramsList__item-appQw")
                    if (len(temp) != 0): 
                        apartment_about = temp
                        break
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
                            if (item_split[1] == " балкон") or (item_split[1] == " лоджия"): balcony = True
                            else: balcony = False
                        case "Отделка" | "Ремонт":
                            if (item_split[1] == " чистовая") or (item_split[1] == " дизайнерский"): condition = "современная отделка"
                            elif (item_split[1] == " косметический") or (item_split[1] == " евро"): condition = "муниципальный ремонт"
                            elif (item_split[1] == " предчистовая") or (item_split[1] == " без отделки") or (item_split[1] == " требует ремонта"): condition = "без отделки"
            except: continue
            if ((rooms is None) or (apatments_area is None) or (kitchen_area is None) or (apartment_floor is None) or (house_floors is None) or (condition is None)):
                count_skiped_apartment += 1
                print(f"link {link}     rooms: {(rooms is None)}    apatments_area: {(apatments_area is None)}  kitchen_area: {(kitchen_area is None)}   apartment_floor: {(apartment_floor is None)}   house_floors: {(house_floors is None)}    condition:{(condition is None)}")
                continue

            # Получение информации о доме
            try:
                house_about = soup.find('div', class_="style-item-params-McqZq")
                for div in house_about:
                    temp = div.find_all("li", class_="style-item-params-list-item-aXXql")
                    if (len(temp) != 0):
                        house_about = temp
                        break
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
            except: continue
            if ((segment is None) or (material is None)):
                count_skiped_apartment += 1
                continue

            # Получение информации о местоположении
            try:
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
            except: continue
            if ((address is None) or (undeground_name is None) or (undeground_minutes is None)):
                count_skiped_apartment += 1
                print(f"skip because: adress: {(address is None)}     undeground_name: {(undeground_name is None)}     undeground_minutes: {(undeground_minutes is None)}")
                continue

            # Получение стоимости квартиры
            try:
                price = soup.find_all('div', class_="style-item-price-PuQ0I")
                for div in price:
                    temp = div.find_all('span', class_="js-item-price")
                    if (len(temp) != 0):
                        price = sub('\xa0', '', temp[0].text)
                        price = int(price)
                        break
            except: continue
            if (price is None):
                count_skiped_apartment +=1
                continue

            # segment
            # 1 - Новостройка
            # 2 - современное жилье
            # 3 - старый жилой фонд
            # material
            # 1 - монолитный
            # 2 - кирпичный
            # 3 - панельный
            apartments_info = {
                "link": link.split("/")[-1],
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
                "condition": condition,
                "price": price
            }
            count_apartment_done += 1
            self.database.add_apartment(apartments_info)

        driver.quit()
        print(f"Всего квартир: {len(links)}\nОбработано {count_apartment_done}\nПропущенно {count_skiped_apartment}")
