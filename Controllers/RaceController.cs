using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Object.Data;
using Object.Interfaces;
using Object.Models;
using Object.ViewModel;

namespace Object.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        public RaceController(IRaceRepository raceRepository)
        {
            _raceRepository = raceRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Races> races= await _raceRepository.GetAll();
            return View(races);
        }
        public async Task<IActionResult> Detail(int Id)
        {
            Races races = await _raceRepository.GetByIdAsync(Id);
            IEnumerable<Races> raceList = await _raceRepository.GetAll();
            RaceDetailsViewModel viewModel = new RaceDetailsViewModel()
            {
                races = races,
                raceList = raceList
            };
            return View(viewModel);
        }
    }
}
