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
    public class IndexModel : PageModel
    {
        private readonly ISliderRepository _slider;

        public IndexModel(ISliderRepository slider)
        {
            _slider = slider;
        }

        [BindProperty]
        public SliderDto dto { get; set; }
        public ListSliderDto ListSlider { get; set; }

        public void OnGet()
        {
            ListSlider = _slider.GetListSliderDto();
        }

        public async Task<IActionResult> OnPost( CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return Page();

            await _slider.AddSlider(dto, cancellationToken);

            return Redirect("/Administration/Media/Slider");
        }

        public async Task<IActionResult> OnPostEnable(int id, CancellationToken cancellationToken)
        {
            await _slider.Enable(id, cancellationToken);
            return Redirect("/Administration/Media/Slider");
        }

        public async Task<IActionResult> OnPostDisable(int id, CancellationToken cancellationToken)
        {
            await _slider.Disable(id, cancellationToken);
            return Redirect("/Administration/Media/Slider");
        }
    }
}
