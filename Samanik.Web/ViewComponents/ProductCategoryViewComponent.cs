using Data.Contracts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web.ViewComponents
{
    public class ProductCategoryViewComponent : ViewComponent
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public ProductCategoryViewComponent(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _productCategoryRepository.GetListProductCategory(1,40);

            return View(categories);
        }
    }
}
