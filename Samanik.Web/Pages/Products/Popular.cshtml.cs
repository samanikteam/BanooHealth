using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.Products
{
    public class PopularModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IProductRepository _productRepository;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly ICommentRepository _CommentRepository;
        private readonly IProductArticleRepository _ProductArticleRepository;
        private readonly IProductCategoryRepository _ProductCategoryRepository;

        public PopularModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory,
            ICommentRepository commentRepository, IProductArticleRepository productArticleRepository,
            IProductRepository productRepository, IProductCategoryRepository ProductCategoryRepository)
        {
            _Repasitory = repasitory;
            _productRepository = productRepository;
            _ProductArticleRepository = productArticleRepository;
            _CRepasitory = cRepasitory;
            _CommentRepository = commentRepository;
            _ProductCategoryRepository = ProductCategoryRepository;
        }

        [BindProperty]
        public CommentDto Commentdto { get; set; }
        public ArticleDto articleDto { get; set; }
        public ListArticleDto listArticleDto { get; set; }
        public ListProCategoryDto listProCategoryDtoMother { get; set; }
        public ListProCategoryDto listProCategoryDtoChild { get; set; }
        public ListCommentDto listArticleCommentsDto { get; set; }
        public ListCommentDto SidebarComments { get; set; }

        public ListProductDto listProductDto { get; set; }
        public List<Category> listArticleCategoryDto { get; set; }

        public ListProductCategoryDto listProductCategoryDto { get; set; }
        public void OnGet(string ProductCatPath = null , string slug=null)
        {

            listProductDto = _productRepository.GetListProduct();
            listProductCategoryDto = _ProductCategoryRepository.GetListProductCategory();
        }
    }
}
