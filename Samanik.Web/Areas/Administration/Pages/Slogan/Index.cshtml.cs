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

namespace Samanik.Web.Areas.Administration.Pages.Slogan
{
    public class IndexModel : PageModel
    {
        private readonly ISloganRepository _sloganRepository;

        public IndexModel(ISloganRepository sloganRepository)
        {
            _sloganRepository = sloganRepository;
        }

        [BindProperty]
        public SloganDto dto { get; set; }
        public ListSloganDto ListsloganDto { get; set; }

        public void OnGet()
        {
            ListsloganDto = _sloganRepository.GetListSlogans();
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            await _sloganRepository.AddSlogan(dto, Image, cancellationToken);

            return Redirect("/Administration/Slogan");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _sloganRepository.Active(id, cancellationToken);
            return Redirect("/Administration/Slogan");
        }

        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _sloganRepository.Deactive(id, cancellationToken);
            return Redirect("/Administration/Slogan");
        }
    }
}
