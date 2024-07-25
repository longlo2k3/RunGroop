using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Object.Data;
using Object.Interfaces;
using Object.Models;

namespace Object.Controllers
{
    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        public ClubController( IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            return View(clubs);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            Club club = await _clubRepository.GetByIdAsync(Id);
            IEnumerable<Club> clubs = await _clubRepository.GetAll();
            ClubDetailsViewModel viewModel = new ClubDetailsViewModel()
            {
                Club = club,
                ClubList = clubs
            };
            return View(viewModel);
        }
    }
}
