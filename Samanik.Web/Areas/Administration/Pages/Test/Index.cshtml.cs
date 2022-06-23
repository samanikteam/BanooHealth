using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Test
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepasitory;
        public IndexModel(IProductRepository productRepasitory)
        {
            _productRepasitory = productRepasitory;
        }
        [BindProperty]
        public ProductDto dto { get; set; }
        public ListProductDto listProductDto { get; set; }
        public void OnGet()
        {
           // ViewData["ProductCategories"] = new SelectList(_productCategoryRepository.GetProductCategories(), "Id", "Title");
            listProductDto = _productRepasitory.GetListProduct();
        }
    }
}
