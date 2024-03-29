﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts;
using Data.Models;
using Data.Models.Constants;
using Data.Repositories;
using Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Samanik.Web.Areas.Administration.Pages.Blog.Articles
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IArticleRepasitory _Repasitory;
        private readonly IArticelCategoryRepasitory _CRepasitory;
        private readonly IArticleCategoryAssignRepository _CAssignRepository;
        private readonly IAuthorizationService _authorizationService;


        public IndexModel(IArticleRepasitory repasitory, IArticelCategoryRepasitory cRepasitory, IArticleCategoryAssignRepository cAssignRepository, IAuthorizationService authorizationService)
        {
            _Repasitory = repasitory;
            _CRepasitory = cRepasitory;
            _CAssignRepository = cAssignRepository;
            _authorizationService = authorizationService;
        }

        [BindProperty]
        public ArticleDto dto { get; set; }
        public ListArticleDto listArticleDto { get; set; }
        //Add By Vahid
        public PagingData PagingData { get; set; }
        public int PageSize = 8;
        public IActionResult OnGet(int PageNum = 1)
        {
            if (_authorizationService.AuthorizeAsync(User, Permissions.Samanik.Blogs).Result.Succeeded)
            {
                ViewData["ArticleCategories"] = new SelectList(_CRepasitory.GetArticleCategories().Where(a => a.IsDelete == false), "Id", "Title");
                listArticleDto = _Repasitory.GetListArticle(PageNum, PageSize);
                //Add By vahid
                StringBuilder QParam = new StringBuilder();
                if (PageNum != 0)
                {
                    QParam.Append($"/Administration/Blog/Articles/Index?PageNum=-");
                    //Administration / Blog / Articles / Index
                }
                if (listArticleDto.Articles.Count >= 0)
                {
                    PagingData = new PagingData
                    {
                        CurrentPage = PageNum,
                        RecordsPerPage = PageSize,
                        TotalRecords = listArticleDto.count,
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

        public async Task<IActionResult> OnPost(List<IFormFile> Image, List<int> ListCategoryId, CancellationToken cancellationToken)
        {
            //if (!ModelState.IsValid)
            //    return Page();

            var exist = await _Repasitory.IsExistArticle(dto.Title);
            if (exist)
            {
                ModelState.AddModelError("Title", "مجله با مشخصات وارد شده تکراری است.");
                return Page();
            }


            var RegisterUserId = "Admin";
            //var RegisterUserId = User.FindFirst("UserId").Value;
            var ArticleId = await _Repasitory.AddAsync(dto, RegisterUserId, Image, cancellationToken);

            await _CAssignRepository.AddAssign(ListCategoryId, ArticleId, cancellationToken);

            return Redirect("/Administration/Blog/Articles/Index");
        }

        public async Task<IActionResult> OnPostActive(int id, CancellationToken cancellationToken)
        {
            await _Repasitory.Active(id, cancellationToken);
            return Redirect("/Administration/Blog/Articles/Index");
        }
        public async Task<IActionResult> OnPostDeactive(int id, CancellationToken cancellationToken)
        {
            await _Repasitory.Deactive(id, cancellationToken);
            return Redirect("/Administration/Blog/Articles/Index");
        }
    }
}
