using CalcForPriceFlat;
using CalcPriceOfFlat;
using System.Collections;
using System.Collections.Generic;
// LocationFlat,NumbersOfRoom,MarketSegment,NumberOfStoreys,WallMaterial,FloorLocation,ApartmentArea,KitchentArea,balcony,DistanseFromMetroStation,Price,repair
Flat flat = new Flat("", 2, "Современное жилье", 22, "", 19, 58.7, 11, true, 11, "Эконом");//21 300 000

Flats[] arrayflats = new Flats[3];
arrayflats[0] = new Flats("", 2, "Современное жилье", 22, "", 20, 58.7, 11, true, 11, 2130000, "Эконом");

arrayflats[1] = new Flats("", 2, "Современное жилье", 22, "", 19, 57.2, 9, true, 11, 2250000, "Эконом");

arrayflats[2] = new Flats("", 2, "Современное жилье", 17, "", 15, 58.6, 14, true, 10, 2140000, "Эконом");



CalcPriceOfFlats.PriceOfFlat(flat, arrayflats);
{ }
