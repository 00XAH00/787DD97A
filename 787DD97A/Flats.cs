using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcForPriceFlat
{
    public class Flats
    {
        public Flats(ushort NumberOfStoreys, ushort FloorLocation,
            double ApartmentArea, ushort KitchentArea, bool balcony,
            ushort DistanseFromMetroStation, uint Price, string repair)
        {
            this.NumberOfStoreys = NumberOfStoreys;
            this.FloorLocation = FloorLocation;
            this.ApartmentArea = ApartmentArea;
            this.KitchentArea = KitchentArea;
            this.balcony = balcony;
            this.DistanceFromMetroStation = DistanseFromMetroStation;
            this.repair = repair;
            this.Price = Price;
        }

        

        /*Входные параметры (из БД)*/
        public ushort NumberOfStoreys { get; set; } = 0;           //формат ushort (0-32000)        
        public ushort FloorLocation { get; set; } = 0;             //этаж продаваемой квартиры формат ushort (0-32000)
        public double ApartmentArea { get; set; } = 0;             //площадь квартиры
        public ushort KitchentArea { get; set; } = 0;              //площадь кухни
        public bool balcony { get; set; } = false;                 //наличие или отсутствие балкона
        public ushort DistanceFromMetroStation { get; set; } = 0;  //удалённость от ближайшего метро
        public string repair { get; set; } = "";                   //ремонт, может принимать "Без отделки","Эконом","Улучшенный"
        public double Price { get; set; } = 0;                     //цена
        
        /*Необходимы при вычислении, но не выходные*/
        public float weightprocent = 0;
        public float weightanalog = 0;
        
    }
}
