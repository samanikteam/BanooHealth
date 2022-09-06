using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Product.Filter
{
    public class IndexModel : PageModel
    {
        private readonly IFilterRepository _filterRepository;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(IFilterRepository filterRepository, IAuthorizationService authorizationService)
        {
            _filterRepository = filterRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public FilterDto dto{ get; set; }
        public ListFilterDto Listdto{ get; set; }
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 15;
        public IActionResult OnGet(int PageNum = 1)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
            {
                Listdto = _filterRepository.GetListFilter(PageNum);
                //Add By vahid
                StringBuilder QParam = new StringBuilder();
                if (PageNum != 0)
                {
                    QParam.Append($"/Administration/Product/Filter/Index?PageNum=-");
                    //Administration / Blog / Articles / Index
                }
                if (Listdto.filters.Count >= 0)
                {
                    PagingData = new PagingData
                    {
                        CurrentPage = PageNum,
                        RecordsPerPage = PageSize,
                        TotalRecords = Listdto.count,
                        UrlParams = QParam.ToString(),
                        LinksPerPage = 7
                    };
                }
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
