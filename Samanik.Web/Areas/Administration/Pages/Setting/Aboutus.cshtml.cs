using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Setting
{
    public class AboutusModel : PageModel
    {
        private readonly IAboutRepository _aboutRepository;
        public AboutusModel(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        [BindProperty]
        public AboutDto dto { get; set; }

        public void OnGet()
        {
            dto = _aboutRepository.GetAbout();
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken)
        {
            await _aboutRepository.UpdateAboutus(dto, Image, cancellationToken);

            return Redirect("/administration/setting/Aboutus");
        }
    }
}
