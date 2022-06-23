using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Pharmacy.Product
{
    public class EditProductModel : PageModel
    {
        private readonly IPharmacyProduct _pharmacyProduct;

        public EditProductModel(IPharmacyProduct pharmacyProduct)
        {
            _pharmacyProduct = pharmacyProduct;
        }

        [BindProperty]
        public PharmacyProductDto dto { get; set; }

        public void OnGet(int id)
        {
            dto = _pharmacyProduct.GetProductPharmactById(id);
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
