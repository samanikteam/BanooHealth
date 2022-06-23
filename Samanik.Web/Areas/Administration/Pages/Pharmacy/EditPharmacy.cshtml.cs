using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Pharmacy
{
    public class EditPharmacyModel : PageModel
    {
        private readonly IPharmacyRepository _pharmacyRepository;

        public EditPharmacyModel(IPharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        [BindProperty]
        public PharmacyDto dto { get; set; }
        public void OnGet(int id)
        {
            dto = _pharmacyRepository.GetPharmacyById(id);
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
