﻿using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Samanik.Web.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IArticleRepasitory _articleRepasitory;
        private readonly IArticelCategoryRepasitory _articelCategoryRepasitory;
        private readonly ISliderRepository _sliderRepository;
        private readonly IBannerRepository _bannerRepository;
        private readonly ISearchLiveRepository _searchLiveRepository;
        private readonly ISloganRepository _sloganRepository;
        private readonly ISiteSettingRepository _siteSettingRepository;
        private readonly INewsRepository _newsRepository;
        private readonly IProductRepository _productRepository;

        public IndexModel(ILogger<IndexModel> logger, IArticleRepasitory articleRepasitory, IArticelCategoryRepasitory articelCategoryRepasitory, ISliderRepository sliderRepository, IBannerRepository bannerRepository, ISearchLiveRepository searchLiveRepository, ISloganRepository sloganRepository, ISiteSettingRepository siteSettingRepository, INewsRepository newsRepository, IProductRepository productRepository)
        {
            _logger = logger;
            _articleRepasitory = articleRepasitory;
            _articelCategoryRepasitory = articelCategoryRepasitory;
            _sliderRepository = sliderRepository;
            _bannerRepository = bannerRepository;
            _searchLiveRepository = searchLiveRepository;
            _sloganRepository = sloganRepository;
            _siteSettingRepository = siteSettingRepository;
            _newsRepository = newsRepository;
            _productRepository = productRepository;
        }

        public ListArticleDto ArticleDto { get; set; }
        //search Live
        // public SearchLiveWithArticleDto searchLiveWithArticleDto { get; set; }
        public ListArticleCategoryDto ArticleCategoryDto { get; set; }
        public ListSliderDto sliderDto { get; set; }
        public BannerDto bannerDto { get; set; }
        public ListSloganDto sloganDto { get; set; }
        public SiteSettingDto settingDto { get; set; }
        public ListProductDto productDto { get; set; }
        public void OnGet()
        {
            ArticleDto = _articleRepasitory.GetListArticle(1,12);
            ArticleCategoryDto = _articelCategoryRepasitory.GetListArticleCategory(1,30);
            sliderDto = _sliderRepository.GetListSliderDto(1,12);
            bannerDto = _bannerRepository.GetBanner();
            sloganDto = _sloganRepository.GetListSlogans();
            settingDto = _siteSettingRepository.GetSetting();
            productDto = _productRepository.GetListProduct(1,12);
        }
        public async Task<IActionResult> OnPostNews(string Email,CancellationToken cancellationToken)
        {
            await _newsRepository.AddEmail(Email, cancellationToken);

            return RedirectToPage("/Index");
        }
    }
}
