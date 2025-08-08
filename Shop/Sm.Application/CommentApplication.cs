using System.Collections.Generic;
using System.Linq;
using Shop.Application;
using Sm.Application.Contracts.Comment;
using Sm.Domain.CommentAgg;

namespace Sm.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;


        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public OperationResult AddComment(AddComment command)
        {
            var operation = new OperationResult();


            var comment = new Comment(command.Name, command.Email, command.Message, command.ProductId);

            _commentRepository.Create(comment);
            
            return operation.Succedded();
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            return _commentRepository.Search(searchModel);
        }

        public OperationResult Confirmed(long id)
        {
            var operation = new OperationResult();

            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages.NotFound);
            
            comment.Confirmed();
            return operation.Succedded();
            
        }

        public OperationResult Cancel(long id)
        {
            var operation = new OperationResult();

            var comment = _commentRepository.Get(id);
            if (comment == null)
                return operation.Failed(ApplicationMessages.NotFound);
            
            comment.Cancel();
            return operation.Succedded();
        }
    }
}