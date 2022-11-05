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
        [HttpGet]
        public IQueryable<Apartment> Test()
        {
            var Apartments = _context.Apartments.Where(u => u.Undeground.Equals("Аминьевская"));

            foreach (Apartment u in Apartments)
            {
                Console.WriteLine($"{u.Id}.{u.Adress} - {u.Undeground} {u.Undeground_minutes}");
            }
            return Apartments;
        }

        
    }
}

