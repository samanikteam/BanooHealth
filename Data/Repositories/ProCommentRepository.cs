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
    }
}
