using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Setting
{
    public class ContactUsModel : PageModel
    {
        private readonly IContactUsRepository _contactUsRepository;

        public ContactUsModel(IContactUsRepository contactUsRepository)
        {
            _contactUsRepository = contactUsRepository;
        }

        [BindProperty]
        public ContactusDto Dto { get; set; }

        public void OnGet()
        {
            Dto = _contactUsRepository.GetContactus();
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            await _contactUsRepository.UpdateAsync(Dto, cancellationToken);

            return Redirect("/Administration");
        }
    }
}
