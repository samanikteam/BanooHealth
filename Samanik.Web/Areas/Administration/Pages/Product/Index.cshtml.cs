using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Entities.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepasitory;
        private readonly IProductCategoryRepository _productCategoryRepasitory;
        private readonly IProductArticleRepository _productArticleRepository;
        private readonly IArticleRepasitory _articleRepository;
        
        public IndexModel(IProductRepository productRepasitory, IArticleRepasitory articleRepository,
            IProductArticleRepository productArticleRepository , IProductCategoryRepository productCategoryRepasitory)
        {
            _productRepasitory = productRepasitory;
            _articleRepository = articleRepository;
            _productArticleRepository = productArticleRepository;
            _productCategoryRepasitory = productCategoryRepasitory;
        }
        [BindProperty]
        public ProductDto dto { get; set; }
        public ListProductDto listProductDto { get; set; }
        public ListProductCategoryDto listProductCategoryDto { get; set; }
       
        public void OnGet()
        {
            #region Product Category
            listProductCategoryDto = _productCategoryRepasitory.GetListProductCategory();
            ViewData["ProductCategoryMother"]= new SelectList(_productCategoryRepasitory.GetListProductCategory().ProductCategories.Where(x=>x.ParentId==null), "Id", "Title");
            #endregion


            //ViewData["ProductCategoriesChildFirst"] = new SelectList(listProCategoryChildFirst, "Id", "ProductCatTitle");
            //ViewData["ProductCategoriesChildSecond"] = new SelectList(listProCategoryChildSecond, "Id", "ProductCatTitle");
            ViewData["ArticleList"] = new SelectList(_articleRepository.GetArticlesForComment(), "Id", "Title");
            listProductDto = _productRepasitory.GetListProduct();
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image1, CancellationToken cancellationToken, List<int> articleListId, int productCategoriesMother=0,
            int productCategoriesChild1 = 0 , int productCategoriesChild2 = 0 , int productCategoriesChild3=0)
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
            //if (productCategoriesChild1 == 0 && productCategoriesChild2 == 0 && productCategoriesChild3 == 0)
            //{
            //    dto.CategoryId = productCategoriesMother;
            //}
            //if (productCategoriesChild2 == 0 && productCategoriesChild3 == 0)
            //{
            //    dto.CategoryId = productCategoriesChild1;
            //}

            //if (productCategoriesChild2 != 0 && productCategoriesChild3 == 0 )
            //{
            //    dto.CategoryId = productCategoriesChild2;
            //}


            //if (productCategoriesChild3 != 0)
            //{
            //    dto.CategoryId = productCategoriesChild3;
            //}

            //var RegisterUserId = User.FindFirst("UserId").Value;
            var productId = await _productRepasitory.AddAsync(dto, RegisterUserId, Image1, cancellationToken);
            //add to table product Article
            await _productArticleRepository.AddProductArticle(articleListId, productId, cancellationToken);

            //add to table product Category
            //await _proCategoryRepository.AddProductCategory(categoryListId, productId, cancellationToken);

            return Redirect("/Administration/Product/Index");
        }
        public JsonResult OnGetGetProductCategoryChild(int id)
        {
            var objsub = _productCategoryRepasitory.GetListProductCategoryByCategoryId(id);

            return new JsonResult(objsub.ProductCategories.Select(c => new { c.Id, c.Title }));
        }

    }
}
