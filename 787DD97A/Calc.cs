using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcForPriceFlat;


using ClosedXML;
using ClosedXML.Excel;

namespace CalcPriceOfFlat
{

    public class CalcPriceOfFlats                        //калькулятор стоимости квартиры
    {

        public static void SetParametersTabel(string namefile)
        {
            string file = namefile;

            IXLWorkbook wb = new XLWorkbook(file);



            IXLWorksheet ws = wb.Worksheets.Worksheet(2);//floor

            for (int i = 2; i < 6; i++)
                for (int j = 2; j < 6; j++)
                {
                    Console.WriteLine(ws.Cell(i, j).Value);
                    object value = ws.Cell(i, j).Value;
                    area[i - 1, j - 1] = Convert.ToDouble(value);
                }
            ws = wb.Worksheets.Worksheet(3);//distancefromMetro

            for (int i = 2; i < 7; i++)
                for (int j = 2; j < 8; j++)
                {
                    Console.WriteLine(ws.Cell(i, j).Value);
                    object value = ws.Cell(i, j).Value;
                    distanceformetro[i - 2, j - 2] = Convert.ToDouble(value);
                }


            wb.SaveAs(file);
        }

        static double[,] floor = new double[,]
        {
            { 0.0,   -7,  -3.1 },
            { 7.5,  0.0,   4.2 },
            { 3.2, -4.0,   0.0 }
        };

        static double[,] area = new double[,]
        {//если значение больше 14 - оно экстремальное
            {   0.0,   6.0,  14.0,  21.0,  28.0, 31.0 },
            {  -6.0,   0.0,   7.0,  14.0,  21.0, 24.0 },
            { -12.0,  -7.0,   0.0,   6.0,  13.0, 16.0 },
            { -17.0, -12.0,  -6.0,   0.0,   6.0,  9.0 },
            { -22.0, -17.0, -11.0,  -6.0,   0.0,  3.0 },
            { -24.0, -19.0, -13.0,  -8.0,  -3.0,  0.0 }
        };

        static double[,] kitchenArea = new double[,]
        {
            { 0.0,- 2.9, -8.3 },
            { 3.0,  0.0, -5.5 },
            { 9.0,  5.8,  0.0 }
        };

        static double[,] balcon = new double[,]
        {
            { 0.0, -5.0 },
            { 5.3,  0.0 }
        };

        static double[,] distanceformetro = new double[,]
        {
            { 0,  5.0,    10,     15,    30,    60,   90  },
            { 5,  0.0,   7.0,   12.0,  17.0,  24.0,  29.0 },
            {10, -7.0,   0.0,    4.0,   9.0,  15.0,  20.0 },
            {15,-11.0,  -4.0,    0.0,   5.0,  11.0,  15.0 },
            {30,-15.0,  -8.0,   -5.0,   0.0,   6.0,  10.0 },
            {60,-19.0, -13.0,  -10.0,  -6.0,   0.0,   4.0 },
            {90,-22.0, -17.0,  -13.0,  -9.0,  -4.0,   0.0 }
        };

        static double[,] repair = new double[,]
        {
            {     0.0, -13400.0, -20100.0 },
            { 13400.0,      0.0,  -6700.0 },
            { 20100.0,   6700.0,      0.0 }
        };



        static public double PriceOfFlat(Flat flat, Flats[] flats, int size_massive)                         //цена квартиры относительно другого массива квартир
        {
            for (int i = 0; i < size_massive; i++)
            {
                CorrectPrice(flat, flats[i]);
                Console.WriteLine();
            }
            double localprice = 0;
            for (int i = 0; i < size_massive; i++)//общая средняя цена без корректировок
            {
                localprice += flats[i].Price / size_massive;
            }
            double finalprice = 0;
            for (int i = 0; i < size_massive; i++)
            {
                flats[i].weightanalog = 1 / flats[i].weightprocent; //считаем вес процента нужного объекта(квартиры)*
                float local = 0;
                for (int j = 0; j < size_massive; j++)  //вычисление 
                {
                    local += 1 / flats[j].weightprocent;    //считаем вес процента каждго объекта, 
                }
                flats[i].weightanalog = flats[i].weightanalog / local;//финальная формула - делим вес вычесляемого объекта на все остальные
                finalprice += (flats[i].Price / flats[i].ApartmentArea) * flats[i].weightanalog;
            }


            Console.WriteLine("Диапазон от " + finalprice * flat.ApartmentArea + " до " + localprice);
            return finalprice * flat.ApartmentArea;
            /* 1* - вес процента нужного объекта - насколько данный объект влияет на формирование цены конечного продукта
             * вычисляется с помощью еденицы делённой на количество процентов (применённых к корректировке цены данного объекта)
             * 
             */
        }
        static public double PriceOfFlat(Flat flat, Flats flats)                         //цена квартиры, тестовая функция
        {

            CorrectPrice(flat, flats);

            return flats.Price;
        }
        static public void CorrectPrice(Flat flat, Flats flats)
        {
            Console.WriteLine(flats.Price / flats.ApartmentArea + " до всех корректировок");
            Console.WriteLine(CorrectTorg(flats) + " торг");
            Console.WriteLine(CorrectOfApartmentArea(flat, flats) + " корректировка на площадь жилья");
            Console.WriteLine(CorrectOfDistanseFromMetroStation(flat, flats) + " корректировка на время от метро");
            Console.WriteLine(CorrectFloor(flat, flats) + " корректировка на этаж");
            Console.WriteLine(CorrectCitchen(flat, flats) + " корректировка на площадь кухни");
            Console.WriteLine(CorrectBalcon(flat, flats) + " корректировка на наличие балкона");
            Console.WriteLine(CorrectOfRepair(flat, flats) + " корректировка на ремонт");

        }

        /*Функции корректировки стоиомости подобранных квартир*/
        static private double CorrectTorg(Flats flats)
        {
            flats.weightprocent += (float)4.5;
            flats.Price -= flats.Price / 100 * 4.5;
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectFloor(Flat flat, Flats flats)                         //корректировка по этажам
        {
            int i = -1;
            int j = -1;
            if (flat.FloorLocation == 1) i = 0;
            else if (flat.FloorLocation == flat.NumberOfStoreys) i = 2;
            else i = 1;//если не 1 и не последний этаж то значит средний этаж

            if (flats.FloorLocation == 1) j = 0;
            else if (flats.FloorLocation == flats.NumberOfStoreys) j = 2;
            else j = 1;//если не 1 и не последний этаж то значит средний этаж

            flats.Price += (flats.Price / 100 * floor[i, j]);
            flats.weightprocent += (float)floor[i, j];

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectCitchen(Flat flat, Flats flats)
        {
            /*Для того, чтобы работало, сделать на 1 меньше i при срабатывании условия*/
            int i = -1;
            int j = -1;
            if (flat.KitchentArea < 7) i = 1;
            else if (flat.KitchentArea >= 7 && flat.KitchentArea < 10) i = 2;
            else if (flat.KitchentArea >= 10 && flat.KitchentArea < 15) i = 3;

            if (flats.KitchentArea < 7) j = 1;
            else if (flats.KitchentArea >= 7 && flats.KitchentArea < 10) j = 2;
            else if (flats.KitchentArea >= 10 && flats.KitchentArea < 15) j = 3;

            flats.Price += flats.Price / 100 * kitchenArea[i, j];
            flats.weightprocent += (float)kitchenArea[i, j];

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectBalcon(Flat flat, Flats flats)
        {
            if (flat.balcony == flats.balcony) return flats.Price / flats.ApartmentArea;
            if (flat.balcony)
                if (!flats.balcony)
                {
                    flats.Price += flats.Price / 100 * balcon[1, 0];//если балкон есть в оцениваемой квартире, но нет в эталоне
                    flats.weightprocent += (float)balcon[1, 0];
                }
            if (!flat.balcony)
                if (flats.balcony)
                {
                    flats.Price -= flats.Price / 100 * balcon[0, 1];//если балкона нет в оцениваемой квартире, но есть в эталоне
                    flats.weightprocent += (float)balcon[0, 1];
                }
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfRepair(Flat flat, Flats flats)
        {
            if (flat.repair == flats.repair) return flats.Price / flats.ApartmentArea;

            int i = -1;
            int j = -1;

            if (flat.repair == "Без отделки" || flat.repair == "без отделки") i = 1;
            else if (flat.repair == "Эконом" || flat.repair == "эконом") i = 2;
            else if (flat.repair == "Улучшенный" || flat.repair == "улучшенный") i = 3;

            if (flats.repair == "Без отделки" || flat.repair == "без отделки") j = 1;
            else if (flats.repair == "Эконом" || flat.repair == "эконом") j = 2;
            else if (flats.repair == "Улучшенный" || flat.repair == "улучшенный") j = 3;

            double priceLocal = FUNCm2(flats.ApartmentArea, flats.Price);
            flats.weightprocent += ((float)repair[i, j] / (float)priceLocal) * 100f;
            priceLocal += repair[i, j];
            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfApartmentArea(Flat flat, Flats flats)
        {

            int i = -1;
            int j = -1;

            if (flat.ApartmentArea < 30) i = 1;
            else if (flat.ApartmentArea >= 30 && flat.ApartmentArea < 50) i = 2;
            else if (flat.ApartmentArea >= 50 && flat.ApartmentArea < 65) i = 3;
            else if (flat.ApartmentArea >= 65 && flat.ApartmentArea < 90) i = 4;
            else if (flat.ApartmentArea >= 90 && flat.ApartmentArea < 120) i = 5;
            else if (flat.ApartmentArea > 120) i = 6;

            if (flats.ApartmentArea < 30) j = 1;
            else if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) j = 2;
            else if (flats.ApartmentArea >= 50 && flats.ApartmentArea < 65) j = 3;
            else if (flats.ApartmentArea >= 65 && flats.ApartmentArea < 90) j = 4;
            else if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) j = 5;
            else if (flats.ApartmentArea > 120) j = 6;

            flats.Price += (flats.Price / 100 * area[i, j]);
            flats.weightprocent += (float)area[i, j];

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfDistanseFromMetroStation(Flat flat, Flats flats)
        {
            int i = -1;
            int j = -1;
            if (flat.DistanceFromMetroStation < distanceformetro[1, 0]) i = 1;
            else if (flat.DistanceFromMetroStation >= 5 && flat.DistanceFromMetroStation < 10) i = 2;
            else if (flat.DistanceFromMetroStation >= 10 && flat.DistanceFromMetroStation < 15) i = 3;
            else if (flat.DistanceFromMetroStation >= 15 && flat.DistanceFromMetroStation < 30) i = 4;
            else if (flat.DistanceFromMetroStation >= 30 && flat.DistanceFromMetroStation < 60) i = 5;
            else if (flat.DistanceFromMetroStation >= 60 && flat.DistanceFromMetroStation < 90) i = 6;

            if (flats.DistanceFromMetroStation < 5) j = 1;
            else if (flats.DistanceFromMetroStation >= 5 && flats.DistanceFromMetroStation < 10) j = 2;
            else if (flats.DistanceFromMetroStation >= 10 && flats.DistanceFromMetroStation < 15) j = 3;
            else if (flats.DistanceFromMetroStation >= 15 && flats.DistanceFromMetroStation < 30) j = 4;
            else if (flats.DistanceFromMetroStation >= 30 && flats.DistanceFromMetroStation < 60) j = 5;
            else if (flats.DistanceFromMetroStation >= 60 && flats.DistanceFromMetroStation < 90) j = 6;

            flats.Price += (flats.Price / 100 * distanceformetro[i, j]);
            flats.weightprocent += (float)distanceformetro[i, j];

            return flats.Price / flats.ApartmentArea;
        }

        /*служебные функции*/
        static private double FUNCm2(double s, double price)
        {
            return price / s;
        }
        static private double FUNCprice(double s, double m2Price)
        {
            return m2Price * s;
        }



    }
}
