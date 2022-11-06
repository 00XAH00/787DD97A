
using _787DD97A_API.Models;
using CalcForPriceFlat;
using System;
namespace _787DD97A_API.Classes
{
    public class SortFlat
    {

        static public Flats[] SortFlats(IQueryable<Apartment> apartments, Apartment apartment )
        {

            /**/
            Apartment[] ApartmentsArray =  apartments.ToArray();

            int CounterFlats = 0;


            for ( int i = 0; i < ApartmentsArray.Length; i++ )
            {
                if (ApartmentsArray[i].Undeground == apartment.Undeground 
                    && Math.Abs(ApartmentsArray[i].Undeground_minutes - apartment.Undeground_minutes) <= 35
                    && ApartmentsArray[i].Segment == apartment.Segment
                    && ApartmentsArray[i].Material == apartment.Material
                    && ApartmentsArray[i].Rooms == apartment.Rooms
                    && ApartmentsArray[i].House_floors == apartment.House_floors)
                {
                    CounterFlats++;
                }
                ApartmentsArray = DeleteForIndex(ApartmentsArray, i);//удаляем элементы с другим названием метро


            }

            

        

            


            Flats[] flats = new Flats[CounterFlats];
            if (ApartmentsArray.Length < 3) return flats;
            for (int i = 0; i < ApartmentsArray.Length; i++)
            {
                flats[i].DistanceFromMetroStation = (ushort)ApartmentsArray[i].Undeground_minutes;
                flats[i].NumberOfStoreys = (ushort)ApartmentsArray[i].House_floors;
                flats[i].FloorLocation = (ushort)ApartmentsArray[i].Apartment_floor;
                flats[i].ApartmentArea = (ushort)ApartmentsArray[i].Apatments_area;
                flats[i].KitchentArea = (ushort)ApartmentsArray[i].Kitchen_area;
                flats[i].balcony = ApartmentsArray[i].Balcony;
                flats[i].repair = ApartmentsArray[i].Condition;
                flats[i].Price = ApartmentsArray[i].Price;
            }

            return flats;
        }


        static Apartment[] DeleteForIndex(Apartment[] ApartmentsArray, int index)
        {
            Apartment[] apartments = new Apartment[ApartmentsArray.Length];
            int j = 0;
            for (int i = 0; i < ApartmentsArray.Length; i++)
            {
                if(i!=index)
                {
                    apartments[i] = ApartmentsArray[j];
                    j++;
                }
            }
            return apartments;
        }

    }
}
