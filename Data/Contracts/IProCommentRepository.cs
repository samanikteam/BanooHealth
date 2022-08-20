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

        /// <summary>
        /// لیست نظرات جهت پاسخ به آنها
        /// </summary>
        /// <param name="PageNum"> شماره صفحه </param>
        /// <returns></returns>
        ListProCommentDto GetListAnswerComments(int PageNum = 1);

        /// <summary>
        /// لیست تمام نظرات یک نفر برو روی یک مجله
        /// </summary>
        /// <returns></returns>
        ListProCommentDto GetListProCommentByEmailForOneProduct(int id, string email, int PageNum = 1);

        /// <summary>
        /// لیست تمام پاسخ هایی که به نظرات  یک نفر برو روی یک مجله داده شده است
        /// </summary>
        /// <returns></returns>
        ListProCommentDto GetListAnswerProCommentByEmailForOneProduct(int id, int productId, string email, int PageNum = 1);

        /// <summary>
        /// پاسخ به کامنت
        /// </summary>
        /// <param name="CommentDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task AnswerComment(ProCommentDto CommentDto, CancellationToken cancellationToken);

        Task Cancel(int id, CancellationToken cancellationToken);

    }
}
