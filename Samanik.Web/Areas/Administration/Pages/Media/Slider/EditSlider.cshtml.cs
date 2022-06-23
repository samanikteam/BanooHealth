using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Media.Slider
{
    public class EditSliderModel : PageModel
    {
        private readonly ISliderRepository _sliderRepository;

        public EditSliderModel(ISliderRepository sliderRepository)
        {
            _sliderRepository = sliderRepository;
        }

        [BindProperty]
        public SliderDto Dto { get; set; }

        public void OnGet(int id)
        {
            Dto = _sliderRepository.GetSliderById(id);
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
