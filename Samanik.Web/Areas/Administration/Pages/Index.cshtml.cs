using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IArticleRepasitory _articleRepasitory;
        private readonly IProductRepository _productRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IProCommentRepository _proCommentRepository;
        private readonly IPharmacyRepository _pharmacyRepository;

        public IndexModel(IArticleRepasitory articleRepasitory, IProductRepository productRepository, ICommentRepository commentRepository, IProCommentRepository proCommentRepository, IPharmacyRepository pharmacyRepository)
        {
            _articleRepasitory = articleRepasitory;
            _productRepository = productRepository;
            _commentRepository = commentRepository;
            _proCommentRepository = proCommentRepository;
            _pharmacyRepository = pharmacyRepository;
        }

        public ListArticleDto ArticleDto { get; set; }
        public ListCommentDto commentDto { get; set; }
        public ListProCommentDto procommentDto { get; set; }
        public ListProductDto productDto { get; set; }
        public ListPharmacyDto pharmacyDto { get; set; }

        public void OnGet()
        {
            ArticleDto = _articleRepasitory.GetListArticle(1,5);
            productDto = _productRepository.GetListProduct(1,5);
            commentDto = _commentRepository.GetListComments(1,50);
            procommentDto = _proCommentRepository.GetListProComments();
            pharmacyDto = _pharmacyRepository.GetListPharmacy(1,50);
        }

    }
}
