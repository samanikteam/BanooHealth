using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Setting
{
    public class IndexModel : PageModel
    {
        private readonly ISiteSettingRepository _repository;

        public IndexModel(ISiteSettingRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public SiteSettingDto Dto { get; set; }

        public void OnGet()
        {
            Dto = _repository.GetSetting();
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
