
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Product
{
    public class ProductDetailModel : PageModel
    {
        private readonly IProductRepository _productRepasitory;
        private readonly IProductArticleRepository _productArticleRepository;
        private readonly IProGalleryRepository _proGalleryRepository;
        private readonly IArticleRepasitory _articleRepository;
        private readonly IProductCategoryRepository _productCategoryRepasitory;
        private readonly IProCategoriesRepository _proCategoriesRepository;
        private readonly IAuthorizationService _authorizationService;


        public ProductDetailModel(IProductRepository productRepasitory, IProductArticleRepository productArticleRepository,
            IProGalleryRepository proGalleryRepository, IArticleRepasitory articleRepository, IProductCategoryRepository productCategoryRepasitory,
            IProCategoriesRepository proCategoriesRepository, IAuthorizationService authorizationService)
        {
            _productRepasitory = productRepasitory;
            _productArticleRepository = productArticleRepository;
            _proGalleryRepository = proGalleryRepository;
            _articleRepository = articleRepository;
            _productCategoryRepasitory = productCategoryRepasitory;
            _proCategoriesRepository = proCategoriesRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ProductDto dto { get; set; }
        public ProGalleryDto proGalleryDto { get; set; }
        public ListProGalleryDto listProGalleryDto { get; set; }


        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
            {
                ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
                ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");

                var productId = id;
                ViewData["productId"] = productId;
                dto = _productRepasitory.GetProductByProductId(productId);
                listProGalleryDto = _proGalleryRepository.GetListPorGalleryByProductId(id);

                return Page();
            }
            else
            {
                return Redirect("/login/logout");
            }
        }


        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");

            await _productRepasitory.Active(id, cancellationToken);
            return Redirect("/Administration/Product/ProductDetail/"+id);
        }

        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");

            await _productRepasitory.Deactive(id, cancellationToken);
            return Redirect("/Administration/Product/ProductDetail/" + id);
        }

        public async Task<IActionResult> OnPostActivate(int id,int productId, CancellationToken cancellationToken)
        {
            ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");

            await _proGalleryRepository.Activate(id, cancellationToken);
            return Redirect("/Administration/Product/ProductDetail/" + productId);
        }


        public async Task<IActionResult> OnPostAddProductPictureToGallery(List<IFormFile> image1, int productId, CancellationToken cancellationToken)
        {

            if (!ModelState.IsValid)
                return Page();
            ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");

            proGalleryDto = new ProGalleryDto();
            var RegisterUserId = "Admin";
            proGalleryDto.ProductId = productId;
            //var RegisterUserId = User.FindFirst("UserId").Value;
            var proGalleryId = await _proGalleryRepository.AddAsync(proGalleryDto, RegisterUserId, image1, cancellationToken);

            return Redirect("/Administration/Product/ProductDetail/" + productId);
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken, List<int> articleListId, List<int> productCategoryId)
        {
            if (!ModelState.IsValid)
                return Page();

            ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");

            var RegisterUserId = "Admin";
            var productId = await _productRepasitory.UpdateAsync(dto, RegisterUserId, Image, cancellationToken);

            if (articleListId.Count != 0)
                await _productArticleRepository.AddProductArticle(articleListId, productId, cancellationToken);

            if (productCategoryId.Count != 0)
                await _proCategoriesRepository.AddProductCategory(productCategoryId, productId, cancellationToken);

            return Redirect("/Administration/Product/ProductDetail/" + dto.Id);
        }
    }
}
