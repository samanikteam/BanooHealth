using Data.Models;
using Data.Repositories;
using Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface IProCommentRepository : IRepository<ProComment>
    {
        //اینجا async نمیخوره
        Task AddProComment(ProCommentDto ProCommentDto, CancellationToken cancellationToken);

        ListProCommentDto GetListProComment(int id);

        ListProCommentDto GetListProComments();
        void AddComment(ProCommentDto proCommentDto);
    }
}
