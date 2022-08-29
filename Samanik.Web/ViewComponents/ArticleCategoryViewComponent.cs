using Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web.ViewComponents
{

    public class ArticleCategoryViewComponent :  ViewComponent
    {
        private readonly IArticelCategoryRepasitory _categoryRepository;

        public ArticleCategoryViewComponent(IArticelCategoryRepasitory categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.GetListArticleCategory(1,30);

            return View(categories);
        }
    }
}
