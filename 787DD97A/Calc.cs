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
using DocumentFormat.OpenXml.InkML;

namespace CalcPriceOfFlat
{

    public class CalcPriceOfFlats                        //калькулятор стоимости квартиры
    {

        public static void SetParametersTabel(string namefile)//парсер Excel файла для параметров калькулятора
        {         

            IXLWorkbook wb = new XLWorkbook(namefile);

            IXLWorksheet ws = wb.Worksheets.Worksheet(1);//floor

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {                    
                    object value = ws.Cell(i + 2, j + 2).Value;
                    floor[i, j] = Convert.ToDouble(value);
                }
            
            ws = wb.Worksheets.Worksheet(2);//area

            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    object value = ws.Cell(i+1, j+1).Value;
                    area[i , j ] = Convert.ToDouble(value);
                }

            ws = wb.Worksheets.Worksheet(3);//kicthenarea

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                {
                    object value = ws.Cell(i + 1, j + 1).Value;
                    kitchenArea[i, j] = Convert.ToDouble(value);
                }

            ws = wb.Worksheets.Worksheet(4);//balcon

            for (int i = 0; i < 2; i++)
                for (int j = 0; j < 2; j++)
                {
                    object value = ws.Cell(i + 2, j + 2).Value;
                    balcon[i, j] = Convert.ToDouble(value);
                }
            ws = wb.Worksheets.Worksheet(5);//distancefromMetro

            for (int i = 0; i < 7; i++)
                for (int j = 0; j < 7; j++)
                {
                    object value = ws.Cell(i + 1, j + 1).Value;
                    distance[i, j] = Convert.ToDouble(value);
                }
            ws = wb.Worksheets.Worksheet(6);//repair

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    object value = ws.Cell(i + 2, j + 2).Value;
                    repair[i, j] = Convert.ToDouble(value);
                }
            ws = wb.Worksheets.Worksheet(7);//torg

            
            
            object value2 = ws.Cell(1, 1).Value;
            torg = Convert.ToDouble(value2);
            

            wb.SaveAs(namefile);
        }

        static double torg = 4.5;

        static double[,] floor = new double[,]
        {
            { 0.0,   -7,  -3.1 },
            { 7.5,  0.0,   4.2 },
            { 3.2, -4.0,   0.0 }
        };

        static double[,] area = new double[7, 7]
        {   //если значение больше 14 - оно экстремальное
            {   0,    30,    50,    65,    90,   120,  150 },
            {  30,   0.0,   6.0,  14.0,  21.0,  28.0, 31.0 },
            {  50,  -6.0,   0.0,   7.0,  14.0,  21.0, 24.0 },
            {  65, -12.0,  -7.0,   0.0,   6.0,  13.0, 16.0 },
            {  90, -17.0, -12.0,  -6.0,   0.0,   6.0,  9.0 },
            { 120, -22.0, -17.0, -11.0,  -6.0,   0.0,  3.0 },
            { 150, -24.0, -19.0, -13.0,  -8.0,  -3.0,  0.0 }
        };

        static double[,] kitchenArea = new double[,]
        {
            {  0,   7,   10,   15 },
            {  7, 0.0, -2.9, -8.3 },
            { 10, 3.0,  0.0, -5.5 },
            { 15, 9.0,  5.8,  0.0 }
        };

        static double[,] balcon = new double[,]
        {
            { 0.0, -5.0 },
            { 5.3,  0.0 }
        };

        static double[,] distance = new double[,]
        {
            //если значение больше 14 - оно экстремальное
            {  0,   5.0,    10,     15,    30,    60,   90  },
            {  5,   0.0,   7.0,   12.0,  17.0,  24.0,  29.0 },
            { 10,  -7.0,   0.0,    4.0,   9.0,  15.0,  20.0 },
            { 15, -11.0,  -4.0,    0.0,   5.0,  11.0,  15.0 },
            { 30, -15.0,  -8.0,   -5.0,   0.0,   6.0,  10.0 },
            { 60, -19.0, -13.0,  -10.0,  -6.0,   0.0,   4.0 },
            { 90, -22.0, -17.0,  -13.0,  -9.0,  -4.0,   0.0 }
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
                if (flats[i]!= null)
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
            Console.WriteLine(CorrectTorg(flats));
            Console.WriteLine(CorrectOfApartmentArea(flat, flats));
            Console.WriteLine(CorrectOfDistanseFromMetroStation(flat, flats));
            Console.WriteLine(CorrectFloor(flat, flats));
            Console.WriteLine(CorrectCitchen(flat, flats));
            Console.WriteLine(CorrectBalcon(flat, flats));
            Console.WriteLine(CorrectOfRepair(flat, flats));
        }

        /*Функции корректировки стоиомости подобранных квартир*/
        static private double CorrectTorg(Flats flats)
        {
            flats.weightprocent += Math.Abs((float)torg);
            flats.Price -= flats.Price / 100 * torg;
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
            flats.weightprocent += Math.Abs((float)floor[i, j]);

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectCitchen(Flat flat, Flats flats)
        {
            /*Для того, чтобы работало, сделать на 1 меньше i при срабатывании условия*/
            int i = -1;
            int j = -1;
            if (flat.KitchentArea < kitchenArea[1, 0]) i = 1;
            else if (flat.KitchentArea >= kitchenArea[1, 0] && flat.KitchentArea < kitchenArea[2, 0]) i = 2;
            else if (flat.KitchentArea >= kitchenArea[2, 0] && flat.KitchentArea < kitchenArea[3, 0]) i = 3;
            else i = 3;
            if (flats.KitchentArea < kitchenArea[0, 1]) j = 1;
            else if (flats.KitchentArea >= kitchenArea[0, 1] && flats.KitchentArea < kitchenArea[0, 2]) j = 2;
            else if (flats.KitchentArea >= kitchenArea[0, 2] && flats.KitchentArea < kitchenArea[0, 3]) j = 3;
            else j = 3;
            flats.Price += flats.Price / 100 * kitchenArea[i, j];
            flats.weightprocent += Math.Abs((float)kitchenArea[i, j]);

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectBalcon(Flat flat, Flats flats)
        {
            if (flat.balcony == flats.balcony) return flats.Price / flats.ApartmentArea;
            if (flat.balcony)
                if (!flats.balcony)
                {
                    flats.Price += flats.Price / 100 * balcon[1, 0];//если балкон есть в оцениваемой квартире, но нет в эталоне
                    flats.weightprocent += Math.Abs((float)balcon[1, 0]);
                }
            if (!flat.balcony)
                if (flats.balcony)
                {
                    flats.Price -= flats.Price / 100 * balcon[0, 1];//если балкона нет в оцениваемой квартире, но есть в эталоне
                    flats.weightprocent += Math.Abs((float)balcon[0, 1]);
                }
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfRepair(Flat flat, Flats flats)
        {
            if (flat.repair == flats.repair) return flats.Price / flats.ApartmentArea;

            int i = -1;
            int j = -1;

            if (flat.repair == "Без отделки" || flat.repair == "без отделки") i = 1;
            else if (flat.repair == "Эконом" || flat.repair == "эконом" || flat.repair == "Муниципальный ремонт") i = 2;
            else if (flat.repair == "Улучшенный" || flat.repair == "улучшенный" || flat.repair == "современная отделка") i = 3;

            if (flats.repair == "Без отделки" || flat.repair == "без отделки") j = 1;
            else if (flats.repair == "Эконом" || flat.repair == "эконом" || flats.repair == "Муниципальный ремонт") j = 2;
            else if (flats.repair == "Улучшенный" || flat.repair == "улучшенный" || flats.repair == "современная отделка") j = 3;

            double priceLocal = FUNCm2(flats.ApartmentArea, flats.Price);
            flats.weightprocent += Math.Abs((float)repair[i, j] / (float)priceLocal) * 100f;
            priceLocal += repair[i, j];
            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfApartmentArea(Flat flat, Flats flats)
        {

            int i = -1;
            int j = -1;

            if (flat.ApartmentArea < area[1,0]) i = 1;
            else if (flat.ApartmentArea >= area[1, 0] && flat.ApartmentArea < area[2, 0]) i = 2;
            else if (flat.ApartmentArea >= area[2, 0] && flat.ApartmentArea < area[3, 0]) i = 3;
            else if (flat.ApartmentArea >= area[3, 0] && flat.ApartmentArea < area[4, 0]) i = 4;
            else if (flat.ApartmentArea >= area[4, 0] && flat.ApartmentArea < area[5, 0]) i = 5;
            else if (flat.ApartmentArea > area[5, 0] ) i = 6;

            if (flats.ApartmentArea < area[0, 1]) j = 1;
            else if (flats.ApartmentArea >= area[0, 1] && flats.ApartmentArea < area[0, 2]) j = 2;
            else if (flats.ApartmentArea >= area[0, 2] && flats.ApartmentArea < area[0, 3]) j = 3;
            else if (flats.ApartmentArea >= area[0, 3] && flats.ApartmentArea < area[0, 4]) j = 4;
            else if (flats.ApartmentArea >= area[0, 4] && flats.ApartmentArea < area[0, 5]) j = 5;
            else if (flats.ApartmentArea > area[0, 5]) j = 6;

            flats.Price += (flats.Price / 100 * area[i, j]);
            flats.weightprocent +=Math.Abs((float)area[i, j]);

            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfDistanseFromMetroStation(Flat flat, Flats flats)
        {
            int i = -1;
            int j = -1;
            if (flat.DistanceFromMetroStation < distance[1, 0]) i = 1;
            else if (flat.DistanceFromMetroStation >= distance[1, 0] && flat.DistanceFromMetroStation < distance[2, 0]) i = 2;
            else if (flat.DistanceFromMetroStation >= distance[2, 0] && flat.DistanceFromMetroStation < distance[3, 0]) i = 3;
            else if (flat.DistanceFromMetroStation >= distance[3, 0] && flat.DistanceFromMetroStation < distance[4, 0]) i = 4;
            else if (flat.DistanceFromMetroStation >= distance[4, 0] && flat.DistanceFromMetroStation < distance[5, 0]) i = 5;
            else if (flat.DistanceFromMetroStation >= distance[5, 0] && flat.DistanceFromMetroStation < distance[6, 0]) i = 6;

            if (flats.DistanceFromMetroStation < distance[0, 1]) j = 1;
            else if (flats.DistanceFromMetroStation >= distance[0, 1] && flats.DistanceFromMetroStation < distance[0, 2]) j = 2;
            else if (flats.DistanceFromMetroStation >= distance[0, 2] && flats.DistanceFromMetroStation < distance[0, 3]) j = 3;
            else if (flats.DistanceFromMetroStation >= distance[0, 3] && flats.DistanceFromMetroStation < distance[0, 4]) j = 4;
            else if (flats.DistanceFromMetroStation >= distance[0, 4] && flats.DistanceFromMetroStation < distance[0, 5]) j = 5;
            else if (flats.DistanceFromMetroStation >= distance[0, 5] && flats.DistanceFromMetroStation < distance[0, 6]) j = 6;

            flats.Price += (flats.Price / 100 * distance[i, j]);
            flats.weightprocent += Math.Abs((float)distance[i, j]);

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
