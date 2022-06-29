using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.MainPage.ProductsShop
{
    public class ProductByProductCategoryModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IBannerRepository _bannerRepository;
        private readonly IProductCategoryRepository _ProductCategoryRepository;

        public ProductByProductCategoryModel(IProductRepository productRepository, IBannerRepository bannerRepository, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _bannerRepository = bannerRepository;
            _ProductCategoryRepository = productCategoryRepository;
        }

        [BindProperty]

        public ListProductDto listProductDto { get; set; }
        public List<Entities.Products.Product> products { get; set; }
        public List<Category> listArticleCategoryDto { get; set; }
        public BannerDto bannerDto { get; set; }

        public ListProductCategoryDto listProductCategoryDto { get; set; }
        public void OnGet(int productCategoryId,string slug)
        {
            bannerDto = _bannerRepository.GetBanner();
            listProductDto = _productRepository.GetListProductsByProductCategoryId(productCategoryId);
            listProductCategoryDto = _ProductCategoryRepository.GetListProductCategory();
        }
    }
}
