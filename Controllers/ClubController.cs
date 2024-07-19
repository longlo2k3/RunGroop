using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Object.Data;
using Object.Models;

namespace Object.Controllers
{
    public class ClubController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ClubController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Club> clubs = _context.Clubs.ToList();
            return View(clubs);
        }
        public IActionResult Detail(int Id)
        {
            Club club = _context.Clubs.Include(a => a.Address).FirstOrDefault(p => p.Id==Id);
            List<Club> clubs = _context.Clubs.ToList();
            ClubDetailsViewModel viewModel = new ClubDetailsViewModel
            {
                Club = club,
                ClubList = clubs
            };
            return View(viewModel);
        }
    }
}
