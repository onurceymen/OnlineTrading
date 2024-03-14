using Data.Entity;
using MainMVC.Services.HomeServices;
using MainMVC.Services.ProductServices;
using MainMVC.ViewModels.ProductViewModel;
using Microsoft.AspNetCore.Mvc;


namespace MainMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly CreateService _createService;
        private readonly ProductDetailService _productDetailService;
        private readonly ProductService _productService;
        private readonly DeleteService _deleteService;
        private readonly ListingService _listingService;
        private readonly CommentService _commentService;



        public ProductController(CreateService createService, ProductDetailService productDetailService, ProductService productService, DeleteService deleteService, ListingService listingService, CommentService commentService)
        {
            _createService = createService;
            _productDetailService = productDetailService;
            _productService = productService;
            _deleteService = deleteService;
            _listingService = listingService;
            _commentService = commentService;
        }

        public IActionResult Index()
        {
            _listingService.GetAllProducts();
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model, User seller, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = await _createService.CreateProductAsync(model, seller, category);

            // Redirect to the details page of the created product
            return RedirectToAction("Details", new { id = product.Id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _productService.GetProductById(id);
            if (product == null)
            {
                return NotFound(); // Ürün bulunamadıysa 404 hatası döndür
            }

            var model = new EditViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Details = product.Details,
                Price = product.Price
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(EditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Model geçerlilik kontrolü başarısız olursa formu tekrar göster
            }

            var product = _productService.GetProductById(model.Id);
            if (product == null)
            {
                return NotFound(); // Ürün bulunamadıysa 404 hatası döndür
            }

            product.Name = model.Name;
            product.Details = model.Details;
            product.Price = model.Price;

            _productService.UpdateProduct(product);

            return RedirectToAction("Index"); // Başarılıysa index sayfasına yönlendir
        }

        public async Task<IActionResult> Delete(int productId)
        {
           await _deleteService.DeleteProductAsync(productId);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();

        }

        [HttpPost]
        [Route("/Product/Details/{id}")]
        public IActionResult Details(int productId)
        {
            var productDetailViewModel = _productDetailService.GetProductDetail(productId);
            if (productDetailViewModel == null)
            {
                return NotFound();
            }


            return View(productDetailViewModel);
        }

        [HttpGet]
        public IActionResult AddComment()
        {
            return View(new CommentViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Yorum ekleme işlemi
                var result = await _commentService.AddCommentAsync(model.UserName, model.Content, model.ProductId, model.StarCount);
                if (!result)
                {
                    ModelState.AddModelError("", "Ürün veya kullanıcı bulunamadı."); // Kullanıcı veya ürün bulunamazsa hata mesajı ekle
                    return View(model);
                }

                return RedirectToAction("Index", "Home"); // Yorum başarıyla eklendi, anasayfaya yönlendir.
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Yorum eklenirken bir hata oluştu."); // Hata olursa, hata mesajını ekle ve sayfayı tekrar göster.
                return View(model);
            }
        }
    }
}
