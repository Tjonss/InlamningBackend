
using InlamningBackend.Models.Responses.CommentResponses;
using InlamningBackend.Models.Responses.StatusResponses;
using InlamningBackend.Models.Responses.UserResponses;

namespace InlamningBackend.Models.Responses.IssueResponses
{
    public class IssueResponse
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public UserResponse User { get; set; }
        public StatusResponse Status { get; set; }
        public IEnumerable<CommentResponse> Comments { get; set; }
    }
}
