
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data.Repositories;
using VisitorManagment.Core.Convertors;

namespace Data.Repositories
{
    public class ArticleRepasitory : Repository<Article>, IArticleRepasitory
    {
        private readonly IArticleCategoryAssignRepository _articleCategoryAssignRepository;
        private readonly ApplicationDbContext _context;

        public ArticleRepasitory(ApplicationDbContext dbContext, IArticleCategoryAssignRepository articleCategoryAssignRepository
            , ApplicationDbContext context) : base(dbContext)
        {
            _articleCategoryAssignRepository = articleCategoryAssignRepository;
            _context = context;
        }

        public async Task<int> AddAsync(ArticleDto ArticleDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            Article article = new Article()
            {
                Title = ArticleDto.Title,
                // ArticleCategoryId = ArticleDto.ArticleCategoryId,
                Description = ArticleDto.Description,
                KeyWords = ArticleDto.KeyWords,
                Slug = ArticleDto.Slug,
                Text = ArticleDto.Text,
                RegisterUserId = RegisterUserId,
                AvatarTitle = ArticleDto.AvatarTitle,
                AvatarAlt = ArticleDto.AvatarAlt,
                RegisterDate = DateTime.Now,
                IsDelete = false,
                Visit = 1
            };

            #region Add Avatar(FileStream) in Model
            //if (Image != null)
            //{
            //    using (var stream = new MemoryStream())
            //    {
            //        _ = Image.CopyToAsync(stream, cancellationToken);
            //        article.Avatar = stream.ToArray();
            //    }
            //}

            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        article.Avatar = stream.ToArray();
                    }
                }
            }
            #endregion

            await base.AddAsync(article, cancellationToken);
            return article.Id;
        }

        public async Task<bool> IsExistArticle(string title)
        {
            return await TableNoTracking.AnyAsync(p => p.Title == title);
        }

      
       
        public ListArticleDto GetListArticle(int PageNum = 1, int PageSize = 12)
        {
            var article = Table.OrderByDescending(a => a.RegisterDate);
            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListArticleDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = article.Count();
            list.PageCount = (int)Math.Ceiling(article.Count() / (double)take);
            list.Articles = article.Select(t => new ArticleDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Text = t.Description,
                Avatar = t.Avatar,
                KeyWords = t.KeyWords,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Visit = t.Visit,
                Slug = t.Slug,
                IsDelete = t.IsDelete

            }).OrderBy(u => u.Title).Skip(skip).Take(take).ToList();

            return list;
        }

        public async Task<Article> GetByIdAsync(CancellationToken cancellationToken, int id)
        {
            var h = await base.GetByIdAsync(cancellationToken, id);
            return h;
        }

        public Article GetArticle(int id)
        {
            return base.GetById(id);
        }

        public List<Article> GetArticlesForComment()
        {
            return Table.ToList();
        }

        public async Task<int> UpdateAsync(ArticleDto ArticleDto, string RegisterUserId, List<IFormFile> Image, CancellationToken cancellationToken)
        {
            var article = await base.GetByIdAsync(cancellationToken, ArticleDto.Id);
            var listArticleCategoryId = _articleCategoryAssignRepository.GetListArticleCategoryIdWithArticleId(article.Id);

            article.Title = ArticleDto.Title;
            article.Description = ArticleDto.Description;
            //article.ArticleCategoryId = listArticleCategoryId;
            article.KeyWords = ArticleDto.KeyWords;
            article.Slug = ArticleDto.Slug;
            article.Text = ArticleDto.Text;
            article.LastUpdateUserId = RegisterUserId;
            article.AvatarTitle = ArticleDto.AvatarTitle;
            article.AvatarAlt = ArticleDto.AvatarAlt;
            article.LastUpdateDate = DateTime.Now;
            article.IsDelete = false;
            article.Visit = ArticleDto.Visit;



            foreach (var item in Image)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        article.Avatar = stream.ToArray();
                    }
                }
            }


            await base.UpdateAsync(article, cancellationToken);
            return article.Id;
        }

        public ArticleDto GetArticleById(int id)
        {
            var article = GetById(id);
            var listArticleCategoryId = _articleCategoryAssignRepository.GetListArticleCategoryIdWithArticleId(article.Id);

            var res = new ArticleDto()
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                Text = article.Text,
                Avatar = article.Avatar,
                AvatarTitle = article.AvatarTitle,
                AvatarAlt = article.AvatarAlt,
                Slug = article.Slug,
                KeyWords = article.KeyWords,
                RegisterDate = article.RegisterDate.ToShamsi(),
                ArticleCategoryId = listArticleCategoryId,
                Visit = article.Visit

            };

            return res;
        }

        public List<Article> GetListArticleById(int articleId)
        {
            var result = Table.Where(x => x.Id == articleId).ToList();

            return result;
        }


        public List<Article> GetListArticleFromTableProductArticleByArticleId(List<int> articleId)
        {
            var listArticle = Table.ToList();
            List<Article> result = new List<Article>();
            foreach (var item in articleId)
            {
                result = listArticle.Where(x => x.Id == item).ToList();
            }

            return result;
        }

        /// <summary>
        /// محصولات مرتبط با بلاگ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProductDto GetListProductByArticleId(int id)
        {
            var result1 = Table.Where(x => x.Id == id).Include(x => x.Category).Include(x => x.ProductArticles)
                        .ThenInclude(x => x.Article)
                        .Include(x => x.ProductArticles).ThenInclude(x => x.Article).SingleOrDefault();


            var result = Table.Where(x => x.Id == id).Include(x => x.ProductArticles).ThenInclude(x => x.Product).SingleOrDefault();

            var product = new ProductDto
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                Avatar1 = result.Avatar,
                AvatarTitle1 = result.AvatarTitle,
                AvatarAlt1 = result.AvatarAlt,
                Slug = result.Slug,
                KeyWords = result.KeyWords,
                RegisterDate = result.RegisterDate,
                ListArticleLinked = result.ProductArticles.Select(x => x.Article).ToList(),
                ListProductLinked = result.ProductArticles.Select(x => x.Product).ToList()
            };
            return product;
        }

        public List<Article> SearchArticleMainPage(string title)
        {
            var resultSearch = Table.Where(s => s.Title.Contains(title)).ToList();

            return resultSearch;
        }

        public List<Article> searchArticle(string title)
        {
            List<Article> articles = Table.Where(c => c.Title.Contains(title)).ToList();

            return articles;
        }

        public async Task Visited(int id, CancellationToken cancellationToken)
        {
            var article = GetById(id);
            article.Visited();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public ArticleDto GetArticleBySlug(string slug)
        {
            var article = Table.Where(a => a.Slug == slug).SingleOrDefault();
            var listArticleCategoryId = _articleCategoryAssignRepository.GetListArticleCategoryIdWithArticleId(article.Id);

            var res = new ArticleDto()
            {
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                Text = article.Text,
                Avatar = article.Avatar,
                AvatarTitle = article.AvatarTitle,
                AvatarAlt = article.AvatarAlt,
                Slug = article.Slug,
                KeyWords = article.KeyWords,
                RegisterDate = article.RegisterDate.ToShamsi(),
                ArticleCategoryId = listArticleCategoryId,
                Visit = article.Visit
            };

            return res;
        }
        public ListArticleDto GetListArticlesByArticleCategoryId(int articleCategoryId , int PageNum = 1, int PageSize = 0)
        {
            var listarticleId = _articleCategoryAssignRepository.GetListArticleIdWithArticleCategoryId(articleCategoryId);

            var articles = new List<Article>();

            foreach (var item in listarticleId)
            {
                var product = Table.Where(x => x.Id == item).FirstOrDefault();

                articles.Add(product);
            }

            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListArticleDto();
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = articles.Count();
            list.PageCount = (int)Math.Ceiling(articles.Count() / (double)take);
            list.Articles = articles.Select(t => new ArticleDto()
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description,
                Text = t.Description,
                Avatar = t.Avatar,
                KeyWords = t.KeyWords,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Visit = t.Visit
            }).OrderByDescending(x => x.RegisterDate).Skip(skip).Take(take).ToList();
            // OrderBy(u => u.Title).Skip(skip).Take(take).ToList();
            return list;
        }

        public async Task Active(int id, CancellationToken cancellationToken)
        {
            var article = GetById(id);
            article.IsDelete = false;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Deactive(int id, CancellationToken cancellationToken)
        {
            var article = GetById(id);
            article.IsDelete = true;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }



        //public ArticleDto GetListBlogByProductId(int id)
        //{
        //    var result1 = Table.Where(x => x.Id == id).Include(x => x.Category).Include(x => x.ProductArticles)
        //    .ThenInclude(x => x.Article)
        //    .Include(x => x.ProductArticles).ThenInclude(x => x.Article).SingleOrDefault();


        //    var result = Table.Where(x => x.Id == id).Include(x => x.ProductArticles).ThenInclude(x => x.Product).SingleOrDefault();

        //    var product = new ProductDto
        //    {
        //        Id = result.Id,
        //        Title = result.Title,
        //        Description = result.Description,
        //        Avatar1 = result.Avatar,
        //        AvatarTitle1 = result.AvatarTitle,
        //        AvatarAlt1 = result.AvatarAlt,
        //        Slug = result.Slug,
        //        KeyWords = result.KeyWords,
        //        RegisterDate = result.RegisterDate,
        //        ListArticleLinked = result.ProductArticles.Select(x => x.Article).ToList(),
        //        ListProductLinked = result.ProductArticles.Select(x => x.Product).ToList()
        //    };
        //    return product;
        //}

        #region MyRegion
        public ArticleQueryModel GetArticleDetails(int id)
        {
            var article = Table
           .Include(x => x.Category)
           .Where(x => x.RegisterDate <= DateTime.Now)
           .Select(x => new ArticleQueryModel
           {
               Id = x.Id,
               Title = x.Title,
               CategoryName = x.Category.Title,
               CategorySlug = x.Category.Slug,
               Slug = x.Slug,
               Description = x.Description,
               Keywords = x.KeyWords,
               MetaDescription = x.Description,
               Picture = x.Avatar,
               PictureAlt = x.AvatarAlt,
               PictureTitle = x.AvatarTitle,
               PublishDate = x.RegisterDate.ToShamsi(),
               ShortDescription = x.Description,
           }).FirstOrDefault(x => x.Id == id);

                    if (!string.IsNullOrWhiteSpace(article.Keywords))
                        article.KeywordList = article.Keywords.Split(",").ToList();

            
                    //var comments =
                    //    .Where(x => !x.IsCanceled)
                    //    .Where(x => x.IsConfirmed)
                    //    .Where(x => x.Type == CommentType.Article)
                    //    .Where(x => x.OwnerRecordId == article.Id)
                    //    .Select(x => new CommentQueryModel
                    //    {
                    //        Id = x.Id,
                    //        Message = x.Message,
                    //        Name = x.Name,
                    //        ParentId = x.ParentId,
                    //        CreationDate = x.CreationDate.ToFarsi()
                    //    }).OrderByDescending(x => x.Id).ToList();

            //foreach (var comment in comments)
            //{
            //    if (comment.ParentId > 0)
            //        comment.parentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
            //}

            //article.Comments = comments;

            return article;
        }
        #endregion
    }
}
