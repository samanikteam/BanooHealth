﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Product.ProductCategory
{
    public class IndexModel : PageModel
    {
        private readonly IProductCategoryRepository _Repository;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(IProductCategoryRepository repository, IAuthorizationService authorizationService)
        {
            _Repository = repository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ProductCategoryDto dto { get; set; }
        public ListProductCategoryDto ListProductCategoryDto { get; set; }
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 12;

        public IActionResult OnGet(int PageNum = 1)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Product).Result.Succeeded)
            {
                ViewData["ProductCategories"] = new SelectList(_Repository.GetProductCategories(), "Id", "Title");
                ListProductCategoryDto = _Repository.GetListProductCategory(PageNum, PageSize);
                //Add By vahid
                StringBuilder QParam = new StringBuilder();
                if (PageNum != 0)
                {
                    QParam.Append($"/Administration/Product/ProductCategory?PageNum=-");
                    //Administration / Blog / Articles / Index
                }
                if (ListProductCategoryDto.ProductCategories.Count >= 0)
                {
                    PagingData = new PagingData
                    {
                        CurrentPage = PageNum,
                        RecordsPerPage = PageSize,
                        TotalRecords = ListProductCategoryDto.count,
                        UrlParams = QParam.ToString(),
                        LinksPerPage = 7
                    };
                }
                return Page();

            }
            else
            {
                return Redirect("/login/logout");
            }
       
        }

        public async Task<IActionResult> OnPost(List<IFormFile> Image, CancellationToken cancellationToken)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            var exist = await _Repository.IsExistProductCategory(dto.Title, Convert.ToInt32(dto.ParentId));
            if (exist)
            {
                ViewData["ArticleCategories"] = new SelectList(_Repository.GetProductCategories(), "Id", "Title");
                ModelState.AddModelError("Title", "دسته‌بندی با مشخصات وارد شده تکراری است.");
                return Page();
            }
            await _Repository.AddProductCategory(dto, Image, cancellationToken);

            return Redirect("/Administration/Product/ProductCategory");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _Repository.Active(id, cancellationToken);
            return Redirect("/Administration/Product/ProductCategory");
        }

        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _Repository.Deactive(id, cancellationToken);
            return Redirect("/Administration/Product/ProductCategory");
        }
    }
}
