using System.Collections.Generic;
using Cm.Application.Contracts.Comment;
using Shop.Domain;

namespace Cm.Domain.CommentAgg
{
    public interface ICommentRepository : IBaseRepository<long, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}