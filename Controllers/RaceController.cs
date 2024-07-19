﻿using Microsoft.AspNetCore.Mvc;
using Object.Data;
using Object.Models;

namespace Object.Controllers
{
    public class RaceController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RaceController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Races> races=_context.Races.ToList();
            return View(races);
        }
    }
}
