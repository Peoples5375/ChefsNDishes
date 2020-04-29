using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsNDishes.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context { get; set; } 

        public HomeController(MyContext context)
        {
            _context = context;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.ChefData = _context.Chefs.Include(j => j.CreatedDish).ToList();
            return View();
        }
        [HttpGet("New")]
        public IActionResult New()
        {
            return View();
        }
        [HttpPost("NewChef")]
        public IActionResult NewChef(Chef chef)
        {
            if(ModelState.IsValid)
            {
                _context.Add(chef);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("New");
            }
        }
        [HttpGet("Dishes")]
        public IActionResult Dishes()
        {
            ViewBag.DishData = _context.Dishes.Include(j => j.Creator).ToList();
            return View();
        }
        [HttpGet("NewDish")]
        public IActionResult NewDish()
        {
            ViewBag.ChefOptions = _context.Chefs.Include(j =>j.CreatedDish).ToList();
            return View();
        }

        [HttpPost("CreateDish")]
        public IActionResult CreateDish(Dish newDish)
        {
            if(ModelState.IsValid)
            {
                _context.Dishes.Add(newDish);
                _context.SaveChanges();
                Console.WriteLine("///////////////////////////////////");
                return Redirect("/Dishes");
            }
            else
            {
                Console.WriteLine("ppppppppppppppppppppppppppppppp");
                return View("NewDish");
            }
        }
    }
}
