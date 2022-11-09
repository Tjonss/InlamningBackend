using System.ComponentModel.DataAnnotations;

namespace InlamningBackend.Models.Requests.CommentRequests
{
    public class CommentRequest
    {
        [Required]
        public string Message { get; set; }

        [Required]
        public int IssueId { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
