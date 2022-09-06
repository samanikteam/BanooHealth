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

namespace Samanik.Web.Areas.Administration.Pages.Media.Slider
{
    public class EditSliderModel : PageModel
    {
        private readonly ISliderRepository _sliderRepository;
        private readonly IAuthorizationService _authorizationService;


        public EditSliderModel(ISliderRepository sliderRepository, IAuthorizationService authorizationService)
        {
            _sliderRepository = sliderRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public SliderDto Dto { get; set; }

        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Resaneh).Result.Succeeded)
            {
                Dto = _sliderRepository.GetSliderById(id);
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

            await _sliderRepository.UpdateAsync(Dto, cancellationToken);

            return Redirect("/Administration/media/slider");
        }
    }
}
