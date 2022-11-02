import datetime
import time, threading, pprint
from bs4 import BeautifulSoup

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
        for link in links[5:8]: # [0:1]
            driver.get(link)
            source = driver.page_source
            soup = BeautifulSoup(source, 'lxml')
            apartment_about = soup.find('div', class_="style-item-view-block-SEFaY")
            apartment_about = apartment_about.find_all('li', class_="params-paramsList__item-appQw")

            rooms = None
            segment = None
            house_floors = None
            material = None
            apartment_floor = None
            apatments_area = None
            kitchen_area = None
            balcony = False

            condition = None
            for item in apartment_about:
                item_split = item.text.split(":")

                match item_split[0]:
                    case "Количество комнат":
                        rooms = item_split[1]
                    case "Общая площадь":
                        apatments_area = item_split[1].split("\xa0")[0]
                    case "Площадь кухни":
                        kitchen_area = item_split[1].split("\xa0")[0]
                    case "Этаж":
                        floor = item_split[1].split(" из ")
                        apartment_floor = floor[0]
                        house_floors = floor[1]
                    case "Балкон или лоджия":
                        if (item.text.lower() == "балкон") or (item.text.lower() == "лоджия"): balcony = True
                        else: balcony = False
                    case "Отделка":
                        condition = item_split[1]
                    # case "Отделка":
                    #     material = item_split[1]
                    # case "Срок сдачи":
                    #     segment = 1

            house_about = soup.find('div', class_="style-item-params-McqZq")
            house_about = house_about.find_all("li", class_="style-item-params-list-item-aXXql")
            for item in house_about:
                item_split = item.text.split(":")

                # print([item_split[0]])
                match item_split[0]:
                    case "Название новостройки":
                        segment = 1
                    case "Год постройки":
                        year = int(item_split[1])
                        print(year)
                        if (year > int(datetime.date.today().year)-5): segment = 1
                        elif (year >= 2000): segment = 2
                        elif  (year < 2000): segment = 3
                    case "Тип дома":
                        if (item_split[1] == "\xa0монолитно-кирпичный") or (item_split[1] == "\xa0монолитный"): material = 1
                        elif (item_split[1] == "\xa0кирпичный"): material = 2
                        elif (item_split[1] == "\xa0панельный"): material = 3

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
                    "rooms": rooms,
                    "segment": segment,
                    "house_floors": house_floors,
                    "material": material,
                    "apartment_floor": apartment_floor,
                    "apatments_area": apatments_area,
                    "kitchen_area": kitchen_area,
                    "balcony": balcony,
                    "metro_distance": None,
                    "condition": condition
                }
            })
            #  = apartment_about[5]
            time.sleep(0.5)
        driver.quit()

        pprint.pprint(apartments_info)
