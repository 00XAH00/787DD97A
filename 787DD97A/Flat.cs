using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcForPriceFlat
{
    public class Flat
    {
        public Flat(ushort NumberOfStoreys, ushort FloorLocation, 
            double ApartmentArea, ushort KitchentArea,bool balcony,
            ushort DistanseFromMetroStation, string repair)
        {
            this.NumberOfStoreys = NumberOfStoreys;
            this.FloorLocation = FloorLocation;
            this.ApartmentArea = ApartmentArea;
            this.KitchentArea = KitchentArea;
            this.balcony = balcony;
            this.DistanceFromMetroStation = DistanseFromMetroStation;
            this.repair = repair;

        }

        public Flat()
        {
            this.NumberOfStoreys = 0;
            this.FloorLocation = 0;
            this.ApartmentArea = 0;
            this.KitchentArea = 0;
            this.balcony = false;
            this.DistanceFromMetroStation = 0;
            this.repair = "Без отделки";
        }

        /*Обязательные ценообразующие факторы*/
        public ushort NumberOfStoreys { get; set; } = 0;           //количество этажей от 0 до 65535
        /*Необязательные ценообразующие факторы*/
        public ushort FloorLocation { get; set; } = 0;             //этаж продаваемой квартиры
        public double ApartmentArea { get; set; } = 0;             //площадь квартиры
        public ushort KitchentArea { get; set; } = 0;              //площадь кухни
        public bool balcony { get; set; } = false;                 //наличие или отсутствие балкона
        public ushort DistanceFromMetroStation { get; set; } = 0;  //удалённость от ближайшего метро
        public string repair { get; set; } = "";

    }
}
