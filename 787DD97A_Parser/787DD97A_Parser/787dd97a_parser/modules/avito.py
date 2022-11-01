import requests
from bs4 import BeautifulSoup

from selenium import webdriver
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.common.by import By

class avito():
    def __init__(self):
        self.URL = "https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg"
        opts = Options()
        self.driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=opts)


    def get_links(self, page:int) -> BeautifulSoup:
        self.driver.get(self.URL)
        # tracks = self.driver.find_elements(By.CLASS_NAME, 'link-link-MbQDP')
        tracks = self.driver.find_elements(By.CLASS_NAME, 'iva-item-content-rejJg') # Находим блок с квартирой
        # print(tracks[1].text)


        # print(len(tracks))
        # links = tracks[0].find_elements(By.CLASS_NAME, 'link-link-MbQDP')
        # print("start out")
        # print(len(links))
        # for link in links[0:10]: print(link.get_property("href") )

        for item in tracks:
            links = item.find_elements(By.CLASS_NAME, 'link-link-MbQDP') # Находим ссылки
            print(links[0].get_property('href'))

    def get_apartments(self, soup:BeautifulSoup):
        apartments_div = soup.find_all('div', class_="iva-item-content-rejJg")
        print(apartments_div)
