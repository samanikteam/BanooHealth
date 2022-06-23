using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Product
{
    public class ProductManagementModel : PageModel
    {
        private readonly IProductRepository _productRepasitory;
        private readonly IProductCategoryRepository _productCategoryRepasitory;
        private readonly IArticleRepasitory _articleRepository;
        private readonly IProductArticleRepository _productArticleRepository;
        private readonly IProCategoriesRepository _proCategoriesRepository;

        public ProductManagementModel(IProductRepository productRepasitory, IProductCategoryRepository productCategoryRepasitory, IProductArticleRepository productArticleRepository, IArticleRepasitory articleRepository, IProCategoriesRepository proCategoriesRepository)
        {
            _productRepasitory = productRepasitory;
            _productCategoryRepasitory = productCategoryRepasitory;
            _productArticleRepository = productArticleRepository;
            _articleRepository = articleRepository;
            _proCategoriesRepository = proCategoriesRepository;
        }

        [BindProperty]
        public ProductDto dto { get; set; }
        public ListProductDto listProductDto { get; set; }
        public ListProductCategoryDto listProductCategoryDto { get; set; }

        public void OnGet()
        {
            #region Product Category
            listProductCategoryDto = _productCategoryRepasitory.GetListProductCategory();
            ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
            #endregion
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");
            listProductDto = _productRepasitory.GetListProduct();
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image1, CancellationToken cancellationToken, List<int> articleListId, List<int> productCategoryId)
        {
            if (!ModelState.IsValid)
                return Page();

            var exist = await _productRepasitory.IsExistProduct(dto.Title);

            if (exist)
            {
                listProductDto = _productRepasitory.GetListProduct();
                ModelState.AddModelError("", "محصول با مشخصات وارد شده تکراری است.");
                return Page();
            }

            var RegisterUserId = "Admin";
            var productId = await _productRepasitory.AddAsync(dto, RegisterUserId, Image1, cancellationToken);

            //add to table product Article
            await _productArticleRepository.AddProductArticle(articleListId, productId, cancellationToken);
            await _proCategoriesRepository.AddProductCategory(productCategoryId, productId, cancellationToken);


            return Redirect("/Administration/Product/ProductManagement");
        }
    }
}
