
using Microsoft.AspNetCore.Mvc;
using _787DD97A_API.Classes;
using _787DD97A_API.Models;

using CalcForPriceFlat;
using CalcPriceOfFlat;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _787DD97A_API.Controllers
{
    [Route("api/[controller]")]
    public class ApartmentController : Controller
    {
        private ApplicationContext _context;
        IQueryable<Apartment> Apartments;
        static Flats[] flats;
        static Apartment[] flat;
        IWebHostEnvironment _appEnvironment;

        public ApartmentController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;

        }



        /*ДЛЯ КНОПКИ - НАЙТИ АНАЛОГИ*/
        // GET: api/values
        [HttpGet("Get")]
        public IQueryable<Apartment> Test(Apartment UndegroundName)
        {

            Apartments = _context.Apartments.Where(u => u.Undeground.Equals(UndegroundName.Undeground))
                                            .Where(u => u.Rooms.Equals(UndegroundName.Rooms));
            flats = SortFlat.SortFlats(Apartments, UndegroundName);

            return Apartments;
        }



        /*ДЛЯ КНОПКИ - Рассчитать стоимость*/
        [HttpGet("Geter")]
        public double Test2(Flat UndegroundName)
        {
            if (flats != null)
                return CalcPriceOfFlats.PriceOfFlat(UndegroundName, flats, flats.Length);
            else return 0;
        }



        /*ДЛЯ КНОПКИ - удалить аналог*/

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public Flats[] Delete(int id)
        {
            return SortFlat.DeleteForIndexFlat(flats, id);
        }


        /*ДЛЯ КНОПКИ - Загрузить Excel*/
        [HttpPost("{AddFile}")]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                // путь к папке Files
                string path = uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                flat = ExcelRW.GetFlatsForExcel(path);
                
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetFile()
        {
            flat = ExcelRW.GetFlatsForExcel("primer.xlsx");

            for (int i = 0; i < flat.Length; i++)
            {
                Apartments = _context.Apartments.Where(u => u.Rooms.Equals(flat[i].Rooms))
                    .Where(u => u.Condition.Equals(flat[i].Condition))
                    .Where(u => u.Material.Equals(flat[i].Material));
                    flats = SortFlat.SortFlats(Apartments, flat[i]);
                ExcelRW.SetFlats(i, "primer.xlsx", 
                    CalcPriceOfFlats.PriceOfFlat(
                        SortFlat.Convert(flat[i]), flats, flats.Length)); 
            }
            
            // Путь к файлу
            string file_path = Path.Combine(_appEnvironment.ContentRootPath, "primer.xlsx");
            // Тип файла - content-type
            string file_type = "application/xlsx";
            // Имя файла - необязательно
            string file_name = "primer.xlsx";
            return PhysicalFile(file_path, file_type, file_name);
        }

    }
}

