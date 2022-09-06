using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Product.ProductCategory
{
    public class EditModel : PageModel
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IAuthorizationService _authorizationService;


        public EditModel(IProductCategoryRepository productCategoryRepository, IAuthorizationService authorizationService)
        {
            _productCategoryRepository = productCategoryRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ProductCategoryDto dto { get; set; }

        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
            {
                ViewData["ProductCategories"] = new SelectList(_productCategoryRepository.GetProductCategories(), "Id", "Title");
                dto = _productCategoryRepository.GetProductCategorybyId(id);
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }
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
