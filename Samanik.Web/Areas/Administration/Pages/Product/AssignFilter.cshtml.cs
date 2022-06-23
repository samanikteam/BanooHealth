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

namespace Samanik.Web.Areas.Administration.Pages.Product
{
    public class AssignFilterModel : PageModel
    {
        private readonly IProductFilterRepository _productFilterRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFilterRepository _FilterRepository;

        public AssignFilterModel(IProductFilterRepository productFilterRepository, IProductRepository productRepository, IFilterRepository filterRepository)
        {
            _productFilterRepository = productFilterRepository;
            _productRepository = productRepository;
            _FilterRepository = filterRepository;
        }

        [BindProperty]
        public ProductFilterDto dto { get; set; }
        public ListProductFilterDto listProductFilterDto { get; set; }

        public void OnGet(int id)
        {
            listProductFilterDto = _productFilterRepository.GetListProductFilters(id);
            //ViewData["ProductList"] = new SelectList(_productRepository.GetProducts(), "Id", "Title");
            ViewData["FilterList"] = new SelectList(_FilterRepository.Getfilters(), "Id", "Title");
            ViewData["ProductId"] = id;
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();
            await _productFilterRepository.AddProductFilter(dto, cancellationToken);
            return Redirect("/Administration/Product/AssignFilter/"+dto.ProductId);
        }

        public async Task<IActionResult> OnPostConfirm(int id, CancellationToken cancellationToken)
        {
            await _productFilterRepository.Active(id, cancellationToken);
            return Redirect("/Administration/Product/AssignFilter");
        }

        public async Task<IActionResult> OnPostCancel(int id, CancellationToken cancellationToken)
        {
            await _productFilterRepository.Deactive(id, cancellationToken);
            return Redirect("/Administration/Product/AssignFilter");
        }
    }
}
