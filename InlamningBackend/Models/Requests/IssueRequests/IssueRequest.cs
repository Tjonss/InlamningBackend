using System.ComponentModel.DataAnnotations;

namespace InlamningBackend.Models.Requests.IssueRequests
{
    public class IssueRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Guid UserId { get; set; }


    }
}
