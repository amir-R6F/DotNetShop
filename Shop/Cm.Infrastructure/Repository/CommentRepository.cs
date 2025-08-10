using System.Collections.Generic;
using System.Linq;
using Cm.Application.Contracts.Comment;
using Cm.Domain.CommentAgg;
using Shop.Application;
using Shop.Infrastructure;

namespace Cm.Infrastructure.Repository
{
    public class CommentRepository : BaseRepository<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _context;
        
        public CommentRepository(CommentContext context) : base(context)
        {
            _context = context;
        }

        public List<CommentViewModel> Search(CommentSearchModel searchModel)
        {
            var query = _context.Comments.Select(x => new CommentViewModel
            {
                Id = x.Id,
                Email = x.Email,
                Name = x.Name,
                Message = x.Message,
                IsCanceled = x.IsCanceled,
                IsConfirmed = x.IsConfirmed,
                CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name == searchModel.Name);
            
            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email == searchModel.Email);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}