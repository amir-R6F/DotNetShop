using System.Collections.Generic;
using Shop.Application;

namespace Sm.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        OperationResult AddComment(AddComment command);
        List<CommentViewModel> Search(CommentSearchModel searchModel);
        OperationResult Confirmed(long id);
        OperationResult Cancel(long id);
    }
}