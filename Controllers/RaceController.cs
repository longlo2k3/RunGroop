using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Object.Data;
using Object.Interfaces;
using Object.Models;
using Object.Services;
using Object.ViewModel;

namespace Object.Controllers
{
    public class RaceController : Controller
    {
        private readonly IRaceRepository _raceRepository;
        private readonly IPhotoService _photoService;
        public RaceController(IRaceRepository raceRepository, IPhotoService photoService)
        {
            _raceRepository = raceRepository;
            _photoService = photoService;
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

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            Races race = await _raceRepository.GetByIdAsync(Id);
            if(race == null)
            {
                ModelState.AddModelError("", "Err null race");
                return View(race);
            }
            else
            {
                EditRaceViewModel raceVM = new EditRaceViewModel
                {
                    Address = race.Address,
                    Title = race.Title, 
                    Description = race.Description, 
                    URL = race.Image,
                    RaceCategory = race.RaceCategory,
                };
                return View(raceVM);
            } 
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int Id, EditRaceViewModel raceVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Fail Load Page");
                return View(raceVM);
            }
            Races userace = await _raceRepository.GetByIdAsyncNoTracking(Id);
            if (userace == null) {
                return View("Error");
            }
            var photoResult = await _photoService.AddPhotoAsync(raceVM.Image);
            if (photoResult == null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(raceVM);
            }
            if (!string.IsNullOrEmpty(userace.Image)) {
                _photoService.DeletePhotoAsync(userace.Image);
            }
            var race = new Races
            {
                Id = Id,
                AddressID = raceVM.AddressID,
                Title = raceVM.Title,
                Description = raceVM.Description,
                Image = photoResult.Url.ToString(),
                Address = new Address
                {
                    Street = raceVM.Address.Street,
                    City = raceVM.Address.City,
                    State = raceVM.Address.State
                },
                RaceCategory = raceVM.RaceCategory
            };
            _raceRepository.Update(race);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(raceVM.Image);
                var race = new Races
                {
                    Title = raceVM.Title,
                    Description = raceVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        City = raceVM.Address.City,
                        State = raceVM.Address.State,
                        Street = raceVM.Address.Street,
                    },
                    RaceCategory = raceVM.RaceCategory
                };
                _raceRepository.Add(race);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Fail Load Page");
                return View(raceVM);
            }
        }

    }
}
