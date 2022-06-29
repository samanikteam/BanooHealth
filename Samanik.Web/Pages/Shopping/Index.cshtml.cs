using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Entities.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.MainPage.ProductsShop
{
    public class ProductShopModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _ProductCategoryRepository;
        private readonly IBannerRepository _bannerRepository;

        public ProductShopModel(IProductRepository productRepository, IProductCategoryRepository productCategoryRepository, IBannerRepository bannerRepository)
        {
            _productRepository = productRepository;
            _ProductCategoryRepository = productCategoryRepository;
            _bannerRepository = bannerRepository;
        }

        [BindProperty]
        public ListProCategoryDto listProCategoryDtoChild { get; set; }
        public BannerDto bannerDto { get; set; }
        public ListProductDto listProductDto { get; set; }

        public ListProductCategoryDto listProductCategoryDto { get; set; }
        public void OnGet( string ProductCatPath = null)
        {
            bannerDto = _bannerRepository.GetBanner();
            listProductDto = _productRepository.GetListProduct();
            listProductCategoryDto = _ProductCategoryRepository.GetListProductCategory();
        }


    }
}
