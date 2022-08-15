using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 15;
        public void OnGet(int PageNum = 1)
        {
            listPharmacyProductDto = _pharmacyProduct.GetListPharmacyProducts(PageNum);
            ViewData["PharmaciesList"] = new SelectList(_pharmacyRepository.GetPharmacies(), "Id", "Name");
            ViewData["Products"] = new SelectList(_productRepository.GetProducts(), "Id", "Title");
            //Add By vahid
            StringBuilder QParam = new StringBuilder();
            if (PageNum != 0)
            {
                QParam.Append($"/Administration/Pharmacy/Product/Index?PageNum=-");
                //Administration / Blog / Articles / Index
            }
            if (listPharmacyProductDto.PharmacyProducts.Count >= 0)
            {
                PagingData = new PagingData
                {
                    CurrentPage = PageNum,
                    RecordsPerPage = PageSize,
                    TotalRecords = listPharmacyProductDto.count,
                    UrlParams = QParam.ToString(),
                    LinksPerPage = 7
                };
            }
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
