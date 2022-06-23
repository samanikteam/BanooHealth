using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Product.ProductCategory
{
    public class EditModel : PageModel
    {
        private readonly IProductCategoryRepository _productCategoryRepository;

        public EditModel(IProductCategoryRepository productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        [BindProperty]
        public ProductCategoryDto dto { get; set; }

        public void OnGet(int id)
        {
            ViewData["ProductCategories"] = new SelectList(_productCategoryRepository.GetProductCategories(), "Id", "Title");
            dto = _productCategoryRepository.GetProductCategorybyId(id);
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken, List<IFormFile> Image)
        {
            if (!ModelState.IsValid)
                return Page();

            await _productCategoryRepository.UpdateCategory(dto,  Image, cancellationToken);

            return Redirect("/Administration/Product/ProductCategory");
        }
    }
}
