using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Entities.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.MainPage.ProductsShop
{
    [AllowAnonymous]
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

        public PagingData PagingData { get; set; }
        public int PageSize = 16;

        public void OnGet(string ProductCatPath = null, int PageNum = 1)
        {
            bannerDto = _bannerRepository.GetBanner();
            listProductDto = _productRepository.GetListProduct(PageNum, PageSize);
            listProductCategoryDto = _ProductCategoryRepository.GetListProductCategory(1, 50);

            //Add By vahid
            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Shopping?PageNum=-");
            }
            if (listProductDto.Products.Count >= 0)
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    TotalRecords = listProductDto.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 7
                };
            }
        }


    }
}
