using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Pages.MainPage
{
    [AllowAnonymous]
    public class AboutModel : PageModel
    {
        private readonly IAboutRepository _aboutRepository;

        public AboutModel(IAboutRepository aboutRepository)
        {
            _aboutRepository = aboutRepository;
        }

        public AboutDto dto { get; set; }

        public void OnGet()
        {
            dto = _aboutRepository.GetAbout();
        }
    }
}
