from modules.avito import avito

"""
https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg
"""


def main():
    av = avito()
    # links = av.get_links()
    links = [
            "https://www.avito.ru/moskva/kvartiry/4-k._kvartira_796m_230et._2455631355",
            "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_454m_1131et._2447132880",
            "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_617m_215et._2403787091",
            "https://www.avito.ru/moskva/kvartiry/1-k._kvartira_415m_2030et._2451522868",
            "https://www.avito.ru/moskva/kvartiry/3-k._kvartira_1118m_921et._2518299010",
            "https://www.avito.ru/moskva/kvartiry/2-k._kvartira_642m_418et._2642105789"]

    av.get_apartments(links)

    # av.get_apartments(soup)

if __name__ == "__main__":
    main()