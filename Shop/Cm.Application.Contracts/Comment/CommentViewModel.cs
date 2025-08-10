namespace Cm.Application.Contracts.Comment
{
    public class CommentViewModel
    {
        
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public long OwnerId { get; set; }
        public int Type { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsCanceled { get; set; }
        public string CreationDate { get; set; }
    }
}