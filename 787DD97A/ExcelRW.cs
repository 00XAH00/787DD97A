using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML;
using ClosedXML.Excel;
namespace CalcForPriceFlat
{
    public class ExcelRW
    {
        //возвращает кварты из Excel файла namefile
        public static Flat[] GetFlatsForExcel(string namefile)
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

            Flat[] flat = new Flat[count_flat];
            object value;
            for (int i = 0; i< count_flat; i++)
            {
                flat[i] = new Flat();



                object numberOfStoryes = ws.Cell(2 + i, 4).Value;
               
                flat[i].NumberOfStoreys = Convert.ToUInt16(numberOfStoryes);
                
                

                object FloorLocation = ws.Cell(2 + i, 6).Value;
               
                flat[i].FloorLocation = Convert.ToUInt16(FloorLocation);



                object Area = ws.Cell(2 + i, 7).Value;

                flat[i].ApartmentArea = Convert.ToDouble(Area);



                object areaKitchen = ws.Cell(2 + i, 8).Value;

                flat[i].KitchentArea = (ushort)(Convert.ToInt32(areaKitchen));



                object Balcon = ws.Cell(2 + i, 9).Value;

                if (Balcon == "Да")
                {
                    flat[i].balcony = true;
                }
                else
                {
                    flat[i].balcony = false;
                }



                object distanceForMetro = ws.Cell(2 + i, 10).Value;

                flat[i].DistanceFromMetroStation = (ushort)(Convert.ToInt32(distanceForMetro));




                object repair = ws.Cell(2 + i, 11).Value;

                flat[i].repair = Convert.ToString(repair);

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
