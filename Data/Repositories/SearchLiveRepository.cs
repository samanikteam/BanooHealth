using Data.Contracts;
using Data.Models;
using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
   public class SearchLiveRepository : Repository<Article>, ISearchLiveRepository
    {
        public SearchLiveRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        //public SearchLiveWithArticleDto LiveTagSearch(string search)
        //{
        //    // Call your method to search your data source here.
        //    // I'll use entity framework to query my DB
        //    var res = Table.Where(x=>x.Title.Contains(search));
        //    SearchLiveWithArticleDto list = new SearchLiveWithArticleDto() { };


        //    list.Articles = res.Select(t => new ArticleDto()
        //    {
        //        Id = t.Id,
        //        Title = t.Title,

        //    }).ToList();

        //    return list;

        //}
    }
}
