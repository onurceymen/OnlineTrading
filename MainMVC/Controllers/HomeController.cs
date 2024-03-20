using Data.Context;
using MainMVC.Services.HomeServices;
using MainMVC.Services.ProductServices;
using MainMVC.ViewModels.HomeViewModel;
using Microsoft.AspNetCore.Mvc;


namespace MainMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ContactService _contactService;
        private readonly HomeService _homeService;

        public HomeController(AppDbContext context, ContactService contactService, HomeService homeService)
        {
            _context = context;
            _contactService = contactService;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/about-us")]
        public IActionResult AboutUs()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _contactService.SaveContactMessage(model);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }


        public IActionResult Listing()
        {
            var products = _homeService.GetAllProductsListings(); 
            return View(products);
        }

        [HttpGet]
        public IActionResult ProductDetail()
        {
            return View();
        }

        [HttpPost]
        [Route("{categoryName}-{title}-{id}/details")]
        public IActionResult ProductDetail(int id)
        {
            var productDetailViewModel = _homeService.GetProductDetail(id);
            if (productDetailViewModel == null)
            {
                return NotFound();
            }

            return View(productDetailViewModel);
        }


    }
}
