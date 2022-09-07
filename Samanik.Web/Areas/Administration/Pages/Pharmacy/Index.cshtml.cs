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

namespace Samanik.Web.Areas.Administration.Pages.Pharmacy
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(IPharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        [BindProperty]
        public PharmacyDto dto { get; set; }
        public ListPharmacyDto listPharmacy { get; set; }
       
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 12;
        public IActionResult OnGet(int PageNum = 1)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Resaneh).Result.Succeeded)
            {
                listPharmacy = _pharmacyRepository.GetListPharmacy(PageNum);
                listPharmacy = _pharmacyRepository.GetListPharmacy(PageNum, PageSize);
                //Add By vahid
                StringBuilder QParam = new StringBuilder();
                if (PageNum != 0)
                {
                    QParam.Append($"/Administration/Pharmacy/Index?PageNum=-");
                    //Administration / Blog / Articles / Index
                }
                if (listPharmacy.pharmacies.Count >= 0)
                {
                    PagingData = new PagingData
                    {
                        CurrentPage = PageNum,
                        RecordsPerPage = PageSize,
                        TotalRecords = listPharmacy.count,
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

            await _pharmacyRepository.AddPharmacy(dto, cancellationToken);

            return Redirect("/Administration/Pharmacy");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _pharmacyRepository.Enable(id, cancellationToken);
            return Redirect("/Administration/Pharmacy");
        }

        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _pharmacyRepository.Disable(id, cancellationToken);
            return Redirect("/Administration/Pharmacy");
        }
    }
}
