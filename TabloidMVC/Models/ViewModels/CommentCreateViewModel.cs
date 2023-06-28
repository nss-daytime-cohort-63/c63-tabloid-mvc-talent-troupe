namespace TabloidMVC.Models.ViewModels
{
    public class CommentCreateViewModel
    {
        public int PostId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}