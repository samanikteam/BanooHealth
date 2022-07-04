using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly INewsRepository _newsRepository;
        private readonly IArticelCategoryRepasitory _articelCategoryRepasitory;
        private readonly IArticleRepasitory _articleRepasitory;
        private readonly ISiteSettingRepository _siteSettingRepository;

        public FooterViewComponent(INewsRepository newsRepository, IArticelCategoryRepasitory articelCategoryRepasitory, IArticleRepasitory articleRepasitory, ISiteSettingRepository siteSettingRepository)
        {
            _newsRepository = newsRepository;
            _articelCategoryRepasitory = articelCategoryRepasitory;
            _articleRepasitory = articleRepasitory;
            _siteSettingRepository = siteSettingRepository;
        }

        public ListArticleDto ArticleDto { get; set; }
        public ListArticleCategoryDto ArticleCategoryDto { get; set; }
        public SiteSettingDto settingDto { get; set; }
        public NewsDto newsDto { get; set; }

        public IViewComponentResult Invoke()
        {
            //ArticleDto = _articleRepasitory.GetListArticle();
            ArticleCategoryDto = _articelCategoryRepasitory.GetListArticleCategory();
            //settingDto = _siteSettingRepository.GetSetting();
            return View(ArticleCategoryDto);
        }
    }
}
