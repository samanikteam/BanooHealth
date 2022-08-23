using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public ProductCategoryDto productCategoryDto { get; set; }

        public ListProductCategoryDto listProductCategoryDto { get; set; }
            //Add By Vahid
            public PagingData PagingData { get; set; }
            public int PageSize = 8;
        public void OnGet(int productCategoryId,string slug, int PageNum = 1)
        {
            bannerDto = _bannerRepository.GetBanner();
            listProductDto = _productRepository.GetListProductsByProductCategoryId(productCategoryId, PageNum);
            listProductCategoryDto = _ProductCategoryRepository.GetListProductCategory(PageNum);
            productCategoryDto = _ProductCategoryRepository.GetProductCategorybyId(productCategoryId);
            //Add By vahid
            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Shopping/ProductCategory/" + productCategoryId + "?PageNum=-");

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
