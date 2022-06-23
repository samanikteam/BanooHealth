using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Product.Filter
{
    public class IndexModel : PageModel
    {
        private readonly IFilterRepository _filterRepository;

        public IndexModel(IFilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }

        [BindProperty]
        public FilterDto dto{ get; set; }
        public ListFilterDto Listdto{ get; set; }

        public void OnGet()
        {
            Listdto = _filterRepository.GetListFilter();
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            await _filterRepository.AddFilter(dto, cancellationToken);

            return Redirect("/Administration/Product/Filter");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _filterRepository.Active(id, cancellationToken);
            return Redirect("/Administration/Product/Filter");
        }

        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _filterRepository.Deactive(id, cancellationToken);
            return Redirect("/Administration/Product/Filter");
        }
    }
}
