using System.Collections.Generic;
using Shop.Domain;
using Sm.Application.Contracts.Comment;

namespace Sm.Domain.CommentAgg
{
    public interface ICommentRepository : IBaseRepository<long, Comment>
    {
        List<CommentViewModel> Search(CommentSearchModel searchModel);
    }
}