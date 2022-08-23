using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class FooterViewModel
    {
        public ListArticleDto ListArticle { get; set; }
        public ListArticleCategoryDto listArticleCategory { get; set; }
        public SiteSettingDto settingDto { get; set; }
        public ContactusDto contactusDto { get; set; }

    }
}
