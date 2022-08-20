using Data.Models;
using Data.Repositories;
using Entities.Articles;
using Entities.Products;
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
        /// <summary>
        /// پاسخ به کامنت
        /// </summary>
        /// <param name="CommentDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AnswerComment(CommentDto CommentDto,CancellationToken cancellationToken);

        List<Comment> GetComments();

        ListCommentDto GetListComments(int PageNum = 1);
        /// <summary>
        /// لیست نظرات جهت پاسخ به آنها
        /// </summary>
        /// <param name="PageNum"> شماره صفحه </param>
        /// <returns></returns>
        ListCommentDto GetListAnswerComments(int PageNum = 1);

        /// <summary>
        /// لیست تمام نظرات یک نفر برو روی یک مجله
        /// </summary>
        /// <returns></returns>
        ListCommentDto GetListCommentByEmailForOneArticle(int articleId , string email , int PageNum = 1);

        /// <summary>
        /// لیست تمام پاسخ هایی که به نظرات  یک نفر برو روی یک مجله داده شده است
        /// </summary>
        /// <returns></returns>
        ListCommentDto GetListAnswerCommentByEmailForOneArticle(int id, int articleId, string email, int PageNum = 1);

        Task Confirm(int id, CancellationToken cancellationToken);
        Task Cancel(int id, CancellationToken cancellationToken);

        //Article Comment list
        ListCommentDto GetListArticleComment(int id);
    }
}
