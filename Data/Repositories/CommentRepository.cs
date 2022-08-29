using Common.Utilities;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VisitorManagment.Core.Convertors;

namespace Data.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task AddComment(CommentDto CommentDto, CancellationToken cancellationToken)
        {
            Comment comment = new Comment()
            {
                Name = CommentDto.Name,
                Email = CommentDto.Email,
                Message = CommentDto.Message,
                ArticleId = CommentDto.ArticleId,
                Status = Statuses.New,
                RegisterDate = DateTime.Now
            };

            await base.AddAsync(comment, cancellationToken);
        }

        /// <summary>
        /// پاسخ به کامنت
        /// </summary>
        /// <param name="CommentDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task AnswerComment(CommentDto CommentDto, CancellationToken cancellationToken)
        {
            #region وضعیت کامنت رو هم باید به پاسخ داده شده تغییر بدیم
            //var commnet = Table.Where(x => x.Id == CommentDto.Id && x.Status == Statuses.Confirm).FirstOrDefault();
            //commnet.Status = Statuses.Answerd;
            //await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            #endregion

            //await AddComment(CommentDto, cancellationToken);
            Comment comment = new Comment()
            {
                Name = CommentDto.Name,
                Email = CommentDto.Email,
                Message = CommentDto.Message,
                ArticleId = CommentDto.ArticleId,
                Status = Statuses.Confirm,
                ParentId = CommentDto.Id,
                RegisterDate = DateTime.Now
            };

            await base.AddAsync(comment , cancellationToken);

        }

        public async Task Cancel(int id, CancellationToken cancellationToken)
        {
            var comment = GetById(id);
            comment.Cancel();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task Confirm(int id, CancellationToken cancellationToken)
        {
            var comment = GetById(id);
            comment.Confirm();
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        public List<Comment> GetComments()
        {
            return Table.ToList();
        }




        /// <summary>
        /// لیست تمام پاسخ هایی که به نظرات  یک نفر برو روی یک مجله داده شده است
        /// </summary>
        /// <returns></returns>
        public ListCommentDto GetListAnswerCommentByEmailForOneArticle(int id, int articleId, string email, int PageNum = 1)
        {
            var comments = Table.Include(_ => _.Article)
                        .Where(x => x.Status == Statuses.Answer && x.ArticleId == articleId && x.ParentId == id)
                        .OrderByDescending(a => a.RegisterDate);
            var take = 15;
            var skip = (PageNum - 1) * take;
            var list = new ListCommentDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = comments.Count();
            list.PageCount = (int)Math.Ceiling(comments.Count() / (double)take);

            list.Comments = comments.Select(t => new CommentDto()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ArticleId = t.ArticleId,
                ArticleTitle = t.Article.Title,
            }).OrderBy(u => u.Name).Skip(skip).Take(take).ToList();

            return list;
        }

        /// <summary>
        /// لیست نظرات جهت پاسخ به آنها
        /// </summary>
        /// <param name="PageNum"> شماره صفحه </param>
        /// <returns></returns>
        public ListCommentDto GetListAnswerComments(int PageNum = 1, int PageSize = 2)
        {
            var comments = Table.Include(_ => _.Article).Where(x => x.Status == Statuses.Confirm).OrderByDescending(a => a.RegisterDate);
            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListCommentDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = comments.Count();
            list.PageCount = (int)Math.Ceiling(comments.Count() / (double)take);

            list.Comments = comments.Select(t => new CommentDto()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ArticleId = t.ArticleId,
                ArticleTitle = t.Article.Title,
            }).Skip(skip).Take(take).ToList();

            return list;
        }

        public ListCommentDto GetListArticleComment(int id)
        {
            var articleComments = Table.Where(x=>x.Status==Statuses.Confirm && x.ArticleId==id).OrderBy(x => x.RegisterDate);
            var list = new ListCommentDto() { };

            list.Comments = articleComments.Select(t => new CommentDto()
            {
                Id = t.Id,
                ParentId = t.ParentId,
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
            }).ToList();


            return list;
        }

        /// <summary>
        /// لیست تمام نظرات یک نفر برو روی یک مجله
        /// </summary>
        /// <returns></returns>
        public ListCommentDto GetListCommentByEmailForOneArticle(int id, string email, int PageNum = 1)
        {
            var comments = Table.Include(_ => _.Article)
                .Where(x => x.Status == Statuses.Confirm && x.Id == id && x.Email == email)
                .OrderByDescending(a => a.RegisterDate);
            var take = 15;
            var skip = (PageNum - 1) * take;
            var list = new ListCommentDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = comments.Count();
            list.PageCount = (int)Math.Ceiling(comments.Count() / (double)take);

            list.Comments = comments.Select(t => new CommentDto()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email.ToString(),
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ArticleId = t.ArticleId,
                ArticleTitle = t.Article.Title,
            }).Skip(skip).Take(take).ToList();

            return list;
        }

        public ListCommentDto GetListComments(int PageNum = 1, int PageSize = 0)
        {
            var comments = Table.Include(_ => _.Article).OrderByDescending(a => a.RegisterDate);
            var take = PageSize;
            var skip = (PageNum - 1) * take;
            var list = new ListCommentDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = comments.Count();
            list.PageCount = (int)Math.Ceiling(comments.Count() / (double)take);

            list.Comments = comments.Select(t => new CommentDto()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ArticleId = t.ArticleId,
                ArticleTitle = t.Article.Title,
            }).Skip(skip).Take(take).ToList();

            return list;
        }

        public ListCommentDto GetListAnswerComment()
        {
            var articleComments = Table.OrderBy(x => x.RegisterDate);
            var list = new ListCommentDto() { };

            list.Comments = articleComments.Select(t => new CommentDto()
            {
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
            }).ToList();

            return list;
        }
    }
}
