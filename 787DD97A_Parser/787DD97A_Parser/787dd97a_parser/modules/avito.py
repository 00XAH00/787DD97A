import requests, time
from bs4 import BeautifulSoup

from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.desired_capabilities import DesiredCapabilities
from selenium.webdriver.common.by import By

class avito():
    def __init__(self):
        self.URL = "https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg"
        opts = Options()

        caps = DesiredCapabilities().CHROME
        caps["pageLoadStrategy"] = "eager"  #  interactive
        opts.add_argument("--headless")
        self.driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=opts, desired_capabilities=caps)


    def get_links(self, page:int) -> BeautifulSoup:
        self.driver.get(self.URL)
        last_page = int(self.driver.find_elements(By.CLASS_NAME, 'pagination-item-JJq_j')[-2].text)+1

        start_time = time.time()
        apartment_links = []
        for page in range(1, last_page):
            self.driver.get(f"{self.URL}?p={page}")

            tracks = self.driver.find_elements(By.CLASS_NAME, 'iva-item-content-rejJg') # Находим блок с квартирой
            print(page)
            for item in tracks:
                links = item.find_elements(By.CLASS_NAME, 'link-link-MbQDP') # Находим ссылки
                apartment_links.append(links[0].get_property('href'))
                # print(links[0].get_property('href'))
        print("--- %s seconds ---" % (time.time() - start_time))
            
        print(len(apartment_links))

    def get_apartments(self, soup:BeautifulSoup):
        apartments_div = soup.find_all('div', class_="iva-item-content-rejJg")
        print(apartments_div)
