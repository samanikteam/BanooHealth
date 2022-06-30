using Data.Contracts;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Samanik.Web.Areas.Administration.Pages.Product.Filter
{
    public class EditModel : PageModel
    {
        private readonly IFilterRepository _filterRepository;

        public EditModel(IFilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }

        [BindProperty]
        public Entities.Products.Filter dto { get; set; }
        public ListFilterDto Listdto { get; set; }

        public void OnGet(int id)
        {
            dto = _filterRepository.GetById(id);
        }

        public IActionResult OnPost()
        {
            _filterRepository.Edit(dto);

            return Redirect("/administration/product/filter");
        }
    }
}
