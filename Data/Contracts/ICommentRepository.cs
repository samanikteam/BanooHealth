using Data.Models;
using Data.Repositories;
using Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task AddComment(CommentDto CommentDto,CancellationToken cancellationToken);

        List<Comment> GetComments();

        ListCommentDto GetListComments();

        Task Confirm(int id, CancellationToken cancellationToken);
        Task Cancel(int id, CancellationToken cancellationToken);

        //Article Comment list
        ListCommentDto GetListArticleComment(int id);
    }
}
