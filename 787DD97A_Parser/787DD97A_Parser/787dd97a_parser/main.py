from modules.avito import avito

"""
https://www.avito.ru/moskva/kvartiry/prodam/novostroyka-ASgBAQICAUSSA8YQAUDmBxSOUg
"""


def main():
    av = avito()
    soup = av.get_links(1)

    # av.get_apartments(soup)

if __name__ == "__main__":
    main()