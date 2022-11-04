from modules.avito import avito
import time
from json import dump

"""
https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg
"""


def main():
    av = avito()
    links = av.get_links(100)
    print(f"Квартир для обработки: {len(links)}")
    time.sleep(120)
    start_time = time.time()
    apartments = av.get_apartments(links)
    print("--- %s seconds ---" % (time.time() - start_time))

    with open('data.json', 'w') as f:
        dump(apartments, f)
    # # av.get_apartments(soup)

    # av.proxy_test()

if __name__ == "__main__":
    main()