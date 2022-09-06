using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Setting
{
    public class AboutusModel : PageModel
    {
        private readonly IAboutRepository _aboutRepository;
        private readonly IAuthorizationService _authorizationService;

        public AboutusModel(IAboutRepository aboutRepository, IAuthorizationService authorizationService)
        {
            _aboutRepository = aboutRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public AboutDto dto { get; set; }

        public IActionResult OnGet()
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Setting).Result.Succeeded)
            {
                dto = _aboutRepository.GetAbout();
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }
            
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken)
        {
            await _aboutRepository.UpdateAboutus(dto, Image, cancellationToken);

            return Redirect("/administration/setting/Aboutus");
        }
    }
}
