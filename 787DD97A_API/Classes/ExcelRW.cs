using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;
using ClosedXML.Excel;

using Microsoft.AspNetCore.Mvc;
using _787DD97A_API.Classes;
using _787DD97A_API.Models;

using CalcForPriceFlat;
using CalcPriceOfFlat;

namespace CalcForPriceFlat
{
    public class ExcelRW
    {
        //возвращает кварты из Excel файла namefile
        public static Apartment[] GetFlatsForExcel(string namefile)
        {
            IXLWorkbook wb = new XLWorkbook(namefile);

            IXLWorksheet ws = wb.Worksheets.Worksheet(1);//floor

            int count_flat = 1;
            
            while(true)//подсчёт количества квартир
            {
                object value2 = ws.Cell(count_flat+1,1).Value;
                if ((string)value2 == "" )
                {
                    break;
                }
                else
                {
                    count_flat++;
                }
            }
            count_flat--;//квартиры всегда получаются на 1 больше

            Apartment[] flat = new Apartment[count_flat];
            object value;
            for (int i = 0; i< count_flat; i++)
            {
                flat[i] = new Apartment();


                object Adress = ws.Cell(2 + i, 1).Value;

                flat[i].Adress = (string)Adress;


                object Rooms = ws.Cell(2 + i, 2).Value;

                flat[i].Rooms = Convert.ToUInt16(Rooms);

                object House_floors = ws.Cell(2 + i, 4).Value;
               
                flat[i].House_floors = Convert.ToUInt16(House_floors);
                
                

                object Apartment_floor = ws.Cell(2 + i, 6).Value;
               
                flat[i].Apartment_floor = Convert.ToUInt16(Apartment_floor);



                object Apatments_area = ws.Cell(2 + i, 7).Value;

                flat[i].Apatments_area = Convert.ToUInt16(Apatments_area);



                object Kitchen_area = ws.Cell(2 + i, 8).Value;

                flat[i].Kitchen_area = (ushort)(Convert.ToInt32(Kitchen_area));



                object Balcon = ws.Cell(2 + i, 9).Value;

                if (Balcon == "Да")
                {
                    flat[i].Balcony = true;
                }
                else
                {
                    flat[i].Balcony = false;
                }



                object distanceForMetro = ws.Cell(2 + i, 10).Value;

                flat[i].Undeground_minutes = (ushort)(Convert.ToInt32(distanceForMetro));




                object repair = ws.Cell(2 + i, 11).Value;

                flat[i].Condition = Convert.ToString(repair);

                flat[i].Undeground = "";
                flat[i].Link = "";
            }
            
            
            wb.SaveAs(namefile);
            
            return flat;
        }


        public static void SetFlats(int i, string namefile,double price)
        {
            IXLWorkbook wb = new XLWorkbook(namefile);

            IXLWorksheet ws = wb.Worksheets.Worksheet(1);//floor      

            
            ws.Cell(2 + i, 12).Value = price;
            
            wb.SaveAs(namefile);
        }
    }
}
