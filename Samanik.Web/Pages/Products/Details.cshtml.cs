using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.MainPage.ProductDetails
{
    [AllowAnonymous]
    public class ProductDetailWithPharmacyModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly ICommentRepository _CommentRepository;
        private readonly IProductArticleRepository _ProductArticleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProCommentRepository _proCommentRepository;
        private readonly IProGalleryRepository _proGalleryRepository;
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IPharmacyProduct _pharmacyProductRepository;
        private readonly IProductFilterRepository _productFilterRepository;

        public ProductDetailWithPharmacyModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory, ICommentRepository commentRepository, IProductArticleRepository productArticleRepository, IProductRepository productRepository, IProCommentRepository proCommentRepository, IProGalleryRepository proGalleryRepository, IPharmacyRepository pharmacyRepository, IPharmacyProduct pharmacyProductRepository, IProductFilterRepository productFilterRepository)
        {
            _Repasitory = repasitory;
            _CRepasitory = cRepasitory;
            _CommentRepository = commentRepository;
            _ProductArticleRepository = productArticleRepository;
            _productRepository = productRepository;
            _proCommentRepository = proCommentRepository;
            _proGalleryRepository = proGalleryRepository;
            _pharmacyRepository = pharmacyRepository;
            _pharmacyProductRepository = pharmacyProductRepository;
            _productFilterRepository = productFilterRepository;
        }

        [BindProperty]
        public ProductDto productDto { get; set; }
        [BindProperty]
        public ProCommentDto ProCommentDto { get; set; }
        public ArticleDto articleDto { get; set; }
        public ListProductDto listProductDto { get; set; }
        public ListProCommentDto listProCommentsDto { get; set; }
        public ListProCommentDto SidebarComments { get; set; }

        public ListProCategoryDto listProductCategoryDto { get; set; }
        public ListProGalleryDto listProGalleryDto { get; set; }
        public ListPharmacyDto listPharmacyDto { get; set; }
        public ListPharmacyWithProductDto listPharmacyWithProductDto { get; set; }
        public ListProductDetailWithPharmacyDto listProductDetailWithPharmacyDto { get; set; }
        public ListProductFilterDto listProductFilterDto { get; set; }
        public ProductArticleDto productArticleDto { get; set; }


        public void OnGet(int id,string slug)
        {
            listProductFilterDto = _productFilterRepository.GetListProductFilters(id);
            productDto = _productRepository.GetProductByProductId(id);
            listProCommentsDto = _proCommentRepository.GetListProComment(id);
            listProGalleryDto = _proGalleryRepository.GetListPorGalleryByProductId(id);
            listPharmacyWithProductDto = _pharmacyProductRepository.FindListPharmacyProductWhenExistProduct(id);
            productArticleDto = _ProductArticleRepository.GetListArticleAndProductByProductId(id);

        }
        public IActionResult OnPost(CancellationToken cancellationToken)
        {
            var x = ProCommentDto;
            _proCommentRepository.AddProComment(ProCommentDto, cancellationToken);

            return Redirect("/Products/" + ProCommentDto.ProductId);
        }
    }
}
