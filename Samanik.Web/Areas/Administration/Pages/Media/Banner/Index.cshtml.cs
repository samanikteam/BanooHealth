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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Media.Banner
{
    public class IndexModel : PageModel
    {
        private readonly IBannerRepository _bannerRepository;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(IBannerRepository bennerRepository, IAuthorizationService authorizationService)
        {
            _bannerRepository = bennerRepository;
            _authorizationService = authorizationService;
        }
        [BindProperty]
        public BannerDto Dto { get; set; }
       
        public IActionResult OnGet()
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Resaneh).Result.Succeeded)
            {
                Dto = _bannerRepository.GetBanner();
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

            await _bannerRepository.UpdateAsync(Dto, cancellationToken);

            return Redirect("/Administration/Media/Banner");
        }
    }
}
