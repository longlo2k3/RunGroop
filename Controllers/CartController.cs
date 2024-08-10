using Microsoft.AspNetCore.Mvc;
using Object.Interfaces;
using Object.Models;

namespace Object.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IPhotoService _photoService;
        public CartController(ICartRepository cartRepository, IPhotoService photoService)
        {
            _cartRepository = cartRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Cart> carts = await _cartRepository.GetAll();
            return View(carts);
        }
    }
}
