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
    public class IndexModel : PageModel
    {
        private readonly ISiteSettingRepository _repository;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(ISiteSettingRepository repository, IAuthorizationService authorizationService)
        {
            _repository = repository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public SiteSettingDto Dto { get; set; }

        public IActionResult OnGet()
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Setting).Result.Succeeded)
            {
                Dto = _repository.GetSetting();
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }
        }
        public async Task<IActionResult> OnPost(CancellationToken cancellationToken)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            await _repository.UpdateAsync(Dto, cancellationToken);

            return Redirect("/Administration/Setting");
        }
    }
}
