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
        private readonly IContactUsRepository _contactUsRepository;

        public FooterViewComponent(INewsRepository newsRepository, IArticelCategoryRepasitory articelCategoryRepasitory, IArticleRepasitory articleRepasitory, ISiteSettingRepository siteSettingRepository, IContactUsRepository contactUsRepository)
        {
            _newsRepository = newsRepository;
            _articelCategoryRepasitory = articelCategoryRepasitory;
            _articleRepasitory = articleRepasitory;
            _siteSettingRepository = siteSettingRepository;
            _contactUsRepository = contactUsRepository;
        }

        public NewsDto newsDto { get; set; }

        public IViewComponentResult Invoke()
        {
            FooterViewModel mymodel = new FooterViewModel();
            mymodel.ListArticle = _articleRepasitory.GetListArticle();
            mymodel.listArticleCategory = _articelCategoryRepasitory.GetListArticleCategory();
            mymodel.settingDto = _siteSettingRepository.GetSetting();
            mymodel.contactusDto = _contactUsRepository.GetContactus();
            return View(mymodel);
        }
    }
}
