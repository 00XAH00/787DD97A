using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using _787DD97A_API.Classes;
using _787DD97A_API.Models;
using DocumentFormat.OpenXml.Spreadsheet;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _787DD97A_API.Controllers
{
    [Route("api/[controller]")]
    public class ApartmentController : Controller
    {
        private ApplicationContext _context;

        public ApartmentController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/values
        [HttpGet("Get")]
        public IQueryable<Apartment> Test(Apartment UndegroundName)
        {
            var Apartments = _context.Apartments.Where(u => u.Undeground.Equals(UndegroundName));

            while (Apartments == null)//если 
            {
                SortFlat.SortFlats(Apartments, UndegroundName);
                Apartments = _context.Apartments.Where(u => u.Undeground.Equals(UndegroundName));
            }
            return Apartments;
        }
        
        
    }
}

