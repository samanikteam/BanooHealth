using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Pharmacy.Product
{
    public class IndexModel : PageModel
    {
        private readonly IPharmacyProduct _pharmacyProduct;
        private readonly IProductRepository _productRepository;
        private readonly IPharmacyRepository _pharmacyRepository;

        public IndexModel(IPharmacyProduct pharmacyProduct, IProductRepository productRepository, IPharmacyRepository pharmacyRepository)
        {
            _pharmacyProduct = pharmacyProduct;
            _productRepository = productRepository;
            _pharmacyRepository = pharmacyRepository;
        }

        [BindProperty]
        public PharmacyProductDto dto { get; set; }
        public ListPharmacyProductDto listPharmacyProductDto { get; set; }

        public void OnGet()
        {
            listPharmacyProductDto = _pharmacyProduct.GetListPharmacyProducts();
            ViewData["PharmaciesList"] = new SelectList(_pharmacyRepository.GetPharmacies(), "Id", "Name");
            ViewData["Products"] = new SelectList(_productRepository.GetProducts(), "Id", "Title");
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            await _pharmacyProduct.AddPharmacyProduct(dto, cancellationToken);

            return Redirect("/Administration/Pharmacy/Product");
        }

        public async Task<IActionResult> OnPostConfirm(int id, CancellationToken cancellationToken)
        {
            await _pharmacyProduct.Confirm(id, cancellationToken);
            return Redirect("/Administration/Pharmacy/Product/Index");
        }

        public async Task<IActionResult> OnPostCancel(int id, CancellationToken cancellationToken)
        {
            await _pharmacyProduct.Cancel(id, cancellationToken);
            return Redirect("/Administration/Pharmacy/Product/Index");
        }

    }
}
