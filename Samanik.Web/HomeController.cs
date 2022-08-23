using Data.Contracts;
using Entities.Articles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Samanik.Web
{
    public class HomeController : Controller
    {
        private readonly IArticleRepasitory _Repasitory;

        public HomeController(IArticleRepasitory repasitory)
        {
            _Repasitory = repasitory;
        }

        public List<Article> listArticleSearch{ get; set; }
        public IActionResult Index(string Name ,int SurName)
        {
            if (SurName == 1)
            {
                listArticleSearch = _Repasitory.SearchArticleMainPage(Name);
                return View(listArticleSearch);
            }

            if(SurName == 2)
            {
                //بشه پروئداتنت
                listArticleSearch = _Repasitory.SearchArticleMainPage(Name);
                return View(listArticleSearch);
            }

            return View();
        }
    }
}
