using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 15;

        public void OnGet(int PageNum = 1)
        {
            #region Product Category
            listProductCategoryDto = _productCategoryRepasitory.GetListProductCategory();
            ViewData["ProductCategory"] = new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories, "Id", "Title");
            #endregion
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");
            listProductDto = _productRepasitory.GetListProduct(PageNum);
            //Add By vahid
            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Administration/Product/ProductManagement?PageNum=-");
                //Administration / Blog / Articles / Index
            }
            if (listProductDto.Products.Count >= 0)
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    TotalRecords = listProductDto.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 7
                };
            }
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
