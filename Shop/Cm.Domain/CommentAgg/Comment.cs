using System.Collections.Generic;
using Shop.Domain;

namespace Cm.Domain.CommentAgg
{
    public class Comment : EntityBase
    {


        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public long OwnerId { get; private set; }
        public int Type { get; private set; }
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }

        public Comment(string name, string email, string website, string message, long ownerId, int type, long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            OwnerId = ownerId;
            Type = type;
            ParentId = parentId;
        }

        public void Confirmed()
        {
            IsConfirmed = true;
        }

        public void Cancel()
        {
            IsCanceled = true;
        }
    }
}