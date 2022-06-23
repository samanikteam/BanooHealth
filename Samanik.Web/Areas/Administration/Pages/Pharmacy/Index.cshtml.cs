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
    public class IndexModel : PageModel
    {
        private readonly IPharmacyRepository _pharmacyRepository;

        public IndexModel(IPharmacyRepository pharmacyRepository)
        {
            _pharmacyRepository = pharmacyRepository;
        }

        [BindProperty]
        public PharmacyDto dto { get; set; }
        public ListPharmacyDto listPharmacy { get; set; }

        public void OnGet()
        {
            listPharmacy = _pharmacyRepository.GetListPharmacy();
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
