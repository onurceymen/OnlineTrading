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
        private readonly ListingService _listingService;
        private readonly ContactService _contactService;
        private readonly ProductDetailService _productDetailService;
        private readonly CommentService _commentService;

        public HomeController(AppDbContext context, ListingService listingService, ContactService contactService, ProductDetailService productDetailService, CommentService commentService)
        {
            _context = context;
            _listingService = listingService;
            _contactService = contactService;
            _productDetailService = productDetailService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("/about-us")]
        public IActionResult AboutUs()
        {
            // Register action implementation
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return RedirectToAction("Index");
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
            _listingService.GetAllProducts();
            return View(_listingService);
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
            var productDetailViewModel = _productDetailService.GetProductDetail(id);
            if (productDetailViewModel == null)
            {
                return NotFound();
            }

            return View(productDetailViewModel);
        }


    }
}
