import requests, time, threading
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
        self.caps = DesiredCapabilities().CHROME
        self.caps["pageLoadStrategy"] = "eager"  #  interactive
        self.driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)


    def get_links(self) -> BeautifulSoup:
        apartment_links = []
        def thread_links_get(start:int, end:int):
            driver = webdriver.Chrome('/Users/xah/Projects/787DD97A_calc/787DD97A_Parser/787DD97A_Parser/787dd97a_parser/modules/chromedriver', options=self.opts, desired_capabilities=self.caps)
            for page in range(start, end):
                driver.get(f"{self.URL}?p={page}")
                source = driver.page_source
                soup = BeautifulSoup(source, 'lxml')

                print(page)
                apartments_div = soup.find_all('div', class_='iva-item-content-rejJg') # Находим блок с квартирой
                for item in apartments_div:
                    link = item.find_all('a', class_='link-link-MbQDP')[0].get('href') # Находим ссылку
                    apartment_links.append(f"https://www.avito.ru{link}")

            driver.quit()

        self.driver.get(self.URL)
        last_page = int(self.driver.find_elements(By.CLASS_NAME, 'pagination-item-JJq_j')[-2].text)+1 # находим последнюю страницу
        self.driver.quit()


        # thread_first = threading.Thread(target=thread_links_get, args=(1, 2))
        thread_first = threading.Thread(target=thread_links_get, args=(1, (last_page//3)))
        thread_second = threading.Thread(target=thread_links_get, args=((last_page//3), (last_page//3)*2))
        thread_third = threading.Thread(target=thread_links_get, args=((last_page//3)*2, last_page))

        start_time = time.time()
        thread_first.start()
        thread_second.start()
        thread_third.start()

        thread_first.join()
        thread_second.join()
        thread_third.join()
        print("--- %s seconds ---" % (time.time() - start_time))
        #
        # Потоки не доделаны, почитать про ускорение bs4
        #


        print(len(apartment_links))

    def get_apartments(self, soup:BeautifulSoup):
        apartments_div = soup.find_all('div', class_="iva-item-content-rejJg")
        print(apartments_div)
