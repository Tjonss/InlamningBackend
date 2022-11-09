using System.ComponentModel.DataAnnotations;

namespace InlamningBackend.Models.Requests.IssueRequests
{
    public class IssueUpdateRequest
    {
        [Required]
        public int StatusId { get; set; }
    }
}
