using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.MainPage
{
    public class ContactModel : PageModel
    {
        private readonly IContactUsRepository _contactuserepository;
        private readonly ICallRepository _callRepository;

        public ContactModel(IContactUsRepository contactuserepository, ICallRepository callRepository)
        {
            _contactuserepository = contactuserepository;
            _callRepository = callRepository;
        }
        public ContactusDto dto { get; set; }

        [BindProperty]
        public CallDto callDto { get; set; }

        public void OnGet()
        {
            dto = _contactuserepository.GetContactus();
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            await _callRepository.AddAsync(callDto, cancellationToken);
            return Redirect("/Contact");
        }
    }
}
