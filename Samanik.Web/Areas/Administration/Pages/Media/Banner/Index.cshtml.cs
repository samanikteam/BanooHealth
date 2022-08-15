using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Media.Banner
{
    public class IndexModel : PageModel
    {
        private readonly IBannerRepository _bannerRepository;

        public IndexModel(IBannerRepository bennerRepository)
        {
            _bannerRepository = bennerRepository;
        }

        [BindProperty]
        public BannerDto Dto { get; set; }
       
        public void OnGet()
        {
            Dto = _bannerRepository.GetBanner();
          
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
