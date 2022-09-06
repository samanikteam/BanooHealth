using Common.Utilities;
using Data.Contracts;
using Data.Models;
using Entities.Products;
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
    public class ProCommentRepository : Repository<ProComment>, IProCommentRepository
    {

        public ProCommentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public void AddComment(ProCommentDto proCommentDto)
        {
            throw new NotImplementedException();
        }

        public async Task AddProComment(ProCommentDto ProCommentDto, CancellationToken cancellationToken)
        {
            ProComment comment = new ProComment()
            {
                Name = ProCommentDto.Name,
                Email = ProCommentDto.Email,
                Message = ProCommentDto.Message,
                ProductId = ProCommentDto.ProductId,
                Status = Statuses.New,
                RegisterDate = DateTime.Now
            };

           await base.AddAsync(comment , cancellationToken);
        }

        public ListProCommentDto GetListAnswerProCommentByEmailForOneProduct(int id, int productId, string email, int PageNum = 1)
        {
            var comments = Table.Include(_ => _.Product)
                                   .Where(x => x.Status == Statuses.Answer && x.ProductId == productId && x.ParentId == id)
                                   .OrderByDescending(a => a.RegisterDate);
            var take = 15;
            var skip = (PageNum - 1) * take;
            var list = new ListProCommentDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = comments.Count();
            list.PageCount = (int)Math.Ceiling(comments.Count() / (double)take);

            list.ProComments = comments.Select(t => new ProCommentDto()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ProductId = t.ProductId,
                ProdoctTitle = t.Product.Title,
            }).OrderBy(u => u.Name).Skip(skip).Take(take).ToList();

            return list;
        }

        public ListProCommentDto GetListAnswerComments(int PageNum = 1)
        {
            var comments = Table.Include(_ => _.Product).Where(x => x.Status == Statuses.Confirm).OrderByDescending(a => a.RegisterDate);
            var take = 15;
            var skip = (PageNum - 1) * take;
            var list = new ListProCommentDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = comments.Count();
            list.PageCount = (int)Math.Ceiling(comments.Count() / (double)take);

            list.ProComments = comments.Select(t => new ProCommentDto()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ProductId = t.ProductId,
                ProdoctTitle = t.Product.Title,
            }).Skip(skip).Take(take).ToList();

            return list;
        }

        public ListProCommentDto GetListProComment(int id)
        {
            var proComments = Table.Where(a => a.ProductId == id && a.Status == Statuses.Confirm).OrderBy(x => x.RegisterDate);
            var list = new ListProCommentDto() { };

            list.ProComments = proComments.Select(t => new ProCommentDto()
            {
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
            }).ToList();

            return list;
        }

        public ListProCommentDto GetListProCommentByEmailForOneProduct(int id, string email, int PageNum = 1)
        {
            var comments = Table.Include(_ => _.Product)
                            .Where(x => x.Status == Statuses.Confirm && x.Id == id && x.Email == email)
                            .OrderByDescending(a => a.RegisterDate);
            var take = 15;
            var skip = (PageNum - 1) * take;
            var list = new ListProCommentDto() { };
            list.CurrentPage = PageNum;
            list.skip = skip;
            list.count = comments.Count();
            list.PageCount = (int)Math.Ceiling(comments.Count() / (double)take);

            list.ProComments = comments.Select(t => new ProCommentDto()
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email.ToString(),
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ProductId = t.ProductId,
                ProdoctTitle = t.Product.Title,
            }).Skip(skip).Take(take).ToList();

            return list;
        }

        public ListProCommentDto GetListProComments()
        {
            var comments = Table.Include(_ => _.Product).OrderByDescending(a => a.RegisterDate);
            var list = new ListProCommentDto() { };

            list.ProComments = comments.Select(t => new ProCommentDto()
            {
                //Id = t.Id,
                Name = t.Name,
                Email = t.Email,
                Message = t.Message,
                RegisterDate = t.RegisterDate.ToShamsi(),
                Status = t.Status,
                ProductId = t.ProductId,
                //ProdoctTitle = t.Product.Title,
            }).ToList();

            return list;
        }

        public async Task AnswerComment(ProCommentDto CommentDto, CancellationToken cancellationToken)
        {
            #region وضعیت کامنت رو هم باید به پاسخ داده شده تغییر بدیم
            var commnet = Table.Where(x => x.Id == CommentDto.Id && x.Status == Statuses.Confirm).FirstOrDefault();
            commnet.Status = Statuses.Answerd;
            await DbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            #endregion

            ProComment comment = new ProComment()
            {
                Name = CommentDto.Name,
                Email = CommentDto.Email,
                Message = CommentDto.Message,
                ProductId = CommentDto.ProductId,
                Status = Statuses.Answer,
                ParentId = CommentDto.Id,
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
    }
}
