
using Common.Utilities;
using Data.Contracts;
using Data.Models;
using Entities.Articles;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public ListCommentDto GetListArticleComment(int id)
        {
            var articleComments = Table.Where(a => a.ArticleId == id && a.Status == Statuses.Confirm).OrderBy(x => x.RegisterDate);
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

        public ListCommentDto GetListComments()
        {
            var comments = Table.Include(_=>_.Article).OrderByDescending(a => a.RegisterDate);
            var list = new ListCommentDto() { };

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
            }).ToList();

            return list;
        }
    }
}
