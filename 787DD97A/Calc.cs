using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalcForPriceFlat;

namespace CalcPriceOfFlat
{

    public class CalcPriceOfFlats                        //калькулятор стоимости квартиры
    {
        
        static public double PriceOfFlat(Flat flat, Flats[] flats)                         //цена квартиры относительно другого массива квартир
        {
            for(int i = 0; i < 3;i++)
            {
                CorrectPrice(flat, flats[i]);
                Console.WriteLine();
            }
            double localprice = 0;
            for(int i = 0; i < 3;i++)
            {
                localprice += flats[i].Price/3;
            }
            double finalprice = 0;
            for (int i = 0; i < 3; i++)
            {
                flats[i].weightanalog = 1 / flats[i].weightprocent;
                float local = 0;
                for (int j = 0; j < 3; j++)
                {
                    local += 1 / flats[j].weightprocent;
                }
                flats[i].weightanalog = flats[i].weightanalog / local;
                finalprice += (flats[i].Price / flats[i].ApartmentArea) * flats[i].weightanalog;
            }


            Console.WriteLine("Диапазон от " + finalprice * flat.ApartmentArea + " до " + localprice);
            return finalprice * flat.ApartmentArea;
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
            //если две квартиры обе не находятся на 1 или на последнем этаже, или не на одном и том же этаже, то ничего не начесляем
            if (flat.FloorLocation != 1 && flats.FloorLocation != 1 && 
                flat.FloorLocation != flat.NumberOfStoreys && flats.FloorLocation != flats.NumberOfStoreys ||
                flat.FloorLocation == flats.FloorLocation)
            {
                return flats.Price / flats.ApartmentArea;
            }
            else
            {
                ushort flatNumberOfStroreys = flat.NumberOfStoreys;
                if (flat.FloorLocation == 1)
                {
                    //если квартира, цену которой узнаем, находится на 1 этаже

                    if (flats.FloorLocation != flats.NumberOfStoreys)
                    {
                        //если квартира, НЕ цену которой мы узнаем находится на среднем этаже (не на первом и не на последнем)
                        flats.Price -= (flats.Price / 100 * 7);
                        flats.weightprocent += (float)7;
                    }
                    else
                    {
                        //если квартира, НЕ цену которой мы узнаем находится на последнем этаже 
                        flats.Price -=(flats.Price / 100 * 3.1);
                        flats.weightprocent += (float)3.1;
                    }
                }    
                else if(flat.FloorLocation ==  flat.NumberOfStoreys)//если квартира. цену которой мы ищем, находится на послденем этаже
                {
                    if (flats.FloorLocation == 1)
                    {
                        //если квартира, НЕ цену которой мы узнаем находится на первом этаже 
                        flats.Price += (flats.Price / 100 * 3.2);
                        flats.weightprocent += (float)3.2;
                    }
                    else
                    {
                        //если квартира, НЕ цену которой мы узнаем находится на последнем этаже 
                        flats.Price -= (flats.Price / 100 * 4);
                        flats.weightprocent += 4;
                    }
                }
                //если квартира (цену которой ищем) на среднем этаже
                else
                {
                    if (flats.FloorLocation == 1)
                    {
                        //если квартира, НЕ цену которой мы узнаем находится на первом этаже 
                        flats.Price += flats.Price / 100 * 7.5;
                        flats.weightprocent += (float)7.5;
                    }
                    else
                    {
                        //если квартира, НЕ цену которой мы узнаем находится на последнем этаже 
                        flats.Price += flats.Price / 100 * 4.2;
                        flats.weightprocent += (float)4.2;
                    }
                }
            }
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectCitchen(Flat flat, Flats flats)
        {
            if (flat.KitchentArea < 7 && flats.KitchentArea < 7) return flats.Price / flats.ApartmentArea; ;

            if(flat.KitchentArea >= 7 && flat.KitchentArea < 10 
                && flats.KitchentArea >= 7 && flats.KitchentArea < 10) return flats.Price / flats.ApartmentArea; ;

            if (flat.KitchentArea >= 10 && flat.KitchentArea < 15
                && flats.KitchentArea >= 10 && flats.KitchentArea < 15) return flats.Price / flats.ApartmentArea; ;
            

            if(flat.KitchentArea < 7
                && flats.KitchentArea >= 7 && flats.KitchentArea < 10)
            {
                flats.Price -= flats.Price / 100 * 2.9;
                flats.weightprocent += (float)2.9;
            }

            if (flat.KitchentArea < 7
                && flats.KitchentArea >= 10 && flats.KitchentArea < 15)
            {
                flats.Price -= flats.Price / 100 * 8.3;
                flats.weightprocent += (float)8.3;
            }




            if (flat.KitchentArea >= 7 && flat.KitchentArea < 10
                && flats.KitchentArea < 7)
            {
                flats.Price += flats.Price / 100 * 3;
                flats.weightprocent += 3;
            }

            if (flat.KitchentArea >= 7 && flat.KitchentArea < 10
               && flats.KitchentArea >= 10 && flats.KitchentArea < 15)
            {
                flats.Price -= flats.Price / 100 * 5.5;
                flats.weightprocent += (float)5.5;
            }




            if (flat.KitchentArea >= 10 && flat.KitchentArea < 15
                && flats.KitchentArea < 7)
            {
                flats.Price += flats.Price / 100 * 9;
                flats.weightprocent += 9;
            }

            if (flat.KitchentArea >= 10 && flat.KitchentArea < 15
               && flats.KitchentArea >= 7 && flats.KitchentArea < 10)
            {
                flats.Price += flats.Price / 100 * 5.8;
                flats.weightprocent += (float)5.8;

            }
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectBalcon(Flat flat, Flats flats)
        {
            if (flat.balcony == flats.balcony) return flats.Price / flats.ApartmentArea;
            if (flat.balcony)
                if (!flats.balcony)
                {
                    flats.Price += flats.Price / 100 * 5.3;//если балкон есть в оцениваемой квартире, но нет в эталоне
                    flats.weightprocent += (float)5.3;
                }
            if (!flat.balcony)
                if (flats.balcony)
                {
                    flats.Price -= flats.Price / 100 * 5;//если балкона нет в оцениваемой квартире, но есть в эталоне
                    flats.weightprocent += 5;
                }
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfRepair(Flat flat, Flats flats)
        {
            if(flat.repair == flats.repair) return flats.Price / flats.ApartmentArea;
            switch (flat.repair)
            {
                case "Без отделки":
                    {
                        if(flats.repair == "Эконом")
                        {
                            int priceLocal = (int)FUNCm2(flats.ApartmentArea, flats.Price);
                            flats.weightprocent += (13400f / (float)priceLocal) * 100f;
                            priceLocal -= 13400;
                            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);
                        }
                        if (flats.repair == "Улучшенный")
                        {
                            int priceLocal = (int)FUNCm2(flats.ApartmentArea, flats.Price);
                            flats.weightprocent += (20100f / (float)priceLocal) * 100f;
                            priceLocal -= 20100;                            
                            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);
                        }
                        break;
                    }
                case "Эконом":
                    {
                        if (flats.repair == "Без отделки")
                        {
                            int priceLocal = (int)FUNCm2(flats.ApartmentArea, flats.Price);
                            flats.weightprocent += (13400f / (float)priceLocal) * 100f;
                            priceLocal += 13400;                            
                            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);
                        }
                        if (flats.repair == "Улучшенный")
                        {
                            int priceLocal = (int)FUNCm2(flats.ApartmentArea, flats.Price);
                            flats.weightprocent += (6700f / (float)priceLocal) * 100f;
                            priceLocal -= 6700;                            
                            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);
                        }
                        break;
                    }
                case "Улучшенный":
                    {
                        if (flats.repair == "Без отделки")
                        {
                            int priceLocal = (int)FUNCm2(flats.ApartmentArea, flats.Price);
                            flats.weightprocent += (20100f / (float)priceLocal) * 100f;
                            priceLocal += 20100;                            
                            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);
                        }
                        if (flats.repair == "Эконом")
                        {
                            int priceLocal = (int)FUNCm2(flats.ApartmentArea, flats.Price);
                            flats.weightprocent += (6700f / (float)priceLocal) * 100f;
                            priceLocal += 6700;
                            flats.Price = FUNCprice(priceLocal, flats.ApartmentArea);
                        }
                        break;
                    }
            }
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfApartmentArea(Flat flat, Flats flats)
        {
           if(flat.ApartmentArea < 30) { FUNCarea(flats,1); return flats.Price / flats.ApartmentArea;  }
           if (flat.ApartmentArea >= 30 && flat.ApartmentArea < 50) { FUNCarea(flats, 2); return flats.Price / flats.ApartmentArea; }
           if (flat.ApartmentArea >= 50 && flat.ApartmentArea < 65) { FUNCarea(flats, 3); return flats.Price / flats.ApartmentArea; }
           if (flat.ApartmentArea >= 65 && flat.ApartmentArea < 90) { FUNCarea(flats, 4); return flats.Price / flats.ApartmentArea; }
           if (flat.ApartmentArea >= 90 && flat.ApartmentArea < 120) { FUNCarea(flats, 5); return flats.Price / flats.ApartmentArea; }
           if (flat.ApartmentArea > 120) { FUNCarea(flats, 6); return flats.Price / flats.ApartmentArea; }
            return flats.Price / flats.ApartmentArea;
        }
        static private double CorrectOfDistanseFromMetroStation(Flat flat, Flats flats)
        {
            if (flat.DistanceFromMetroStation < 5) { FUNCdistanse(flats, 1); return flats.Price / flats.ApartmentArea; }
            if (flat.DistanceFromMetroStation >= 5 &&  flat.DistanceFromMetroStation < 10) { FUNCdistanse(flats, 2); return flats.Price / flats.ApartmentArea; }
            if (flat.DistanceFromMetroStation >= 10 && flat.DistanceFromMetroStation < 15) { FUNCdistanse(flats, 3); return flats.Price / flats.ApartmentArea; }
            if (flat.DistanceFromMetroStation >= 15 && flat.DistanceFromMetroStation < 30) { FUNCdistanse(flats, 4); return flats.Price / flats.ApartmentArea; }
            if (flat.DistanceFromMetroStation >= 30 && flat.DistanceFromMetroStation < 60) { FUNCdistanse(flats, 5); return flats.Price / flats.ApartmentArea; }
            if (flat.DistanceFromMetroStation >= 60 && flat.DistanceFromMetroStation < 90) { FUNCdistanse(flats, 6); return flats.Price / flats.ApartmentArea; }
            return flats.Price / flats.ApartmentArea;
        }

        /*служебные функции*/
        static private double FUNCm2(double s, double price)
        {
            return price/s;
        }
        static private double FUNCprice(int s, double m2Price)
        {
            return m2Price * s;
        }
        static private void FUNCarea(Flats flats, int i)
        {
            switch (i)
            {
                case 1://<30
                    {
                        if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) { flats.Price += (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        if (flats.ApartmentArea >= 50 && flats.ApartmentArea < 65) { flats.Price += (flats.Price / 100 * 14); flats.weightprocent += 14; return; }
                        /*  
                         *  ЭКСТРЕМАЛЬНЫЕ ЗНАЧЕНИЯ ФУНКЦИЙ 
                         *  ИСПОЛЬЗОВАТЬ ТОЛЬКО ПРИ МАЛОМ КОЛИЧЕСТВЕ КВАРТИР-ЭТАЛОНОВ
                        if (flats.ApartmentArea >= 65 && flats.ApartmentArea < 90) { flats.Price += (flats.Price / 100 * 21); return; }
                        if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) { flats.Price += (flats.Price / 100 * 28); return; }
                        if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 31); return; }*/
                        break;
                    }
                case 2://30-50
                    {
                        if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        if (flats.ApartmentArea >= 50 && flats.ApartmentArea < 65) { flats.Price += (flats.Price / 100 * ((1/0.93-1)*100)); flats.weightprocent += 7; return; }
                        if (flats.ApartmentArea >= 65 && flats.ApartmentArea < 90) { flats.Price += (flats.Price / 100 * 14); flats.weightprocent += 14; return; }
                        /*
                         * 
                        if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) { flats.Price += (flats.Price / 100 * 21); return; }
                        if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 24); return; }*/
                        break;
                    }
                case 3://50-65
                    {
                        if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 12); return; }
                        if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) { flats.Price -= (flats.Price / 100 * ((1 / 0.93 - 1) * 100)); flats.weightprocent += 7; return; }
                        if (flats.ApartmentArea >= 65 && flats.ApartmentArea < 90) { flats.Price += (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) { flats.Price += (flats.Price / 100 * 13); flats.weightprocent += 13; return; }
                        //if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 16); return; }
                        break;
                    }
                case 4://65-90
                    {
                        //if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 17); return; }
                        if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) { flats.Price -= (flats.Price / 100 * 12); flats.weightprocent += 12; return; }
                        if (flats.ApartmentArea >= 50 && flats.ApartmentArea < 65) { flats.Price -= (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) { flats.Price += (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 9); return; }
                        break;
                    }
                case 5://90-120
                    {
                        //if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 22); return; }
                        //if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) { flats.Price -= (flats.Price / 100 * 17); return; }
                        if (flats.ApartmentArea >= 50 && flats.ApartmentArea < 65) { flats.Price -= (flats.Price / 100 * 11); flats.weightprocent += 11; return; }
                        if (flats.ApartmentArea >= 65 && flats.ApartmentArea < 90) { flats.Price -= (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        
                        if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 3); return; }
                        break;
                    }
                case 6://>120
                    {
                        //if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 24); return; }
                        //if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) { flats.Price -= (flats.Price / 100 * 7); return; }
                        //if (flats.ApartmentArea >= 50 && flats.ApartmentArea < 65) { flats.Price += (flats.Price / 100 * 7); return; }
                        if (flats.ApartmentArea >= 65 && flats.ApartmentArea < 90) { flats.Price -= (flats.Price / 100 * 8); flats.weightprocent += 8; return; }
                        if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) { flats.Price -= (flats.Price / 100 * 3); flats.weightprocent += 3; return; }
                         break;
                    }

            }
        }
        static private void FUNCdistanse(Flats flats, int i)
        {
            switch (i)
            {
                case 1://<5
                    {
                        if (flats.DistanceFromMetroStation >= 5 && flats.DistanceFromMetroStation < 10) { flats.Price += (flats.Price / 100 * 7); flats.weightprocent += 7; return; }
                        if (flats.DistanceFromMetroStation >= 10 && flats.DistanceFromMetroStation < 15) { flats.Price += (flats.Price / 100 * 12); flats.weightprocent += 12; return; }
                        /*  
                         *  ЭКСТРЕМАЛЬНЫЕ ЗНАЧЕНИЯ ФУНКЦИЙ 
                         *  ИСПОЛЬЗОВАТЬ ТОЛЬКО ПРИ МАЛОМ КОЛИЧЕСТВЕ КВАРТИР-ЭТАЛОНОВ
                        if (flats.ApartmentArea >= 65 && flats.ApartmentArea < 90) { flats.Price += (flats.Price / 100 * 21); return; }
                        if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) { flats.Price += (flats.Price / 100 * 28); return; }
                        if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 31); return; }*/
                        break;
                    }
                case 2://5-10
                    {
                        if (flats.DistanceFromMetroStation < 5) { flats.Price -= (flats.Price / 100 * 7); flats.weightprocent += 7; return; }
                        if (flats.DistanceFromMetroStation >= 10 && flats.DistanceFromMetroStation < 15) { flats.Price += (flats.Price / 100 * 4); flats.weightprocent += 4; return; }
                        if (flats.DistanceFromMetroStation >= 15 && flats.DistanceFromMetroStation < 30) { flats.Price += (flats.Price / 100 * 9); flats.weightprocent += 9; return; }
                        /*
                         * 
                        if (flats.ApartmentArea >= 90 && flats.ApartmentArea < 120) { flats.Price += (flats.Price / 100 * 21); return; }
                        if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 24); return; }*/
                        break;
                    }
                case 3://10-15
                    {
                        if (flats.DistanceFromMetroStation < 5) { flats.Price -= (flats.Price / 100 * 11); flats.weightprocent += 11; return; }
                        if (flats.DistanceFromMetroStation >= 5 && flats.DistanceFromMetroStation < 10) { flats.Price -= (flats.Price / 100 * 4); flats.weightprocent += 4; return; }
                        if (flats.DistanceFromMetroStation >= 15 && flats.DistanceFromMetroStation < 30) { flats.Price += (flats.Price / 100 * 5); flats.weightprocent += 5; return; }
                        if (flats.DistanceFromMetroStation >= 30 && flats.DistanceFromMetroStation < 60) { flats.Price += (flats.Price / 100 * 11); flats.weightprocent += 11; return; }
                        //if (flats.ApartmentArea > 120) { flats.Price += (flats.Price / 100 * 16); return; }
                        break;
                    }
                case 4://15-30
                    {
                        //if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 17); return; }
                        if (flats.DistanceFromMetroStation >= 5 && flats.DistanceFromMetroStation < 10) { flats.Price -= (flats.Price / 100 * 8); flats.weightprocent += 8; return; }
                        if (flats.DistanceFromMetroStation >= 10 && flats.DistanceFromMetroStation < 15) { flats.Price -= (flats.Price / 100 * 5); flats.weightprocent += 5; return; }
                        if (flats.DistanceFromMetroStation >= 30 && flats.DistanceFromMetroStation < 60) { flats.Price += (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        if (flats.DistanceFromMetroStation >= 60 && flats.DistanceFromMetroStation < 90) { flats.Price += (flats.Price / 100 * 10); flats.weightprocent += 10; return; }
                        break;
                    }
                case 5://30-60
                    {
                        //if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 22); return; }
                        //if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) { flats.Price -= (flats.Price / 100 * 17); return; }
                        if (flats.DistanceFromMetroStation >= 10 && flats.DistanceFromMetroStation < 15) { flats.Price -= (flats.Price / 100 * 10); flats.weightprocent += 10; return; }
                        if (flats.DistanceFromMetroStation >= 15 && flats.DistanceFromMetroStation < 30) { flats.Price -= (flats.Price / 100 * 6); flats.weightprocent += 6; return; }
                        if (flats.DistanceFromMetroStation >= 60 && flats.DistanceFromMetroStation < 90) { flats.Price += (flats.Price / 100 * 4); flats.weightprocent += 4; return; }
                        break;
                    }
                case 6://>60-90
                    {
                        //if (flats.ApartmentArea < 30) { flats.Price -= (flats.Price / 100 * 24); return; }
                        //if (flats.ApartmentArea >= 30 && flats.ApartmentArea < 50) { flats.Price -= (flats.Price / 100 * 7); return; }
                        //if (flats.ApartmentArea >= 50 && flats.ApartmentArea < 65) { flats.Price += (flats.Price / 100 * 7); return; }
                        if (flats.DistanceFromMetroStation >= 15 && flats.DistanceFromMetroStation < 30) { flats.Price -= (flats.Price / 100 * 9); flats.weightprocent += 9; return; }
                        if (flats.DistanceFromMetroStation >= 30 && flats.DistanceFromMetroStation < 60) { flats.Price -= (flats.Price / 100 * 4); flats.weightprocent += 4; return; }
                        break;
                    }

            }
        }

    }
    public class Flats:Flat
    {
        public Flats(string LocationFlat, byte NumbersOfRoom, string MarketSegment,
            ushort NumberOfStoreys, string WallMaterial, ushort FloorLocation,
            double ApartmentArea, ushort KitchentArea, bool balcony,
            ushort DistanseFromMetroStation, uint Price, string repair) 

            :base( LocationFlat,  NumbersOfRoom,  MarketSegment,
             NumberOfStoreys,  WallMaterial,  FloorLocation,
             ApartmentArea,  KitchentArea,  balcony,
             DistanseFromMetroStation, repair)
        {

            this.Price = Price;

        }

      

        public double Price { get; set; } = 0;                        //цена

        public float weightprocent = 0;
        public float weightanalog= 0;
        /*цены квартиры рассчитывается и возвращается методом. Поле - "цена квартиры" использовать 
         * нецелесообразно из-за размера переменной, которая при этом будет использоваться*/

    }
}
