using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Object.Data;
using Object.Interfaces;
using Object.Models;
using Object.ViewModel;

namespace Object.Controllers
{

    public class ClubController : Controller
    {
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        public ClubController( IClubRepository clubRepository,IPhotoService photoService)
        {
            _clubRepository = clubRepository;
            _photoService = photoService;
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(clubVM.Image);
                var club = new Club
                {
                    Title = clubVM.Title,
                    Description = clubVM.Description,
                    Image = result.Url.ToString(),
                    Address = new Address
                    {
                        Street = clubVM.Address.Street,
                        City = clubVM.Address.City,
                        State = clubVM.Address.State
                    },
                    ClubCategory = clubVM.ClubCategory
                };
                _clubRepository.Add(club);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(clubVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id) {
            Club club = await _clubRepository.GetByIdAsync(id);
            if(club == null) return View(club);
            ViewModel.EditClubViewModel clubVM = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                AddressID = club.AddressId,
                Address = club.Address,
                ClubCategory = club.ClubCategory,
                URL = club.Image,
            };
            return View(clubVM);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int id,EditClubViewModel clubVM)
        {
            if (!ModelState.IsValid) {
                ModelState.AddModelError("", "Fail Load Page");
                return View(clubVM);
            }
            Club userClub = await _clubRepository.GetByIdAsyncNoTracking(id);
            if (userClub == null)
            {
                return View("Error");
            }

            var photoResult = await _photoService.AddPhotoAsync(clubVM.Image);

            if (photoResult.Error != null)
            {
                ModelState.AddModelError("Image", "Photo upload failed");
                return View(clubVM);
            }

            if (!string.IsNullOrEmpty(userClub.Image))
            {
                _ = _photoService.DeletePhotoAsync(userClub.Image);
            }

            var club = new Club
            {
                Id = id,
                AddressId = clubVM.AddressID,
                Title = clubVM.Title,
                Description = clubVM.Description,
                Image = photoResult.Url.ToString(),
                Address = new Address
                {
                    Street = clubVM.Address.Street,
                    City = clubVM.Address.City,
                    State = clubVM.Address.State
                },
                ClubCategory = clubVM.ClubCategory
            };

            _clubRepository.Update(club);

            return RedirectToAction("Index");
        }
    }
}
