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
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryRepository _Repository;

        public IndexModel(IProductCategoryRepository repository)
        {
            _Repository = repository;
        }

        [BindProperty]
        public ProductCategoryDto dto { get; set; }
        public ListProductCategoryDto ListProductCategoryDto { get; set; }


        public void OnGet()
        {
            ViewData["ProductCategories"] = new SelectList(_Repository.GetProductCategories(), "Id", "Title");
            ListProductCategoryDto = _Repository.GetListProductCategory();
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            var exist = await _Repository.IsExistProductCategory(dto.Title, Convert.ToInt32(dto.ParentId));
            if (exist)
            {
                ViewData["ArticleCategories"] = new SelectList(_Repository.GetProductCategories(), "Id", "Title");
                ModelState.AddModelError("Title", "دسته‌بندی با مشخصات وارد شده تکراری است.");
                return Page();
            }
            await _Repository.AddProductCategory(dto, Image, cancellationToken);

            return Redirect("/Administration/Product/ProductCategory");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _Repository.Active(id, cancellationToken);
            return Redirect("/Administration/Product/ProductCategory");
        }

        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _Repository.Deactive(id, cancellationToken);
            return Redirect("/Administration/Product/ProductCategory");
        }
    }
}
