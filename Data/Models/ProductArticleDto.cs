using Entities.Articles;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class ProductArticleDto
    {
        public List<Article> ListArticle { get; set; }
        public List<Product> ListProduct { get; set; }
    }
}
