using Data.Models;
using Data.Repositories;
using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ISearchLiveRepository : IRepository<Article>
    {
        //SearchLiveWithArticleDto LiveTagSearch(string search);
    }
}
