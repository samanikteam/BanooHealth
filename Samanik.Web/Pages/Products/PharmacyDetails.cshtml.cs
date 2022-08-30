using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.Products
{
    public class PharmacyDetailsModel : PageModel
    {
        private readonly IPharmacyRepository _pharmacyRepository;
        private readonly INewsRepository _newsRepository;
        public PharmacyDetailsModel(IPharmacyRepository pharmacyRepository, INewsRepository newsRepository)
        {
            _pharmacyRepository = pharmacyRepository;
            _newsRepository = newsRepository;
        }

        public PharmacyDto dto { get; set; }

        [BindProperty]
        public NewsDto newsDto { get; set; }

        public void OnGet(int id)
        {
            dto = _pharmacyRepository.GetPharmacyById(id);
        }

        public async Task<IActionResult> OnPost(string Email,int id, CancellationToken cancellationToken)
        {
            await _newsRepository.AddEmail(Email, cancellationToken);

            return Redirect("/Products/PharmacyDetails/"+id);
        }
    }
}
