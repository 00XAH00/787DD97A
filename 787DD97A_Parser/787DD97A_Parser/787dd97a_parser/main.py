from modules.avito import avito
import time
from json import dump
import schedule



av = avito()

def AvitoParsing():
    print("Start Parsing")
    links = av.get_links(1)
    print(f"Квартир для обработки: {len(links)}")
    time.sleep(120)
    start_time = time.time()
    av.get_apartments(links)
    print("--- %s seconds ---" % (time.time() - start_time))
    print("End parsing")

def main():
    while True:
        schedule.every().day.at("00:00").do(AvitoParsing)

if __name__ == "__main__":
    main()
    # AvitoParsing()