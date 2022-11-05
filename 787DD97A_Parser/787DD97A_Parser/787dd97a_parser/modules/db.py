import pymysql
from os import environ
from os.path import join, dirname
from dotenv import load_dotenv


class db():
    def __init__(self):
        self.HOST = self.get_from_env('BDHost')
        self.BDNAME = self.get_from_env('BDName')
        self.USER = self.get_from_env('BDUser')
        self.PASSWORD = self.get_from_env('BDPass')
        self.PORT = int(self.get_from_env('BDPort'))

    def get_from_env(self, key):
        dotenv_pass = join(dirname(__file__), '.env')
        load_dotenv(dotenv_pass)
        return environ.get(key)

    def add_apartment(self, apartment:dict):
        try:
            connection = pymysql.connect(host=self.HOST, database=self.BDNAME, user=self.USER, password=self.PASSWORD, port=self.PORT, cursorclass=pymysql.cursors.DictCursor)
            try:
                with connection.cursor() as cursor:
                    cursor.execute('INSERT INTO `Apartments` ' +
                    '(`Adress`, `Undeground`, `Undeground_minutes`, `Rooms`, `Segment`, `House_floors`, `Material`, `Apartment_floor`, `Kitchen_area`, `Balcony`, `Condition`, `Link`, `Apatments_area`) ' +
                    f'VALUES (\'{apartment.get("adress")}\', \'{apartment.get("undeground")}\', {apartment.get("undeground_minutes")}, {apartment.get("rooms")}, {apartment.get("segment")}, {apartment.get("house_floors")}, {apartment.get("material")}, {apartment.get("apartment_floor")}, {apartment.get("kitchen_area")}, {apartment.get("balcony")}, \'{apartment.get("condition")}\', \'{apartment.get("link")}\', {apartment.get("apatments_area")})')
                    connection.commit()
                    return 0
            finally:
                connection.close()
        except:
            return False