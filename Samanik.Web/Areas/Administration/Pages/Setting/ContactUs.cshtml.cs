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

namespace Samanik.Web.Areas.Administration.Pages.Setting
{
    public class ContactUsModel : PageModel
    {
        private readonly IContactUsRepository _contactUsRepository;
        private readonly ICallRepository _callRepository;
        private readonly IAuthorizationService _authorizationService;


        public ContactUsModel(IContactUsRepository contactUsRepository, ICallRepository callRepository, IAuthorizationService authorizationService)
        {
            _contactUsRepository = contactUsRepository;
            _callRepository = callRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ContactusDto Dto { get; set; }
        public ListCallDto listCallDto { get; set; }

        public IActionResult OnGet()
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Setting).Result.Succeeded)
            {
                Dto = _contactUsRepository.GetContactus();
                listCallDto = _callRepository.GetListCall();
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }
        }

        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            await _contactUsRepository.UpdateAsync(Dto, cancellationToken);

            return Redirect("/administration/setting/ContactUs");
        }

        public async Task<IActionResult> OnPostConfirm(int id, CancellationToken cancellationToken)
        {
            await _callRepository.Confirm(id,cancellationToken);
            return Redirect("/administration/setting/ContactUs");
        }
    }
}
