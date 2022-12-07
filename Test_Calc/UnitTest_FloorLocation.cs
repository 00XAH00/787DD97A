using CalcForPriceFlat;
using CalcPriceOfFlat;

namespace Test_Calc
{
    [TestClass]
    public class FloorLocation
    {

        [TestMethod]
        public void TestClassic()
        {
            //
            // NumberOfStoreys,  FloorLocation, ApartmentArea,  KitchentArea, balcony, DistanseFromMetroStation,  repair
            Flat flat = new Flat(22, 18, 58.7, 11, true, 11, "Эконом");//21 300 000

            Flats[] arrayflats = new Flats[3];
            arrayflats[0] = new Flats(20, 14, 58.7, 11, true, 11, 21300000, "Эконом");
            arrayflats[1] = new Flats(20, 19, 57.2, 9, true, 11, 22500000, "Эконом");
            arrayflats[2] = new Flats(18, 15, 58.6, 14, true, 10, 21400000, "Эконом");

            Assert.AreEqual(20930777.469269644, CalcPriceOfFlats.PriceOfFlat(flat, arrayflats, 3));

        }


        [TestMethod]
        public void TestDouble()
        {
            //                   double(int classic)
            // NumberOfStoreys,  FloorLocation, ApartmentArea,  KitchentArea, balcony, DistanseFromMetroStation,  repair
            Flat flat = new Flat(22, 18.1, 58.7, 11, true, 11, "Эконом");//21 300 000

            Flats[] arrayflats = new Flats[3];
            arrayflats[0] = new Flats(20, 14, 58.7, 11, true, 11, 21300000, "Эконом");
            arrayflats[1] = new Flats(20, 19, 57.2, 9, true, 11, 22500000, "Эконом");
            arrayflats[2] = new Flats(18, 15, 58.6, 14, true, 10, 21400000, "Эконом");

            Assert.AreEqual(20930777.469269644, CalcPriceOfFlats.PriceOfFlat(flat, arrayflats, 3));

        }
        [TestMethod]
        public void TestMinusDouble()
        {
            //                   
            // NumberOfStoreys,  FloorLocation, ApartmentArea,  KitchentArea, balcony, DistanseFromMetroStation,  repair
            Flat flat = new Flat(22, -18, 58.7, 11, true, 11, "Эконом");//21 300 000

            Flats[] arrayflats = new Flats[3];
            arrayflats[0] = new Flats(20, 14, 58.7, 11, true, 11, 21300000, "Эконом");
            arrayflats[1] = new Flats(20, 19, 57.2, 9, true, 11, 22500000, "Эконом");
            arrayflats[2] = new Flats(18, 15, 58.6, 14, true, 10, 21400000, "Эконом");

            Assert.AreEqual(20930777.469269644, CalcPriceOfFlats.PriceOfFlat(flat, arrayflats, 3));

        }


    }
}
