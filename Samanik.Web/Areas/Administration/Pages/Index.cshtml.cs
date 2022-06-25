using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages
{
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
            ArticleDto = _articleRepasitory.GetListArticle();
            productDto = _productRepository.GetListProduct();
            commentDto = _commentRepository.GetListComments();
            procommentDto = _proCommentRepository.GetListProComments();
            pharmacyDto = _pharmacyRepository.GetListPharmacy();
        }

    }
}
