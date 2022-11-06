
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


            int CounterFlat = 0;

            for ( int i = 0; i < ApartmentsArray.Length; i++ )
            {
                georooords.Calc_Distance(ApartmentsArray[i].Adress, apartment.Adress);
                if (Math.Abs(ApartmentsArray[i].Undeground_minutes - apartment.Undeground_minutes) <= 35
                    && ApartmentsArray[i].Segment == apartment.Segment
                    && ApartmentsArray[i].Material == apartment.Material
                    && ApartmentsArray[i].Rooms == apartment.Rooms
                    && ApartmentsArray[i].House_floors == apartment.House_floors
                    && georooords.rasst <=0.01)
                {
                    CounterFlat++;
                }
                else
                ApartmentsArray = DeleteForIndex(ApartmentsArray, i);//удаляем элементы с другим названием метро
            }

            Flats[] flats = new Flats[CounterFlat];

            if (ApartmentsArray.Length == 0) return flats;

            for (int i = 0; i < CounterFlat; i++)
            {
                flats[i] = new Flats();
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


        public static Apartment[] DeleteForIndex(Apartment[] ApartmentsArray, int index)
        {
            Apartment[] apartments = new Apartment[ApartmentsArray.Length-1];
            int j = 0;
            for (int i = 0; i < ApartmentsArray.Length-1; i++)
            {
                if(i!=index)
                {
                    apartments[i] = ApartmentsArray[j];
                    j++;
                }
            }
            return apartments;
        }

        public static Flats[] DeleteForIndexFlat(Flats[] flat, int index)
        {
            Flats[] flats = new Flats[flat.Length - 1];
            int j = 0;
            for (int i = 0; i < flat.Length; i++)
            {
                if (i != index)
                {
                    flats[i] = flat[j];
                    j++;
                }
            }
            return flats;
        }

        public static Flat Convert(Apartment ApartmentsArray)
        {
            Flat flats = new Flat();
            flats.DistanceFromMetroStation = (ushort)ApartmentsArray.Undeground_minutes;
            flats.NumberOfStoreys = (ushort)ApartmentsArray.House_floors;
            flats.FloorLocation = (ushort)ApartmentsArray.Apartment_floor;
            flats.ApartmentArea = (ushort)ApartmentsArray.Apatments_area;
            flats.KitchentArea = (ushort)ApartmentsArray.Kitchen_area;
            flats.balcony = ApartmentsArray.Balcony;
            flats.repair = ApartmentsArray.Condition;
          
            return flats;
        }

        public static Apartment Convert(Flat flats)
        {
           Apartment apartment = new Apartment();
           apartment.Undeground_minutes = flats.DistanceFromMetroStation;
           apartment.House_floors = flats.NumberOfStoreys;
           apartment.Apartment_floor = flats.FloorLocation;
           apartment.Apatments_area = (uint)flats.ApartmentArea;
           apartment.Kitchen_area = flats.KitchentArea;
           apartment.Balcony = flats.balcony;
           apartment.Condition = flats.repair;

            return apartment;
        }
    }
}
