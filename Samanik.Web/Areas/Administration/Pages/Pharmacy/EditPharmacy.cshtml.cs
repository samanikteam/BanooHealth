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

namespace Samanik.Web.Areas.Administration.Pages.Pharmacy
{
    [Authorize]
    public class EditPharmacyModel : PageModel
    {
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly IAuthorizationService _authorizationService;


        public EditPharmacyModel(IPharmacyRepository pharmacyRepository, IAuthorizationService authorizationService)
        {
            _pharmacyRepository = pharmacyRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public PharmacyDto dto { get; set; }
        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.DarooKhaneh).Result.Succeeded)
            {
                dto = _pharmacyRepository.GetPharmacyById(id);
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

            await _pharmacyRepository.UpdateAsync(dto,cancellationToken);

            //return Redirect("/Administration/Pharmacy/EditPharmacy?id=" + dto.Id);
            return Redirect("/Administration/Pharmacy");
        }
    }
}
