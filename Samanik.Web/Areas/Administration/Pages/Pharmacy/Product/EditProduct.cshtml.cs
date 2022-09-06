using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Pharmacy.Product
{
    public class EditProductModel : PageModel
    {
        private readonly IPharmacyProduct _pharmacyProduct;
        private readonly IAuthorizationService _authorizationService;


        public EditProductModel(IPharmacyProduct pharmacyProduct, IAuthorizationService authorizationService)
        {
            _pharmacyProduct = pharmacyProduct;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public PharmacyProductDto dto { get; set; }

        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Resaneh).Result.Succeeded)
            {
                dto = _pharmacyProduct.GetProductPharmactById(id);
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }

        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            await _pharmacyProduct.EditPharmacyProduct(dto,cancellationToken);

            return Redirect("/Administration/Pharmacy/Product");
        }

    }
}
