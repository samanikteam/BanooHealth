using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Repositories;
using Entities.Articles;
using Entities.Products;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.Blog
{
    public class ArticleSearchModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IProductRepository _productRepasitory;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly ICommentRepository _commnetRepository;

        public ArticleSearchModel(IArticleRepasitory repasitory, IProductRepository productRepasitory, IProductCategoryRepository productCategoryRepository, ICommentRepository commnetRepository)
        {
            _Repasitory = repasitory;
            _productRepasitory = productRepasitory;
            _productCategoryRepository = productCategoryRepository;
            _commnetRepository = commnetRepository;
        }

        [BindProperty]
        public List<Article> listArticle { get; set; }
        public ListArticleDto listArticle2 { get; set; }
        public ListProductCategoryDto listProductCategoryDto { get; set; }
        public List<Entities.Products.Product> listProduct { get; set; }
        public ListCommentDto commentDto { get; set; }
        public void OnGet(int categoryId , string title="")
        {
            commentDto = _commnetRepository.GetListComments();
            listArticle2 = _Repasitory.GetListArticle();
            if (categoryId==1)
            {
                ViewData["SelectedArticle"] = "true";
                listArticle = _Repasitory.searchArticle(title);
            }
            if (categoryId==2)
            {
                ViewData["SelectedProduct"] = "true";
                //دسته بندی محصولات
                listProduct = _productRepasitory.searchProduct(title);
                listProductCategoryDto = _productCategoryRepository.GetListProductCategory();
            }
        }
    }
}
