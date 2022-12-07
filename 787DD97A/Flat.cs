using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcForPriceFlat
{
    public class Flat
    {

        /*
        public Flat(double NumberOfStoreys, double FloorLocation,
            double ApartmentArea, double KitchentArea, bool balcony,
            ushort DistanseFromMetroStation, string repair)*/
        public Flat(double NumberOfStoreys_in, double FloorLocation_in,
            double ApartmentArea_in, double KitchentArea_in, bool balcony,
            double DistanseFromMetroStation_in, string repair_in)
        {
            double NumberOfStoreys = NumberOfStoreys_in;

            double FloorLocation = Convert.ToDouble(FloorLocation_in);

            double ApartmentArea = Convert.ToDouble(ApartmentArea_in);

            double KitchentArea = KitchentArea_in;

            this.balcony = balcony;

            if (repair_in == "современная отделка" || repair_in == "Современная отделка" ||
                repair_in == "Чистовая отделка" || repair_in == "чистовая отделка" ||
                repair_in == "предчистовая отделка" || repair_in == "Предчистовая отделка" ||
                repair_in == "Без отделки" || repair_in == "без отделки" ||
                repair_in == "Эконом" || repair_in == "Эконом"
                )
            {
                this.repair = repair_in;
            }
            else
            {
                Console.WriteLine("Значение введено не верно : Отделка. Отделка будет приравнено без отделки");
                this.repair = "без отделки";
            }

            if (DistanseFromMetroStation_in < 0 || (DistanseFromMetroStation_in - Convert.ToDouble(Convert.ToInt32(DistanseFromMetroStation_in))) != 0)
            {
                this.DistanceFromMetroStation = (ushort)Math.Abs(DistanseFromMetroStation_in);
                Console.WriteLine("Значение введено не верно : Время до метро меньше нуля. Время до метро будет рассчитано без знака -, и округлено");
            }
            else
            {

                this.DistanceFromMetroStation = Convert.ToUInt16(Math.Abs(DistanseFromMetroStation_in));
            }



            if (NumberOfStoreys < 0 || (NumberOfStoreys - Convert.ToDouble(Convert.ToInt32(NumberOfStoreys))) != 0)
            {
                Console.WriteLine("Значение введено не верно : Количество этажей. Количество этажей будет округлено");
            }
            else this.NumberOfStoreys = (ushort)NumberOfStoreys;

            if (FloorLocation < 0 || (FloorLocation - Convert.ToDouble(Convert.ToInt32(FloorLocation))) != 0)
            {
                Console.WriteLine("Значение введено не верно : Этаж квартиры. Этаж квартиры будет округлено");

            }
            else this.FloorLocation = (ushort)FloorLocation;

            if (ApartmentArea < 0)
            {
                this.ApartmentArea = Math.Abs(ApartmentArea);
                Console.WriteLine("Значение введено не верно : Площадь квартиры. Площадь квартиры будет рассчитано без знака -");
            }
            else
                this.ApartmentArea = ApartmentArea;

            if (KitchentArea < 0 || (KitchentArea - Convert.ToDouble(Convert.ToInt32(KitchentArea))) != 0)
            {
                this.KitchentArea = (ushort)(KitchentArea);
                Console.WriteLine("Значение введено не верно : Площадь кухни. Площадь кухни будет рассчитано без знака -");
            }
            else
                this.KitchentArea = (ushort)(KitchentArea);

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
