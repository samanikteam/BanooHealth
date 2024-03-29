using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Utilities;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.Blog
{
    [AllowAnonymous]
    public class ArticleDetailsModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly ICommentRepository _CommentRepository;
        private readonly IProductRepository _productRepository;

        public ArticleDetailsModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory, ICommentRepository commentRepository,
            IProductRepository productRepository)
        {
            _Repasitory = repasitory;
            _CRepasitory = cRepasitory;
            _CommentRepository = commentRepository;
            _productRepository = productRepository;
        }

        [BindProperty]
        public CommentDto Commentdto { get; set; }
        public ArticleDto articleDto { get; set; }
        public ListArticleDto listArticleDto { get; set; }
        public ListCommentDto listArticleCommentsDto { get; set; }
        public ListCommentDto SidebarComments { get; set; }
        public ListCommentDto listAnswerComment { get; set; }

        public ProductDto productDto { get; set; }

        public ArticleQueryModel Article;



        public List<Category> listArticleCategoryDto { get; set; }
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 12;
        public IActionResult OnGet(int id, CancellationToken cancellationToken , int PageNum = 1)
        {

            #region MyRegion
            //Article = arcre.GetArticleDetails(id);
            #endregion

            articleDto = _Repasitory.GetArticleById(id);
            listArticleCategoryDto = _CRepasitory.GetArticleCategories();
            listArticleDto = _Repasitory.GetListArticle(PageNum,PageSize);
            listArticleCommentsDto = _CommentRepository.GetListArticleComment(id);
            SidebarComments = _CommentRepository.GetListComments(PageNum,PageSize);
            productDto = _Repasitory.GetListProductByArticleId(id);
            _Repasitory.Visited(id, cancellationToken);


            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Blog/ArticleDetails.cshtml/" + id + " ?PageNum=-");

            }
            if (listArticleDto.Articles.Count >= 0 || SidebarComments.Comments.Count >= 0)
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    TotalRecords = listArticleDto.count,
                    TotalRecordsd = SidebarComments.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 7
                };
            }

            return Page();

        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            await _CommentRepository.AddComment(Commentdto, cancellationToken);
            articleDto = _Repasitory.GetArticleById(Commentdto.ArticleId);
            listArticleCategoryDto = _CRepasitory.GetArticleCategories();
            listArticleDto = _Repasitory.GetListArticle();
            listArticleCommentsDto = _CommentRepository.GetListArticleComment(Commentdto.ArticleId);
            SidebarComments = _CommentRepository.GetListComments();
            productDto = _Repasitory.GetListProductByArticleId(Commentdto.ArticleId);



            return Page();
        }
    }
}
