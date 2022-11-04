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

    # def add_apartment(self, apartment):
    #     try:
    #         connection = pymysql.connect(host=self.host, database=self.bdname, user=self.user, password=self.password, port=self.port, cursorclass=pymysql.cursors.DictCursor)
    #         try:
    #             with connection.cursor() as cursor:
    #                 cursor.execute('INSERT INTO `Apartments` ' +
    #                 '(Adress, Undeground, Undeground_minutes, Rooms, Segment, House_floors, Material, Apartment_floor, Kitchen_area, Balcony, Condition) ' +
    #                 f'VALUES (\'{chat_id}\', \'{username}\', \'None\', \'{0}\', \'{0}\')')
    #                 connection.commit()
    #                 return 0 
    #         finally:
    #             connection.close()
    #     except:
    #         return False

