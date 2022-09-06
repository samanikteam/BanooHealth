using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IAuthorizationService _authorizationService;


        public AssignFilterModel(IProductFilterRepository productFilterRepository, IProductRepository productRepository, IFilterRepository filterRepository, IAuthorizationService authorizationService)
        {
            _productFilterRepository = productFilterRepository;
            _productRepository = productRepository;
            _FilterRepository = filterRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ProductFilterDto dto { get; set; }
        public ListProductFilterDto listProductFilterDto { get; set; }
        public int pi = 0;

        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
            {
                listProductFilterDto = _productFilterRepository.GetListProductFilters(id);
                ViewData["FilterList"] = new SelectList(_FilterRepository.Getfilters(), "Id", "Title");
                ViewData["ProductId"] = id;
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

            await _productFilterRepository.AddProductFilter(dto, cancellationToken);
            return Redirect("/Administration/Product/AssignFilter/" + dto.ProductId);
        }

        public async Task<IActionResult> OnPostActive(int id,int Productid, CancellationToken cancellationToken)
        {
            await _productFilterRepository.Active(id, cancellationToken);
            return Redirect("/Administration/Product/AssignFilter/" + Productid);
        }

        public async Task<IActionResult> OnPostDeactive(int id,int Productid, CancellationToken cancellationToken)
        {
            await _productFilterRepository.Deactive(id, cancellationToken);
            return Redirect("/Administration/Product/AssignFilter/" + Productid);
        }
    }
}
