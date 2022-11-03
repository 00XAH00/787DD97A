from modules.avito import avito
import time

"""
https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg
"""


def main():
    av = avito()
    links = av.get_links(50)
    # links = [
    #         "https://www.avito.ru/moskva/kvartiry/4-k._kvartira_796m_230et._2455631355",
    #         "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_454m_1131et._2447132880",
    #         "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_617m_215et._2403787091",
    #         "https://www.avito.ru/moskva/kvartiry/1-k._kvartira_415m_2030et._2451522868",
    #         "https://www.avito.ru/moskva/kvartiry/3-k._kvartira_1118m_921et._2518299010",
    #         "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_642m_418et._2642105789",
    #         "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_41m_35et._2586354037",
    #         "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_442m_35et._2604204256",
    #         "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_52m_412et._2587872760",
    #         "https://www.avito.ru/moskva/kvartiry/1-k._kvartira_444m_2024et._2525418572",
    #         "https://www.avito.ru/moskva/kvartiry/3-k._kvartira_614m_39et._2565463437",
    #         "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_65m_3257et._2553404305",
    #         "https://www.avito.ru/moskva/kvartiry/1-k._kvartira_282m_916et._2563627013"]
    time.sleep(5)
    print(f"Квартир для обработки: {len(links)}")
    start_time = time.time()
    av.get_apartments(links)
    print("--- %s seconds ---" % (time.time() - start_time))

    # av.get_apartments(soup)

if __name__ == "__main__":
    main()