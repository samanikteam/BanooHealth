using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using System.Text;

namespace Samanik.Web.Areas.Administration.Pages.Product.Filter
{
    public class EditModel : PageModel
    {
        private readonly IFilterRepository _filterRepository;
        private readonly IAuthorizationService _authorizationService;


        public EditModel(IFilterRepository filterRepository, IAuthorizationService authorizationService)
        {
            _filterRepository = filterRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public Entities.Products.Filter dto { get; set; }
        public ListFilterDto Listdto { get; set; }

        public IActionResult OnGet(int id)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
            {
                dto = _filterRepository.GetById(id);
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }
        }

        public IActionResult OnPost()
        {
            _filterRepository.Edit(dto);

            return Redirect("/administration/product/filter");
        }
    }
}
