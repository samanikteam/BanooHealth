using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Repositories;
using Entities.Articles;
using Entities.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.Blog
{
    [AllowAnonymous]
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
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public void OnGet(int categoryId, string title = "", int PageNum = 1, int PageSize=10)
        {
            commentDto = _commnetRepository.GetListComments(PageNum, PageSize);
            listArticle2 = _Repasitory.GetListArticle(PageNum, PageSize);
            if (categoryId == 1)
            {
                ViewData["SelectedArticle"] = "true";
                listArticle = _Repasitory.searchArticle(title);
            }
            if (categoryId == 2)
            {
                ViewData["SelectedProduct"] = "true";
                //دسته بندی محصولات
                listProduct = _productRepasitory.searchProduct(title);
                listProductCategoryDto = _productCategoryRepository.GetListProductCategory();
            }

            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Blog/ArticleSearch.cshtml/" + categoryId + " ?PageNum=-");

            }
            if ( listArticle2.Articles.Count>= 0)
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    //TotalRecords = commentDto.count,
                    TotalRecords = listArticle2.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 6
                };
            }
        }
    }
}
