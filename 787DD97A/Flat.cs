using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcForPriceFlat
{
    public class Flat
    {
        public Flat(string LocationFlat, byte NumbersOfRoom, string MarketSegment,
            ushort NumberOfStoreys, string WallMaterial, ushort FloorLocation, 
            double ApartmentArea, ushort KitchentArea,bool balcony,
            ushort DistanseFromMetroStation, string repair)
        {
            this.LocationFlat = LocationFlat;
            this.NumbersOfRoom = NumbersOfRoom;
            this.MarketSegment = MarketSegment;
            this.NumberOfStoreys = NumberOfStoreys;
            this.WallMaterial = WallMaterial;
            this.FloorLocation = FloorLocation;
            this.ApartmentArea = ApartmentArea;
            this.KitchentArea = KitchentArea;
            this.balcony = balcony;
            this.DistanceFromMetroStation = DistanseFromMetroStation;
            this.repair = repair;

        }

        /*Обязательные ценообразующие факторы*/
        public string LocationFlat { get; set; } = "";             //местоположение квартиры
        public byte NumbersOfRoom { get; set; } = 0;               //количество комнат от 0 до 255
        public string MarketSegment { get; set; } = "";            //сегмент рынка - вторичка, новостройка
        public ushort NumberOfStoreys { get; set; } = 0;           //количество этажей от 0 до 65535
        public string WallMaterial { get; set; } = "";             //материал стен
        /*Необязательные ценообразующие факторы*/
        public ushort FloorLocation { get; set; } = 0;             //этаж продаваемой квартиры
        public double ApartmentArea { get; set; } = 0;             //площадь квартиры
        public ushort KitchentArea { get; set; } = 0;              //площадь кухни
        public bool balcony { get; set; } = false;                 //наличие или отсутствие балкона
        public ushort DistanceFromMetroStation { get; set; } = 0;  //удалённость от ближайшего метро
        public string repair { get; set; } = "";
        /*цены квартиры рассчитывается и возвращается методом. Поле - "цена квартиры" использовать 
         * нецелесообразно из-за размера переменной, которая при этом будет использоваться*/

    }
}
